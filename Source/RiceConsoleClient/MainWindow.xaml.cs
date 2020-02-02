﻿using System;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Windows;
using Rice.Core.Abstractions.Transport;
using Rice.Core.Unity;
using Unity;

namespace RiceConsoleClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Send_OnClick(object sender, RoutedEventArgs e)
        {
            Error.Text = string.Empty;

            var uc = new UnityContainer();
            uc.AddRice(p=>new ModuleDependencyLoader(p));
            
            var transferableModuleFactory = uc.Resolve<ITransportableModuleFactory>();
            var transferable = await transferableModuleFactory.Create(DllPath.Text, AssemblyName.Text, FindDependencyStrategies.Default);

            var httpMessageHandler = new HttpClientHandler();
            if (httpMessageHandler.SupportsAutomaticDecompression)
            {
                httpMessageHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }
            using var client = new HttpClient(httpMessageHandler);
            using var request = new HttpRequestMessage(HttpMethod.Post, Url.Text);
            using var httpContent = CreateJsonContent(transferable);
            request.Content = httpContent;

            string content = string.Empty;

            try
            {
                using var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None)
                    .ConfigureAwait(false);
                
                content = await response.Content.ReadAsStringAsync();
                
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpRequestException)
            {
                Dispatcher.Invoke(() =>
                {
                    Error.Text = $"Exception:{httpRequestException}{Environment.NewLine}Response:{content}";
                });
            }
        }

        //private static HttpContent CreateCompressedJsonContent2(object obj)
        //{
        //    if (obj == null) throw new ArgumentNullException(nameof(obj));
            
        //    string json = JsonConvert.SerializeObject(obj);
        //    byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        //    MemoryStream ms = new MemoryStream();
        //    using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
        //    {
        //        gzip.Write(jsonBytes, 0, jsonBytes.Length);
        //    }
        //    ms.Position = 0;
        //    StreamContent content = new StreamContent(ms);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    content.Headers.ContentEncoding.Add("gzip");

        //    return content;
        //}

        //private static HttpContent CreateCompressedJsonContent(object content)
        //{
        //    if (content == null) throw new ArgumentNullException(nameof(content));
            
        //    var ms = new MemoryStream();
        //    var gzip = new GZipStream(ms, CompressionMode.Compress, true);
        //    SerializeJsonIntoStream(content, gzip);
        //    ms.Seek(0, SeekOrigin.Begin);

        //    HttpContent httpContent = new StreamContent(ms);
            
        //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    httpContent.Headers.ContentEncoding.Add("gzip");

        //    return httpContent;
        //}

        private static HttpContent CreateJsonContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true);
            using var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None };
            var js = new JsonSerializer();
            js.Serialize(jtw, value);
            jtw.Flush();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Url.Text = "http://localhost:5000/api/Modules";
            DllPath.Text = @"C:\Users\squir\source\repos\Rice\Dependencies\TestModule\TestModule.dll";
            AssemblyName.Text = "TestModule";
        }
    }
}
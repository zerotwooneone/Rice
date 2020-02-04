using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rice.Core.Abstractions.ModuleLoad;
using Rice.Core.Abstractions.Transport;

namespace RiceConsole.Api
{
    [Route("api/Modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly ILoadableModuleFactory _loadableModuleFactory;
        private readonly ITranportableModuleWriter _tranportableModuleWriter;
        private readonly IModuleLoader _moduleLoader;

        public ModulesController(ITranportableModuleWriter tranportableModuleWriter, 
            IModuleLoader moduleLoader, 
            ILoadableModuleFactory loadableModuleFactory)
        {
            _tranportableModuleWriter = tranportableModuleWriter;
            _moduleLoader = moduleLoader;
            _loadableModuleFactory = loadableModuleFactory;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] TransportableModule transportableModule)
        {
            if (transportableModule == null) throw new ArgumentNullException(nameof(transportableModule));

            var rootOutput = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Rice", "RiceConsole");
            var directoryPath = Path.Combine(rootOutput, $"{DateTime.Now:yyyy.MM.dd.HH.mm.ss.ffff}");
            Directory.CreateDirectory(directoryPath);
            
            var dllFileName= $"{transportableModule.AssemblyName}.dll";
            
            var fullPath = Path.Combine(directoryPath, dllFileName);

            await _tranportableModuleWriter.WriteToFile(fullPath, transportableModule);

            var loadableModule = _loadableModuleFactory.Create(fullPath, transportableModule.AssemblyName);

            try
            {
                var module = _moduleLoader.GetModule(loadableModule);
                var result = module.Execute(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

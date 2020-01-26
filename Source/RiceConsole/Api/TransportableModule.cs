using Rice.Core.Abstractions.Transport;

namespace RiceConsole.Api
{
    public class TransportableModule : ITransportableModule
    {
        public string AssemblyName { get; set; }
        public byte[] Bytes { get; set; }
    }
}
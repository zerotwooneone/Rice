using Rice.Core.Abstractions.Transport;

namespace RiceConsole.Api
{
    public class TransportableDependency : ITransportableDependency
    {
        public string AssemblyName { get; set; }
        public byte[] Bytes { get; set; }
    }
}
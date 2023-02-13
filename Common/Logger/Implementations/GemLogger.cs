using Microsoft.Extensions.Logging;

namespace Common.Logger.Implementations
{
    public class GemLogger : IGemLogger
    {
        private readonly Serilog.ILogger _Logger;
        public GemLogger(Serilog.ILogger logger) 
        {
            _Logger = logger;
        }

        public void Log(string message) 
        {
            var a = Path.DirectorySeparatorChar;
            var b = Path.GetRandomFileName();
        }
    }
}

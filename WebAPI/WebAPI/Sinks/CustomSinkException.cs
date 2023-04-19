using Serilog.Configuration;
using Serilog;

namespace WebAPI.Sinks
{
    public static class CustomSinkException
    {

        public static LoggerConfiguration CustomSink(this LoggerSinkConfiguration loggerConfiguration)
        {
            return loggerConfiguration.Sink(new CustomSink());
        }
    }
}

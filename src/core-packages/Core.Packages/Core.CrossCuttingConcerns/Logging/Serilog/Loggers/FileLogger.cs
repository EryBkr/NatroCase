using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class FileLogger : LoggerServiceBase
{
    public FileLogger(IConfiguration configuration)
    {
        FileLogConfiguration logConfig =
            configuration.GetSection("SerilogLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        string logFolderPath = Directory.GetCurrentDirectory() + logConfig.FolderPath;
        string date = DateTime.UtcNow.ToString("yyyy-MM-dd");

        Logger = new LoggerConfiguration()
             .WriteTo.Logger(lc => lc
                 .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                 .WriteTo.File($"{logFolderPath}/Log-Information-{date}.txt",
                     rollingInterval: RollingInterval.Day,
                     fileSizeLimitBytes: 5000000,
                     retainedFileCountLimit: null,
                     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
             )
             .WriteTo.Logger(lc => lc
                 .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                 .WriteTo.File($"{logFolderPath}/Log-Warning-{date}.txt",
                     rollingInterval: RollingInterval.Day,
                     fileSizeLimitBytes: 5000000,
                     retainedFileCountLimit: null,
                     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
             )
             .WriteTo.Logger(lc => lc
                 .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                 .WriteTo.File($"{logFolderPath}/Log-Error-{date}.txt",
                     rollingInterval: RollingInterval.Day,
                     fileSizeLimitBytes: 5000000,
                     retainedFileCountLimit: null,
                     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
             )
             .WriteTo.Logger(lc => lc
                 .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                 .WriteTo.File($"{logFolderPath}/Log-Debug-{date}.txt",
                     rollingInterval: RollingInterval.Day,
                     fileSizeLimitBytes: 5000000,
                     retainedFileCountLimit: null,
                     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
             )
             .CreateLogger();
    }
}

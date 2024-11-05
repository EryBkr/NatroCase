using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.MSSqlServer;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class MsSqlLogger : LoggerServiceBase
{
    public MsSqlLogger(IConfiguration configuration)
    {
        MsSqlConfiguration logConfiguration =
            configuration.GetSection("SerilogLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = logConfiguration.TableName,
            AutoCreateSqlDatabase = logConfiguration.AutoCreateSqlTable
        };

        ColumnOptions columnOptions = new();

        Logger = new LoggerConfiguration().WriteTo
                    .MSSqlServer(logConfiguration.ConnectionString, sinkOptions, columnOptions: columnOptions)
                    .CreateLogger();
    }

}

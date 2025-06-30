using System.Runtime.CompilerServices;
using ILogger = Serilog.ILogger;

namespace APIServer.Utilities;

public static class SerilogExtensions
{
	public static void LogException(this ILogger logger, Exception ex, object? context = null, [CallerMemberName] string? methodName = null)
	{
		logger.Error(ex, "Exception in [{Method}]: {Message}, Context: {@Context}", methodName, ex.Message, context);
	}

	public static void LogError(this ILogger logger, ResultCode resultCode, object? context = null, [CallerMemberName] string? methodName = null)
	{
		logger.Error("Error in [{Method}]: {ResultCode}, Context: {@Context}", methodName, resultCode, context);
	}

	public static void LogInfo(this ILogger logger, string message, object? context = null, [CallerMemberName] string? methodName = null)
	{
		logger.Information("Info in [{Method}]: {Message}, Context: {@Context}", methodName, message, context);
	}
}

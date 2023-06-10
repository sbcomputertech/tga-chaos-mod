using System.Linq;
using BepInEx.Logging;
using DiscordRPC.Logging;
using LogLevel = DiscordRPC.Logging.LogLevel;

namespace ChaosMod;

public class DiscordBepinexLogger : ILogger
{
    private readonly ManualLogSource source;

    public DiscordBepinexLogger(LogLevel level)
    {
        Level = level;
        source = new ManualLogSource("ChaosDiscordRPC");
    }
    
    public void Trace(string message, params object[] args)
    {
        source.LogInfo(message + string.Concat(args.Select(o => o + ", ")));
    }

    public void Info(string message, params object[] args)
    {
        source.LogMessage(message + string.Concat(args.Select(o => o + ", ")));
    }

    public void Warning(string message, params object[] args)
    {
        source.LogWarning(message + string.Concat(args.Select(o => o + ", ")));
    }

    public void Error(string message, params object[] args)
    {
        source.LogError(message + string.Concat(args.Select(o => o + ", ")));
    }

    public LogLevel Level { get; set; }
}
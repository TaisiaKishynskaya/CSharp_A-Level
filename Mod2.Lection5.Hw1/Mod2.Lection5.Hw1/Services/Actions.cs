using Mod2.Lection5.Hw1.Models;

namespace Mod2.Lection5.Hw1.Services;

internal class Actions
{
    internal delegate Result MethodDelegate();

    internal static Result StartMethod()
    {
        var logType = (LogType[])Enum.GetValues(typeof(LogType));

        foreach (var type in logType)
        {
            try
            {
                throw new CustomException();
            }
            catch (CustomException)

            {
                Logger.Instance.Log(LogType.Info, "Start method");
            }
        }

        return new Result { Status = true };
    }

    internal static Result SkippedLogicInMethod() 
    {
        var logType = (LogType[])Enum.GetValues(typeof(LogType));

        foreach (var type in logType)
        {
            try
            {
                throw new CustomException();
            }
            catch (CustomException)
            {
                Logger.Instance.Log(LogType.Warning, "Skipped Logic In Method");
            }
        }

        return new Result { Status = true };
    }

    internal static Result BreakLogic() 
    {
        return new Result { Status = false };
    }
}

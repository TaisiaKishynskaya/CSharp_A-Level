using Mod2.Lection5.Hw1.Models;

namespace Mod2.Lection5.Hw1.Services;

internal class Actions
{
    internal static Result SetMessage(LogType selectedLogType)
    {
        try
        {
            throw new CustomException();
        }
        catch (CustomException) when (selectedLogType == LogType.Info)
        {
            Logger.Instance.Log(LogType.Info, "Start method");
            return new Result { Status = true };
        }
        catch (CustomException) when (selectedLogType == LogType.Warning)
        {
            Logger.Instance.Log(LogType.Warning, "Skipped Logic In Method");
            return new Result { Status = true };
        }
        catch
        {
            return new Result { Status = false };
        }
    }
}

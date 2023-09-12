namespace Mod2.Lection5.Hw1;

internal class Actions
{
    internal delegate Result MethodDelegate();

    internal static Result StartMethod()
    {
        Logger.Instance.Info("Start method");

        return new Result { Status = true };
    }

    internal static Result SkippedLogicInMethod()
    {
        Logger.Instance.Warning("Skipped Logic In Method");

        return new Result { Status = true };
    }

    internal static Result BreakLogic()
    {
        return new Result { Status = false };
    }
}

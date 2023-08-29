﻿namespace Mod2.Lection1.Hw1
{
    internal class Actions
    {
        internal delegate Result MethodDelegate();

        internal static Result StartMethod()
        {
            Logger.GetInstance.Info("Start method");

            return new Result { Status = true };
        }

        internal static Result SkippedLogicInMethod()
        {
            Logger.GetInstance.Warning("Skipped Logic In Method");

            return new Result { Status = true };
        }

        internal static Result BreakLogic()
        {
            return new Result { Status = false };
        }
    }
}
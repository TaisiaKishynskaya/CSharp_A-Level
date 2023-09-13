using Mod2.Lection5.Hw1.Models;
using Mod2.Lection5.Hw1.Services;

namespace Mod2.Lection5.Hw1;

internal class Starter
{
    internal static void Run()
    {
        var random = new Random();

        for (var i = 0; i < 100; i++)
        {
            var dictionary = new Dictionary<int, Actions.MethodDelegate>
                {
                    { 1, Actions.StartMethod },
                    { 2, Actions.SkippedLogicInMethod },
                    { 3, Actions.BreakLogic }
                };

            var randomMethod = random.Next(1, 3);
            dictionary[randomMethod]();

            if (randomMethod == 3)
            {
                Logger.Instance.Log(LogType.Error, $"Action failed by a reason: {Result.ErrorMessage}");
            }
        }

        //File.WriteAllText("log.txt", Logger.Instance.GetLogs());
    }
}

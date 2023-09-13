using Mod2.Lection5.Hw1.Models;
using Mod2.Lection5.Hw1.Services;
using System.Collections.Generic;

namespace Mod2.Lection5.Hw1;

internal class Starter
{
    internal static void Run()
    {
        var random = new Random();

        for (var i = 0; i < 100; i++)
        {
            var dictionary = new Dictionary<int, LogType>
                {
                    { 1, LogType.Info },
                    { 2, LogType.Warning },
                    { 3, LogType.Error }
                };

            var randomMethod = random.Next(1, 3);
            var selectedLogType = dictionary[randomMethod];

            Actions.SetMessage(selectedLogType);

            if (randomMethod == 3)
            {
                Logger.Instance.Log(LogType.Error, $"Action failed by a reason: {Result.ErrorMessage}");
            }
        }

        //File.WriteAllText("log.txt", Logger.Instance.GetLogs());
    }
}

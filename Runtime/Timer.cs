namespace Utilities
{
    using System;
    using System.Diagnostics;
    using Debug = UnityEngine.Debug;

    public static class Timer
    {
        public static void LogTime(string actionName, Action action)
        {
            var stopWatch = Stopwatch.StartNew();
            action();
            stopWatch.Stop();
            Debug.Log($"{actionName} took {Convert.ToInt32(stopWatch.ElapsedMilliseconds)} ms.");
        }
    }
}
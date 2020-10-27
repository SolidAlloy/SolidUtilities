namespace SolidUtilities.Helpers
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;
    using Debug = UnityEngine.Debug;

    public enum TimeUnit { Milliseconds, Nanoseconds }

    /// <summary>
    /// Basic timer that logs execution time of a method or part of the method. It does not warm up the execution and
    /// runs the actions only once.
    /// </summary>
    public static class Timer
    {
        private const int NanosecondsInAMillisecond = 1000000;

        /// <summary>Log time in ms the action took.</summary>
        /// <param name="actionName">Name of the action which execution is measured.</param>
        /// <param name="timeUnit">The time unit to use (ms, ns, etc.)</param>
        /// <param name="action">Action to execute.</param>
        /// <example><code>
        /// LogTime("Show popup", () =>
        /// {
        ///     var dropdownWindow = new DropdownWindow(rect);
        ///     dropdownWindow.ShowInPopup();
        /// });
        /// </code></example>
        [PublicAPI] public static void LogTime(string actionName, TimeUnit timeUnit, Action action)
        {
            var stopWatch = Stopwatch.StartNew();
            action();
            stopWatch.Stop();

            switch (timeUnit)
            {
                case TimeUnit.Milliseconds:
                    Debug.Log($"{actionName} took {Convert.ToInt32(stopWatch.ElapsedMilliseconds)} ms.");
                    break;
                case TimeUnit.Nanoseconds:
                    Debug.Log($"{actionName} took {Convert.ToInt32(stopWatch.Elapsed.TotalMilliseconds * NanosecondsInAMillisecond)} ns.");
                    break;
                default:
                    Debug.Log($"{nameof(LogTime)} does not have an implementation of the following time unit yet: {timeUnit}.");
                    break;
            }
        }
    }
}
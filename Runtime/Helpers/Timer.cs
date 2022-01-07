namespace SolidUtilities
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;
    using Debug = UnityEngine.Debug;

    /// <summary>
    /// Basic timer that logs execution time of a method or part of the method. It does not warm up the execution and
    /// runs the actions only once.
    /// </summary>
    [PublicAPI]
    public class Timer : IDisposable
    {
        private TimeUnit _timeUnit;
        private Stopwatch _stopwatch;
        private string _actionName;
        private int _iterationCount;
        public string Message;

        private Timer(string actionName, TimeUnit timeUnit, int iterationCount)
        {
            _timeUnit = timeUnit;
            _stopwatch = Stopwatch.StartNew();
            _actionName = actionName;
            _iterationCount = iterationCount;
            Message = null;
        }

        private enum TimeUnit { Milliseconds, Nanoseconds }

        /// <summary>Log time the action took in milliseconds.</summary>
        /// <param name="actionName">Name of the action which execution is measured.</param>
        /// <param name="iterationCount">Number of iterations an action is run inside the timer. Defaults to 1.</param>
        /// <returns>New instance of timer.</returns>
        /// <example><code>
        /// using (Timer.CheckInMilliseconds("Show popup"))
        /// {
        ///     var dropdownWindow = new DropdownWindow(rect);
        ///     dropdownWindow.ShowInPopup();
        /// }
        /// </code></example>
        public static Timer CheckInMilliseconds(string actionName, int iterationCount = 1)
        {
            return new Timer(actionName, TimeUnit.Milliseconds, iterationCount);
        }

        /// <summary>Log time the action took in nanoseconds.</summary>
        /// <param name="actionName">Name of the action which execution is measured.</param>
        /// <param name="iterationCount">Number of iterations an action is run inside the timer. Defaults to 1.</param>
        /// <returns>New instance of timer.</returns>
        /// <example><code>
        /// using (Timer.CheckInNanoseconds("Show popup"))
        /// {
        ///     var dropdownWindow = new DropdownWindow(rect);
        ///     dropdownWindow.ShowInPopup();
        /// }
        /// </code></example>
        public static Timer CheckInNanoseconds(string actionName, int iterationCount = 1)
        {
            return new Timer(actionName, TimeUnit.Nanoseconds, iterationCount);
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            int totalTime = GetTotalTime();
            string unitName = GetUnitName();

            string message = $"{_actionName} took {totalTime} {unitName}.";

            if (_iterationCount > 1)
                message += $" One iteration took {GetIterationTime(totalTime)} {unitName} on average.";

            Message = message;
            Debug.Log(Message);
        }

        private int GetTotalTime()
        {
            const int nanosecondsInAMillisecond = 1000000;

            switch (_timeUnit)
            {
                case TimeUnit.Milliseconds:
                    return Convert.ToInt32(_stopwatch.ElapsedMilliseconds);
                case TimeUnit.Nanoseconds:
                    return Convert.ToInt32(_stopwatch.Elapsed.TotalMilliseconds * nanosecondsInAMillisecond);
            }

            throw new NotImplementedException();
        }

        private int GetIterationTime(int totalTime) => totalTime / _iterationCount;

        private string GetUnitName()
        {
            switch (_timeUnit)
            {
                case TimeUnit.Milliseconds:
                    return "ms";
                case TimeUnit.Nanoseconds:
                    return "ns";
            }

            throw new NotImplementedException();
        }
    }
}
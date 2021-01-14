namespace SolidUtilities.Helpers
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
    public readonly struct Timer : IDisposable
    {
        private readonly TimeUnit _timeUnit;
        private readonly Stopwatch _stopwatch;
        private readonly string _actionName;

        private Timer(string actionName, TimeUnit timeUnit)
        {
            _timeUnit = timeUnit;
            _stopwatch = Stopwatch.StartNew();
            _actionName = actionName;
        }

        private enum TimeUnit { Milliseconds, Nanoseconds }

        /// <summary>Log time the action took in milliseconds.</summary>
        /// <param name="actionName">Name of the action which execution is measured.</param>
        /// <returns>New instance of timer.</returns>
        /// <example><code>
        /// using (Timer.CheckInMilliseconds("Show popup"))
        /// {
        ///     var dropdownWindow = new DropdownWindow(rect);
        ///     dropdownWindow.ShowInPopup();
        /// }
        /// </code></example>
        public static Timer CheckInMilliseconds(string actionName)
        {
            return new Timer(actionName, TimeUnit.Milliseconds);
        }

        /// <summary>Log time the action took in nanoseconds.</summary>
        /// <param name="actionName">Name of the action which execution is measured.</param>
        /// <returns>New instance of timer.</returns>
        /// <example><code>
        /// using (Timer.CheckInNanoseconds("Show popup"))
        /// {
        ///     var dropdownWindow = new DropdownWindow(rect);
        ///     dropdownWindow.ShowInPopup();
        /// }
        /// </code></example>
        public static Timer CheckInNanoseconds(string actionName)
        {
            return new Timer(actionName, TimeUnit.Nanoseconds);
        }

        public void Dispose()
        {
            const int nanosecondsInAMillisecond = 1000000;

            _stopwatch.Stop();

            switch (_timeUnit)
            {
                case TimeUnit.Milliseconds:
                    Debug.Log($"{_actionName} took {Convert.ToInt32(_stopwatch.ElapsedMilliseconds)} ms.");
                    break;
                case TimeUnit.Nanoseconds:
                    Debug.Log($"{_actionName} took {Convert.ToInt32(_stopwatch.Elapsed.TotalMilliseconds * nanosecondsInAMillisecond)} ns.");
                    break;
            }
        }
    }
}
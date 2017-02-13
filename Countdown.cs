using System;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace QuickTimer
{
    public class Countdown : INotifyPropertyChanged
    {
        private TimeSpan _interval = TimeSpan.FromMilliseconds(100);
        private Timer _timer;
        private Stopwatch _sw = new Stopwatch();


        #region Constructors

        public Countdown()
        {
            Duration = TimeSpan.FromMinutes(0);
            SetRemaining();

            AutoResetEvent autoEvent = new AutoResetEvent(false);
            TimerCallback _tcb = OnTimerTick;
            _timer = new Timer(_tcb, autoEvent, TimeSpan.FromMilliseconds(0), _interval);
        }


        public Countdown(double minutes, bool run = false)
        {
            Duration = TimeSpan.FromMinutes(minutes);
            SetRemaining();

            if (run) _sw.Start();

            AutoResetEvent autoEvent = new AutoResetEvent(false);
            TimerCallback _tcb = OnTimerTick;
            _timer = new Timer(_tcb, autoEvent, TimeSpan.FromMilliseconds(0), _interval);
        }


        public Countdown(TimeSpan initialDuration, bool run)
        {
            Duration = initialDuration;
            SetRemaining();

            if (run) _sw.Start();

            AutoResetEvent autoEvent = new AutoResetEvent(false);
            TimerCallback _tcb = OnTimerTick;
            _timer = new Timer(_tcb, autoEvent, TimeSpan.FromMilliseconds(0), _interval);
        }

        #endregion Constructors

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #region Properties

        private TimeSpan _duration;
        /// <summary>
        /// The total TimeSpan of the timer countdown.
        /// Used to calculate percent complete as Remaining / Duration.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value > TimeSpan.FromTicks(0) ? value : TimeSpan.FromTicks(0);
                NotifyPropertyChanged("Duration");
                SetRemaining();
            }
        }


        /// <summary>
        /// The TimeSpan of the remaining countdown.
        /// Used to calculate percent complete as Remaining / Duration.
        /// </summary>
        public TimeSpan Remaining { get; private set; }

        /// <summary>
        /// Format the Remaining property as hh:mm:ss.f
        /// </summary>
        public string RemainString => Remaining.ToString(@"hh\:mm\:ss\.f");

        /// <summary>
        /// Calculated as Duration - Remaining.
        /// </summary>
        public TimeSpan Elapsed { get { return Duration - Remaining; } }

        public bool IsRunning { get { return _sw.IsRunning; } }

        #endregion Properties


        #region Duration change methods

        public void AddDuration(TimeSpan span)
        {
            if (Duration.Ticks < 0)
            {
                Duration = TimeSpan.Zero;
            }
            Duration += span;
            SetRemaining();
        }


        public void AddDurationHours(double hours)
        {
            AddDuration(TimeSpan.FromHours(hours));
        }


        public void AddDurationMinutes(double minutes)
        {
            AddDuration(TimeSpan.FromMinutes(minutes));
        }


        public void AddDurationSeconds(double seconds)
        {
            AddDuration(TimeSpan.FromSeconds(seconds));
        }
        #endregion Duration change methods


        #region Timer control methods

        public void Start()
        {
            _sw.Start();
        }


        public void Stop()
        {
            _sw.Stop();
        }


        public void Reset()
        {
            _sw.Reset();
            // Set Duration so that NotifyPropertyChanged is raised.
            Duration = Duration;
        }


        public void Restart()
        {
            _sw.Restart();
        }

        public void Clear()
        {
            _sw.Reset();
            Duration = TimeSpan.FromTicks(0);
        }

        #endregion Timer control methods

        private void SetRemaining()
        {
            if (_sw.Elapsed <= Duration)
            {
                Remaining = Duration - _sw.Elapsed;
            }
            else
            {
                Stop();
                Remaining = TimeSpan.FromTicks(0);
                _sw.Reset();
                Finished(this, new EventArgs());
            }
            NotifyPropertyChanged("Remaining");
            NotifyPropertyChanged("RemainString");
        }

        private void OnTimerTick(object state)
        {
            if (IsRunning)
            {
                SetRemaining();
            }
        }


        public event EventHandler Finished = delegate { };
    }
}

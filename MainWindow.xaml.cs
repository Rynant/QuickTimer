using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;
using System.ComponentModel;

namespace QuickTimer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Countdown clock;
        public BackgroundWorker worker;

        public MainWindow()
        {

            if (App.CommandLineTime > 0)
            {
                clock = new Countdown(App.CommandLineTime, true);

            }
            else
            {
                clock = new Countdown();
            }

            clock.Finished += OnFinished;
            InitializeComponent();
        }

        public void OnFinished(object sender, EventArgs args)
        {
            SystemSounds.Exclamation.Play();
            Action bringToTop = () =>
            {
                Window1.Activate();
                Window1.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
            };
            Dispatcher.BeginInvoke(bringToTop);
        }

        private void minute_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            clock.AddDurationMinutes(Convert.ToInt32(b.Content));
        }


        private void minute_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button b = (Button)sender;
            clock.AddDurationMinutes(-1 * Convert.ToInt32(b.Content));
        }


        private void second_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            clock.AddDurationSeconds(Convert.ToInt32(b.Content));
        }


        private void second_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button b = (Button)sender;
            clock.AddDurationSeconds(-1 * Convert.ToInt32(b.Content));
        }


        private void start_Click(object sender, RoutedEventArgs e)
        {
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            clock.Start();
        }


        private void pause_Click(object sender, RoutedEventArgs e)
        {
            if (clock.IsRunning)
                clock.Stop();
            else
                clock.Start();
        }


        private void clear_Click(object sender, RoutedEventArgs e) => clock.Clear();

        private void reset_Click(object sender, RoutedEventArgs e) => clock.Reset();
        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) => DataContext = clock;
    }
}

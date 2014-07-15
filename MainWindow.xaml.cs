using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace QuickTimer
{

    public static class MyCmd
    {
        public static readonly RoutedUICommand Reset = new RoutedUICommand();
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Countdown clock;

        public MainWindow()
        {
            InitializeComponent();

            if (App.CommandLineTime > 0)
            {
                clock = new Countdown(App.CommandLineTime, true);

            }
            else
            {
                clock = new Countdown();
            }

            clock.Finished += new EventHandler(OnFinished);

        }

        public void OnFinished(object sender, EventArgs args)
        {
            SystemSounds.Exclamation.Play();
            Action bringToTop = () =>
            {
                Window1.Topmost = true;
                Window1.TaskbarItemInfo.ProgressValue = 1.0;
                Window1.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
            };
            Dispatcher.BeginInvoke(bringToTop);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.DataContext = clock;
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
            this.clock.Start();
        }


        private void pause_Click(object sender, RoutedEventArgs e)
        {
            if (clock.IsRunning)
            {
                clock.Stop();
            }
            else
            {
                clock.Start();
            }
        }


        private void clear_Click(object sender, RoutedEventArgs e)
        {
            clock.Clear();
        }


        private void reset_Click(object sender, RoutedEventArgs e)
        {
            clock.Reset();
        }
    }
}

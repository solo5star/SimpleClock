using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimpleClock
{
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        Func<string> titleTicker;

        public MainWindow()
        {
            InitializeComponent();

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);

            dt.Start();

            titleTicker = () => textBlock_Clock.Text;
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            Title = titleTicker();

            textBlock_Clock.Text = DateTime.Now.ToString("tt hh:mm:ss");
            
            TimeSpan ts = sw.Elapsed;
            textBlock_Stopwatch.Text = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        private void button_StopwatchToggle_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                button_StopwatchToggle.Content = "Start";
                sw.Stop();
            }
            else
            {
                button_StopwatchToggle.Content = "Stop";
                sw.Start();
            }
        }

        private void button_StopwatchReset_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            button_StopwatchToggle.Content = "Start";
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabItem_Clock.IsSelected)
            {
                titleTicker = () => textBlock_Clock.Text;
            }
            else if (tabItem_Stopwatch.IsSelected)
            {
                titleTicker = () => textBlock_Stopwatch.Text;
            }
        }
    }
}

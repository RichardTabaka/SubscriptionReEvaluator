using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SubReEvaluator
{
    /// <summary>
    /// Interaction logic for WatchingWindow.xaml
    /// </summary>
    public partial class WatchingWindow : Window
    {
        public WatchingWindow()
        {
            InitializeComponent();
            UsingDb();
            
        }
        
        public int hours = 0;
        public int minutes = 0;
        public int seconds = 0;
        public SubContext db = new SubContext();
        public List<string> names = new List<string>();
        public List<Subscription> subs = new List<Subscription>();

        public void UsingDb()
        {
            foreach (var sub in db.Subscriptions)
            {
                names.Add(sub.Name);
            }
            WatchingSubNameCB.ItemsSource = names;

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            StopTimer();
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();

        }

        private void WatchingSubNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(var sub in db.Subscriptions)
            {
                if (sub.Name == (string)WatchingSubNameCB.SelectedItem)
                {
                    var timeSpan = TimeSpan.FromHours(sub.Time);
                    hours = timeSpan.Hours;
                    minutes = timeSpan.Minutes;
                    seconds = timeSpan.Seconds;
                }
            }
            string timeString = $"{hours}:{minutes}:{seconds}";
            TimeLabel.Content = timeString;
        }



        // Timer Funcionality:


        public DispatcherTimer dt = new DispatcherTimer();
            
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            StopTimer();
        }

        // StopTimer() has the functionality outside of the Stop_Click function to allow it to be used if return to menu button is clicked while the timer is running
        private void StopTimer()
        {
            dt.Stop();
            double newTime = (hours + ((double)minutes / 60) + ((double)seconds / 60 / 60));
            foreach(var sub in db.Subscriptions)
            {
                if(sub.Name == (string)WatchingSubNameCB.SelectedItem)
                {
                    sub.Time = newTime;
                }
            }
            db.SaveChanges();
        }
        
        private void Dt_Tick(object sender, EventArgs e)
        {
            seconds++;
            if(seconds == 60)
            {
                seconds = 0;
                minutes++;
                if(minutes == 60)
                {
                    minutes = 0;
                    hours++;
                }
            }

            string timeString = $"{hours}:{minutes}:{seconds}";
            TimeLabel.Content = timeString;
        }
    }
}

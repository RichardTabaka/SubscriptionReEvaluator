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

namespace SubReEvaluator
{
    /// <summary>
    /// Interaction logic for EditSubWindow.xaml
    /// </summary>
    public partial class EditSubWindow : Window
    {
        public EditSubWindow()
        {
            InitializeComponent();
            BuildPage();
        }
        public void BuildPage()
        {
            // Create dropdown for Item Selection
            // Doing this instead of a textbox guarauntees the name is correct for updating
            SubContext db = new SubContext();
            List<string> names = new List<string>();
            foreach (var sub in db.Subscriptions)
            {
                names.Add(sub.Name);
            }
            SubNameCB.ItemsSource = names;
        }

        private void SubmitEditButtonClick(object sender, RoutedEventArgs e)
        {
            SubContext db = new SubContext();
            Subscription toBeUpdated = new Subscription();
            foreach(var sub in db.Subscriptions)
            {
                if(sub.Name == (string)SubNameCB.SelectedItem)
                {
                    toBeUpdated = sub;
                }
            }
            toBeUpdated.Cost = Convert.ToDouble(EditCost.Text);
            toBeUpdated.Time = Convert.ToDouble(EditTime.Text);
            db.Update(toBeUpdated);
            db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void SubNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubContext db = new SubContext();
            foreach (var sub in db.Subscriptions)
            {
                if (sub.Name == (string)SubNameCB.SelectedItem)
                {
                    EditCost.Text = sub.Cost.ToString();
                    EditTime.Text = sub.Time.ToString();
                }
            }
        }

        private void SubmitDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SubContext db = new SubContext();
            foreach (var sub in db.Subscriptions)
            {
                if (sub.Name == (string)SubNameCB.SelectedItem)
                {
                    db.Remove(sub);
                    db.SaveChanges();
                }
            }

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}

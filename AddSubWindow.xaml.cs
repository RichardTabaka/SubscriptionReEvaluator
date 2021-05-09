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
    /// Interaction logic for AddSubWindow.xaml
    /// </summary>
    public partial class AddSubWindow : Window
    {
        public AddSubWindow()
        {
            InitializeComponent();
        }

        private void SubmitSubButton_Click(object sender, RoutedEventArgs e)
        {
            SubContext db = new SubContext();
            Subscription newSub = new Subscription() {
                Cost = Convert.ToDouble(InputCost.Text),
                Name = InputName.Text,
                Time = 0
            };
            db.Add(newSub);
            db.SaveChanges();

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}

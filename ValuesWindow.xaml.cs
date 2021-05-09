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
    /// Interaction logic for ValuesWindow.xaml
    /// </summary>
    public partial class ValuesWindow : Window
    {
        public ValuesWindow()
        {
            InitializeComponent();
            GetData();
        }
        public void GetData()
        {
            SubContext db = new SubContext();
            List<Subscription> subs = new List<Subscription>();
            foreach(var sub in db.Subscriptions)
            {
                if(sub.Name != "")
                {
                    sub.Value = Math.Round(sub.Time / sub.Cost, 3);
                    subs.Add(sub);
                }
            } 
            // Sort the List to provide the Data in most valuable to least in the datagrid
            subs.Sort((y, x) => x.Value.CompareTo(y.Value));
            ValueDataGrid.ItemsSource = subs;
        }
    }
}

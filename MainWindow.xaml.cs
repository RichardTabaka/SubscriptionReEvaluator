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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace SubReEvaluator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InsertData();
            
        }
        
        public void InsertData()
        {
            SubContext db = new SubContext();
            //Label SubAdder;
            foreach (var sub in db.Subscriptions)
            {
                SP.Children.Add(new Label() { Content = $"{sub.Name}", FontSize = 16, HorizontalAlignment = HorizontalAlignment.Center });
                //SubAdder = new Label() { Content = $"Cost: {sub.Cost} Time Spent Using: {sub.Time}", FontSize = 16, HorizontalAlignment = HorizontalAlignment.Center };
                //SP.Children.Add(SubAdder);
            }

            Button addSubButton = new Button() { Name = "AddSubButton", Content = "Add a Subscription", FontSize = 14, Width = 150, Height = 20 };
            
            addSubButton.Click += new RoutedEventHandler(BtnClick);
            SP.Children.Add(addSubButton);

            Button editSubButton = new Button() { Name = "EditSubButton", Content = "Edit a Subscription", FontSize = 14, Width = 150, Height = 20 };

            editSubButton.Click += new RoutedEventHandler(BtnClick2);
            SP.Children.Add(editSubButton);

            Button viewValuesButton = new Button() { Name = "ViewValuesButton", Content = "View Values", FontSize = 14, Width = 150, Height = 20 };

            viewValuesButton.Click += new RoutedEventHandler(BtnClick3);
            SP.Children.Add(viewValuesButton);

        }

        private void BtnClick3(object sender, RoutedEventArgs e)
        {
            ValuesWindow vw = new ValuesWindow();
            vw.Show();
        }

        private void BtnClick2(object sender, RoutedEventArgs e)
        {
            EditSubWindow es = new EditSubWindow();
            es.Show();
            this.Close();
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            AddSubWindow a = new AddSubWindow();
            a.Show();
            this.Close();
            
        }
    }
    public class Subscription // Entity
    {
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Time{ get; set; }
        public double Value { get; set; }
    }

    public class SubContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public SubContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Some example data for design purposed
        {
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription()
                {
                    SubscriptionId = 1,
                    Cost = 4.99,
                    Name = "Netflix",
                    Time = 5
                }, new Subscription()
                {
                    SubscriptionId = 2,
                    Name = "Hulu",
                    Cost = 12.99,
                    Time = 10
                }
                );

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = SubscriptionsDb.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

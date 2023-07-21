using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace new_demo
{
    /// <summary>
    /// Логика взаимодействия для Seller.xaml
    /// </summary>
    public partial class Seller : Window
    {
        Staff user=new Staff();
        DateTime loginTime= DateTime.Now;
        DispatcherTimer warningTimer = new DispatcherTimer();
        DispatcherTimer endSessionTimer = new DispatcherTimer();
        public Seller(Staff user)
        {
            this.user = user;
            InitializeComponent();
            fName.Text = user.FIO.Split(' ')[1];
            lName.Text = user.FIO.Split(' ')[0];
            position.Text = user.Position;
            photo.Source = new BitmapImage(new Uri("Resources/" + user.FIO.Split(' ')[0] + ".jpeg", UriKind.Relative)); 
            loginTime = DateTime.Now;
            DispatcherTimer sessionTimer = new DispatcherTimer();
            sessionTimer.Interval = TimeSpan.FromSeconds(1);
            sessionTimer.IsEnabled = true;
            sessionTimer.Tick += ShowTimer;
            sessionTimer.Start();
            
            warningTimer.Interval = TimeSpan.FromSeconds(10);
            warningTimer.IsEnabled = true;
            warningTimer.Tick += ShowWarning;
            warningTimer.Start();

            endSessionTimer.Interval = TimeSpan.FromSeconds(30);
            endSessionTimer.IsEnabled = true;
            endSessionTimer.Tick += EndSession;
            endSessionTimer.Start();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder createOrder = new CreateOrder();

            if (createOrder.ShowDialog() == true)
            {
                MessageBox.Show("true");
            }
            else MessageBox.Show("false");
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(false);
            window.Show();
            Close();
        }

        private void ShowTimer(object sender, EventArgs e)
        {
            sessionTimer.Text = (DateTime.Now - loginTime).ToString();
        }

        private void ShowWarning(object sender, EventArgs e) { 
            MessageBox.Show("Осталось 15 минут");
            warningTimer.Stop();
            warningTimer.Tick -= ShowWarning;
        }

        private void EndSession(object sender, EventArgs e)
        {
            
            MainWindow window = new MainWindow(true);
            window.Show();
            Close();
            endSessionTimer.Stop();
            endSessionTimer.Tick -= ShowWarning;

        }


    }
}

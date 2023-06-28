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

namespace new_demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordBox.Visibility = Visibility.Collapsed;
            passwordBoxView.Visibility = Visibility.Visible;
            passwordBoxView.Text = passwordBox.Password;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBoxView.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
            passwordBox.Password = passwordBoxView.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text;
            string password="";
            if (passwordCB.IsChecked==true) {
                password = passwordBoxView.Text;
            }
            else
            {
                password = passwordBox.Password;
            }
               
            
            try
            {
                Staff user = null;
                using (demo7Entities entities = new demo7Entities())
                {
                    user = entities.Staff.Where(s => s.Login == login && s.Password == password).FirstOrDefault();
                }
                if (user != null)
                {
                    Seller window = new Seller(user);
                    window.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }
    }
}

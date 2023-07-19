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

namespace new_demo
{
    /// <summary>
    /// Логика взаимодействия для Seller.xaml
    /// </summary>
    public partial class Seller : Window
    {
        Staff user=new Staff();
        public Seller(Staff user)
        {
            this.user = user;
            InitializeComponent();
            fName.Text = user.FIO.Split(' ')[1];
            lName.Text = user.FIO.Split(' ')[0];
            position.Text = user.Position;
            photo.Source = new BitmapImage(new Uri("Resources/" + user.FIO.Split(' ')[0] + ".jpeg", UriKind.Relative)); 
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
    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        List<Clients> clients = new List<Clients>();
        List<Services> services = new List<Services>();
        public CreateOrder()
        {
            InitializeComponent();
            int lastOrder;
            using (demoEntities entities = new demoEntities())
            {
                clients = entities.Clients.ToList();
                services = entities.Services.ToList();
                var order = entities.Orders.ToList();
                lastOrder = order[order.Count - 1].ID + 1;
            }
            clientsGrid.ItemsSource = clients;
            service1.ItemsSource = services;
            service1.DisplayMemberPath = "ServiceName";
            orderNumberTB.Text = lastOrder.ToString();
            orderContent.Visibility = Visibility.Hidden;
        }

        private void orderNumberTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                orderContent.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int? id = Convert.ToInt32(orderNumberTB.Text);
                if (id == null) { e.Handled = true; MessageBox.Show("Введите номер заказа"); return; }
                Clients client = (Clients)clientsGrid.SelectedItem;
                if (client == null) { e.Handled = true; MessageBox.Show("Выберите клиента"); return; }

                DateTime date = DateTime.Now;
                var boxex = orderContent.Children.OfType<ComboBox>().ToList();
                List<Services> services = new List<Services>();
                foreach (var serv in boxex)
                {
                    Services service = (Services)serv.SelectedItem;
                    if (service == null) { MessageBox.Show("Выберите услугу"); return; }
                    services.Add(service);
                }
                string servStr = services[0].ID.ToString();
                if (services.Count > 1)
                {
                    for (int i = 1; i < services.Count; i++)
                        servStr += ", " + services[i].ID.ToString();
                }
                Orders order = new Orders();
                order.ID = (int)id;
                order.OrderCode = client.ClientCode.ToString() + "/" + date.Day.ToString() + "." + date.Month.ToString()
                    + "." + date.Year.ToString();
                order.CreateDate = date.Date;
                order.OrderTime = date.TimeOfDay;
                order.ClientCode = client.ClientCode;
                order.Services = servStr;
                order.Status = "Новая";

                using (demoEntities entities = new demoEntities())
                {
                    foreach (Services s in services)
                    {
                        order.Services1.Add(entities.Services.Find(s.ID));
                    }
                    entities.Orders.Add(order);
                    entities.SaveChanges();
                }
                DialogResult = true;
            }
            catch { MessageBox.Show("some error"); }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var boxex = orderContent.Children.OfType<ComboBox>().ToList();
            ComboBox last = boxex[boxex.Count - 1];
            ComboBox newServ = new ComboBox();
            string lastName = last.Name;
            newServ.Name = lastName.Substring(0, lastName.Length - 1) + ((int)lastName[lastName.Length - 1] + 1).ToString();
            newServ.ItemsSource = services;
            newServ.DisplayMemberPath = "ServiceName";
            orderContent.Children.Add(newServ);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CreateClient createClient = new CreateClient();
            if (createClient.ShowDialog() == true)
            {
                MessageBox.Show("true");
            }
            else MessageBox.Show("false");
        }
    }
}

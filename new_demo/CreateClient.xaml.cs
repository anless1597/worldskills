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
    /// Логика взаимодействия для CreateClient.xaml
    /// </summary>
    public partial class CreateClient : Window
    {
        Clients lastClient= new Clients();
        public CreateClient()
        {
            InitializeComponent();
            int lastClientNum;
            using (demo7Entities entities = new demo7Entities())
            {
                var clients = entities.Clients.ToList();
                lastClient = clients[clients.Count - 1];
                lastClientNum = lastClient.ClientCode;
            }
            clientCodeTB.Text = lastClient.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int? clientCode;
            string fio, passport, adress, email, password, phoneNumber;
            DateTime? birthday;

            clientCode = Convert.ToInt32(clientCodeTB.Text);
            fio = fioTB.Text;
            passport = "Серия "+seriaTB.Text+" Номер "+nomerTB.Text;
            adress = indexTB.Text+", г. "+cityTB.Text+", ул. "+streetTB.Text+", "+houseTB.Text+", кв. "+apartmentTB.Text;
            email = emailTB.Text;
            birthday = birthdayTB.SelectedDate;

            password = "cl" + (Convert.ToInt32(lastClient.Password.Substring(2, lastClient.Password.Length - 2)) + 1).ToString();

            Clients client = new Clients();
            client.ClientCode = (int)clientCode;
            client.FIO = fio;
            client.Email = email;
            client.Password = password; 
            client.Adress= adress;
            client.Birthday = (DateTime)birthday;
            client.Passport= passport;
        }
    }
}

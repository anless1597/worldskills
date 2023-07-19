using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace new_demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int loginCounter = 0;
        string captchaString = "";
        bool captchaEnabled = false;
        public MainWindow()
        {
            InitializeComponent();
            captchaImage.Visibility = Visibility.Collapsed;
            captchaText.Visibility = Visibility.Collapsed;
            generateCaptchaButton.Visibility = Visibility.Collapsed;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text;
            string password = "";
            if (passwordCB.IsChecked == true) {
                password = passwordBoxView.Text;
            }
            else
            {
                password = passwordBox.Password;
            }


            try
            {
                Staff user = null;
                using (demoEntities entities = new demoEntities())
                {
                    user = entities.Staff.Where(s => s.Login == login && s.Password == password).FirstOrDefault();
                }
                if (user != null&&(captchaEnabled == false|| captchaEnabled == true && captchaText.Text == captchaString))
                {
                        Seller window = new Seller(user);
                        window.Show();
                        Close();
                }
                else
                {
                    loginCounter++;
                    MessageBox.Show("Неверно введены данные");
                    if (loginCounter >= 2) {
                        ShowCaptcha();
                        if (captchaEnabled == false)
                        {
                            captchaImage.Visibility = Visibility.Visible;
                            captchaText.Visibility = Visibility.Visible;
                            generateCaptchaButton.Visibility = Visibility.Visible;
                            captchaEnabled = true;
                        }
                        else
                        {
                            loginButton.IsEnabled = false;
                            await EnableLogin();
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        async Task EnableLogin(){
            await Task.Delay(10000);
            loginButton.IsEnabled = true;
        }


        private void ShowCaptcha()
        {
            Bitmap bitmap = GenerateCaptha();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                captchaImage.Source = bitmapImage;
            }
        }
        private Bitmap GenerateCaptha()
        {
            Bitmap captha = new Bitmap(150, 50);
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            captchaString = "";
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                captchaString += alphabet[rnd.Next(0, alphabet.Length)];
            }
            using (Graphics gr = Graphics.FromImage(captha))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.Clear(System.Drawing.Color.White);

                int ch_wid = 30;
                int ch_hei = 30;

                for (int i = 0; i < captchaString.Length; i++)
                {
                    float font_size = rnd.Next(50, 60);
                    using (Font the_font = new Font("Arial", font_size, System.Drawing.FontStyle.Bold))
                    {
                        // Центрируем текст.
                        using (StringFormat string_format = new StringFormat())
                        {
                            string_format.Alignment = StringAlignment.Center;
                            string_format.LineAlignment = StringAlignment.Center;
                            RectangleF rectf = new RectangleF(i * ch_wid + 15, 10, ch_wid, ch_hei);
                            int X = i * ch_wid + 30;
                            // Преобразование текста в путь.
                            using (GraphicsPath graphics_path = new GraphicsPath())
                            {
                                graphics_path.AddString(captchaString[i].ToString(), the_font.FontFamily, (int)the_font.Style, the_font.Size, rectf, string_format);
                                // Произвольные случайные параметры деформации.
                                PointF[] pts =
                                {
                                        new PointF(
                                            (float)(X + rnd.Next(ch_wid) / 4),
                                            (float)(10+rnd.Next(ch_hei) / 4)),
                                        new PointF(
                                            (float)(X + ch_wid - rnd.Next(ch_wid) / 4),
                                            (float)(10+rnd.Next(ch_hei) / 4)),
                                        new PointF(
                                            (float)(X + rnd.Next(ch_wid) / 4),
                                            (float)(10+ch_hei - rnd.Next(ch_hei) / 4)),
                                        new PointF(
                                            (float)(X + ch_wid - rnd.Next(ch_wid) / 4),
                                            (float)(10+ch_hei - rnd.Next(ch_hei) / 4))
                                };
                                System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
                                graphics_path.Warp(pts, rectf, mat,WarpMode.Perspective, 0);

                                // Поворачиваем бит случайным образом.
                                float dx = (float)(X + ch_wid / 2);
                                float dy = (float)(ch_hei / 2);
                                gr.TranslateTransform(-dx, -dy, MatrixOrder.Append);
                                int angle = rnd.Next(-30, 30);
                                gr.RotateTransform(angle, MatrixOrder.Append);
                                gr.TranslateTransform(dx, dy, MatrixOrder.Append);

                                gr.FillPath(System.Drawing.Brushes.Blue, graphics_path);
                                gr.ResetTransform();
                            }
                        }
                    }
                }
                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
                for(int i = 0; i< 3; i++)
                {
                    gr.DrawLine(pen, rnd.Next(0,75), rnd.Next(0, 50), rnd.Next(75, 150), rnd.Next(0, 50));
                }
                for (int i = 0; i < 150; ++i)
                    for (int j = 0; j < 50; ++j)
                        if (rnd.Next() % 20 == 0)
                            captha.SetPixel(i, j, System.Drawing.Color.Gray);

            }
            return captha;
        }

        private void generateCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            ShowCaptcha();
        }
    }
}

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

namespace Negr.PageApp
{
    /// <summary>
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AreCredentialsValid(TextLogin.Text, TextPassword.Password))
            {
                var user = Requests.Requests.GetUser(TextLogin.Text, TextPassword.Password);
                if (user != null)
                {
                    this.NavigationService.Navigate(new MainPage(user));
                }
                else
                {
                    MessageBox.Show("Не правильно!!\n пароль или логин");
                }
            }
            else
            {
                MessageBox.Show("Не заполнены поля логина или пароля");
            }
        }

        public static bool AreCredentialsValid(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }
    }
}

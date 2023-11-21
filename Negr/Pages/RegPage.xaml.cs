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
using Negr.ClassApp;

namespace Negr.PageApp
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isUserCreated = CreateUser(txtLogin.Text, txtPassword.Password, txtBoxEmail.Text);
                if (isUserCreated)
                {
                    this.NavigationService.Navigate(new AutoPage());
                }
                else
                {
                    MessageBox.Show("Этот пользователь уже существует !!!");
                }
            }
            catch
            { }
        }


        public static bool CreateUser(string username, string password, string email)
        {
            var existingUser = Requests.Requests.GetUserByUsername(username);
            if (existingUser == null)
            {
                Users newUser = new Users()
                {
                    Username = username,
                    Password = password,
                    Email = email
                };

                Requests.Requests.AddUser(newUser);
                return true;
            }
            return false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutoPage());
        }
    }
}

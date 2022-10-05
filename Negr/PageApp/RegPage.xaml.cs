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
                var autoStudent = DBConection.GreenAntiCafeWithBearEntities.Logins.Where(x => x.login == txtLogin.Text).FirstOrDefault();
                if(autoStudent == null)
                {
                    Balances newBalance = new Balances();
                    Users newUsers = new Users();
                    newUsers.Name = txtBoxName.Text;
                    Logins login = new Logins()
                    {
                        login = txtLogin.Text,
                        Password = txtPassword.Text,
                    };

                    newUsers.Logins.Add(login);
                    newUsers.Balances.Add(newBalance);

                    DBConection.GreenAntiCafeWithBearEntities.Users.Add(newUsers);
                    DBConection.GreenAntiCafeWithBearEntities.SaveChanges();
                    
                    this.NavigationService.Navigate(new AutoPage());
                }
               else

                {
                    MessageBox.Show("This user already exists!!");
                }
            }
            catch { }
        }
    }
}

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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Users CorrUser;
        public MainPage(Logins CorrUser)
        {
            InitializeComponent();
            this.CorrUser = CorrUser.Users;
            var CountUs = DBConection.GreenAntiCafeWithBearEntities.Balances.Where(x => x.User_id == CorrUser.User_id).FirstOrDefault();

        }
    }
}

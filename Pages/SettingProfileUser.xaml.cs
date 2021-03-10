using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace EntertainmentX.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingProfileUser.xaml
    /// </summary>
    public partial class SettingProfileUser : Page
    {
        public SettingProfileUser()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var userInf = Entities.GetContext().Users.Where(i => i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault();

            userInf.Login = TxtLogin.Text;
            userInf.Email = TxtEmail.Text;

            if(TxtNewPassword.Text != "" && TxtNewPassword.Text == TxtNewPassRepeat.Text)
            {
                userInf.Password = TxtNewPassword.Text;
            }

            Entities.GetContext().Entry(userInf).State = EntityState.Modified;

            Entities.GetContext().SaveChanges();

            // обращение к родительскому окну
            MainWindow mainWindow = Classes.ParentMainWindow.parentWindow as MainWindow;
            mainWindow.TxtPsevdoUser.Text = userInf.Login;
        }
    }
}

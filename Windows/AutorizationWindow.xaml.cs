using System;
using System.Collections.Generic;
using System.IO;
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

namespace EntertainmentX.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        private bool FileFlag = false;
        public AutorizationWindow()
        {
            InitializeComponent();

            using (FileStream fstream = File.OpenRead(@"FilesUser/AutInfUser.txt"))
            {
                byte[] array = new byte[fstream.Length];

                fstream.Read(array, 0, array.Length);

                string[] textFromFile = System.Text.Encoding.Default.GetString(array).Split(',');

                if(textFromFile.Length == 2)
                {
                    TxtLogin.Text = textFromFile[0].ToString();
                    TxtPassword.Password = textFromFile[1].ToString();
                    FileFlag = true;
                }

            }
        }

        private void BtnGoAut_Click(object sender, RoutedEventArgs e)
        {
            var res = Entities.GetContext().Users.Where(i => i.Email == TxtLogin.Text && i.Password == TxtPassword.Password && i.IdStatusActiveEmail == true).ToList();
            if (res.Any())
            {
                if (ChkUserAut.IsChecked == true && FileFlag == false)
                {
                    using (FileStream fstream = new FileStream($"FilesUser/AutInfUser.txt", FileMode.OpenOrCreate))
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(res.FirstOrDefault().Email+","+ res.FirstOrDefault().Password);
                        fstream.Write(array, 0, array.Length);
                    }
                }
                Classes.IdUserClass.IdUser = res.FirstOrDefault().IdUser;
                MainWindow mainWindow = new MainWindow();
                mainWindow.TxtPsevdoUser.Text = res.FirstOrDefault()?.Login;
                mainWindow.Show();
                this.Close();
            }
        }
    }
}

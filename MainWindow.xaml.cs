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

namespace EntertainmentX
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Classes.ParentMainWindow.parentWindow = this;

            Pages.ListGamePage listGamePage = new Pages.ListGamePage();

            ///////////////////////////////////////////////////////////
            Pages.ListGamePage.ListGamesBuff.Add(Entities.GetContext().Games.ToList());

            listGamePage.TypeListUser = "MainList";

            MainFrame.Navigate(listGamePage);

            Classes.Manager.MainFrame = MainFrame;

            UserFoto.Source = new BitmapImage(new Uri(@"https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/1024px-User_icon_2.svg.png"));

            Random rnd = new Random();
            var gamesAll = Entities.GetContext().Games.ToList();
            var res = gamesAll.Select(i => gamesAll[rnd.Next(0, gamesAll.Count)]).ToList().FirstOrDefault();
            TxtNameMainGame.Text = res.Name;
            ImgMainGame.Source = new BitmapImage(new Uri(res.Poster));

            LstGenre.ItemsSource = Entities.GetContext().Genres.ToList();
        }

        private void BtnMainPage_Click(object sender, RoutedEventArgs e)
        {
            Pages.ListGamePage listGamePage = new Pages.ListGamePage();

            listGamePage.TypeListUser = "MainList";

            Classes.Manager.MainFrame.Navigate(listGamePage);
        }

        private void BtnFavGame_Click(object sender, RoutedEventArgs e)
        {
            Pages.ListGamePage listGamePage = new Pages.ListGamePage();

            listGamePage.TypeListUser = "FaveList";

            Classes.Manager.MainFrame.Navigate(listGamePage);
        }

        private void BtnSettingProfile_Click(object sender, RoutedEventArgs e)
        {
            Pages.SettingProfileUser settingProfileUser = new Pages.SettingProfileUser();

            var userInf = Entities.GetContext().Users.Where(i => i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault();

            settingProfileUser.TxtLogin.Text = userInf.Login;
            settingProfileUser.TxtEmail.Text = userInf.Email;

            Classes.Manager.MainFrame.Navigate(settingProfileUser);
        }

        private void GenresFilter_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;

            string nameGenres = cmd.Content.ToString();

            var res = from genres in Entities.GetContext().Genres where genres.NameGenre == nameGenres
                      join gtog in Entities.GetContext().GamesToGenres on genres.IdGenre equals gtog.IdGenre
                      join games in Entities.GetContext().Games on gtog.IdGame equals games.IdGame
                      select games;

            Pages.ListGamePage.ListGamesBuff[0] = res.ToList();

        }
    }
}

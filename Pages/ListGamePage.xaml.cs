using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ListGamePage.xaml
    /// </summary>
    public partial class ListGamePage : Page
    {
        public ListGamePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LstGame.ItemsSource = Entities.GetContext().Games.ToList();
        }

        private void LstGame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GameMainPage gameMainPage = new GameMainPage();

            var ItemGame = LstGame.SelectedItem as Games;
            gameMainPage.LblGameName.Content = ItemGame?.Name;
            gameMainPage.ImgGamePoster.Source = new BitmapImage(new Uri(ItemGame?.Poster));
            gameMainPage.TxtDescGame.Text = ItemGame?.Description;
            gameMainPage.TxtDateRealse.Text = ItemGame.DateRelease == null ? null : ItemGame.DateRelease.Value.ToLongDateString();
            gameMainPage.TxtCountViews.Text = ItemGame?.CountViews.ToString();
            gameMainPage.TxtDeveloperGame.DataContext = ItemGame?.Developers;
            gameMainPage.TxtPublisherGame.DataContext = ItemGame?.Publishers;
            gameMainPage.LstGenreGame.ItemsSource = ItemGame?.ListGenres;
            gameMainPage.LstLangInter.ItemsSource = ItemGame?.ListLangInterface;
            gameMainPage.TxtTypeEdition.DataContext = ItemGame?.TypesEdition;
            gameMainPage.LstPlatformGame.ItemsSource = ItemGame?.ListPlatforms;
            gameMainPage.LstVoiceLang.ItemsSource = ItemGame?.ListLangVoiceActing;
            gameMainPage.TxtTabletGame.DataContext = ItemGame?.TypeTablet;
            gameMainPage.TxtSysRequipOS.DataContext = ItemGame?.SystemRequirements;
            gameMainPage.TxtSysRequipCPU.DataContext = ItemGame?.SystemRequirements;
            gameMainPage.TxtSysRequipRAM.DataContext = ItemGame?.SystemRequirements;
            gameMainPage.TxtSysRequipGPU.DataContext = ItemGame?.SystemRequirements;
            gameMainPage.TxtSysRequipHDD.DataContext = ItemGame?.SystemRequirements;
            gameMainPage.WbNav.NavigateToString(Display(ItemGame?.URLTrailer));
            gameMainPage.LstScreenShoptGame.ItemsSource = Entities.GetContext().ScreenshotsGame.Where(i => i.IdGame == ItemGame.IdGame).ToList();
            gameMainPage.LstTorrentsGame.ItemsSource = Entities.GetContext().Torrents.Where(i => i.IdGame == ItemGame.IdGame).ToList();
            gameMainPage.IdGame = ItemGame.IdGame;
            gameMainPage.LstComment.ItemsSource = Entities.GetContext().CommentsGame.Where(i => i.IdGame == ItemGame.IdGame).ToList().OrderByDescending(i => i.Datetime);
            gameMainPage.TxtCountComments.Text = Entities.GetContext().CommentsGame.Where(i => i.IdGame == ItemGame.IdGame).ToList().OrderByDescending(i => i.Datetime).Count().ToString();
            gameMainPage.TxtCountViews.Text = ItemGame.CountViews.ToString();
            gameMainPage.TxtCountFollowers.Text = Entities.GetContext().FavotritesGame.Where(i => i.IdGame == ItemGame.IdGame).Count().ToString();
            gameMainPage.BtnSubs.Content = Entities.GetContext().FavotritesGame.Where(i => i.IdGame == ItemGame.IdGame && i.IdUser == Classes.IdUserClass.IdUser).Any() ? "Отписаться от обновления" : "Подписаться на обновление";

            Classes.Manager.MainFrame.Navigate(gameMainPage);
        }

        public string Display(string path)
        {
            string page =
                 "<html>"
                + "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\" />"
                + "<body>" + "\r\n"
                + $"<iframe width = \"718\" height = \"400\" src = {path} frameborder = \"0\" allow = \"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen ></ iframe >"
                           + "</body></html>";
            return page;
        }

        private void TxtSearchGame_TextChanged(object sender, TextChangedEventArgs e)
        {
            LstGame.ItemsSource = Entities.GetContext().Games.Where(i =>i.Name.Contains(TxtSearchGame.Text)).ToList();
        }
    }
}

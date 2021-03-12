using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
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
        public string TypeListUser { get; set; }

        public static ObservableCollection<List<Games>> ListGamesBuff = new ObservableCollection<List<Games>>();

        public ListGamePage()
        {
            InitializeComponent();

            ListGamesBuff.CollectionChanged += ListGamesBuff_CollectionChanged;

            //ListGamesBuff.Add(Entities.GetContext().Games.ToList());
            //ListGamesBuff.RemoveAt(1);
            //ListGamesBuff[0] = Entities.GetContext().Games.ToList();
        }

        private void ListGamesBuff_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LstGame.ItemsSource = ListGamesBuff[0].ToList();
            //switch (e.Action)
            //{
            //    case NotifyCollectionChangedAction.Add: // если добавление
            //        List<Games> newGames = e.NewItems[0] as List<Games>;
            //        MessageBox.Show($"Добавлен новый объект: {newGames.Select(i=>i.Name)}");
            //        break;
            //    case NotifyCollectionChangedAction.Remove: // если удаление
            //        List<Games> oldGames = e.OldItems[0] as List<Games>;
            //        MessageBox.Show($"Удален объект: {oldGames.Select(i => i.Name)}");
            //        break;
            //    case NotifyCollectionChangedAction.Replace: // если замена
            //        List<Games> replacedGames = e.OldItems[0] as List<Games>;
            //        List<Games> replacingGames = e.NewItems[0] as List<Games>;
            //        MessageBox.Show($"Объект {replacedGames.Select(i => i.Name)} заменен объектом {replacingGames.Select(i => i.Name)}");
            //        break;
            //}
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch (TypeListUser)
            {
                case "MainList":
                    LstGame.ItemsSource = Entities.GetContext().Games.ToList();
                    break;
                case "FaveList":
                    LstGame.ItemsSource = Entities.GetContext().Users.Where(i => i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault().ListFavGame;
                    break;
                default:
                    LstGame.ItemsSource = Entities.GetContext().Games.ToList();
                    break;
            }

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

            if (!Entities.GetContext().ViewsGameHistory.Where(i => i.idGame == ItemGame.IdGame && i.IdUser == Classes.IdUserClass.IdUser).Any())
            {
                var game = Entities.GetContext().Games.Where(i => i.IdGame == ItemGame.IdGame).FirstOrDefault();
                game.CountViews += 1;

                Entities.GetContext().Entry(game).State = EntityState.Modified;

                Entities.GetContext().SaveChanges();

                var gameHist = new ViewsGameHistory();
                gameHist.idGame = ItemGame.IdGame;
                gameHist.IdUser = Classes.IdUserClass.IdUser;
                gameHist.Datetime = DateTime.Now;

                Entities.GetContext().Entry(gameHist).State = EntityState.Added;

                Entities.GetContext().SaveChanges();
            }

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
            switch (TypeListUser)
            {
                case "MainList":
                    LstGame.ItemsSource = Entities.GetContext().Games.Where(i => i.Name.Contains(TxtSearchGame.Text)).ToList();
                    break;
                case "FaveList":
                    LstGame.ItemsSource = Entities.GetContext().Users.Where(i => i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault().ListFavGame.Where(i => i.Name.Contains(TxtSearchGame.Text)).ToList();
                    break;
                default:
                    LstGame.ItemsSource = Entities.GetContext().Games.Where(i => i.Name.Contains(TxtSearchGame.Text)).ToList();
                    break;
            }
        }

    }
}

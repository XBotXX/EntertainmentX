using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Логика взаимодействия для GameMainPage.xaml
    /// </summary>
    public partial class GameMainPage : Page
    {
        public int IdGame { get; set; }

        public GameMainPage()
        {
            InitializeComponent();
        }

        private void BtnGoTorent_Clicked(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is Torrents)
            {

                Torrents URLTor = (Torrents)cmd.DataContext;

                string dirWithTorrent = @"C: \Users\User\Downloads";

                System.Diagnostics.Process.Start(URLTor.URLDownloads);

                //Process.Start(System.IO.Path.Combine(dirWithTorrent, deleteme.URLDownloads));
                //MessageBox.Show(deleteme.URLDownloads.ToString());

            }
        }

        private void BtnGreateComment_Click(object sender, RoutedEventArgs e)
        {
            var commentsGame = new CommentsGame();
            commentsGame.Text = TxtComment.Text;
            commentsGame.IdGame = IdGame;
            commentsGame.IdUser = Classes.IdUserClass.IdUser;
            commentsGame.Datetime = DateTime.Now;

            Entities.GetContext().Entry(commentsGame).State = EntityState.Added;

            Entities.GetContext().SaveChanges();

            DoLStComment();
        }

        public void DoLStComment()
        {
            LstComment.ItemsSource = Entities.GetContext().CommentsGame.Where(i => i.IdGame == IdGame).ToList().OrderByDescending(i=>i.Datetime);
        }

        private void LikeComment_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is CommentsGame)
            {
                CommentsGame comment = (CommentsGame)cmd.DataContext;

                var commentsGame = comment;

                if (cmd.Background != Brushes.Green)
                {
                    commentsGame.CountLike += 1;
                }
                else
                {
                    commentsGame.CountLike -= 1;
                    DeleteCommentGameHist(comment.IdComments);
                }

                Entities.GetContext().Entry(commentsGame).State = EntityState.Modified;

                Entities.GetContext().SaveChanges();

                if (Entities.GetContext().CommentGameHistory.Where(i => i.IdComment == comment.IdComments && i.IdUser == Classes.IdUserClass.IdUser).Any())
                {
                    commentsGame.CountDisLike -= 1;
                    EditCommentGameHist(comment.IdComments, "Like");
                }
                else if(cmd.Background != Brushes.Green)
                {
                    AddCommentGameHist(comment.IdComments, "Like");
                }

                DoLStComment();
            }
        }

        public void AddCommentGameHist(int IdComments,string typeBtn)
        {
            var commentGameHist = new CommentGameHistory();
            commentGameHist.IdComment = IdComments;
            commentGameHist.IdUser = Classes.IdUserClass.IdUser;

            if(typeBtn == "Like")
            {
                commentGameHist.ColorLike = "Green";
                commentGameHist.ColorDisLike = "";
            }
            else if(typeBtn == "DisLike")
            {
                commentGameHist.ColorLike = "";
                commentGameHist.ColorDisLike = "Green";
            }
            
            commentGameHist.Datetime = DateTime.Now;

            Entities.GetContext().Entry(commentGameHist).State = EntityState.Added;

            Entities.GetContext().SaveChanges();
        }

        public void EditCommentGameHist(int IdComments, string typeBtn)
        {
            var commentGameHist = Entities.GetContext().CommentGameHistory.Where(i=>i.IdComment == IdComments && i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault();
            commentGameHist.IdComment = IdComments;
            commentGameHist.IdUser = Classes.IdUserClass.IdUser;

            if (typeBtn == "Like")
            {
                commentGameHist.ColorLike = "Green";
                commentGameHist.ColorDisLike = "";
            }
            else if (typeBtn == "DisLike")
            {
                commentGameHist.ColorLike = "";
                commentGameHist.ColorDisLike = "Green";
            }

            commentGameHist.Datetime = DateTime.Now;

            Entities.GetContext().Entry(commentGameHist).State = EntityState.Modified;

            Entities.GetContext().SaveChanges();
        }

        public void DeleteCommentGameHist(int IdComments)
        {
            var commentGameHist = Entities.GetContext().CommentGameHistory.Where(i => i.IdComment == IdComments && i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault();

            Entities.GetContext().Entry(commentGameHist).State = EntityState.Deleted;

            Entities.GetContext().SaveChanges();
        }

        private void DisLikeComment_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is CommentsGame)
            {
                CommentsGame comment = (CommentsGame)cmd.DataContext;

                var commentsGame = comment;

                if (cmd.Background != Brushes.Green)
                {
                    commentsGame.CountDisLike += 1;
                }
                else
                {
                    commentsGame.CountDisLike -= 1;
                    DeleteCommentGameHist(comment.IdComments);
                }

                Entities.GetContext().Entry(commentsGame).State = EntityState.Modified;

                Entities.GetContext().SaveChanges();

                if (Entities.GetContext().CommentGameHistory.Where(i => i.IdComment == comment.IdComments && i.IdUser == Classes.IdUserClass.IdUser).Any())
                {
                    commentsGame.CountLike -= 1;
                    EditCommentGameHist(comment.IdComments, "DisLike");
                }
                else if (cmd.Background != Brushes.Green)
                {
                    AddCommentGameHist(comment.IdComments, "DisLike");
                }

                DoLStComment();
            }
        }

        private void BtnSubs_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.Content == "Подписаться на обновление")
            {
                var favotritesGame = new FavotritesGame();
                favotritesGame.IdGame = IdGame;
                favotritesGame.IdUser = Classes.IdUserClass.IdUser;

                Entities.GetContext().Entry(favotritesGame).State = EntityState.Added;

                Entities.GetContext().SaveChanges();

                cmd.Content = "Отписаться от обновления";

                TxtCountFollowers.Text = Entities.GetContext().FavotritesGame.Where(i => i.IdGame == IdGame).Count().ToString();
            }
            else
            {
                var favotritesGame = Entities.GetContext().FavotritesGame.Where(i => i.IdGame == IdGame && i.IdUser == Classes.IdUserClass.IdUser).FirstOrDefault();

                Entities.GetContext().Entry(favotritesGame).State = EntityState.Deleted;

                Entities.GetContext().SaveChanges();

                cmd.Content = "Подписаться на обновление";

                TxtCountFollowers.Text = Entities.GetContext().FavotritesGame.Where(i => i.IdGame == IdGame).Count().ToString();
            }
        }
    }
}

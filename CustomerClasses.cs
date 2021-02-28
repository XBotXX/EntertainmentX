using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertainmentX
{
    class CustomerClasses
    {
    }

    public partial class Games
    {
        public List<Genres> ListGenres
        {
            get
            {
                return GamesToGenres.Select(i => i.Genres).ToList();
            }
        }
    }

    public partial class Games
    {
        public List<Language> ListLangInterface
        {
            get
            {
                return InterfaceLanguage.Select(i => i.Language).ToList();
            }
        }
    }

    public partial class Games
    {
        public List<Language> ListLangVoiceActing
        {
            get
            {
                return VoiceActingLanguage.Select(i => i.Language).ToList();
            }
        }
    }

    public partial class Games
    {
        public List<Platforms> ListPlatforms
        {
            get
            {
                return GamesToPlatform.Select(i => i.Platforms).ToList();
            }
        }
    }

    public partial class CommentsGame
    {
        public string CommentColorLike
        {
            get
            {
                return CommentGameHistory.Select(i=>i.ColorLike).FirstOrDefault();
            }
        }
    }

    public partial class CommentsGame
    {
        public string CommentColorDisLike
        {
            get
            {
                return CommentGameHistory.Select(i => i.ColorDisLike).FirstOrDefault();
            }
        }
    }
}

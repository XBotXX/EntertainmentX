//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntertainmentX
{
    using System;
    using System.Collections.Generic;
    
    public partial class Torrents
    {
        public int IdTorrents { get; set; }
        public string URLDownloads { get; set; }
        public string NameOrg { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public int IdGame { get; set; }
    
        public virtual Games Games { get; set; }
    }
}

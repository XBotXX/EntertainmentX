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
    
    public partial class MarkGame
    {
        public int IdMark { get; set; }
        public int IdUser { get; set; }
        public int IdGame { get; set; }
        public int IdTypeMark { get; set; }
    
        public virtual Games Games { get; set; }
        public virtual TypeMark TypeMark { get; set; }
        public virtual Users Users { get; set; }
    }
}

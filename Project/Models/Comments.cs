//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int ID { get; set; }
        public string text { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public int BlogID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> AuthorID { get; set; }
    
        public virtual Authors Authors { get; set; }
        public virtual Blogs Blogs { get; set; }
        public virtual Users Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models.Class
{
    public class BlogComment
    {
        public IEnumerable<Blogs> BlogComments { get; set; } 
        public IEnumerable<Comments> CommentComments { get; set; }
        //public IEnumerable<Favorites> BlogFavori { get; set; }

       

    }
}
using MyBlog.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Controllers
{
    public class BlogPost
    {
        public int PostId { get; set; }
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Display(Name = "Descrição")]
        public string ShortDescription { get; set; }
        public string Link { get { return ShortDescription.UrlFriendly(50); }  }
        [DataType(DataType.MultilineText)]
        public string Texto { get; set; }
        public string LinkImage { get; set; }

    }
}


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public class ArticleMedia
    {
        public int Id { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int MediaId { get; set; }

        [StringLength(60)]
        public string Title { get; set; }

        public string Description { get; set; }

        [DefaultValue(false)]
        public bool Thumbnail { get; set; }

        public virtual Article Article { get; set; }
        public virtual MediaItem MediaItem { get; set; }
    }
}
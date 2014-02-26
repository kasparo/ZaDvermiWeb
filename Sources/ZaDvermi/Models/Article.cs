using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaDvermi.Models
{
    public enum ArticleType
    {
        PhotoAlbum = 1,
        Board = 2,
        PublicArticle = 3,
        PrivateNotification = 4,
        Performance = 5,
        Book = 6
    }

    public class Article
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Preface { get; set; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int? ParentArticleId { get; set; }

        public int? CreatedById { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public ArticleType ArticleType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "---")]
        public DateTime? ValidFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "---")]
        public DateTime? ValidTo { get; set; }
      
        [Required]
        [DefaultValue(true)]
        [Display(Name = "Zveřejnit")]
        public bool Published { get; set; }

        public virtual ICollection<ArticleMedia> ArticleMedias { get; set; }
        public virtual Article ParentArticle { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
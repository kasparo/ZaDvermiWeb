
using System;
using System.ComponentModel.DataAnnotations;

namespace ZaDvermi.Models
{
    public enum MediaType
    {
        Photo, Document
    }

    public class MediaItem
    {
        public int Id { get; set; }

        [Required]
        public MediaType MediaType { get; set; }

        [Required]
        public string OriginialFileName { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public int CreatedById { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        
        public virtual User CreatedBy { get; set; }

    }
}
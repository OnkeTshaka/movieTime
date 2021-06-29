using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace watchAMovie.Models
{
    public enum Status
    {
        [Display(Name ="New Arrivals")]
        newArrivals,
        [Display(Name = "Coming Soon")]
        comingSoon,
        [Display(Name = "Top Rated")]
        topRated,
        [Display(Name = "Trailers")]
        trailers
    }
    //Add Movies
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        [Display(Name = "Movie ID")]
        public int MovieID { get; set; }

        [Required, StringLength(50), Display(Name = "Name")]
        public string MovieName { get; set; }

        [Required, StringLength(20), Display(Name = "Genre")]
        public string Genre { get; set; }
        [Required, StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, Display(Name = "Showing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ShowingDate { get; set; }
        public byte[] Picture { get; set; }
        public Status movieStatus { get; set; }
        [ForeignKey("theatre")]
        public int Thea_id { get; set; }
        public virtual Theatre theatre { get; set; }



    }
  
    }


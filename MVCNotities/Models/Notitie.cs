using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNotities.Models
{
    public class Notitie
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Titel"), MaxLength(40, ErrorMessage = "Maximum 40 characters"),]
        [Required]
        public string Title { get; set; }
        public string Omschrijving { get; set; }
        public string ImgPath { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:MM}", ApplyFormatInEditMode =true)]
        public DateTime Datum { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNotities.Models
{
    public class NotitieCreateViewModel : Notitie
    {
        [DisplayName("Foto")]
        public IFormFile UploadFile { get; set; } 

    }
}

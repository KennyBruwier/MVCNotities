using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNotities.Models
{
    public class NotitieEditViewModel : Notitie
    {
        [DisplayName("Foto")]
        public IFormFile UploadFile { get; set; }

    }
}

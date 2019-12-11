using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Scrum3.ViewModels
{
    public class OsallistumisetVM
    {
        public int KurssitoteutusID { get; set; }
        public string Kurssi { get; set; }
        public int Laajuus { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Paivamaara { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public int OsallistumisetID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenFinal.Models
{
   public class Patient
    {
        public int IdPatient { get; set; }
        public string Name { get; set; }
        public string PictureBase64 { get; set; }
        public double Latitude { get; set; }
        public string Process { get; set; }
        public double Longitude { get; set; }
    }
}

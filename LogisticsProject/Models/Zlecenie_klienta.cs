using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogisticsProject.Models
{
    public class Zlecenie_klienta
    {
        public virtual Ladunki Ladunki { get; set; }
        public virtual Pojazdy Pojazdy { get; set; }
        public virtual Trasy Trasy { get; set; }
    }
}
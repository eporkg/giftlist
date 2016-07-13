using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gavelister.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public int AmountRequested { get; set; }
        public int AmountBought { get; set; }
        public string Description { get; set; }

        public string Url { get; set; }
        [NotMapped]
        public int NumberLeft { get { return AmountRequested - AmountBought; } }
        [NotMapped]
        public int AmountReserved { get; set; }

    }
}


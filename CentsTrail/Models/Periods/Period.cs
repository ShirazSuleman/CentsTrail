using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CentsTrail.Models.Transactions;

namespace CentsTrail.Models.Periods
{
    public class Period
    {
        public long Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3)]
        [Display(Name = "Period Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}

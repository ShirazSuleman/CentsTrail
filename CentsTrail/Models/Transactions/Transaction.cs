using CentsTrail.Models.BankAccounts;
using CentsTrail.Models.Categories;
using CentsTrail.Models.Periods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentsTrail.Models.Transactions
{
    public class Transaction
    {
        public long Id { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 3)]
        public string Description { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field Amount must be a positive number.")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        public long PeriodId { get; set; }

        [Required]
        public Period Period { get; set; }

        public long BankAccountId { get; set; }

        [Required]
        public BankAccount BankAccount { get; set; }

        public long CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
    }
}

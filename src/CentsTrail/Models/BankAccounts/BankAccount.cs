using CentsTrail.Models.Enums;
using CentsTrail.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentsTrail.Models.BankAccounts
{
    public class BankAccount
    {
        public long Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3)]
        [Display(Name = "Bank Account Name")]
        public string Name { get; set; }

        [Display(Name = "Bank Account Type")]
        [EnumDataType(typeof(BankAccountType))]
        public BankAccountType BankAccountType { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}

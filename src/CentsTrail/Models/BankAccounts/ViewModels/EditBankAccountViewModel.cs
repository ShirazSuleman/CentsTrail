using CentsTrail.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentsTrail.Models.BankAccounts.ViewModels
{
    public class EditBankAccountViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3)]
        [Display(Name = "Bank Account Name")]
        public string Name { get; set; }

        [Display(Name = "Bank Account Type")]
        [EnumDataType(typeof(BankAccountType))]
        public BankAccountType BankAccountType { get; set; }
    }
}

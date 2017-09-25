using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentsTrail.Models.Enums
{
    public enum BankAccountType
    {
        [Display(Name = "Cheque")]
        Cheque = 1,
        [Display(Name = "Savings")]
        Savings = 2,
        [Display(Name = "Credit Card")]
        CreditCard = 3
    }
}

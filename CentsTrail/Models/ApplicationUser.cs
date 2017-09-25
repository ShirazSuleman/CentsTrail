using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CentsTrail.Models.BankAccounts;
using CentsTrail.Models.Periods;
using CentsTrail.Models.Categories;
using CentsTrail.Models.Transactions;

namespace CentsTrail.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<BankAccount> BankAccounts { get; set; }

        public List<Period> Periods { get; set; }

        public List<Category> Categories { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}

using CentsTrail.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CentsTrail.Models.Transactions;

namespace CentsTrail.Models.Categories
{
    public class Category
    {
        public long Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field Limit must be a positive number.")]
        [DataType(DataType.Currency)]
        public decimal? Limit { get; set; }

        [EnumDataType(typeof(CategoryType))]
        [Display(Name = "Category Type")]
        public CategoryType CategoryType { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}

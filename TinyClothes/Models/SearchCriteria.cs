using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class SearchCriteria
    {
        /// <summary>
        /// No Arg Constructor to initialize SearchResults to avoid null ref exception
        /// </summary>
        public SearchCriteria()
        {
            SearchResults = new List<Clothing>();
        }

        [StringLength(50)]
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing: shirt, pants, hat, etc.
        /// </summary>
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [Range(0.0, 9999.9, ErrorMessage = "Minimum Price must be a positive value, $0 to $9999.")]
        [Display(Name = "Minimum Price $")]
        public double? MinPrice { get; set; }

        [Range(1.0, 10000.0, ErrorMessage = "Maximum Price must be a positive value, $1.00 to $10,000.")]
        [Display(Name = "Maximum Price $")]
        public double? MaxPrice { get; set; }

        public List<Clothing> SearchResults { get; set; }
    }
}

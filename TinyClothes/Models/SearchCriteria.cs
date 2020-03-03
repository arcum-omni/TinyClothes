using System;
using System.Collections.Generic;
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

        public string Size { get; set; }

        /// <summary>
        /// The type of clothing: shirt, pants, hat, etc.
        /// </summary>
        public string Type { get; set; }

        public string Title { get; set; }

        public double? MinPrice { get; set; }

        public double? MaxPrice { get; set; }

        public List<Clothing> SearchResults { get; set; }
    }
}

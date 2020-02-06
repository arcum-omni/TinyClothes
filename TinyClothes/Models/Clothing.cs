using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Represents a single clothing item.
    /// </summary>
    public class Clothing
    {
        /// <summary>
        /// The unique identifier for the clothing item.
        /// </summary>
        [Key] // Set as PK
        public int ItemID { get; set; }

        /// <summary>
        /// Clothing Size
        /// </summary>
        [Required(ErrorMessage = "Size Required")]
        public string Size { get; set; }

        /// <summary>
        /// Type of clothing; shirt, pants, etc
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Primary color of outfit
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Retail Price of the item
        /// </summary>
        [Range(0.0, 5000.0)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// Clothing Title,
        /// For pattern recognition go to https://regexr.com/
        /// </summary>
        [Required(ErrorMessage = "Title Required")]
        [StringLength(35)]
        //[RegularExpression("^([A-Za-z0-9])+$")] // Example of regular expression RegEx validation, great in some cases.
        public string Title { get; set; }

        /// <summary>
        /// Clothing item description
        /// </summary>
        [StringLength(800)]
        public string Description { get; set; }
    }
}

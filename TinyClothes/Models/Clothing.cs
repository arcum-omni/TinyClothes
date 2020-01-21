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
        public string Size { get; set; }

        /// <summary>
        /// Type of clothing; shirt, pants, etc
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Primary color of outfit
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Retail Price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Clothing Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Clothing item description
        /// </summary>
        public string Description { get; set; }

    }
}

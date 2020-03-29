using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

        [Range(0.0, 9999.9, ErrorMessage = "Minimum Price must be a positive value, $0.00 to $9999.99")]
        [Display(Name = "Minimum Price $")]
        [PriceLessThanAtrribute(nameof(MaxPrice), ErrorMessage = "Minimum price must be less than maximum price.")]
        public double? MinPrice { get; set; }

        [Range(0.01, 10000.0, ErrorMessage = "Maximum Price must be a positive value, $0.01 to $10,000.00")]
        [Display(Name = "Maximum Price $")]
        public double? MaxPrice { get; set; }

        public List<Clothing> SearchResults { get; set; }

        /// <summary>
        /// Returns true if at lease one search criteria is provided.
        /// </summary>
        /// <returns></returns>
        public bool IsBeingSearched()
        {
            return (MaxPrice.HasValue || MinPrice.HasValue || 
                    Title != null || Type != null || Size != null);

        }
    }

    public class PriceLessThanAtrribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _compareProperty;

        public PriceLessThanAtrribute(string compareProperty)
        {
            _compareProperty = compareProperty;
        }

        /// <summary>
        /// Client side validation not yet working correctly
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            //string error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            //context.Attributes.Add("data-val", "true");
            //context.Attributes.Add("data-val-error", error);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double? currVal = (double?)value;

            ErrorMessage = ErrorMessageString ?? "Minimum price must be less than maximum price"; // ?? is the null-coalescing operator, https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator

            PropertyInfo property = validationContext.ObjectType.GetProperty(_compareProperty);

            // Cannot find supplied property by name
            if (property == null)
            {
                throw new ArgumentNullException($"Cannot find {_compareProperty} property");
            }

            double? compareValue = (double?)property.GetValue(validationContext.ObjectInstance);

            if ((currVal == null || compareValue == null) || currVal < compareValue)
            {
                return ValidationResult.Success;
            }
            //else if (currVal < compareValue)
            //{
            //    return ValidationResult.Success;
            //}
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}

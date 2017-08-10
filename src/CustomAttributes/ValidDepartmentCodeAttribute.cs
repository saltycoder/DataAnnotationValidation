/*
 * Custom data validation attribute
 * 
 * ValidationAttribute Class - https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.validationattribute(v=vs.110).aspx
 *    ValidationResult Class - https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.validationresult(v=vs.110).aspx
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DataAnnotationValidation.CustomAttributes
{

    /// <summary>
    /// All of the validation annotations such as; required, max length, and range are derived from the 
    /// ValidationAttribute class.  This allows for server-side data validation.
    /// 
    /// To enable client-side data validation in ASP.NET Core the IClientModelValidator must be inherited.
    /// To enable client-side data validation in ASP.NET MVC the IClientValidatable must be inherited.
    /// </summary>
    public class ValidDepartmentCodeAttribute: ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// This property is used to take in a list of allowed codes defined in the annotation.  For 
        /// example in the EmployeeViewModel.cs class it looks like this: 
        /// 
        /// [ValidDepartmentCodeAttribute(AllowedDepartmentCodes = "1A,Z7,42", ErrorMessage = "The {0} is invalid.  Valid codes are:  {1}.")]
        /// </summary>
        public string AllowedDepartmentCodes { get; set; }

        /// <summary>
        /// The constructor sets the default error message to be used if the annotation does not contain 
        /// the ErrorMessage property.
        /// </summary>
        public ValidDepartmentCodeAttribute()
            :base("The {0} is invalid.  Valid department codes are:  {1}.")
        {
        }
        
        /// <summary>
        /// Override the IsValid method to provide custom validation logic.
        /// </summary>
        /// <param name="value">
        /// The value parameter is the string being validated.
        /// </param>
        /// <param name="validationContext">
        /// The validationContext gives access to information such as the model type, object instance,
        /// and friendly display name of the property that is being validated.
        /// </param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValidCode(value)){
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }

        /// <summary>
        /// Private function that handles the custom validation.
        /// </summary>
        /// <param name="value">
        /// The value parameter is the string being validated.
        /// </param>
        /// <returns></returns>
        private bool IsValidCode(object value)
        {
            List<string> allowedCodes = new List<string>(AllowedDepartmentCodes.ToUpper().Split(','));

            if (value == null || allowedCodes.Contains(value.ToString().ToUpper()) == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method will define the validation rules the .NET Framework passes to the client. 
        /// 
        /// NOTE:  For the unobtrusive data validation to work in the user’s web browser a JS file 
        ///        needs to be created.  The JS file needs to include code that creates the JQuery 
        ///        adapter.  Reference the “Scripts/custom/allowed-department-codes.js” file.
        /// </summary>
        /// <param name="metadata">The metadata gives access to information such as the friendly display name.</param>
        /// <param name="context">The context encapsulates information about the current HTTP request.</param>
        /// <dependencies>
        /// jquery-validation
        /// jquery-validation-unobtrusive
        /// </dependenciess>
        /// <returns></returns>
        public void AddValidation(ClientModelValidationContext context)
        {
            /* This creates 2 HTML Attributes:
             * 
             * data-val-departmentcode-allowed :  This is the white-list of allowed department codes
             * data-val-allow-departmentcode :  This is the error message to display on the client
             * */
            context.Attributes.Add("data-val-departmentcode-allowed", AllowedDepartmentCodes.ToUpper());
            context.Attributes.Add("data-val-departmentcode", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        }

        /// <summary>
        /// Override the FormatErrorMessage method to create custom server-side and client-side error messages.
        /// </summary>
        /// <param name="name">Friendly name of the property being validated.</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            string message = string.Empty;

            // Determine if an error messages was specified on the property annotation.  If there isn’t use 
            // the default error message defined in the constructor.
            if (ErrorMessage == null)
            {
                message = base.ErrorMessageString;  // Default error message set at the class level
            }
            else
            {
                message = base.ErrorMessage;  // Error message set on the object property
            }

            // Format the error message
            return string.Format(CultureInfo.CurrentCulture, message, name, AllowedDepartmentCodes.ToUpper());

        }
    }

}
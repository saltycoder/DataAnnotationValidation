/*
 * Custom data validation attribute
 * 
 * ValidationAttribute Class - https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.validationattribute(v=vs.110).aspx
 *    ValidationResult Class - https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.validationresult(v=vs.110).aspx
 * 
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using DataAnnotationValidation.ViewModels;

namespace DataAnnotationValidation.CustomAttributes
{
    /// <summary>
    /// All of the validation annotations such as; required, max length, and range are derived from the 
    /// ValidationAttribute class.  This allows for server-side data validation.
    /// 
    /// For the purpose of this demo application this custom validation attribute is assigned to the 
    /// EmployeViewModel class.  Client-side validation is not implemented in this attribute.
    /// </summary>
    public class ValidateEmployeeNumberAttribute: ValidationAttribute
    {

        public ValidateEmployeeNumberAttribute()
        {
        }

        /// <summary>
        /// Override the IsValid method to provide custom validation logic.
        /// </summary>
        /// <param name="value">
        /// The Class Object being validated
        /// </param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            bool isValid = false;

            EmployeeViewModel employee = (EmployeeViewModel)value;

            if(employee.DepartmentCode == "42" && string.IsNullOrEmpty(employee.EmployeeNumber)){
                isValid = false;
            }else{
                isValid = true;
            }

            return isValid;

        }

    }

}
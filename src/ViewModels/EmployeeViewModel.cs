/*
 * EmployeeViewModel is used to demostrate data validation with Data Annotations
 * 
 * DataAnnotations Class:  https://msdn.microsoft.com/en-us/library/System.ComponentModel.DataAnnotations
 *
 */

using System.ComponentModel.DataAnnotations;

using DataAnnotationValidation.CustomAttributes;
using DataAnnotationValidation.Enums;


namespace DataAnnotationValidation.ViewModels
{
    // Custom validation attribute 
    [ValidateEmployeeNumber(ErrorMessage = "Employee Number is required if the Department Code is 42")]
    public class EmployeeViewModel
    {

        [Required(ErrorMessage = "Employee's {0} is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The {0} length must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 -.']*$", ErrorMessage = "The {0} has invalid characters.  Allowable characters are alpha, numeric, spaces, and these symbols - . '.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // Regular expression using a static constant
        [Required(ErrorMessage = "Employee's {0} is required.")]
        [StringLength(75, MinimumLength = 1, ErrorMessage = "The {0} length must be between {2} and {1}")]
        [RegularExpression(Constants.VAL_LAST_NAME, ErrorMessage = "The {0} has invalid characters.  Allowable characters are alpha, numeric, spaces, and these symbols - . '.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Employee's {0} is required.")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "The {0} length must be between {2} and {1}")]
        [RegularExpression(@"^[a-zA-Z0-9 -.']*$", ErrorMessage = "The {0} has invalid characters.  Allowable characters are alpha, numeric, spaces, and these symbols - . '.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        // Custom validation attribute with client-side validation
        [Required(ErrorMessage = "Employee's {0} is required")]
        [ValidDepartmentCodeAttribute(AllowedDepartmentCodes = "1A,Z7,42", ErrorMessage = "The {0} is invalid.  Valid codes are:  {1}.")]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }

        [StringLength(10, MinimumLength = 0, ErrorMessage = "The {0} length must be no more than 10 characters.")]
        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Employees's {0} is required.")]
        [Range(16, 110, ErrorMessage = "The user's {0} must be between {1} and {2}.")]
        [Display(Name = "Age")]
        public int  Age { get; set; }

        // Validate the value against a Enum
        [Required(ErrorMessage = "Employees's {0} is required.")]
        [EnumDataType(typeof(EmployeeStatus), ErrorMessage = "Invalid {0} selected.")]
        [Display(Name = "Status")]
        public int Status { get; set; }

        [StringLength(250, MinimumLength = 0, ErrorMessage = "The {0} length must be between {2} and {1}")]
        [Display(Name = "Description of Employee")]
        public string Description { get; set; }
    }

}
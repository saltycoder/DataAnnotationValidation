/*
 * EmployeeTestViewModel is used on the /Employee/ValidateModel.cshtml View.  To be able 
 * to by-pass client-side validation this model does not utilize data annotations.
 * 
  */

using System.ComponentModel.DataAnnotations;

using DataAnnotationValidation.CustomAttributes;
using DataAnnotationValidation.Enums;


namespace DataAnnotationValidation.ViewModels
{
    public class EmployeeTestViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }

        public string DepartmentCode { get; set; }

        public string EmployeeNumber { get; set; }

        public int  Age { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }
    }

}
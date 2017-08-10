# Data Annotation Validation

This sample application demonstrates using Data Annotations for data validation.  The code demonstrates client-side and server side data validation by utilizing some of the built in .NET validation attributes.  The project shows how to use custom validation attributes to provided data validation on a model property or at the class level.  The custom property attribute includes server side and Java Script code to perform client-side validation.  

The project was created on macOS with Visual Studio Code using ASP.NET Core 1.1.  Keep in mind even though this project was created with ASP.NET Core, Data Annotations still can be utilized in an ASP.NET MVC and Web Forms project.

### Data Model Properties

The code includes the following examples for adding attributes to model properties:

*Properties with validation attributes*

```
[Required(ErrorMessage = "Employee's {0} is required.")]
[StringLength(50, MinimumLength = 1, ErrorMessage = "The {0} length must be between {2} and {1} characters.")]
[RegularExpression(@"^[a-zA-Z0-9 -.']*$", ErrorMessage = "The {0} has invalid characters.  Allowable characters are alpha, numeric, spaces, and these symbols - . '.")]
[Display(Name = "First Name")]
public string FirstName { get; set; }
```

*Custom validation attribute with client-side validation*

```
[ValidDepartmentCodeAttribute(AllowedDepartmentCodes = "1A,Z7,42", ErrorMessage = "The {0} is invalid.  Valid codes are:  {1}.")]
[Display(Name = "Department Code")]
public string DepartmentCode { get; set; }
```

*Class level custom validation*

```
[ValidateEmployeeNumber(ErrorMessage = "Employee Number is required if the Department Code is 42")]
public class EmployeeViewModel
{
```

### Model Validation

Model validation can be done with the following:

*ModelState.IsValid()*

```
if (ModelState.IsValid)
{
  return Redirect("~/");
}
```

*Controller.TryValidateModel()*

```
if (!TryValidateModel(e))
{
  throw new InvalidModelException("Invalid Employee Model");
}
```

*Validator.TryValidateObject()*

```
Validator.TryValidateObject(e, context, results, true);
```

*Validator.TryValidateProperty()*

```
Validator.TryValidateProperty(e.FirstName, context, results);
```
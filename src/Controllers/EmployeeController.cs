
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;

using DataAnnotationValidation.CustomExceptions;
using DataAnnotationValidation.ViewModels;

namespace DataAnnotationValidation.Controllers
{
    public class EmployeeController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            return View("Home");
        }


        [HttpGet]
        public ActionResult New()
        {
            return View("CreateEmployee");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmployeeViewModel model)
        {
            /*
             * Validation is performed here by checking the controller model state dictionary object.
             * 
             * Reference:  https://msdn.microsoft.com/en-us/library/system.web.mvc.controller.modelstate(v=vs.118).aspx#P:System.Web.Mvc.Controller.ModelState
             */

            if (ModelState.IsValid)
            {
                return Redirect("~/");
            }

            // You can add other messages to the Model State
            ModelState.AddModelError(string.Empty, "Oh-Oh...  There was an error processing the model.");

            return View("CreateEmployee", model);
        }

        [HttpGet]
        public ActionResult ModelTest()
        {
            List<string> errors = new List<string>();
            ViewBag.Errors = errors;

            EmployeeTestViewModel model = new EmployeeTestViewModel();
            model.FirstName = "Happy";
            model.LastName = "Gilmore";
            model.Address = "1 Putter Lane";
            model.Age = 10;
            model.Status = 1;
            model.DepartmentCode = "7";
            model.EmployeeNumber = String.Empty;
            model.Description = "";

            return View("ValidateModel", model);
        }

        [HttpPost]
        public ActionResult ValidateModel(EmployeeTestViewModel model, string buttonClicked)
        {

            EmployeeViewModel e = new EmployeeViewModel();
            e.FirstName = model.FirstName;
            e.LastName = model.LastName;
            e.Address = model.Address;
            e.DepartmentCode = model.DepartmentCode;
            e.EmployeeNumber = model.EmployeeNumber;
            e.Age = model.Age;
            e.Status = model.Status;
            e.Description = model.Description;

            var errors = new List<string>();
            var results = new List<ValidationResult>();
            var context = new ValidationContext(e);

            switch (buttonClicked.ToUpper())
            {
                case "TRYVALIDATEMODEL":

                    /*
                     * The Controller.TryValidateModel() will validate the model and return a boolean specifying if the 
                     * model data is valid or not.
                     * 
                     * Reference:  https://msdn.microsoft.com/en-us/library/ff356143(v=vs.118).aspx
                     */

                    try
                    {
                        if (!TryValidateModel(e))
                        {
                            throw new InvalidModelException("Invalid Employee Model");
                        }
                    }
                    catch (InvalidModelException imex)
                    {
                        errors.Add(imex.Message);
                    }

                    ViewBag.ValidationType = "Controller.TryValidateModel()";

                    break;

                case "TRYVALIDATEOBJECT":

                    /*
                     * Use the Validator.TryValidateObject() if you want to get a list of error messages returned
                     * from the data annotation validation.
                     *
                     * Note:  Class level annotations will not get validated untill all properties pass validation.
                     * 
                     * Reference:  https://msdn.microsoft.com/en-us/library/dd411772.aspx
                     */

                    try
                    {
                        Validator.TryValidateObject(e, context, results, true);

                        if (results.Count > 0)
                        {
                            throw new InvalidModelException("Invalid Employee Model", results);
                        }
                    }
                    catch (InvalidModelException imex)
                    {
                        foreach(var err in imex.ErrorMessages){
                            errors.Add(err);
                        }
                    }
                    catch (Exception ex) 
                    {   
                        errors.Add(ex.Message);
                    }

                    ViewBag.ValidationType = "Validator.TryValidateObject()";

                    break;

                case "TRYVALIDATEPROPERTY":

                    /*
                     * Use the Validator.TryValidateProperty() if you want to get a list of error messages returned
                     * from the data annotation validation of a single property.
                     * 
                     * Reference:  https://msdn.microsoft.com/en-us/library/dd411772.aspx
                     */

                    context.MemberName = "FirstName";

                    try
                    {
                        Validator.TryValidateProperty(e.FirstName, context, results);

                        if (results.Count > 0)
                        {
                            throw new InvalidModelException("Invalid First Name Property", results);
                        }
                    }
                    catch(InvalidModelException imex)
                    {
                        foreach(var err in imex.ErrorMessages){
                            errors.Add(err);
                        }
                    }
                    catch (Exception ex) 
                    {   
                        errors.Add(ex.Message);
                    }

                    ViewBag.ValidationType = "Validator.TryValidateProperty()";

                    break;

            }

            ViewBag.Errors = errors;

            return View("ValidateModel", model);

        }

    }
}
/*
 * Custom exception that can be thrown when a model is invalid
 * 
 * Reference:  System.Exception
 *       URL:  https://msdn.microsoft.com/en-us/library/system.exception(v=vs.110).aspx
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataAnnotationValidation.CustomExceptions
{
    public class InvalidModelException : Exception
    {

        private List<string> _errorMessage = new List<String>();

        public List<string> ErrorMessages
        {
            get { return this._errorMessage; }
        }

        public InvalidModelException()
        {
        }
        
        public InvalidModelException(string message):base(message)
        {
        }

        public InvalidModelException(string message, ArrayList errorMessages)
            : base(message)
        {
            foreach (var e in errorMessages)
            {
                _errorMessage.Add(e.ToString());
            }
        }

        public InvalidModelException(string message, Collection<String> errorMessages)
            : base(message)
        {
            foreach (var e in errorMessages)
            {
                _errorMessage.Add(e.ToString());
            }
        }

        public InvalidModelException(string message, List<ValidationResult> errorMessages)
            : base(message)
        {
            foreach(var e in errorMessages)
            {
                _errorMessage.Add(e.ErrorMessage);
            }
        }
        
        public InvalidModelException(string message, System.Exception innerException)
            :base(message,innerException)
        {
        }

    }
}

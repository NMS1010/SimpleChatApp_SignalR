using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Social_Backend.Application.Common.Exceptions
{
    public class ValidationModelException : Exception
    {
        public List<string> Errors { get; }

        public ValidationModelException()
        {
            Errors = new List<string>();
        }

        public ValidationModelException(ModelState modelState)
            : this()
        {
            foreach (var obj in modelState.Errors)
            {
                Errors.Add(obj.ErrorMessage);
            }
        }
    }
}
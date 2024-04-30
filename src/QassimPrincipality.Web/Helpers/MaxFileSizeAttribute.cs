using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.Helpers
{
    public class TESTMaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public TESTMaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }

    //[AttributeUsage(AttributeTargets.Method)]
    public class MaxFileSizeAttribute : Attribute, IAsyncActionFilter
    {
        public MaxFileSizeAttribute(int sizeInByte) { this._maxFileSize = sizeInByte; }
        private int _maxFileSize;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (
                context
                .HttpContext
                .Request
                .Form != null
                && context.HttpContext.Request.Form.Files.Count > 0
                )
            {

                if (
                    context
                    .HttpContext
                    .Request
                    .Form
                    .Files
                    .Any(x => x.Length >= _maxFileSize)
                    )
                {
                    context
                        .Result = new ObjectResult($"Max file size is {_maxFileSize} bytes"
                        /*put here whatever object you want*/)
                        {
                            StatusCode = 413 //Payload Too Large
                        };
                }
                else { await next(); }
            }
            else { await next(); }

        }
    }

    /*
     [MaxFileSize(1024)]
public async Task<ActionResult> SendImages([FromForm] SendAttachment request)
{
 return Ok();
}
     */


}

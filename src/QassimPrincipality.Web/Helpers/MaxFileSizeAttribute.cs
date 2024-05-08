using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            object value,
            ValidationContext validationContext
        )
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
        public MaxFileSizeAttribute(int sizeInByte)
        {
            this._maxFileSize = sizeInByte;
        }

        private int _maxFileSize;

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            if (
                context.HttpContext.Request.Form != null
                && context.HttpContext.Request.Form.Files.Count > 0
            )
            {
                if (context.HttpContext.Request.Form.Files.Any(x => x.Length >= _maxFileSize))
                {
                    context.Result = new ObjectResult(
                        $"Max file size is {_maxFileSize} bytes"
                    /*put here whatever object you want*/)
                    {
                        StatusCode = 413 //Payload Too Large
                    };
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }

    //[AttributeUsage(AttributeTargets.Method)]
    public class MaxFilesSizeAttribute : ValidationAttribute
    {
        public MaxFilesSizeAttribute( int sizesInByte)
        {
            this._maxFilesSize = sizesInByte;
        }

        private int _maxFilesSize;

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            var files = value as List<IFormFile>;
            if (files != null)
            {
                if (files.Sum(c=>c.Length)  > _maxFilesSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"الحجم الاقصى للملفات هو {_maxFilesSize/(1024*1024)} ميجا";
        }
    }
        //[AttributeUsage(AttributeTargets.Method)]
    public class MaxFileSizeValidationAttribute : ValidationAttribute
    {
        public MaxFileSizeValidationAttribute( int sizeInByte)
        {
            this._maxFileSize = sizeInByte;
        }

        private int _maxFileSize;

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            var files = value as IFormFile;
            if (files != null)
            {
                if (files.Length  > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"الحجم الاقصى للملف هو {_maxFileSize/ (1024 * 1024)} ميجا";
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

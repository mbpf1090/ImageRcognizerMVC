using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRcognizerMVC.Models
{
    public class UploadModel : IValidatableObject
    {
        [Required(ErrorMessage = "Please select a photo")]
        public IFormFile Photo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<string> supportedFiles = new List<string> { "png", "jpg" };
            string extension = Path.GetExtension(Photo.FileName).Substring(1);

            if (!supportedFiles.Contains(extension))
            {
                yield return new ValidationResult($"{Photo.FileName}'s file extension is not valid.");

            }
        }
    }
}

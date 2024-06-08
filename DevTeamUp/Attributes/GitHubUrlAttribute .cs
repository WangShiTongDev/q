using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.Attributes
{
    public class GitHubUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string url && Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                if (uriResult.Host.Contains("github.com"))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("GitHub link must be a valid GitHub URL.");
            }
            return new ValidationResult("Invalid URL format.");
        }
    }
}

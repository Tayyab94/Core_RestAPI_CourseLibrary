using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ValidationAttritubes
{

    // Class Level Custom Validation Attributs.
    public class CourseTitleMustBeDifferentFromDescriptionAttributs :ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Course = (CourseForCreationDTO)validationContext.ObjectInstance;

            if (Course.Title.ToLower().Equals(Course.Description.ToLower()))
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(CourseForCreationDTO) }
                    );

            }

            return ValidationResult.Success;
        }
    }
}

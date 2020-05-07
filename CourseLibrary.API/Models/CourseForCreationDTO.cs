using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
                                        // Is applied to Impleent The Custom validation on the Entity Model
    public class CourseForCreationDTO :IValidatableObject
    {
    
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }


        // This function is Inherit from the IvalidatableObject and use to wrie the Custom logic to show the Custom Error
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Title==Description)
            {
                yield return new ValidationResult("The Provided Descrition Should be different  from the Title", new[] {"CourseForCreationDTO"});
            }
        }
    }
}

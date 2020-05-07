using CourseLibrary.API.ValidationAttritubes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{

    [CourseTitleMustBeDifferentFromDescriptionAttributs(ErrorMessage ="Title of Description Must be Different..")]

                                        // Is applied to Impleent The Custom validation on the Entity Model
    public class CourseForCreationDTO // :IValidatableObject
    {
    
        [Required(ErrorMessage ="The Title of the Course is REquired...")]
        [MaxLength(140,ErrorMessage ="Title Length must be 10 characotors....")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }


        //// This function is Inherit from the IvalidatableObject and use to wrie the Custom logic to show the Custom Error
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(Title==Description)
        //    {
        //        yield return new ValidationResult(
        //            "The Provided Descrition Should be different  from the Title", 
        //            new[] {"CourseForCreationDTO"});
        //    }
        //}
    }
}

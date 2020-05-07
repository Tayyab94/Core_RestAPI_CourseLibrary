using CourseLibrary.API.ValidationAttritubes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{

  //  [CourseTitleMustBeDifferentFromDescriptionAttributs(ErrorMessage ="Title of Description Must be Different..")]

                                        // Is applied to Impleent The Custom validation on the Entity Model
    public class CourseForUpdateDTO :CourseForManipulationDTO  // :IValidatableObject
    {
    

        [Required(ErrorMessage ="You Should Fill out the Description first")]


        public override string Description { get => base.Description; set => base.Description= value; }


        //public string Title { get; set; }

        //public string Description { get; set; }


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

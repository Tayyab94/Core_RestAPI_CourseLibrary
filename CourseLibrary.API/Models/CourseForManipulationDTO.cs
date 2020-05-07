using CourseLibrary.API.ValidationAttritubes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentFromDescriptionAttributs(ErrorMessage = "Title of Description Must be Different..")]

    public abstract class CourseForManipulationDTO
    {

        [Required(ErrorMessage = "The Title of the Course is REquired...")]
        [MaxLength(140, ErrorMessage = "Title Length must be 10 characotors....")]
        public string Title { get; set; }

        [MaxLength(100)]
        public virtual string Description { get; set; }
    }
}

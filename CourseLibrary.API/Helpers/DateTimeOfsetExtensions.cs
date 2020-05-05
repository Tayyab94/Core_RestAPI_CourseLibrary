using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOfsetExtensions
    {

        // This Function Returns the Age of the Person
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;

            int age = currentDate.Year - dateTimeOffset.Year;
         
            
           if(currentDate < dateTimeOffset.AddYears(age))
            {
                age--;

            }

            return age;
        }
    }
}

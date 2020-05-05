using AutoMapper;
using CourseLibrary.API.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class AuthorsProfile:Profile
    {


        //Constructor...
        public AuthorsProfile()
        {

            CreateMap<Entities.Author, Models.AuthorDTO>()
                .ForMember(
                    det => det.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                    )
                .ForMember(
                   det => det.Age,
                   opt => opt.MapFrom(
                       src => src.DateOfBirth.GetCurrentAge())
                   );


            CreateMap<Models.AuthorForCreationDTO, Entities.Author>();
        }
    }
}

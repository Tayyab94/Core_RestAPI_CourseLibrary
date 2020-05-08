﻿using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class CourseProfile :Profile
    {

        public CourseProfile()
        {
            CreateMap<Entities.Course, Models.CourseDTO>();
            CreateMap<Models.CourseForCreationDTO, Entities.Course>();

            CreateMap<CourseForUpdateDTO, Course>();


            CreateMap<Course, CourseForUpdateDTO>();


        }
    }
}

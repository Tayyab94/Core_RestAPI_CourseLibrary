 using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CousesController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;
        private readonly IMapper _mapper;

        public CousesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository;
            this._mapper = mapper;
        }


        [HttpGet("list")]
        public ActionResult<IEnumerable<CourseDTO>> GetCoursesForAuthors(Guid createrId)
        {
            if (!courseLibraryRepository.AuthorExists(createrId))
            {
                return NotFound();
            }



            var courseForAuthorsFromRepo = courseLibraryRepository.GetCourses(createrId);

            return Ok(_mapper.Map<IEnumerable<CourseDTO>>(courseForAuthorsFromRepo));
        }


        [HttpGet(Name = "GetCourseForAuthor")]
        [Route("{courseId}")]
        public ActionResult<CourseDTO> GetCourseForAuthor(Guid authorId, Guid courseId)
        {

            if (!courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            var courseForAuthorRepo = courseLibraryRepository.GetCourse(authorId, courseId);

            return Ok(_mapper.Map<CourseDTO>(courseForAuthorRepo));
        }



        [HttpPost]
        public ActionResult<CourseDTO> CreateCourse(Guid authorId, CourseForCreationDTO courseForCreationDTO)
        {
            if (!courseLibraryRepository.AuthorExists(authorId))
            {
                return BadRequest();
            }

            var courseEntiy = _mapper.Map<Course>(courseForCreationDTO);

            courseLibraryRepository.AddCourse(authorId, courseEntiy);
            courseLibraryRepository.Save();


            var courseToReturn = _mapper.Map<CourseDTO>(courseEntiy);

            return CreatedAtRoute("GetCourseForAuthor", new { authorId = authorId, courseId = courseToReturn.Id }, (courseToReturn));

        }

        [HttpPut("{courseId}")]

        public ActionResult UpdateCourseForAuthor(Guid courseId, Guid authorId, CourseForUpdateDTO courseForUpdateDTO)
        {
            if(!courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }


            var courseReturnFromRepo = courseLibraryRepository.GetCourse(authorId, courseId);

            if(courseReturnFromRepo==null)
            {
                var CourseToAdd = _mapper.Map<Course>(courseForUpdateDTO);

                courseLibraryRepository.AddCourse(authorId, CourseToAdd);
                courseLibraryRepository.Save();

                var courseToReturn = _mapper.Map<CourseDTO>(CourseToAdd);

                return CreatedAtRoute("GetCourseForAuthor",
                    new { authorId = authorId, courseId = courseToReturn.Id }, courseToReturn);
            }


            // Map the Entity to the CourseForUpdate 
            // Apply the Update Field Value to the DTo
            // Map the CourseForUpdate Back to the Entity 

            _mapper.Map(courseForUpdateDTO, courseReturnFromRepo);


            courseLibraryRepository.UpdateCourse(courseReturnFromRepo);

            courseLibraryRepository.Save();

            return NoContent();
        }
    
    }

}

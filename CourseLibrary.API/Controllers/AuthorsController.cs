using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController :ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this._courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(_courseLibraryRepository));
            this._mapper = mapper;
            ;
        }


        //  Get All Authors List
        
            // [FromQuery] is used to handle the complete type of parameters in the Query string.....
        [HttpGet()]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors([FromQuery]AuthorResourceParameters authorResourceParameters)
        {


            var authors = _courseLibraryRepository.GetAuthors(authorResourceParameters);

            return Ok(_mapper.Map<IEnumerable<AuthorDTO>>(authors));


            //throw new Exception("Some this");
            //var authorsList = _courseLibraryRepository.GetAuthors();


            //var Authors = new List<AuthorDTO>();

            //return Ok(_mapper.Map<IEnumerable<AuthorDTO>>(authorsList));






            //  Now we are going to user Mapper


            //foreach (var author in authorsList)
            //{

            //    Authors.Add(new AuthorDTO()
            //    {
            //       Id= author.Id,
            //        Name= $"{author.FirstName} {author.LastName}",
            //        MainCategory=author.MainCategory,
            //         Age=author.DateOfBirth.GetCurrentAge()  // this static function returns age 
            //    });
            //}


            //return new JsonResult(Authors);
        }


        // Get Single Author
        [HttpGet("{authorId}",Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _courseLibraryRepository.GetAuthor(authorId);

            if (author == null)
                return NotFound(author);  //status code 404 =>Not Found Result

            // return new JsonResult(author);

            // return Ok(_mapper.Map<Author, AuthorDTO>(author));

            return Ok(_mapper.Map<AuthorDTO>(author));
        
        }
        
        [HttpPost]
        public ActionResult<AuthorDTO>CreateAuthor(AuthorForCreationDTO authorForCreationDTO)
        {
            var authorEntity = _mapper.Map<Author>(authorForCreationDTO);

            _courseLibraryRepository.AddAuthor(authorEntity);

            _courseLibraryRepository.Save();

            var authorToRetrun = _mapper.Map<AuthorDTO>(authorEntity);

                            // RouteName will be that, which is defined in [HttpPost, Name="..Name.."]   
            return CreatedAtRoute("GetAuthor", new { authorId = authorToRetrun.Id }, authorToRetrun);

        }

        [HttpDelete("{authoId}")]

        public ActionResult DeleteAuthor(Guid authorId)
        {
            var authorReturn = _courseLibraryRepository.GetAuthor(authorId);

            if (authorReturn == null) return NotFound();

            _courseLibraryRepository.DeleteAuthor(authorReturn);

            _courseLibraryRepository.Save();


            return NoContent();
        }
    }
}

using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
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
    [Route("api/authorsCollection")]
    public class AuthorCollectionController :ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        public AuthorCollectionController(IMapper mapper, ICourseLibraryRepository courseLibraryRepository)
        {
            this._mapper = mapper;
            this._courseLibraryRepository = courseLibraryRepository;
        }

        [HttpGet("({ids})",Name = "GetAuthorsCollection")]

        public IActionResult GetAuthorsCollection(
            [FromRoute][ModelBinder(BinderType =typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var authorsEntities = _courseLibraryRepository.GetAuthors(ids);

            if(ids.Count()!=authorsEntities.Count())
            {
                return NotFound();
            }


            var authorToReturn = _mapper.Map<IEnumerable<AuthorDTO>>(authorsEntities);

            return Ok(authorToReturn);
        }

        // Collec
        [HttpPost]

        public ActionResult<IEnumerable<AuthorDTO>>CreateAuthorCollection(IEnumerable<AuthorForCreationDTO> authorForCreationDTOs)
        {
            var authorsEntities = _mapper.Map<IEnumerable<Author>>(authorForCreationDTOs);

            foreach (var item in authorsEntities)
            {
                _courseLibraryRepository.AddAuthor(item);
            }

            _courseLibraryRepository.Save();


            var authorCollectionReturm = _mapper.Map<IEnumerable<AuthorDTO>>(authorsEntities);

            var idsAsString = String.Join(",", authorCollectionReturm.Select(s => s.Id));

            return CreatedAtRoute("GetAuthorsCollection", new { ids = idsAsString }, authorCollectionReturm);
        }
    }
}

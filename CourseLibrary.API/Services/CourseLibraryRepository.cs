using CourseLibrary.API.Context;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.ResourceParameters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class CourseLibraryRepository : ICourseLibraryRepository, IDisposable
    {
        private readonly CourseLibraryContext context;

        public CourseLibraryRepository(CourseLibraryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCourse(Guid authorId, Course course)
        {
                if(authorId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (course == null)
                throw new ArgumentNullException(nameof(course));

            course.AuthorId = authorId;

            context.Courses.Add(course);

        }



        public void DeleteCourse(Course course)
        {
            context.Courses.Remove(course);
        }



        public Course GetCourse(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return context.Courses
              .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefault();
        }




        public IEnumerable<Course> GetCourses(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return context.Courses
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title).ToList();
        }

        public void UpdateCourse(Course course)
        {
            // no code in this implementation
        }

        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            // the repository fills the id (instead of using identity columns)
            author.Id = Guid.NewGuid();

            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            context.Authors.Add(author);
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return context.Authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            context.Authors.Remove(author);
        }

        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return context.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return context.Authors.ToList<Author>();
        }


        // Get Authors By Searching Parametes

        public PagedList<Author>GetAuthors(AuthorResourceParameters authorResourceParameters)
        {

            if (authorResourceParameters == null)
                throw new ArgumentNullException(nameof(authorResourceParameters));

            //if (string.IsNullOrEmpty(authorResourceParameters.mainCategory) && string.IsNullOrEmpty(authorResourceParameters.searchQuery))
            //{
            //    return GetAuthors();
            //}
            var collection=context.Authors as IQueryable<Author>;

            if(!string.IsNullOrEmpty(authorResourceParameters.mainCategory))
            {
                var mainCategory = authorResourceParameters.mainCategory.Trim();
                collection = collection.Where(a => a.MainCategory == mainCategory);

            }

            if(!string.IsNullOrEmpty(authorResourceParameters.searchQuery))
            {
              var  searchQery = authorResourceParameters.searchQuery.Trim();

                collection = collection.Where(s => s.MainCategory.Contains(searchQery)
                  || s.FirstName.Contains(searchQery) || s.LastName.Contains(searchQery));
                
            }

            //return collection
            //    .Skip(authorResourceParameters.PageSize * (authorResourceParameters.pageNo-1))
            //    .Take(authorResourceParameters.PageSize)
            //    .ToList();

            return PagedList<Author>.
                Create(collection, authorResourceParameters.pageNo, authorResourceParameters.PageSize);


//            return context.Authors.Where(s => s.MainCategory == mainCategory).ToList();

        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

    }
}

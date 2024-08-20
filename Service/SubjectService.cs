using Won7E1.Data;
using Won7E1.DTOs;
using Won7E1.Models;

namespace Won7E1.Service
{
    public class SubjectService
    {
        private readonly DataContext _dataContext;

        public SubjectService (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// This function is used to create a new subject
        /// </summary>
        /// <param name="request">The DTO containing the subject information</param>
        /// <returns>Return the new subject</returns>
        public Subject CreateSubject(SubjectDto request)
        {
            var newSubject = new Subject
            {
                Name = request.Name,
            };

            _dataContext.Subjects.Add(newSubject);
            _dataContext.SaveChanges();

            return newSubject;
        }
    }
}

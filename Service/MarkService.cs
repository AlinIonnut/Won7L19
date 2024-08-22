using Microsoft.EntityFrameworkCore;
using Won7E1.Data;
using Won7E1.DTOs;
using Won7E1.Models;

namespace Won7E1.Service
{
    public class MarkService
    {
        private readonly DataContext _dataContext;

        public MarkService (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Create a new mark. For that you need a student and a subject
        /// </summary>
        /// <param name="request">The DTO containing the mark information</param>
        /// <returns>Returns the new mark</returns>
        /// <exception cref="Exception">Thrown if the student or subject does not exist</exception>
        public async Task<Mark> CreateMarkAsync(MarkDto request)
        {
            var studentExists = await _dataContext.Students.AnyAsync(s => s.Id == request.StudentId);
            if (!studentExists)
            {
                throw new Exception($"Student with ID {request.StudentId} does not exist.");
            }

            var subjectExists = _dataContext.Subjects.Any(s => s.Id == request.SubjectId);
            if (!subjectExists)
            {
                throw new Exception($"Subject with ID {request.SubjectId} does not exist.");
            }

            var newMark = new Mark
            {
                Value = request.Value,
                DateAssigned = DateTime.Now,
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
            };

            _dataContext.Marks.Add(newMark);
            await _dataContext.SaveChangesAsync();

            return newMark;
        }

        /// <summary>
        /// Get all the marks that a student has for all of the subjects
        /// </summary>
        /// <param name="id">The unique identifier used for student</param>
        /// <returns>Returns a list with all the marks for all the subjects</returns>
        /// <exception cref="Exception">Thrown if the student if not found or if no marks are found</exception>
        public async Task<List<MarksForStudent>> GetAllMarksForStudentAsync(int id)
        {
            var studentExists = await _dataContext.Students.Include(s => s.Mark).ThenInclude(m => m.Subject).FirstOrDefaultAsync(s => s.Id == id);
            if(studentExists == null)
            {
                throw new Exception($"The student with id {id} does not exist!");
            }

            if (studentExists == null || !studentExists.Mark.Any())
            {
                throw new Exception($"No marks found for the student with id {id}!");
            }

            var marks = studentExists.Mark.Select(m => new MarksForStudent
            {
                Id = m.Id,
                Value = m.Value,
                DateAssigned = m.DateAssigned,
                SubjectName = m.Subject.Name
            })
              .ToList();

            return marks;
        }

        /// <summary>
        /// Get all the marks from a single subject
        /// </summary>
        /// <param name="studentId">The unique identifier used for student</param>
        /// <param name="subjectId">The unique identifier used for subject</param>
        /// <returns>Return a list with all the marks</returns>
        /// <exception cref="Exception">Thrown if the student of the subject are not found</exception>
        public async Task<List<MarksFromASubject>> GetAllMarksFromASubjectAsync (int studentId, int subjectId)
        {
            var studentExists = await _dataContext.Students.Include(s => s.Mark).ThenInclude(m => m.Subject).FirstOrDefaultAsync(m => m.Id == studentId);

            if (studentExists == null)
            {
                throw new Exception($"The student with id {studentId} does not exist!");
            }

            var marks = studentExists.Mark
                .Where(m => m.SubjectId == subjectId)
                .Select(m => new MarksFromASubject
                {
                    Id = m.Id,
                    Value = m.Value,
                    DataAssigned = m.DateAssigned,
                    Subject = m.Subject.Name,
                })
                .ToList();

            if (!marks.Any())
            {
                throw new Exception($"The subject with id {subjectId} does not exist!");
            }

            return marks;
        }

        /// <summary>
        /// Get the average marks for one student on all of the subjects
        /// </summary>
        /// <param name="studentId">The unique identifier for the student</param>
        /// <returns>Returns a list with the average marks for all the subjects</returns>
        /// <exception cref="Exception">Thrown if the student or subjects are not found</exception>
        public async Task<List<SubjectAverageDto>> GetSubjectAverageAsync (int studentId)
        {
           var  studentExists = await _dataContext.Students.Include(m => m.Mark).ThenInclude(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == studentId);

            if(studentExists == null)
            {
                throw new Exception($"Student with id {studentId} does not exist!");
            }

            var subjectAverage = studentExists.Mark.GroupBy(m => m.Subject)
                .Select(g => new SubjectAverageDto
                {
                    SubjectName = g.Key.Name,
                    AverageMarks = g.Average(m => m.Value)
                })
                .ToList();

            if (!subjectAverage.Any())
            {
                throw new Exception($"No marks found for the student with id {studentId}.");
            }

            return subjectAverage;
        }

        /// <summary>
        /// Calculate the average marks from all of the subjects for all of the students
        /// </summary>
        /// <param name="order">Order the list ascending (asc) or descending (desc)</param>
        /// <returns>Return a list with all of the students ordered based on the "order" value</returns>
        /// <exception cref="Exception">Thrown if no student was found in the list</exception>
        public async Task<List<StudentsOrderByGrades>> GetStudentsByGradesAsync(string order = "asc")
        {
            var studentsExists = await _dataContext.Students.Include(s => s.Mark).ThenInclude(m => m.Subject).ToListAsync();

            if (!studentsExists.Any())
            {
                throw new Exception($"No student was found!");
            }

            var listOfStudents = studentsExists.Select(s => new StudentsOrderByGrades
            {
                Id = s.Id,
                Name = s.Name,
                FirstName = s.FirstName,
                Age = s.Age,
                AverageMarks = CalculateAverageForStudent(s)
            }).ToList();

            if(order.ToLower() == "desc")
            {
                listOfStudents = listOfStudents.OrderByDescending(s => s.AverageMarks).ToList();
            }
            else
            {
                listOfStudents = listOfStudents.OrderBy(s => s.AverageMarks).ToList();
            }

            return listOfStudents;

        }

        /// <summary>
        /// Calculate the average of marks for each subject
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private double CalculateAverageForStudent(Student student)
        {
            var marksGroupedBySubject = student.Mark
                .GroupBy(m => m.Subject)
                .Select(g => g.Average(m => m.Value))
                .ToList();

            if (!marksGroupedBySubject.Any())
            {
                return 0.0;
            }

            return marksGroupedBySubject.Average();
        }
    }
}

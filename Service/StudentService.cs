using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Won7E1.Data;
using Won7E1.DTOs;
using Won7E1.Models;

namespace Won7E1.Service
{
    public class StudentService 
    {
        private readonly DataContext _dataContext;

        public StudentService (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Get all the students from the database
        /// </summary>
        /// <returns>Returns a list of students</returns>
        /// <exception cref="Exception">Thrown when the list of students is empty</exception>
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _dataContext.Students.ToListAsync();

            if (students == null)
            {
                throw new Exception("The list is empty");
            }
            return students;
        }

        /// <summary>
        /// Get a student by the identifier
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <returns>Returns the student if is found</returns>
        /// <exception cref="Exception">Thrown when no student with the specified ID is found</exception>
        public async Task<Student> GetStudentByIdAsync(int id) 
        {
            var student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new Exception($"The student with id {id} was not found!");
            }
            return student;
        }

        /// <summary>
        /// Get the address of a student based on the unique identifier of the student
        /// </summary>
        /// <param name="id">The unique identifier</param>
        /// <returns>Returns the address of the student</returns>
        /// <exception cref="Exception">Thrown when the student or the address of the student are not found</exception>
        public async Task<AddressDto> GetStudentByAdressAsync(int id)
        {
            var student = await _dataContext.Students.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new Exception($"The student with id {id} was not found!");
            }

            if(student.Address == null)
            {
                throw new Exception($"The address of the following id {id} was not found!");
            }

            var addressDto = new AddressDto
            {
                City = student.Address.City,
                Street = student.Address.Street,
                Number = student.Address.Number,
            };

            return addressDto;
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="request">The DTO containing the student information</param>
        /// <returns>Returns the newly created student</returns>
        public async Task<Student> CreateStudentWithoutAdressAsync(StudentWithoutAddressDto request)
        {
            var newStudent = new Student
            {
                Name = request.Name,
                FirstName = request.FirstName,
                Age = request.Age,
            };

            _dataContext.Students.Add(newStudent);
            await _dataContext.SaveChangesAsync();

            return newStudent;
        }

        /// <summary>
        /// Modify a student object
        /// </summary>
        /// <param name="id">The unique identifier used for student</param>
        /// <param name="request">The DTO containing the student information</param>
        /// <returns>Return a new modified student object</returns>
        /// <exception cref="Exception">Thrown if the student is not found based on the unique identifier</exception>
        public async Task<Student> UpdateStudentAsync(int id, StudentWithoutAddressDto request)
        {
            var student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new Exception($"The student with id {id} was not found!");
            }

            student.Name = request.Name;
            student.FirstName = request.FirstName;
            student.Age = request.Age;

            await _dataContext.SaveChangesAsync();

            return student;
        }

        /// <summary>
        /// Modify the student address and if the address does not exist you can create a new one
        /// </summary>
        /// <param name="id">The unique identifier used for student</param>
        /// <param name="request">The DTO containing the address information</param>
        /// <returns>Retun a the new address</returns>
        /// <exception cref="Exception">Thrown if the student is not found based on the unique identifier</exception>
        public async Task<StudentWithAddress> UpdateStudentAddressAsync(int id, AddressDto request)
        {
            var student = await _dataContext.Students.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new Exception($"The student with id {id} was not found!");
            }

            if (student.Address == null)
            {
                var newAddress = new Address
                {
                    City = request.City,
                    Street = request.Street,
                    Number = request.Number,
                };

                _dataContext.Addresses.Add(newAddress);
                student.Address = newAddress;
            }
            else
            {
                student.Address.Street = request.Street;
                student.Address.City = request.City;
                student.Address.Number = request.Number;
            }

            await _dataContext.SaveChangesAsync();

            var fullInformationsOfStudent = new StudentWithAddress
            {
                Name = student.Name,
                FirstName = student.FirstName,
                Address = student.Address == null ? null : new AddressDto
                {
                    City = student.Address.City,
                    Street = student.Address.Street,
                    Number = student.Address.Number,
                }
            };

            return fullInformationsOfStudent;
        }

        /// <summary>
        /// Delete the student and the marks associeted with the student
        /// </summary>
        /// <param name="studentId">The unique identifier used for student</param>
        /// <param name="deleteAddress">Choose if you want to delete the address too. It is set as false but if you want to delete the address you have to change it to "true"</param>
        /// <exception cref="Exception">Thrown if the student is not found based on the unique identifier</exception>
        public async Task<Student> DeleteStudentAsync(int studentId, bool deleteAddress = false)
        {
            var studentExists = await _dataContext.Students.Include(s => s.Address).Include(s => s.Mark).FirstOrDefaultAsync(s => s.Id == studentId);
            if (studentExists == null)
            {
                throw new Exception($"The student with id {studentId} was not found!");
            }

            if (studentExists.Mark.Any())
            {
                _dataContext.Marks.RemoveRange(studentExists.Mark);
            }

            if (deleteAddress && studentExists.Address != null)
            {
                _dataContext.Addresses.Remove(studentExists.Address);
            }

            _dataContext.Students.Remove(studentExists);

            await _dataContext.SaveChangesAsync();

            return studentExists;
        }
    }
}

using Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Services
{
    public interface IStudentService
    {
        ICollection<StudentsEntity> GetAllStudents();
        void AddStudent(StudentsEntity entity);
        void UpdateStudent(StudentsEntity entity);
        void DeleteUser(int studentId);
    }
}

using Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Services
{
    class StudentService
    {
        public ICollection<StudentsEntity> GetAllStudents()
        {
            List<StudentsEntity> Lstuser = new List<StudentsEntity>();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BLAZORCRUD;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetALLUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentsEntity studentsEntity = new StudentsEntity();
                        studentsEntity.studentid = Convert.ToInt32(reader["studentid"]);
                        studentsEntity.first_name = reader["first_name"].ToString();
                        studentsEntity.last_name = reader["last_name"].ToString();
                        studentsEntity.email = reader["email"].ToString();
                        studentsEntity.gender = reader["gender"].ToString();
                        studentsEntity.CreatedOn = Convert.ToDateTime(reader["created_on"]);
                        Lstuser.Add(studentsEntity);
                    }
                    reader.Close();
                }
                return Lstuser;
            }
        }





    }
}

using Blazor.Shared.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Services
{
   public class StudentService : IStudentService
    {
        private readonly IConfiguration configuration;
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


        public void AddStudent(StudentsEntity entity)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BLAZORCRUD;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertUserInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = entity.first_name;
                    command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = entity.last_name;
                    command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255)).Value = entity.email;
                    command.Parameters.Add(new SqlParameter("@gender", SqlDbType.VarChar, 1)).Value = entity.gender;

                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateStudent(StudentsEntity entity)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BLAZORCRUD;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateUserInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@student_id", SqlDbType.Int)).Value = entity.studentid; 
                    command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = entity.first_name;
                    command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = entity.last_name;
                    command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255)).Value = entity.email;
                    command.Parameters.Add(new SqlParameter("@gender", SqlDbType.VarChar, 1)).Value = entity.gender;

                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteUser(int studentId)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BLAZORCRUD;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteUserInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@studentid", SqlDbType.Int)).Value = studentId;

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

using Dapper;
using StudentManagement.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;


namespace StudentManagement.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IConfiguration _configuration;

        public StudentServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DBconnsction");
            provideName = "System.Data.SqlClient";

        }

        public string ConnectionString { get; }

        public string provideName { get; }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }


        public string DeleteStudent(int StudentId)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var stdnt = dbConnection.Query<Student>("DeleteStudent", new
                    {
                        StudentId = StudentId
                    },
                        commandType: CommandType.StoredProcedure);

                    if (stdnt != null && stdnt.FirstOrDefault().Response == "Delete Successfully")
                    {
                        result = "Delete Successfully";
                    }
                    dbConnection.Close();
                    return result;

                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public object GetStudentsList()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudentList()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    students = dbConnection.Query<Student>("GetStudentList", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return students;

                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return students;

            }
        }

        public string InsertStudent(Student student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var stdnt = dbConnection.Query<Student>("Insertstudent", new
                    {
                        FullName = student.FullName,
                        EmailAddress = student.EmailAddress,
                        City = student.City,
                        CreatedBy = student.CreatedBy
                    },
                        commandType: CommandType.StoredProcedure);

                    if (stdnt != null && stdnt.FirstOrDefault().Response == "Save Successfully")
                    {
                        result = "Save Successfully";
                    }
                    dbConnection.Close();
                    return result;

                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;


            }
        }
        public string UpdateStudent(Student student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var stdnt = dbConnection.Query<Student>("UpdateStudent", new
                    {
                        FullName = student.FullName,
                        EmailAddress = student.EmailAddress,
                        City = student.City,
                        CreatedBy = student.CreatedBy,
                        StudentId = student.StudentId
                    },
                        commandType: CommandType.StoredProcedure);

                    if (stdnt != null && stdnt.FirstOrDefault().Response == "Update Successfully")
                    {
                        result = "Update Successfully";
                    }
                    dbConnection.Close();
                    return result;

                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
    }
}



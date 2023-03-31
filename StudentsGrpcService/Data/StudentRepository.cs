using Dapper;
using System.Data;

namespace StudentsGrpcService.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly Lazy<IDbConnection> _db;

        public StudentRepository(IDbConnection db)
        {
            _db = new Lazy<IDbConnection>(db);
        }

        public IEnumerable<Models.StudentModel> GetStudents() =>
           _db.Value.Query<Models.StudentModel>("SELECT * FROM Students");

        public Models.StudentModel? GetStudent(Guid studentId) =>
            _db.Value.Query<Models.StudentModel>("SELECT * FROM Students WHERE Id=@studentId", new { studentId }).FirstOrDefault();

        public void Create(Models.StudentModel student)
        {        
            var sqlQuery = "INSERT INTO Students (Id, FirstName, LastName, GroupNumber) VALUES(NEWID(), @FirstName, @LastName, @GroupNumber)";
            _db.Value.Execute(sqlQuery, student);
        }

        public void Update(Models.StudentModel student) =>
            _db.Value.Query<Models.StudentModel>("UPDATE Students SET FirstName=@FirstName, LastName=@LastName, GroupNumber=@GroupNumber WHERE Id=@Id", student);

        public void Delete(Guid studentId) =>
            _db.Value.Query<Models.StudentModel>("DELETE FROM Students WHERE Id=@studentId", new { studentId });
    }
}

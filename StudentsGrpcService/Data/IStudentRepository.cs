namespace StudentsGrpcService.Data
{
    public interface IStudentRepository
    {
        IEnumerable<Models.StudentModel> GetStudents();
        Models.StudentModel? GetStudent(Guid studentId);
        void Create(Models.StudentModel student);
        void Update(Models.StudentModel student);
        void Delete(Guid studentId);
    }
}

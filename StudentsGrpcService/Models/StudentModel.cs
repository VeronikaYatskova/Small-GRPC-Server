namespace StudentsGrpcService.Models
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GroupNumber { get; set; }
    }
}

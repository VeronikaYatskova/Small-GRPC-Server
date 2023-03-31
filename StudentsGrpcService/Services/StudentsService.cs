using AutoMapper;
using Grpc.Core;
using StudentsGrpcService.Data;
using StudentsGrpcService.Models;

namespace StudentsGrpcService.Services
{
    public class StudentsService : Student.StudentBase
    {
        private readonly ILogger<StudentsService> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentsService(
            ILogger<StudentsService> logger,
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public override Task<Students> GetStudents(GetStudentsRequest request, ServerCallContext context)
        {
            var students = new Students();
            var studentsList = _studentRepository.GetStudents();
            var studentsGrpcList = _mapper.Map<IEnumerable<StudentModelGrpc>>(studentsList);

            students.Students_.AddRange(studentsGrpcList);

            return Task.FromResult(students);
        }

        public override Task<Message> AddStudent(RequestModelGrpc request, ServerCallContext context)
        {
            var studentModel = _mapper.Map<StudentModel>(request);

            _studentRepository.Create(studentModel);

            return Task.FromResult(new Message { Message_ = "Student created"});
        }

        public override Task<Message> UpdateStudent(UpdateStudentModel request, ServerCallContext context)
        {
            var studentToChange = _studentRepository.GetStudent(new Guid(request.Guid));

            if (studentToChange is null)
            {
                throw new ArgumentNullException(nameof(studentToChange));
            }

            _studentRepository.Update(new StudentModel
            {
                Id = studentToChange.Id,
                FirstName = request.NewStudentInfo.FirstName,
                LastName = request.NewStudentInfo.LastName,
                GroupNumber = request.NewStudentInfo.GroupNumber,
            });

            return Task.FromResult(new Message { Message_ = "Student updated" });
        }

        public override Task<Message> DeleteStudent(StudentGuid request, ServerCallContext context)
        {
            _studentRepository.Delete(new Guid(request.Guid));

            return Task.FromResult(new Message { Message_ = "Student deleted" });
        }
    }
}

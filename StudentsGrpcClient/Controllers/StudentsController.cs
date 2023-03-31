using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using StudentsGrpcService;

namespace StudentsGrpcClient.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly IConfiguration _configuration;
        private readonly Student.StudentClient _studentClient;

        public StudentsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel = GrpcChannel.ForAddress(_configuration["LocalhostAddress"]);
            _studentClient = new Student.StudentClient(_channel);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var list = await _studentClient.GetStudentsAsync(new GetStudentsRequest());

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(RequestModelGrpc student)
        {
            var result = await _studentClient.AddStudentAsync(student);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentModel student)
        {
            var result = await _studentClient.UpdateStudentAsync(student);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(StudentGuid studentGuid)
        {
            var result = await _studentClient.DeleteStudentAsync(studentGuid);

            return Ok(result);
        }
    }
}

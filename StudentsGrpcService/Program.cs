using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentsGrpcService.Data;
using StudentsGrpcService.Services;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("sqlConnection");

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddGrpc(); 
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDbConnection, SqlConnection>(e => new SqlConnection(connectionString));
builder.Services.AddDbContext<StudentDbContext>(opts => 
    opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<StudentsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

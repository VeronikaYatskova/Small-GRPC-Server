using AutoMapper;
using StudentsGrpcService.Models;

namespace StudentsGrpcService.Utils.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RequestModelGrpc, StudentModel>();
            CreateMap<StudentModel, StudentModelGrpc>()
                .ForMember(s => s.Guid, opt => opt.MapFrom(x => x.Id.ToString()));
        }
    }
}

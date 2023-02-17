using AutoMapper;

namespace Common.Application
{
    public class ServiceBase
    {
        public readonly IMapper _Mapper;
        public ServiceBase(IMapper mapper)
        {
                _Mapper = mapper;
        }
    }
}

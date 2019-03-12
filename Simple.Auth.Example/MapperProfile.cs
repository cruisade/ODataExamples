using AutoMapper;
using Simple.Auth.Example.Db;

namespace Simple.Auth.Example
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserApiModel>();
        }
    }
}
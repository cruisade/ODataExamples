using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple.Auth.Example.Db;

namespace Simple.Auth.Example.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _ctx;
        private readonly IMapper _mapper;

        public UserController(UserContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("v1/users")]
        [EnableQuery] // так же позволяет использовать OData
        public async Task<ActionResult<IEnumerable<UserApiModel>>> GetUserListV1()
        {
            // читает всю таблицу, возвращает все поля
            var list = await _ctx.Users
                .Select(model => _mapper.Map<UserApiModel>(model))
                .ToListAsync();

            return list;
        }


        [HttpGet("v2/users")]
        public async Task<ActionResult<IEnumerable<UserApiModel>>> GetUserListV2(ODataQueryOptions<UserApiModel> query)
        {
            // все еще читает всю таблицу, возвращает все поля
            var list = await _ctx.Users
                .Select(model => _mapper.Map<UserApiModel>(model))
                .ApplyODataQuery(query)
                .ToListAsync();

            return list;
        }

        // уязвимый пока не выстален аттрибут [IgnoreDataMember]
        [HttpGet("v3/users")]
        public async Task<ActionResult<IEnumerable<UserApiModel>>> GetUserListV3(ODataQueryOptions<User> query)
        {
            // читает таблицу с where, возвращает все поля
            var list = await _ctx.Users
                .ApplyODataQuery(query)
                .Select(model => _mapper.Map<UserApiModel>(model))
                .ToListAsync();

            return list;
        }

        [HttpGet("v4/users")]
        public async Task<ActionResult<IEnumerable<UserApiModel>>> GetUserListV4(ODataQueryOptions<UserApiModel> query)
        {
            // читает таблицу с where, не возвращает поле Password
            var list = await _ctx.Users
                .ProjectTo<UserApiModel>(_mapper.ConfigurationProvider)
                .ApplyODataQuery(query)
                .ToListAsync();

            return list;
        }

        // other actions
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NistagramOnlineAPI.Model;
using NistagramOnlineAPI.Service;
using NistagramSQLConnection.Model;
using NistagramUtils.DTO;

namespace NistagramOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnlineController : ControllerBase
    {

        private readonly IOnlineService _iOnlineService;
        private readonly IMapper _mapper;

        public OnlineController(IOnlineService iOnlineService, IMapper mapper)
        {
            _iOnlineService = iOnlineService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/[action]")]
        public Object AddNewFollower(NewFollower newFollower)
        {
            Response res = _iOnlineService.AddNewFollower(newFollower);
            return res;
        }

        [HttpGet]
        [Route("/[action]")]
        public Object GetNewFollowers(string id)
        {
            List<User> user = _iOnlineService.GetNewFollowers(id);
            return user;
        }
    }
}

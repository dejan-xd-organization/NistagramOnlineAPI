using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NistagramOnlineAPI.Model;
using NistagramSQLConnection.Model;
using NistagramSQLConnection.Service.Interface;

namespace NistagramOnlineAPI.Service.Implementation
{
    public class OnlineServiceImpl : IOnlineService
    {

        private readonly IUserService _iUserService;

        public OnlineServiceImpl(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        public Response AddNewFollower(NewFollower newFollower)
        {
            Response res = new Response();
            bool status = _iUserService.AddNewFollower(newFollower.myId, newFollower.followerId);
            if (status)
            {
                res.status = "SUCCESS";
                res.message = "added_new_follower";
            }
            else
            {
                res.status = "ERROR";
                res.message = "failed_to_add_new_follower";
            }
            return res;
        }

        public List<User> GetNewFollowers(string id)
        {
            var res = _iUserService.GetNewFollowers(id);

            foreach(Object obj in res)
            {
                var i = obj;
            }

            return null;
        }
    }
}

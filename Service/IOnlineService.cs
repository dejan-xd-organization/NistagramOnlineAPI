using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NistagramOnlineAPI.Model;
using NistagramSQLConnection.Model;

namespace NistagramOnlineAPI.Service
{
    public interface IOnlineService
    {
        Response AddNewFollower(NewFollower newFollower);
        List<User> GetNewFollowers(string id);
    }
}

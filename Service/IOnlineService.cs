using NistagramSQLConnection.Model;
using NistagramUtils.DTO.Follower;
using NistagramUtils.Response;
using System.Collections.Generic;

namespace NistagramOnlineAPI.Service
{
    public interface IOnlineService
    {
        List<WallPost> GetAllWallPosts();
        Response AddNewFollower(NewFollower newFollower);
        List<User> GetNewFollowers(string id);
    }
}

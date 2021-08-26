using NistagramSQLConnection.Model;
using NistagramUtils.DTO;
using NistagramUtils.DTO.Follower;
using NistagramUtils.Response;
using System.Collections.Generic;

namespace NistagramOnlineAPI.Service
{
    public interface IOnlineService
    {
        List<WallPost> GetAllWallPosts();
        Response AddNewFollower(NewFollower newFollower);
        List<UserDto> GetFollowers(string id, int page, bool type);
        List<UserDto> GetNewFollowings(string id, int page);
    }
}

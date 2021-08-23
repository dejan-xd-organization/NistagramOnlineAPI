using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NistagramOnlineAPI.Model
{
    public class NewFollower
    {
        public long myId { get; set; }
        public long followerId { get; set; }
    }
}

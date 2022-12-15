using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private Dictionary<string, HashSet<TUser>> followed;

        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            followed = new Dictionary<string, HashSet<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            bool result = followed.TryAdd(group, new HashSet<TUser>());
            followed[group].Add(user);
            return result;
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                HashSet<TUser> result = new HashSet<TUser>();
                foreach (var set in followed.Values)
                {
                    result.UnionWith(set);
                }
                return new List<TUser>(result);
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if (!followed.ContainsKey(group))
            {
                return new HashSet<TUser>();
            }
            return followed[group];
        }
    }
}

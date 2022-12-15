using System;

namespace Collections
{
    public class User : IUser
    {
        private readonly string fullName;
        private readonly string username;
        private uint? age;

        public User(string fullName, string username, uint? age)
        {
            if (!String.IsNullOrWhiteSpace(fullName))
            {
                this.fullName = fullName;
            }
            if (!String.IsNullOrWhiteSpace(username))
            {
                this.username = username;
            }
            this.age = age;
        }
        
        public uint? Age => age;
        public string FullName => fullName;
        public string Username => username;
        public bool IsAgeDefined => age != null;
        
        // TODO implement missing methods (try to autonomously figure out which are the necessary methods)
    }
}

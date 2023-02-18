using CarSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Core.Entities
{
    public class User
    {
        public UserId UserId { get; private set; }
        public Email Email { get; private set; }
        public Username Username { get; private set; }
        public Password Password { get; private set; }
        public FullName FullName { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User(UserId userId, Email email, Username username, Password password, FullName fullName, Role role, DateTime createdAt)
        {
            UserId = userId;
            Email = email;
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}

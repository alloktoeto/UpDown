using System;
using System.Collections.Generic;
using System.Linq;
using UpDown.Models;
using System.Web;
using System.Web.Security;
using System.Data;

namespace UpDown.Repository
{
    public class UsersRepository: BaseRepository
    {
        public List<user> GetUsers()
        {
            return _db.users.ToList();
        }

        public user GetUserById(Guid id)
        {
            return _db.users.FirstOrDefault(x => x.userID == id);
        }

        public void LoginSignUp(user user)
        {
            var emails = _db.users.Select(x => x.email).ToList();
            if (emails.Contains(user.email))
            {
                return;
            }
            else
            {
                _db.users.AddObject(user);
                _db.SaveChanges();
            }
        }

        public user GetUserByEmail(string email)
        {
            return _db.users.FirstOrDefault(x => x.email == email);
        }

        //public user GetMembershipUserByName(string email)
        //{
        //    user u = _db.users.FirstOrDefault(x => x.email == email);
        //    if (u != null)
        //    {
        //        return u;
        //    }
        //    return null;
        //}


        public MembershipUser GetMembershipUserByName(string email)
        {
            user u = _db.users.FirstOrDefault(x => x.email == email);
            if (u != null)
            {
                return new MembershipUser(
                     "CustomMembershipProvider",
                     u.username,
                     u.userID,
                     u.email,
                     "",
                     null,
                     true,
                     false,
                     (DateTime)u.dateRegistry,
                     DateTime.Now,
                     DateTime.Now,
                     DateTime.Now,
                     DateTime.Now
                );
            }
            return null;
        }

        public string GetUserNameByEmail(string email)
        {
            user u = _db.users.FirstOrDefault(x => x.email == email);
            return u != null ? u.username : "";
        }

        public void CreateUser(string username, string password, string email, string firstname, string lastname)
        {
            user user = new user();
            user.username = username;
            user.password = password;
            user.email = email;
            user.firstname = firstname;
            user.lastname = lastname;
            user.dateRegistry = DateTime.Now;
            SaveUser(user);
        }

        public bool ValidateUser(string username, string password)
        {
            user u = _db.users.FirstOrDefault(x => x.username == username);
            if (u != null && u.password == password)
            {
                return true;
            }
            return false;
        }

        public void SaveUser(user user)
        {
            if (user.userID != null)
            {
                _db.users.AddObject(user);
            }
            else
            {
                _db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
            }

            _db.SaveChanges();
        }
    }
}
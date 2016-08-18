using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;
using UpDown.Repository;

namespace UpDown.Models
{
    public class DataManager
    {
        private UsersRepository usersModel;
        private PrimaryMembershipProvider provider;

        public DataManager(UsersRepository usersModel, PrimaryMembershipProvider provider)
        {
            this.usersModel = usersModel;
            this.provider = provider;
        }

        public UsersRepository Users { get { return usersModel; } }

        public PrimaryMembershipProvider MembershipProvider { get { return provider; } }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;
using System.Configuration;

namespace UpDown.Repository
{
    
    public class BaseRepository
    {
        protected DB_9FCBDB_UpDownDBTestEntities _db;// DB_84979_udsEntities _db;
        protected readonly string _connectionString;

        public BaseRepository()
        {
            _db = new DB_9FCBDB_UpDownDBTestEntities(); // DB_84979_udsEntities();
            _connectionString = ConfigurationManager.ConnectionStrings["DBConnectionTest"].ConnectionString;
        }
    }
}
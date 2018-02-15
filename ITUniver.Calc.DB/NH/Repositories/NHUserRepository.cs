﻿using ITUniver.Calc.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITUniver.Calc.DB.Models;
using NHibernate.Criterion;

namespace ITUniver.Calc.DB.NH.Repositories
{
    public class NHUserRepository : NHBaseRepository<User>, IUserRepository
    {
        public bool Check(string login, string password)
        {
            var session = Helper.GetCurrentSession();
            
            return session
                .QueryOver<User>()
                .And(u => u.Login == login && u.Password == password && u.Status != UserStatus.Deleted)
                .RowCount() > 0;
            
        }

        public User GetByName(string login)
        {
            var session = Helper.GetCurrentSession();
            
            return session.QueryOver<User>()
                .And(u => u.Login == login)
                .SingleOrDefault();
            
        }
        
    }
}
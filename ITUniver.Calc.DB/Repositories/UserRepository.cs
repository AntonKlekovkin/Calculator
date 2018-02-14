using ITUniver.Calc.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.Calc.DB.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public bool Check(string login)
        {
            // to do - не оптимально!
            return GetAll().Any(u => u.Login == login);
        }

        public bool Check(string login, string password)
        {
            // to do - не оптимально!
            return GetAll().Any(u => u.Login == login && u.Password == password);
        }

        public User GetByName(string login)
        {
            throw new NotImplementedException();
        }
    }
}

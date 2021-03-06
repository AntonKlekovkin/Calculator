﻿using ITUniver.Calc.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.Calc.DB.Models
{
    public class User : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual DateTime? BirthDay { get; set; }

        public virtual bool Sex { get; set; }

        public virtual ICollection<HistoryItem> History { get; set; }

        public virtual UserStatus Status { get; set; }

        public virtual Role Role { get; set; }

    }

    public enum UserStatus
    {
        Deleted = 0,
        Active = 1,
        Ban = 2
    }
}

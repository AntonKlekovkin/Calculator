using ITUniver.Calc.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.Calc.DB.Models
{
    public class Operation : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Owner { get; set; }

        public virtual int ArgsCount { get; set; }

        public virtual DateTime CreationDate { get; set; }
    }
}

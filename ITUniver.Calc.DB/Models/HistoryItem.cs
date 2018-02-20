using System;

namespace ITUniver.Calc.DB.Models
{
    public class HistoryItem : IHistoryItem
    {
        public virtual long Id { get; set; }

        public virtual long Operation { get; set; }

        public virtual string Args { get; set; }

        public virtual double? Result { get; set; }

        public virtual DateTime ExecDate { get; set; }

        public virtual User Author { get; set; }

        public virtual long TimeCalculation { get; set; }
    }
}

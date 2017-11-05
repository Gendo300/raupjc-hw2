using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raupjc_hw2_zad2
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public String Text { get; set; }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid();

            DateCreated = DateTime.UtcNow;

            Text = text;
        }

        public bool IsCompleted => DateCompleted.HasValue;

        public DateTime? DateCompleted { get; set;}
    
        public DateTime DateCreated { get; set; }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool Equals(TodoItem item)
        {
            if (this != null && item!=null)
            {
                if (this.Id == item.Id)
                {
                    return true;
                }
                return false;
            }
            else if (this == null && item == null)
            {
                return true;
            }
            return false;
        }

    }
}

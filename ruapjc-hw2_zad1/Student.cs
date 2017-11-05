using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ruapjc_hw2_zad1
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public static bool operator ==(Student student1, Student student2)
        {
            if (student1 != null)
            {
                return student1.Equals(student2);
            }
            return student2 == null;
        }

        public static bool operator !=(Student student1, Student student2)
        {
            if (student1 != null)
            {
                return !student1.Equals(student2);
            }
            return student2 == null;
        }

        public override bool Equals(object otherStudent)
        {
            if (otherStudent is Student)
            {
                Student castedStudent = (Student) otherStudent;
                if (this.Name == castedStudent.Name || this.Name == castedStudent.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Int32.Parse(this.Jmbag);
        }
    }
}
    

    

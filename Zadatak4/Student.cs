using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak4
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

        public static bool operator== (Student s1, Student s2)
        {
            return s1.Equals(s2);
        }
        public static bool operator!= (Student s1,Student s2)
        {
            return !s1.Equals(s2);
        }
        
        public override bool Equals(Object obj)
        {
            Student s = (Student)obj;
            return s.Jmbag.Equals(this.Jmbag);
        }

        public override int GetHashCode()
        {
            return Jmbag.GetHashCode();
        }
    }

    public enum Gender
    {
        Male, Female
    }
}

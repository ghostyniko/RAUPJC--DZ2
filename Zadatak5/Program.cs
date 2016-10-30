using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak4;

namespace Zadatak5
{
    class Program
    {
        static void Main(string[] args)
        {
            University[] universities = GetAllCroatianUniversities();

            Student[] allCroatianStudents = universities.SelectMany(el => el.Students)
                                                        .Distinct()
                                                        .ToArray();

            Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(el => el.Students)
                                                                            .GroupBy(st => st)
                                                                            .Where(group => group.Count() > 1)
                                                                            .Select(group => group.Key)
                                                                            .ToArray();

            Student[] studentsOnMaleOnlyUniversities = universities.Where(un => un.Students
                                                                                  .All(stud => 
                                                                                        stud.Gender.Equals(Gender.Male)))
                                                                   .SelectMany(un => un.Students)
                                                                   .Distinct()
                                                                   .ToArray();
                                                                                                      
        }
    }
}

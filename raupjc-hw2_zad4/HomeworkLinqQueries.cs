using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ruapjc_hw2_zad1;

namespace raupjc_hw2_zad4
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            return intArray.OrderBy(x=>x).GroupBy(x => x).Select(x => "Broj "+ x.Key + " ponavlja se "+ x.Count() +" puta").ToArray();
        }

        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(x => ((x.Students.Where(student => student.Gender == Gender.Female)).Count()) == 0)
                .ToArray();
        }

        public static University[] Linq2_2(University[] universityArray)
        {
            return universityArray.Where(uni =>
                    (uni.Students.Count() <= (universityArray.Select(x => x.Students.Count())).Average())).ToArray();
        }

        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray.SelectMany(uni => uni.Students).Distinct().ToArray();
        }

        public static Student[] Linq2_4(University[] universityArray)
        {
            return (universityArray.Where(x =>
                         ((!x.Students.Any(student => (student.Gender == Gender.Female))) ||
                         ((!x.Students.Any(student => (student.Gender == Gender.Male))))))
                    .SelectMany(uni => uni.Students).Distinct().ToArray());
        }

        public static Student[] Linq2_5(University[] universityArray)
        {
            return (universityArray.SelectMany(uni => uni.Students)
                .Where(s =>
                    (((universityArray.SelectMany(univ => univ.Students).GroupBy(x => x.Name))
                    .Where(stud => stud.Count() > 1)).Select(x=>x.Key)
                .Contains(s.Name))))
                .Distinct().ToArray();
        }
    }
}

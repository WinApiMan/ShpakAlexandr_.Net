using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lab1.Models
{
    [DataContract]
    public class AverageGroupRating
    {
        [DataMember]
        public double Average { get; set; }

        [DataMember]
        public IEnumerable<StudentAverageMark> Students { get; set; }

        [DataMember]
        public IEnumerable<Subject> Subjects { get; set; }

        public AverageGroupRating(double average, IEnumerable<StudentAverageMark> students, IEnumerable<Subject> subjects)
        {
            Average = average;
            Students = students;
            Subjects = subjects;
        }

    }
}

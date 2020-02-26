using System.Runtime.Serialization;

namespace Lab1.Models
{
    [DataContract]
    public class StudentAverageMark
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Patronymic { get; set; }

        [DataMember]
        public double AverageMark { get; set; }

        public StudentAverageMark(string name, string surname, string patronymic, double averageMark)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            AverageMark = averageMark;
        }
    }
}

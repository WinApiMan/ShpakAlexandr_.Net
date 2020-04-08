using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class ClientDto
    {
        public ClientDto(int id, string surname, string name, string patronymic)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Patronymic { get; set; }
    }
}
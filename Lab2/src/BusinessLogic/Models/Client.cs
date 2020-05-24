namespace BusinessLogic.Models
{
    public class Client
    {
        public Client(int id, string surname, string name, string patronymic)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }

        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }
    }
}
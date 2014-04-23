using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Client : IEntity
    {
        public Client()
        {
            
        }

        public Client(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
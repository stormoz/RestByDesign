using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class Client : IEntity
    {
        protected Client()
        { }

        public Client(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }
}
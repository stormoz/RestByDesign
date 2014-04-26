using System;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class SmartTag : IEntity
    {
        protected SmartTag()
        { }

        public SmartTag(string id, string accountId, bool active, bool deleted, int version, DateTime orderedDateTime)
        {
            Id = id;
            AccountId = accountId;
            Active = active;
            Version = version;
            OrderedDateTime = orderedDateTime;
            Deleted = deleted;
        }

        public string Id { get; private set; }
        public string AccountId { get; private set; }
        public bool Active { get; set; }
        public int Version { get; set; }
        public DateTime OrderedDateTime { get; private set; }
        public bool Deleted { get; private set; }
    }
}
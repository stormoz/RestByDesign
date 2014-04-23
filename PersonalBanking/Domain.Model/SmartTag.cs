using System;
using PersonalBanking.Domain.Model.Core;

namespace PersonalBanking.Domain.Model
{
    public class SmartTag : IEntity
    {
        public int Version { get; set; }

        public string Id { get; set; }
        public DateTime OrderedDateTime { get; set; }

        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string AccountId { get; set; }
    }
}
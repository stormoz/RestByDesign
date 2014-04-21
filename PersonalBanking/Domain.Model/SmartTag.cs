using System;

namespace PersonalBanking.Domain.Model
{
    public class SmartTag
    {
        public SmartTag()
        {
            
        }

        public SmartTag(string id, DateTime ordered, bool active, bool deleted)
        {
            Id = id;
            OrderedDateTime = ordered;
            Active = active;
            Deleted = deleted;
        }

        public int Version { get; set; }

        public string Id { get; set; }
        public DateTime OrderedDateTime { get; set; }

        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
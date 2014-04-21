using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBanking.Domain.Model.Core
{
    public interface IEntity<IKey>
    {
        IKey Id { get; set; }
    }
}

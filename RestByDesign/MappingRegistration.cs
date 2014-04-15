using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;

namespace RestByDesign
{
    public class MappingRegistration
    {
        public static void RegisterMappings()
        {
            ModelMapper.AddMapping<Client, ClientModel>(client => new ClientModel { Id = client.Id, Name = client.Name });
        }
    }
}
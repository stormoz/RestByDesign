﻿using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;

namespace RestByDesign
{
    public class MappingRegistration
    {
        public static void RegisterMappings()
        {
            ModelMapper.AddMapping<Client, ClientModel>(client => new ClientModel { Id = client.Id, Name = client.Name });
            ModelMapper.AddMapping<Account, AccountModel>(account => new AccountModel { Id = account.Id, Name = account.Name });
            ModelMapper.AddMapping<SmartTag, SmartTagModel>(tag => new SmartTagModel { Id = tag.Id, Version = tag.Version, Active = tag.Active});
            ModelMapper.AddMapping<SmartTagModel, SmartTag>((tagModel, tag) =>
            {
                tag.Id = tagModel.Id;

                if (tagModel.Version.HasValue)
                    tag.Version = tagModel.Version.Value;

                if (tagModel.Active.HasValue)
                    tag.Active = tagModel.Active.Value;
            });
        }
    }
}
using System;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;

namespace RestByDesign
{
    public static class MappingRegistration
    {
        public static void RegisterMappings()
        {
            ModelMapper.AddMapping<Client, ClientModel>(client => new ClientModel { Id = client.Id, Name = client.Name });
            ModelMapper.AddMapping<Account, AccountModel>(account => new AccountModel { Id = account.Id, Name = account.Name, Balance = account.Balance });
            ModelMapper.AddMapping<SmartTag, SmartTagModel>(tag => new SmartTagModel { Id = tag.Id, Version = tag.Version, Active = tag.Active });
            ModelMapper.AddMapping<SmartTagModel, SmartTag>((tagModel, tag) =>
            {
                if (tagModel.Version.HasValue)
                    tag.Version = tagModel.Version.Value;

                if (tagModel.Active.HasValue)
                    tag.Active = tagModel.Active.Value;
            });
            ModelMapper.AddMapping<Transaction, TransactionModel>(tr => new TransactionModel { Id = tr.Id, Amount = tr.Amount, EffectDate = tr.EffectDate });
            ModelMapper.AddMapping<TransferModel, Transfer>(transfer => new Transfer(transfer.AccountIdFrom, transfer.AccountIdTo, transfer.Amount, transfer.Description));
        }
    }
}
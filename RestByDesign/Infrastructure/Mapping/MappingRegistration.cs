using PersonalBanking.Domain.Model;
using RestByDesign.Models;

namespace RestByDesign.Infrastructure.Mapping
{
    public static class MappingRegistration
    {
        private static bool AlreadyRun;
        private static readonly object padlock = new object();

        public static void RegisterMappings()
        {
            lock (padlock)
            {
                if (AlreadyRun)
                    return;

                ModelMapper.AddMapping<Client, ClientModel>(client => new ClientModel { Id = client.Id, Name = client.Name, Accounts = ModelMapper.Map<Account, AccountModel>(client.Accounts)});
                ModelMapper.AddMapping<Account, AccountModel>(account => new AccountModel { Id = account.Id, Name = account.Name, Balance = account.Balance });
                ModelMapper.AddMapping<SmartTag, SmartTagModel>(tag => new SmartTagModel { Id = tag.Id, Version = tag.Version, Active = tag.Active });
                ModelMapper.AddMapping<SmartTagModel, SmartTag>((tagModel, tag) =>
                {
                    if (tagModel.Version.HasValue)
                        tag.Version = tagModel.Version.Value;

                    if (tagModel.Active.HasValue)
                        tag.Active = tagModel.Active.Value;
                });
                ModelMapper.AddMapping<Transaction, TransactionModel>(tr => new TransactionModel { Id = tr.Id, Amount = tr.Amount, EffectDate = tr.EffectDate, Description = tr.Description });
                ModelMapper.AddMapping<TransferModel, Transfer>(transfer => new Transfer(transfer.AccountFrom, transfer.AccountTo, transfer.Amount, transfer.Description));

                AlreadyRun = true;
            }
        }
    }
}
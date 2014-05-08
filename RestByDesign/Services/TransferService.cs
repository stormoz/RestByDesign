using System;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Services
{
    public class TransferService : ITransferService
    {
        private readonly IUnitOfWork _uow;

        public TransferService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public TransferResult MakeTransfer(string clientId, Transfer transfer)
        {
            var accountFrom = _uow.AccountRepository.GetSingle(x => x.Id == transfer.AccountIdFrom && x.Client.Id == clientId);

            if(accountFrom == null)
                throw new InvalidOperationException("Could not find accountFrom for the client.");

            var accoutTo = _uow.AccountRepository.GetSingle(x => x.Id == transfer.AccountIdTo && x.Client.Id == clientId);
            
            if (accoutTo == null)
                throw new InvalidOperationException("Could not find accountTo for the client.");

            accountFrom.TakeFromAccount(transfer.Amount);
            _uow.AccountRepository.Update(accountFrom);

            accoutTo.PutToAccount(transfer.Amount);
            _uow.AccountRepository.Update(accoutTo);

            var transactionFrom = new Transaction(transfer.Amount * -1, SystemTime.Now, transfer.AccountIdFrom, string.Format("trf to {0}: {1}", accoutTo.Name, transfer.Description));
            _uow.TransactionRepository.Insert(transactionFrom);

            var transactionTo = new Transaction(transfer.Amount, SystemTime.Now, transfer.AccountIdTo, string.Format("trf from {0}: {1}", accountFrom.Name, transfer.Description));
            _uow.TransactionRepository.Insert(transactionTo);

            _uow.SaveChanges();

            return new TransferResult();
        }
    }
}
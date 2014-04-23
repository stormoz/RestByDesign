using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Models;

namespace RestByDesign.Services
{
    public class TransferService : ITransferService
    {
        private readonly IUnitOfWork _uow;

        public TransferService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public TransferResult MakeTransfer(Transfer transfer)
        {
            var accountFrom = _uow.AccountRepository.GetById(x => x.Id == transfer.AccountIdFrom);
            accountFrom.TakeFromAccount(transfer.Amount);
            _uow.AccountRepository.Update(accountFrom);

            var accoutTo = _uow.AccountRepository.GetById(x => x.Id == transfer.AccountIdTo);
            accoutTo.PutToAccount(transfer.Amount);
            _uow.AccountRepository.Update(accoutTo);

            var transactionFrom = new Transaction(transfer.Amount * -1, SystemTime.Now, transfer.AccountIdFrom);
            _uow.TransactionRepository.Insert(transactionFrom);

            var transactionTo = new Transaction(transfer.Amount, SystemTime.Now, transfer.AccountIdTo);
            _uow.TransactionRepository.Insert(transactionTo);

            _uow.SaveChanges();

            return new TransferResult();
        }
    }
}
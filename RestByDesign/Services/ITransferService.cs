using PersonalBanking.Domain.Model;
using RestByDesign.Models;

namespace RestByDesign.Services
{
    public interface ITransferService
    {
        TransferResult MakeTransfer(Transfer transfer);
    }
}
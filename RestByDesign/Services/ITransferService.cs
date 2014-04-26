using PersonalBanking.Domain.Model;
using RestByDesign.Models.Helpers;

namespace RestByDesign.Services
{
    public interface ITransferService
    {
        TransferResult MakeTransfer(string clientId, Transfer transfer);
    }
}
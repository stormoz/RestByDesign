using PersonalBanking.Domain.Model;
using RestByDesign.Services.Helpers;

namespace RestByDesign.Services
{
    public interface ITransferService
    {
        TransferResult MakeTransfer(Transfer transfer);
    }
}
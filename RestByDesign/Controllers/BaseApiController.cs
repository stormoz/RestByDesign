using System.Web.Http;
using RestByDesign.Infrastructure.DataAccess;

namespace RestByDesign.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private readonly IUnitOfWork _uow;
        protected IUnitOfWork UnitOfWork
        {
            get { return _uow; }
        }
        protected BaseApiController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }
    }
}
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Extensions;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models.Base;

namespace RestByDesign.Controllers.Base
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

        protected IHttpActionResult Fail(string message, HttpStatusCode httpCode = HttpStatusCode.BadRequest, object data = null)
        {
            return new FailResult(httpCode, message, data, this);
        }

        /// <summary>
        /// Update an entity from Delta
        /// </summary>
        /// <typeparam name="TEntity">Type of Entity</typeparam>
        /// <typeparam name="TKey">Type of entity primary key</typeparam>
        /// <typeparam name="TEntityModel">Type of entity model</typeparam>
        /// <param name="id">Entity Id</param>
        /// <param name="entityDelta">Entity model delta</param>
        /// <returns>IHttpActionResult</returns>
        protected IHttpActionResult PatchUpdate<TEntity, TKey, TEntityModel>(TKey id, object entityDelta)
            where TEntity : class, IEntity<TKey>
            where TEntityModel : BaseModel
        {
            var entityRepo = UnitOfWork.GetRepository<TEntity, TKey>();
            var item = entityRepo.Get(x => x.Id.Equals(id)).SingleOrDefault();

            if (item == null)
                return NotFound();

            var itemModel = ModelMapper.Map<TEntity, TEntityModel>(item);
            itemModel.PatchFrom(entityDelta);

            Validate(itemModel);

            if (!ModelState.IsValid)
                return Fail("Model state is invalid", data: new {items = ModelState.Errors()});

            ModelMapper.Map(itemModel, item);

            entityRepo.Update(item);
            return Ok(itemModel);
        }

        //Example with Delta (NB: Property binding for Delta is case-sensitive)
        /*protected IHttpActionResult PatchUpdate<TEntity, TKey, TEntityModel>(TKey id, Delta<TEntityModel> entityDelta)
            where TEntity : class, IEntity<TKey>
            where TEntityModel : class
        {
            var changedProperties = entityDelta.GetChangedPropertyNames();
            if (!changedProperties.Any())
                return Fail("Nothing to update", HttpStatusCode.NotModified);

            var entityRepo = UnitOfWork.GetRepository<TEntity, TKey>();
            var item = entityRepo.Get(x => x.Id.Equals(id)).SingleOrDefault();

            if (item == null)
                return NotFound();

            var itemModel = ModelMapper.Map<TEntity, TEntityModel>(item);
            entityDelta.Patch(itemModel);

            Validate(itemModel);

            if (!ModelState.IsValid)
                return Fail("Model state is invalid", data: new { items = ModelState.Errors() });

            ModelMapper.Map(itemModel, item);

            entityRepo.Update(item);
            return Ok(itemModel);
        }*/
    }
}
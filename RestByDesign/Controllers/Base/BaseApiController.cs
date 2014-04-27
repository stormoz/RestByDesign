using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using PersonalBanking.Domain.Model.Core;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;
using RestByDesign.Models.Base;
#if ODATA
using System.Web.Http.OData;
using System.Web.UI.WebControls;
#endif
using RestByDesign.Models.Helpers;

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

#if !ODATA
        /// <summary>
        /// Partial update an entity from object 
        /// </summary>
        /// <typeparam name="TEntity">Type of Entity</typeparam>
        /// <typeparam name="TEntityModel">Type of entity model</typeparam>
        /// <param name="filter">Filter expression to find a single item</param>
        /// <param name="entityDelta">Entity model delta</param>
        /// <returns>IHttpActionResult</returns>
        protected IHttpActionResult PatchUpdate<TEntity, TEntityModel>(Expression<Func<TEntity, bool>> filter, object entityDelta)
            where TEntity : class, IEntity
            where TEntityModel : BaseModel
        {
            var entityRepo = UnitOfWork.GetRepository<TEntity>();
            var item = entityRepo.Get(filter).SingleOrDefault();

            if (item == null)
                return NotFound();

            var itemModel = ModelMapper.Map<TEntity, TEntityModel>(item);
            itemModel.PatchFrom(entityDelta);

            Validate(itemModel);

            if (!ModelState.IsValid)
                return Fail("Model state is invalid", data: new { errors = ModelState.Errors()});

            ModelMapper.Map(itemModel, item);

            entityRepo.Update(item);
            UnitOfWork.SaveChanges();
            return Ok(itemModel);
        }

        public OkNegotiatedContentResult<CollectionWrapper<object>> OkCollection(IEnumerable<object> items, int totalCount)
        {
            return Ok(new CollectionWrapper<object>(items, totalCount));
        }

#else
        //Example with Delta (NB: Property binding for Delta is case-sensitive)
        protected IHttpActionResult PatchUpdate<TEntity, TEntityModel>(Expression<Func<TEntity, bool>> filter, Delta<TEntityModel> entityDelta)
            where TEntity : class, IEntity
            where TEntityModel : class
        {
            var changedProperties = entityDelta.GetChangedPropertyNames();
            if (!changedProperties.Any())
                return Fail("Nothing to update", HttpStatusCode.NotModified);

            var entityRepo = UnitOfWork.GetRepository<TEntity>();
            var item = entityRepo.Get(filter).SingleOrDefault();

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
        }
#endif
    }
}
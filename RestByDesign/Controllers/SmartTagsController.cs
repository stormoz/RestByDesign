using System.Linq;
using System.Web.Http;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.Core.Extensions;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Mapping;
using RestByDesign.Models;

#if ODATA
using System.Web.Http.OData;
#endif

namespace RestByDesign.Controllers
{
    public class SmartTagsController : BaseApiController
    {
        public SmartTagsController(IUnitOfWork uow) : base(uow)
        {  }

        [Route("api/accounts/{accountId}/smarttags")]
        public IHttpActionResult GetAllByAccount(string accountId, string fields = null)
        {
            var tags = UnitOfWork.SmartTagRepository.Get(tag => tag.AccountId.Equals(accountId)).ToList();

            var clientModel = ModelMapper.Map<SmartTag, SmartTagModel>(tags);

            return Ok(clientModel.SelectFields(fields));
        }

        [Route("api/accounts/{accountId}/smarttags/{smartTagNum}")]
        public IHttpActionResult Get(string accountId, int smartTagNum, string fields = null)
        {
            var smartTag = UnitOfWork.SmartTagRepository.Get(tag => tag.AccountId.Equals(accountId),
                tags => tags.OrderBy(tag => tag.Id)).Skip(smartTagNum - 1).Take(1).SingleOrDefault();

            if (smartTag == null)
                return NotFound();

            var smartTagModel = ModelMapper.Map<SmartTag, SmartTagModel>(smartTag);

            return Ok(smartTagModel.SelectFields(fields));
        }

        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Get(string smartTagId, string fields = null)
        {
            var smartTag = UnitOfWork.SmartTagRepository.Get(tag => tag.Id.Equals(smartTagId)).SingleOrDefault();

            if (smartTag == null)
                return NotFound();

            var smartTagModel = ModelMapper.Map<SmartTag, SmartTagModel>(smartTag);

            return Ok(smartTagModel.SelectFields(fields));
        }

        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Patch(string smartTagId, [FromBody]object smartTagModelDelta)
        {
            return PatchUpdate<SmartTag, SmartTagModel>(x => x.Id == smartTagId, smartTagModelDelta);
        }

#if ODATA
        //Example with Delta (NB: mind case-sensivity of properties)
        [HttpPatch]
        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Patch(string smartTagId, [FromBody]Delta<SmartTagModel> smartTagModelDelta)
        {
            return PatchUpdate<SmartTag, string, SmartTagModel>(x => x.Id == smartTagId, smartTagModelDelta);
        }
#endif

        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Delete(string smartTagId)
        {
            UnitOfWork.SmartTagRepository.Delete(x => x.Id == smartTagId);
            UnitOfWork.SaveChanges();
            return Ok();
        }
    }
}

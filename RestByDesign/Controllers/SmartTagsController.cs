using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using PersonalBanking.Domain.Model;
using RestByDesign.Controllers.Base;
using RestByDesign.Infrastructure.DataAccess;
using RestByDesign.Infrastructure.Extensions;
using RestByDesign.Infrastructure.Mappers;
using RestByDesign.Models;

namespace RestByDesign.Controllers
{
    public class SmartTagsController : BaseApiController
    {
        public SmartTagsController(IUnitOfWork uow) : base(uow)
        {  }

        //GET /accounts/123/smarttags
        [Route("api/accounts/{accountId}/smarttags")]
        public IHttpActionResult GetAllByAccount(string accountId, string fields = null)
        {
            var tags = UnitOfWork.SmartTagRepository.Get(tag => tag.AccountId.Equals(accountId)).ToList();

            if (!tags.Any())
                return NotFound();

            var clientModel = ModelMapper.Map<SmartTag, SmartTagModel>(tags);

            return Ok(clientModel.SelectFields(fields));
        }

        //GET /accounts/123/smarttags/2
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

        //GET /smarttags/2
        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Get(string smartTagId, string fields = null)
        {
            var smartTag = UnitOfWork.SmartTagRepository.Get(tag => tag.Id.Equals(smartTagId)).SingleOrDefault();

            if (smartTag == null)
                return NotFound();

            var smartTagModel = ModelMapper.Map<SmartTag, SmartTagModel>(smartTag);

            return Ok(smartTagModel.SelectFields(fields));
        }

        //PATCH /accounts/123/smarttags/2
        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Patch(string smartTagId, [FromBody]object smartTagModelDelta)
        {
            return PatchUpdate<SmartTag, string, SmartTagModel>(smartTagId, smartTagModelDelta);
        }

        //Example with Delta (NB: mind case-sensivity)
        /*[HttpPatch]
        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Patch(string smartTagId, [FromBody]Delta<SmartTagModel> smartTagModelDelta)
        {
            return PatchUpdate<SmartTag, string, SmartTagModel>(smartTagId, smartTagModelDelta);
        }*/

        //DELETE /accounts/123/smarttags/2
        [Route("api/smarttags/{smartTagId}")]
        public IHttpActionResult Delete(string smartTagId)
        {
            UnitOfWork.SmartTagRepository.Delete(smartTagId);
            return Ok();
        }
    }
}

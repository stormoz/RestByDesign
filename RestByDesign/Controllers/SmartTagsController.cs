using System;
using System.Web.Http;

namespace RestByDesign.Controllers
{
    public class SmartTagsController : ApiController
    {
        //GET /accounts/123/smarttags
        public IHttpActionResult GetAll(string accountId)
        {
            throw new NotImplementedException();
        }

        //GET /accounts/123/smarttags/2
        public IHttpActionResult Get(string accountId, string smartTagId)
        {
            throw new NotImplementedException();
        }

        //PATCH /accounts/123/smarttags/2
        public IHttpActionResult Patch(string accountId, string smartTagId/*, Delta<SmartTag> smartTagDelta*/)
        {
            throw new NotImplementedException();
        }

        //DELETE /accounts/123/smarttags/2
        public IHttpActionResult Delete(string accountId, string smartTagId)
        {
            throw new NotImplementedException();
        }
    }
}

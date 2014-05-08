using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NUnit.Framework;
using PersonalBanking.Domain.Model;
using RestByDesign.Infrastructure.JSend;
using RestByDesign.Models;
using RestByDesign.Tests.IntegrationTests.Base;
using RestByDesign.Tests.IntegrationTests.Helpers;
using Shouldly;

namespace RestByDesign.Tests.IntegrationTests
{
    public class SmartTagsControllerTests : OwinIntegrationTestBase
    {
        [Test]
        public void SmartTags_GetAll_GetByAccountId()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags", id);
            var jSend = Server.GetJsendForCollection<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountId_WithFieldsFilter()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags?fields=Active", id);
            var jSend = Server.GetJsendForCollection<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Items.ShouldAllBe(x => x.Id == null);
            jSend.Data.Items.ShouldAllBe(x => x.Version == null);
            jSend.Data.Items.ShouldAllBe(x => x.Active != null);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountIdAndNum()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags/{1}", id, 1);
            var jSend = Server.GetJsendObject<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Active.ShouldNotBe(null);
        }

        [Test]
        public void SmartTags_GetAll_GetByAccountIdAndNum_WithFields()
        {
            var id = "111";
            var url = string.Format("/api/accounts/{0}/smarttags/{1}?fields=Active", id, 1);
            var jSend = Server.GetJsendObject<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldBe(null);
            jSend.Data.Active.ShouldNotBe(null);
            jSend.Data.Version.ShouldBe(null);
        }

        [Test]
        public void SmartTags_GetById()
        {
            var id = "1";
            var url = string.Format("/api/smarttags/{0}", id);
            var jSend = Server.GetJsendObject<SmartTagModel>(url);

            jSend.Status.ShouldBe(JSendStatus.Success);
            jSend.Data.Id.ShouldNotBe(null);
            jSend.Data.Active.ShouldNotBe(null);
        }

        [Test]
        public void SmartTags_GetById_NotFound()
        {
            var id = "3";
            var url = string.Format("/api/smarttags/{0}", id);
            var response = Server.GetJsendObject<object>(url);

            response.Status.ShouldBe(JSendStatus.Error);
            response.Code.ShouldBe((int) HttpStatusCode.NotFound);
        }

        [Test]
        public void SmartTags_Patch()
        {
            var id = "1";
            var url = string.Format("/api/smarttags/{0}", id);
            var jSend = Server.GetJsendObject<SmartTagModel>(url);
            var currentValue = jSend.Data.Active;

            var updatedjSend = Server.GetJsendObject<SmartTagModel>(url, HttpVerbs.Patch, new {active = !currentValue});
            updatedjSend.Status.ShouldBe(JSendStatus.Success);
            updatedjSend.Data.Id.ShouldNotBe(null);
            updatedjSend.Data.Active.ShouldBe(!currentValue);
        }

        [Test]
        public void SmartTags_Patch_WithInvalidModel()
        {
            var id = "1";
            var url = string.Format("/api/smarttags/{0}", id);

            var updatedjSend = Server.GetJsendObject<object>(url, HttpVerbs.Patch, new {active = (bool?) null});
            updatedjSend.Status.ShouldBe(JSendStatus.Fail);

            var jSend = Server.GetJsendObject<SmartTagModel>(url);
            jSend.Data.Active.ShouldNotBe(null);
        }

        [Test]
        public void SmartTags_Delete()
        {
            var smrtTagId = "-1";
            var smartTagRepo = Uow.SmartTagRepository;
            smartTagRepo.Insert(new SmartTag(smrtTagId, "111", true, false, 1, new DateTime(2014, 5, 8)));
            Uow.SaveChanges();

            var tagsTotal = smartTagRepo.Count();

            var url = string.Format("/api/smarttags/{0}", smrtTagId);

            var jSend = Server.GetJsendObject<object>(url, HttpVerbs.Delete);
            jSend.Status.ShouldBe(JSendStatus.Success);

            smartTagRepo.Count().ShouldBe(tagsTotal - 1);
        }
    }
}
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using ReallyTinyCms.Core;
using ReallyTinyCms.Mvc;
using ReallyTinyCms.Mvc.Controllers;
using ReallyTinyCms.Tests.Fakes;

namespace ReallyTinyCms.Tests.Mvc
{
    [TestFixture]
    public class HtmlHelperExtensionsForReallyTinyCmsTests
    {
        private HtmlHelper _helper;
        private string _itemName;
        private ContentServiceFake _contentService;
        
        [SetUp]
        public void SetUp()
        {
            _contentService = new ContentServiceFake
                                  {
                                      ContentRegistration = new ContentSourceRegistration(() => new CmsContentRepositoryFake(), null)
                                  };
            HtmlHelperExtensionsForReallyTinyCms.ContentService = _contentService;
            _helper = CreateHtmlHelper();
            _itemName = "TestContentItem";
        }

        [Test]
        public void ContentFor_ContentServiceIsNull_ThrowsInvalidOperationException()
        {
            HtmlHelperExtensionsForReallyTinyCms.ContentService = null;
            var ex = Assert.Throws<InvalidOperationException>(() => _helper.ContentFor(_itemName));
            Assert.That(ex.Message, Is.StringMatching("The ContentService is null, cannot continue. Registration failure."));
        }

        [Test]
        public void ContentFor_ItemNameSupplied_CallsContentService()
        {
            _helper.ContentFor(_itemName);

            Assert.That(_contentService.ContentForWasCalled, Is.True);
            Assert.That(_contentService.LastContentForValue, Is.EqualTo(_itemName));
        }

        [Test]
        public void ContentFor_ItemNameSuppliedWithDefault_CallsContentService()
        {
            _helper.ContentFor(_itemName, () => "default");

            Assert.That(_contentService.ContentForWasCalled, Is.True);
            Assert.That(_contentService.LastContentForValue, Is.EqualTo(_itemName));
        }

        [Test]
        public void ContentFor_SuppliedWithDefaultValueAndNoTemplate_TemplateReturnsDefaultValue()
        {
            const string defaultValue = "defaultValue";
            Assert.That(_helper.ContentFor(_itemName, () => defaultValue).ToHtmlString(), Is.EqualTo(defaultValue));
        }

        [Test]
        public void ContentFor_ModelSupplied_ModelValueDoesNotChange()
        {
            const string suppliedModel = "SuppliedModel";
            _helper.ViewData.Model = suppliedModel;

            _helper.ContentFor(_itemName);
            
            Assert.That(_helper.ViewData.Model, Is.EqualTo(suppliedModel));
        }

       private static HtmlHelper CreateHtmlHelper()
       {
           ViewEngines.Engines.Clear();
           ViewEngines.Engines.Add(new ViewEngineFake());

           var routeData = new RouteData(); routeData.Values.Add("controller", "fake");
           
           using (var writer = new StringWriter())
           {
               return new HtmlHelper(new ViewContext(new ControllerContext(new HttpContextFake(), routeData, new ControllerFake()), new ViewFake(), new ViewDataDictionary(), new TempDataDictionary(), writer), new ViewPage());
           }
       }
    }
}

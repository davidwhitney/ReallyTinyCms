using System;
using NUnit.Framework;
using ReallyTinyCms.Core;
using ReallyTinyCms.Tests.Fakes;

namespace ReallyTinyCms.Tests.Core
{
    [TestFixture]
    public class ReallyTinyCmsUiSugarTests
    {
        private ContentServiceFake _contentService;
        private ReallyTinyCmsUiSugar _sugar;
        
        private string _itemName;
        
        [SetUp]
        public void SetUp()
        {
            _itemName = "itemName";
            _contentService = new ContentServiceFake();
            _sugar = new ReallyTinyCmsUiSugar(_contentService);
        }

        [Test]
        public void Ctor_NullIContentService_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ReallyTinyCmsUiSugar(null));

            Assert.That(ex.ParamName, Is.StringMatching("contentService"));
            Assert.That(ex.Message, Is.StringMatching("UI Sugar is useless without a contentService"));
        }

        [Test]
        public void ContentFor_ItemNameSupplied_CallsContentService()
        {
            _sugar.ContentFor(_itemName);

            Assert.That(_contentService.ContentForWasCalled, Is.True);
            Assert.That(_contentService.LastContentForValue, Is.EqualTo(_itemName));
        }

        [Test]
        public void ContentFor_ItemNameSuppliedWithDefault_CallsContentService()
        {
            _sugar.ContentFor(_itemName, ()=>"default");

            Assert.That(_contentService.ContentForWasCalledWithDefault, Is.True);
            Assert.That(_contentService.LastContentForValue, Is.EqualTo(_itemName));
        }
    }
}

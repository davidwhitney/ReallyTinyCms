using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace ReallyTinyCms.Tests.Fakes
{
    public class ViewFake : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
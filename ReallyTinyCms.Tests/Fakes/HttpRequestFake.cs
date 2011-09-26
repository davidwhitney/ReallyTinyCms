using System.Web;
using System.Web.Routing;

namespace ReallyTinyCms.Tests.Fakes
{
    public class HttpRequestFake : HttpRequestBase
    {
        public override RequestContext RequestContext
        {
            get
            {
                return new RequestContext();
            }
        }
    }
}
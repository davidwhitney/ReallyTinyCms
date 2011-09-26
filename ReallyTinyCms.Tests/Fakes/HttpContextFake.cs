using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace ReallyTinyCms.Tests.Fakes
{
    public class HttpContextFake : HttpContextBase 
    {
        private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
        public override IDictionary Items { get { return _items; } }

        public override HttpRequestBase Request
        {
            get
            {               
                return new HttpRequestFake();
            }
        }
    }
}
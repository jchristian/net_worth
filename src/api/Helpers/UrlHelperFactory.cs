using System.Net.Http;
using System.Web.Http.Routing;

namespace api.Helpers
{
    public static class UrlHelperFactory
    {
        public static UrlHelper Create(HttpRequestMessage request)
        {
            return new UrlHelper(request);
        }
    }
}
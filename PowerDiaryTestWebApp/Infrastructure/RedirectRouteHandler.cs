using System.Web;
using System.Web.Routing;

namespace PowerDiaryTestWebApp.Infrastructure
{
    public class RedirectRouteHandler : IRouteHandler
    {
        private readonly string _targetUri;
        public RedirectRouteHandler(string targetUri)
        {
            _targetUri = targetUri;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new RedirectHttpHandler(_targetUri);
        }
    }
}
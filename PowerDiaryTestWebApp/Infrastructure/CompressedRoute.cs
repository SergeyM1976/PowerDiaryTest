using System.Web;
using System.Web.Routing;
using PowerDiaryTestWebApp.Domain.Abstract;

namespace PowerDiaryTestWebApp.Infrastructure
{
    public class CompressedRoute : RouteBase
    {
        private readonly ICompressedRoutesRepository _repository;

        public CompressedRoute(ICompressedRoutesRepository repository)
        {
            _repository = repository;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            string requestedUrl = httpContext.Request.FilePath;
            var croute = _repository.GetRouteByCompressedFilePath(requestedUrl);
            

            if (croute != null)
            {
                result = new RouteData(this, new RedirectRouteHandler(croute.Uri));
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
            /*
            VirtualPathData result = null;
            string requestedUrl = requestContext.HttpContext.Request.FilePath;
            var croute = _repository.GetRouteByCompressedFilePath(requestedUrl);


            if (croute != null)
            {
                result = new RouteData(this, new RedirectRouteHandler(croute.Uri));
            }
            return result;
            */
        }
    }
}
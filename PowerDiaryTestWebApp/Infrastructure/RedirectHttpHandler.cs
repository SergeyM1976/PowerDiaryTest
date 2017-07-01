using System.Web;

namespace PowerDiaryTestWebApp.Infrastructure
{
    public class RedirectHttpHandler : IHttpHandler
    {
        private readonly string _targetUri;

        public RedirectHttpHandler(string targetUri)
        {
            _targetUri = targetUri;
        }

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect(_targetUri);
        }
    }
}
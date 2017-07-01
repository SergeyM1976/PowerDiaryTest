using PowerDiaryTestWebApp.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using PowerDiaryTestWebApp.Domain.Abstract;
using PowerDiaryTestWebApp.Domain.Data;

namespace PowerDiaryTestWebApp.Controllers
{
    public class CompressUriController : Controller
    {
        private readonly ICompressedRoutesRepository _repository;
        public CompressUriController(ICompressedRoutesRepository repository)
        {
            _repository = repository;
        }


        public async Task<ActionResult> Index()
        {
            return View(await GetRoutesAsync());
        }

        private async Task<IEnumerable<RouteModel>> GetRoutesAsync()
        {
            var routesInBase = await _repository.GetAllCompressedRoutesAsync();


            return routesInBase.Select(GetModelFromRecord);
        }

        RouteModel GetModelFromRecord(CompressedRoute r)
        {
            var uri = HttpContext.Request.Url;
            Debug.Assert(uri != null, "uri != null");


            return new RouteModel
            {
                Id = r.Id,
                Uri = r.Uri,
                CompressedUri = $"{uri.Scheme}://{uri.Authority}/{r.CompressedUri}",
                CompressedUriUserValue = r.CompressedUri
            };
        }


        [OutputCache(Location = OutputCacheLocation.None)]
        public async Task<ActionResult> UriList()
        {
            return Json(await GetRoutesAsync(), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public async Task<ActionResult> AddUri(string uri)
        {
            var added = await _repository.CreateCompressedRouteForUri(uri);
            var res = new AddRouteResultModel {Route = GetModelFromRecord(added.Item1), AlreadyExists = !added.Item2};
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}
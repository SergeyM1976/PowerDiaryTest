using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PowerDiaryTestWebApp.Domain.Data;

namespace PowerDiaryTestWebApp.Domain.Abstract
{
    public interface ICompressedRoutesRepository
    {
        Task<IEnumerable<CompressedRoute>> GetAllCompressedRoutesAsync();
        Task<Tuple<CompressedRoute, bool>> CreateCompressedRouteForUri(string uri);

        CompressedRoute GetRouteByCompressedFilePath(string compressedUri);
    }
}

using PowerDiaryTestWebApp.Domain.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PowerDiaryTestWebApp.Domain.Data;

namespace PowerDiaryTestWebApp.Domain.Concrete
{
    public class CompressedRoutesRepository : ICompressedRoutesRepository
    {
        private readonly PowerDiaryDbContext _dbcontext;

        public CompressedRoutesRepository(PowerDiaryDbContext dbc)
        {
            _dbcontext = dbc;
        }

        public async Task<IEnumerable<CompressedRoute>> GetAllCompressedRoutesAsync()
        {
            return await _dbcontext.CompressedRoutes.OrderBy(r => r.Uri).ToArrayAsync();
        }

        public async Task<Tuple<CompressedRoute, bool>> CreateCompressedRouteForUri(string uri)
        {
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                var existing = await _dbcontext.CompressedRoutes.FirstOrDefaultAsync(r => r.Uri == uri);
                if (existing != null)
                {
                    return new Tuple<CompressedRoute, bool>(existing, false);
                }


                var cr = new CompressedRoute { Uri = uri };

                _dbcontext.CompressedRoutes.Add(cr);
                await _dbcontext.SaveChangesAsync();
                
                cr.CompressedUri = GetCompressed(cr.Id);
                await _dbcontext.SaveChangesAsync();

                transaction.Commit();
                return new Tuple<CompressedRoute, bool>(cr, true);
            }            
        }


        string GetCompressed(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return Convert.ToBase64String(bytes);            
        }

        public CompressedRoute GetRouteByCompressedFilePath(string compressedFilePath)
        {            
            if (compressedFilePath.StartsWith("/"))
            {
                compressedFilePath = compressedFilePath.Remove(0, 1);
            }
            try
            {
                return 
                    _dbcontext.CompressedRoutes.FirstOrDefault(r => r.CompressedUri == compressedFilePath);
            }
            catch (Exception)   // to avoid break of execution at Model creating time
            {
                return null;
            }
        }
    }
}
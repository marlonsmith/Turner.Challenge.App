using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turner.Challenge.App.Models;
using MongoDB.Driver.Linq;

namespace Turner.Challenge.App.Repository
{
    public class MovieTitleRepository : IMovieTitleRepository
    {
        private readonly IMongoCollection<MovieTitle> _collection;

        public MovieTitleRepository(IMongoCollection<MovieTitle> collection)
        {
            _collection = collection;
        }
        public async Task<List<MovieTitle>> GetByNameAsync(string name)
        {
            var documents = await _collection
                .AsQueryable()
                .Where(movie => movie.NameSortable.ToLower().Contains(name))
                .ToListAsync();

            return documents;
        }
    }
}

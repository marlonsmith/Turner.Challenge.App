using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turner.Challenge.App.Models;
using MongoDB.Driver.Linq;

namespace Turner.Challenge.App.Repository
{
    public class MovieRepository : IMoviesRepository
    {
        private readonly IMongoCollection<Movie> _collection;

        public MovieRepository(IMongoCollection<Movie> collection)
        {
            _collection = collection;
        }
        public async Task<Movie[]> GetByTerm(string searchTerm)
        {
            var documents = await _collection
                .AsQueryable()
                .Where(movie => movie.NameSortable.ToLower().Contains(searchTerm))
                .ToListAsync();

            return documents.ToArray();
        }
    }
}

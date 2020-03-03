using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turner.Challenge.App.Models;

namespace Turner.Challenge.App.Repository
{
    public interface IMovieTitleRepository
    {
        Task<List<MovieTitle>> GetByNameAsync(string name);
    }
}

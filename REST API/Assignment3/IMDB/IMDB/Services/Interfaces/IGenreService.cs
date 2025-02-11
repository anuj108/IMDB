﻿using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;

namespace IMDB.Services.Interfaces
{
    public interface IGenreService
    {
        Task<int> Create(GenreRequest genreRequest);
        Task<IEnumerable<GenreResponse>> Get();
        Task<GenreResponse> Get(int id);

        Task Update(int id,GenreRequest genreRequest);
        Task Delete(int id);
    }
}

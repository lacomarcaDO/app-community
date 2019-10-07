using System;
using System.Threading.Tasks;
using Community.DataObjects;
using System.Collections.Generic;

namespace Community.DataStore.Abstractions
{
    public interface IFavoriteStore : IBaseStore<Favorite>
    {
        Task<bool> IsFavorite(string sessionId);
        Task DropFavorites();
    }
}


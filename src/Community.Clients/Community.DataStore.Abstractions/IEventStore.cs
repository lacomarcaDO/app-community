using System;
using Community.DataObjects;

namespace Community.DataStore.Abstractions
{
    public interface IEventStore : IBaseStore<FeaturedEvent>
    {
    }
}


using System;
using Community.DataObjects;
using System.Threading.Tasks;

namespace Community.DataStore.Abstractions
{
    public interface INotificationStore : IBaseStore<Notification>
    {
        Task<Notification> GetLatestNotification();
    }
}


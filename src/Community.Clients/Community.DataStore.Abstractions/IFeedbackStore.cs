using System;
using System.Threading.Tasks;
using Community.DataObjects;

namespace Community.DataStore.Abstractions
{
    public interface IFeedbackStore : IBaseStore<Feedback>
    {
        Task<bool> LeftFeedback(Session session);
        Task DropFeedback();
    }
}


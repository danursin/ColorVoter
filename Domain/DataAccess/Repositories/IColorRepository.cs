using System.Collections.Generic;

namespace Domain.DataAccess.Repositories
{
    public interface IColorRepository
    {
        void RegisterVote(string color);
        List<KeyValuePair<string, int>> GetVotes();
    }
}
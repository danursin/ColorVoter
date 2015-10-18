using System.Collections.Generic;

namespace Domain.Services
{
    public interface IColorService
    {
        List<KeyValuePair<string, int>>  GetVotes();
        void RegisterVote(string color);
    }
}
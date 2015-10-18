using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Domain.DataAccess.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly ConcurrentDictionary<string, int> _votes; 
        public ColorRepository()
        {
            _votes = new ConcurrentDictionary<string, int>();
        }

        public void RegisterVote(string hexValue)
        {
            if (_votes.ContainsKey(hexValue))
            {
                _votes[hexValue] += 1;
            }
            else
            {
                _votes.TryAdd(hexValue, 1);
            }
        }

        public List<KeyValuePair<string, int>> GetVotes()
        {
            return _votes.ToList();
        }
    }
}

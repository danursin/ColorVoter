using System.Collections.Generic;
using Domain.DataAccess.Repositories;

namespace Domain.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public List<KeyValuePair<string, int>> GetVotes()
        {
            return _colorRepository.GetVotes();
        }

        public void RegisterVote(string hexValue)
        {
            _colorRepository.RegisterVote(hexValue.ToLower());
        }
    }
}

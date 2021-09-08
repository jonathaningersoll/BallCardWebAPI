using BBCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Data
{
    public interface ITeamsRepo
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(int id);
        Task<bool> PutTeam(int id, Team team);
        Task<bool> PostTeam(Team team);
        Task<bool> DeleteTeam(Team team);
        bool TeamExists(int id);
    }
}

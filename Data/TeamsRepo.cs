using BBCards.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Data
{
    public class TeamsRepo : ITeamsRepo
    {
        private AppDbContext _context;

        public TeamsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
            if(await _context.SaveChangesAsync() == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Team> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            return team;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<bool> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return true;
        }

        public async Task<bool> PutTeam(int id, Team team)
        {
            _context.Entry(team).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool TeamExists(int id)
        {
            return  _context.Teams.Any(e => e.TeamId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BBCards.Models;
using BBCards.Data;

namespace BBCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsRepo _teamsRepo;

        public TeamsController(AppDbContext context, ITeamsRepo teamsRepo)
        {
            _teamsRepo = teamsRepo;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult <IEnumerable<Team>>> GetTeams()
        {
            var teams = await _teamsRepo.GetTeams();
            return Ok(teams);
        }



        // GET: api/Teams/5
        // GET Method
        [HttpGet("{id}")]
        public async Task<ActionResult <Team>> GetTeam(int id)
        {
            if (!_teamsRepo.TeamExists(id))
            {
                return NotFound();
            }

            var team = await _teamsRepo.GetTeam(id);
            return team;
        }



        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.TeamId)
            {
                return BadRequest();
            }

            if(!await _teamsRepo.PutTeam(id, team)){
                return NotFound();
            }
            return NoContent();
        }




        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult <Team>> PostTeam(Team team)
        {
            if(!await _teamsRepo.PostTeam(team))
            {
                return BadRequest();
            }
            return CreatedAtAction("GetTeam", new { id = team.TeamId }, team);
        }





        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var team = await _teamsRepo.GetTeam(id);
            if (team == null)
            {
                return NotFound();
            }

            if(!await _teamsRepo.DeleteTeam(team))
            {
                return BadRequest();
            }
            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _teamsRepo.TeamExists(id);
        }
    }
}

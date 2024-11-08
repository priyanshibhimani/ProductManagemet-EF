using Microsoft.EntityFrameworkCore;
using ProductManagemet.Context;
using ProductManagemet.Models;
using ProductManagemet.ServiceContracts;

namespace ProductManagemet.Services
{
    public class PartyService: IPartyService
    {

        private readonly AppDbContext _context;

        public PartyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPartyAsync(Party party)
        {
            var p = new Party()
            {
                PartyName = party.PartyName
            };
            _context.Parties.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePartyAsync(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party != null)
            {
                _context.Parties.Remove(party);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Party>> GetPartiesAsync(string sortOrder = null, string searchTerm = null)
        {
            var partiesQuery = _context.Parties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                partiesQuery = partiesQuery.Where(p => p.PartyName.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(sortOrder))
            {
                switch (sortOrder)
                {
                    case "name_desc":
                        partiesQuery = partiesQuery.OrderByDescending(p => p.PartyName);
                        break;
                    case "name_asc":
                        partiesQuery = partiesQuery.OrderBy(p => p.PartyName);
                        break;
                    default:
                        break;
                }

            }

            return await partiesQuery.ToListAsync();
        }

        public async Task<Party> GetPartiesByIdAsync(int id)
        {
            return await _context.Parties.FindAsync(id);
            
        }

        public async Task<bool> SearchPartyAsync(int id)
        {
            return  _context.Parties.Any(e => e.PartyId == id);
        }

        public async Task UpdatePartyAsync(Party party)
        {
            var p = new Party()
            {
                PartyId=party.PartyId,
                PartyName=party.PartyName
            };
            _context.Parties.Update(p);
            await _context.SaveChangesAsync();
        }
    }
}

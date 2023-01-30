using Microsoft.EntityFrameworkCore;
using VotingData;
using VotingWeb.Models;

namespace VotingWeb.Repositories
{
    public class VotingRepository
    {
        private readonly VotingContext _context;

        public VotingRepository(VotingContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
        }

        public async Task<Candidates> GetAllCandidates()
        {
            var candidateIds = await _context.Voters.Where(v => v.CandidateId != null).Select(i => i.CandidateId ?? 0).ToListAsync();
            var candidates = await _context.Candidates.Select(c => new Candidate
            {
              Id = c.CandidateId,
              Name = c.Name,
            }).ToListAsync();

            foreach(var c in candidates ?? Enumerable.Empty<Candidate>())
            {
                c.VotesNumber = candidateIds.Count(i => i.Equals(c.Id));
            }
            return new Candidates() { CandidateLst = candidates };
        }

        public async Task<Voters> GetAllVoters()
        {
            var voters = await _context.Voters.Select(v => new Voter
            {
                Id = v.VoterId,
                Name = v.Name,
                HasVoted = v.CandidateId != null
            }).ToListAsync();

            return new Voters() { VoterLst = voters };
        }

        public async Task<Voter> AddVoter(string voterName)
        {
            VotingDomain.Voter v = new VotingDomain.Voter
            {
                Name = voterName,
            };

            _context.Voters.Add(v);
            await _context.SaveChangesAsync();

            return new Voter() { Id = v.VoterId, Name = voterName };
        }

        public async Task<Candidate> AddCandidate(string candidateName)
        {
            VotingDomain.Candidate c = new VotingDomain.Candidate
            {
                Name = candidateName,
            };

            _context.Candidates.Add(c);
            await _context.SaveChangesAsync();

            return new Candidate() { Id = c.CandidateId, Name = candidateName };
        }

        public async Task<Voter?> Vote(int voterId, int candidateId)
        {
            var voter = await _context.Voters.FindAsync(voterId);
            var candidate = await _context.Candidates.FindAsync(candidateId);

            if (voter == null || candidate == null)
                return null;

            voter.CandidateId = candidateId;
            await _context.SaveChangesAsync();

            return new Voter() { Id = voter.VoterId, Name = voter.Name, HasVoted = true };
        }
    }
}

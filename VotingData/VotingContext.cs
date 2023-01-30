using Microsoft.EntityFrameworkCore;
using VotingDomain;

namespace VotingData
{
    public class VotingContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Voter> Voters { get; set; }

        public VotingContext(DbContextOptions<VotingContext> options)
            : base(options)
        {
        }

    }
}

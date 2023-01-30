namespace VotingWeb.Models
{
    public class CandidatesVotersViewModel
    {
        public CandidatesVotersViewModel()
        {
            Candidates = new Candidates();
            Voters = new Voters();
        }
        public Candidates Candidates { get; set; }
        public Voters Voters { get; set; }
    }
}

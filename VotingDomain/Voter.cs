namespace VotingDomain
{
    public class Voter
    {
        public int VoterId { get; set; }
        public string Name { get; set; }
        public int? CandidateId {  get; set; }
        public Candidate Candidate { get; set; }    
    }
}

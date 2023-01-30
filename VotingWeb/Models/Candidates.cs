using Microsoft.AspNetCore.Mvc.Rendering;

namespace VotingWeb.Models
{
    public class Candidates
    {
        public Candidates()
        {
            CandidateLst = new List<Candidate>();
        }
        public List<Candidate> CandidateLst { get; set; }

        public int Id { get; set; }
        public IEnumerable<SelectListItem> CandidateItems
        {
            get
            {
                var vs = CandidateLst.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name });
                return vs;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace VotingWeb.Models
{
    public class Voters
    {
        public Voters()
        {
            VoterLst = new List<Voter>();
        }
        public List<Voter> VoterLst { get; set; }
        public int Id { get; set; }
        public IEnumerable<SelectListItem> VoterItems
        {
            get
            {
                var vs = VoterLst
                    .Where(i => !i.HasVoted)
                    .Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Name });
                return vs;
            }
        }
    }
}

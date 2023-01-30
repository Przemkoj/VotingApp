using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using VotingWeb.Models;
using VotingWeb.Repositories;

namespace VotingWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly VotingRepository _votingRepository;

        public HomeController(VotingRepository votingRepository)
        {
            _votingRepository = votingRepository ?? throw new ArgumentNullException(nameof(VotingRepository));
        }

        public async Task<IActionResult> Index()
        {
            CandidatesVotersViewModel candidatesVotersViewModel = new()
            {
                Voters = await _votingRepository.GetAllVoters(),
                Candidates = await _votingRepository.GetAllCandidates(),
            };

            return View(candidatesVotersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(IFormCollection form)
        {
            string? name = form["CandidateName"];

            if (!string.IsNullOrWhiteSpace(name))
            {
                _ = await _votingRepository.AddCandidate(name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddVoter(IFormCollection form)
        {
            string? name = form["VoterName"];

            if (!string.IsNullOrWhiteSpace(name))
            {
                _ = await _votingRepository.AddVoter(name);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int voterId, int candidateId)
        {
            await _votingRepository.Vote(voterId, candidateId);

            var redirectUrl = Url.Action("Index");
            return Json(new { Url = redirectUrl });
        }
    }
}
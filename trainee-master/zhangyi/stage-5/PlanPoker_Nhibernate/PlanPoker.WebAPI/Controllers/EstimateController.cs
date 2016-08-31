using System.Linq;
using System.Web.Http;
using PlanPoker.Common;
using PlanPoker.WebAPI.Models;
using PlanPoker.ILogic;

namespace PlanPoker.WebAPI.Controllers
{
    public class EstimateController : ApiController
    {
        private readonly ICacheManager _cacheManager;

        private readonly IUserLogic _userLogic;
        public EstimateController(ICacheManager cacheManager, IUserLogic userLogic)
        {
            _cacheManager = cacheManager;
            _userLogic = userLogic;
        }

        [Route("api/estimate")]
        [HttpPost]
        public void Insert(Estimate estimate)
        {
            if (_cacheManager.KeyExist(estimate.ProjectId))
            {
                UpdateEsitmate(estimate);
                
            }
            else
            {
                InsertEstimate(estimate);
            }
        }



        [Route("api/estimateDelete")]
        [HttpDelete]
        public void Delete(string projectId)
        {
            if (_cacheManager.KeyExist(projectId))
            {
                _cacheManager.Remove(projectId);
            }
        }


        [Route("api/estimate")]
        [HttpGet]
        public EstimatesViewModel Get(string projectId)
        {
            if (!_cacheManager.KeyExist(projectId)) return null;
            var estimatesViewModel = GetEstimateViewModel(projectId);

            return estimatesViewModel;
        }

        [Route("api/estimateShowCard")]
        [HttpGet]
        public void ShowCard(string projectId)
        {
            if (!_cacheManager.KeyExist(projectId)) return;

            var estimates = _cacheManager.Get<Estimates>(projectId);
            if (estimates != null) estimates.IsShow = true;
        }

        [Route("api/estimateIsCleared")]
        [HttpGet]
        public bool IsCleared(string projectId)
        {
            return !_cacheManager.KeyExist(projectId);
        }

        private void UpdateEsitmate(Estimate estimate)
        {
            var estimates = _cacheManager.Get<Estimates>(estimate.ProjectId);
            var existedUser = estimates.EstimateList.FirstOrDefault(u => u.UserId == estimate.UserId);
            if (existedUser == null)
            {
                estimates.EstimateList.Add(estimate);
            }
            else
            {
                existedUser.SelectedPoker = estimate.SelectedPoker;
            }
        }

        private void InsertEstimate(Estimate estimate)
        {
            var estimates = new Estimates();
            estimates.EstimateList.Add(estimate);
            _cacheManager.Add(estimate.ProjectId, estimates);
        }

        private EstimatesViewModel GetEstimateViewModel(string projectId)
        {
            var estimatesViewModel = new EstimatesViewModel();
            var estimates = _cacheManager.Get<Estimates>(projectId);
            estimatesViewModel.IsShow = estimates.IsShow;

            if (estimates.EstimateList.Count <= 0) return estimatesViewModel;

            foreach (var item in estimates.EstimateList)
            {
                var user = _userLogic.Get(item.UserId);
                estimatesViewModel.EstimateViewModel.Add(new EstimateViewModel
                {
                    ProjectId = item.ProjectId,
                    SelectedPoker = item.SelectedPoker,
                    UserImage = user.Image,
                    UserName = user.UserName
                });
            }
            return estimatesViewModel;
        }

    }
}

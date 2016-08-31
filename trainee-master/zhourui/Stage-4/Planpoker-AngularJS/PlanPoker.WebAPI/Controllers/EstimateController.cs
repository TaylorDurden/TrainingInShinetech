using System.Linq;
using System.Web.Http;
using PlanPoker.Common;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Controllers
{
    public class EstimateController : ApiController
    {
        private readonly IMemoryCacheManager _memoryCacheManager;
        private readonly IUserLogic _userLogic;

        public EstimateController(IMemoryCacheManager memoryCacheManager, IUserLogic userLogic)
        {
            _memoryCacheManager = memoryCacheManager;
            _userLogic = userLogic;
        }

        [Route("api/estimates")]
        [HttpPost]
        public EstimatesViewModel Insert(EstimateViewModel estimateViewModel)
        {
            if (string.IsNullOrEmpty(estimateViewModel?.ProjectId)) return null;

            return _memoryCacheManager.KeyExists(estimateViewModel.ProjectId)
                ? UpdateMemoryCache(estimateViewModel)
                : AddToMemoryCache(estimateViewModel);
        }

        [Route("api/estimatesDelete")]
        [HttpDelete]
        public void Delete(string projectId)
        {
            if (_memoryCacheManager.KeyExists(projectId)) _memoryCacheManager.Remove(projectId);
        }


        [Route("api/estimates")]
        [HttpGet]
        public EstimatesViewModel GetEstimatesById(string projectId)
        {
            return !_memoryCacheManager.KeyExists(projectId)
                ? null
                : _memoryCacheManager.Get<EstimatesViewModel>(projectId);
        }

        [Route("api/showEstimatesResult")]
        [HttpGet]
        public void SetEstimatesIsShow(string projectId)
        {
            if (!_memoryCacheManager.KeyExists(projectId)) return;

            _memoryCacheManager.Get<EstimatesViewModel>(projectId).IsShow = true;
        }

        private EstimatesViewModel AddToMemoryCache(EstimateViewModel estimateViewModel)
        {
            estimateViewModel.UserImage = _userLogic.GetUserImage(estimateViewModel.UserName);

            var estimates = new EstimatesViewModel();
            estimates.Estimates.Add(estimateViewModel);
            _memoryCacheManager.Add(estimateViewModel.ProjectId, estimates);
            
            return _memoryCacheManager.Get<EstimatesViewModel>(estimateViewModel.ProjectId);
        }

        private EstimatesViewModel UpdateMemoryCache(EstimateViewModel estimateViewModel)
        {
            var estimates = _memoryCacheManager.Get<EstimatesViewModel>(estimateViewModel.ProjectId);
            var existedUser = estimates.Estimates.FirstOrDefault(user => user.UserId == estimateViewModel.UserId);

            if (existedUser == null)
            {
                estimateViewModel.UserImage = _userLogic.GetUserImage(estimateViewModel.UserName);
                estimates.Estimates.Add(estimateViewModel);
            }
            else
            {
                existedUser.SelectedPoker = estimateViewModel.SelectedPoker;
            }

            return _memoryCacheManager.Get<EstimatesViewModel>(estimateViewModel.ProjectId);
        }
    }
}
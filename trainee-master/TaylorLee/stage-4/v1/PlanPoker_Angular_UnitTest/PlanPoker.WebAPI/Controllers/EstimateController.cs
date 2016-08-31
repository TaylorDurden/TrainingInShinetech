using System.Linq;
using System.Web.Http;
using PlanPoker.Common;
using PlanPoker.WebAPI.Models;
using PlanPoker.ILogic;

namespace PlanPoker.WebAPI.Controllers
{
    public class EstimateController : ApiController
    {
        private readonly ICacheManager _cacheManger;
        private readonly IUserLogic _userLogic;
        public Estimates Estimates { get; private set; }

        public EstimateController(ICacheManager cacheManger, IUserLogic userLogic)
        {
            _cacheManger = cacheManger;
            _userLogic = userLogic;
            Estimates = new Estimates();
        }

        [Route("api/estimate")]
        [HttpPost]
        public void Insert(Estimate estimate)
        {
            if (_cacheManger.KeyExists(estimate.ProjectId))
            {
                UpdateEstimate(estimate);
            }
            else
            {
                AddEstimate(estimate); 
            }
        }

        [Route("api/estimateDelete")]
        [HttpPost]
        public void Delete(string projectId)
        {
            if (_cacheManger.KeyExists(projectId))
            {
                _cacheManger.Remove(projectId);
            }
        }

        [Route("api/estimate")]
        [HttpGet]
        public EstimatesViewModel Get(string projectId)
        {
            var estimatesViewModel = new EstimatesViewModel();

            estimatesViewModel = _cacheManger.KeyExists(projectId) 
                ? GetEstimatesViewModel(projectId)
                : estimatesViewModel;

            if (Estimates.IsShow) estimatesViewModel.IsShow = true;

            return estimatesViewModel;
        }

        [Route("api/estimateShowCard")]
        [HttpPost]
        public void ShowCard(string projectId)
        {
            if (_cacheManger.KeyExists(projectId))
            {
                Estimates = _cacheManger.Get<Estimates>(projectId);
                Estimates.IsShow = true;
            }
        }

        [Route("api/estimateIsCleared")]
        [HttpGet]
        public bool IsKeyCleared(string projectId)
        {
            return !_cacheManger.KeyExists(projectId);
        }

        private void UpdateEstimate(Estimate estimate)
        {
            Estimates = _cacheManger.Get<Estimates>(estimate.ProjectId);
            var existedEstimate = Estimates.EstimateList.FirstOrDefault(u => u.UserId == estimate.UserId);
            if (existedEstimate == null)
            {
                Estimates.EstimateList.Add(estimate);
            }
            else
            {
                existedEstimate.SelectedPoker = estimate.SelectedPoker;
            }
        }

        private void AddEstimate(Estimate estimate)
        {
            Estimates.EstimateList.Add(estimate);
            _cacheManger.Add(estimate.ProjectId, Estimates);
        }

        private EstimatesViewModel GetEstimatesViewModel(string projectId)
        {
            var estimatesViewModel = new EstimatesViewModel();
            Estimates = _cacheManger.Get<Estimates>(projectId);
            if (Estimates == null) return estimatesViewModel;


            var estimateListCount = Estimates.EstimateList.Count;
            var ids = new int[estimateListCount];
            for (var i = 0; i < estimateListCount; i++)
            {
                ids[i] = int.Parse(Estimates.EstimateList[i].UserId);
            }

            var users = _userLogic.GetByIds(ids);

            var index = 0;
            foreach (var item in Estimates.EstimateList)
            {
                var estimateViewModel = new EstimateViewModel
                {
                    ProjectId = item.ProjectId,
                    SelectedPoker = item.SelectedPoker,
                    UserName = users[index].UserName,
                    UserImage = users[index].Image
                };
                estimatesViewModel.EstimateViewModel.Add(estimateViewModel);
                index++;
            }
            return estimatesViewModel;
        }
    }
}

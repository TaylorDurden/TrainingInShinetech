using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Castle.Components.DictionaryAdapter;
using PlanPoker.Common;
using PlanPoker.WebAPI.Models;
using PlanPoker.ILogic;

namespace PlanPoker.WebAPI.Controllers
{
    public class EstimateController : ApiController
    {
        private readonly ICacheManger _cacheManger;

        private readonly IUserLogic _userLogic;
        public EstimateController(ICacheManger cacheManger, IUserLogic userLogic)
        {
            _cacheManger = cacheManger;
            _userLogic = userLogic;
        }

        [Route("api/estimate")]
        [HttpPost]
        public void Insert(Estimate estimate)
        {

            if (!_cacheManger.KeyExist(estimate.ProjectId))
            {
                var estimates = new Estimates();
                estimates.EstimateList.Add(estimate);
                _cacheManger.Add(estimate.ProjectId, estimates);
            }
            else
            {
                var estimates = _cacheManger.Get<Estimates>(estimate.ProjectId);

                var existedUser = estimates.EstimateList.FirstOrDefault(u => u.UserId == estimate.UserId);

                if (object.Equals(existedUser, null))
                {
                    estimates.EstimateList.Add(estimate);
                }
                else
                {
                    existedUser.SelectedPoker = estimate.SelectedPoker;
                }
            }
        }

        [Route("api/estimateDelete")]
        [HttpGet]
        public void Delete(string projectId)
        {
            if (_cacheManger.KeyExist(projectId))
                _cacheManger.Remove(projectId);
        }


        [Route("api/estimate")]
        [HttpGet]
        public EstimatesViewModel Get(string projectId)
        {
            
            if (!_cacheManger.KeyExist(projectId))
            {
                return null;
            }

            EstimatesViewModel estimatesViewModel = new EstimatesViewModel();
            var es = _cacheManger.Get<Estimates>(projectId);
            
            if (es.EstimateList.Count > 0)
            {
                foreach (var item in es.EstimateList)
                {
                    var u = _userLogic.Get(item.UserId);
                    EstimateViewModel data = new EstimateViewModel
                    {
                        ProjectId = item.ProjectId,
                        SelectedPoker = item.SelectedPoker,
                        UserImage = u.Image,
                        UserName = u.UserName
                    };
                    estimatesViewModel.EstimateViewModel.Add(data);
                    
                }
                if (es.IsShow)
                {
                    estimatesViewModel.IsShow = true;
                }
            }
            return estimatesViewModel;
        }

        [Route("api/estimateShowCard")]
        [HttpGet]
        public void ShowCard(string projectId)
        {
            if (_cacheManger.KeyExist(projectId))
            {
                Estimates es = _cacheManger.Get<Estimates>(projectId);
                es.IsShow = true;
            }
            
        }

        [Route("api/estimateIsCleared")]
        [HttpGet]
        public bool IsCleared(string projectId)
        {
            return !_cacheManger.KeyExist(projectId);
        }

    }
}

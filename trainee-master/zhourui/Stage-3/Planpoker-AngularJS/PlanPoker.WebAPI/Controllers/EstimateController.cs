﻿using System.Linq;
using System.Web.Http;
using PlanPoker.Common;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Models;

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
            if (!_cacheManager.KeyExist(estimate.ProjectId))
            {
                var estimates = new Estimates();
                estimates.EstimateList.Add(estimate);
                _cacheManager.Add(estimate.ProjectId, estimates);
            }
            else
            {
                var estimates = _cacheManager.Get<Estimates>(estimate.ProjectId);

                var existedUser = estimates.EstimateList.FirstOrDefault(u => u.UserId == estimate.UserId);

                if (Equals(existedUser, null))
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
            if (_cacheManager.KeyExist(projectId))
            {
                _cacheManager.Remove(projectId);
            }
        }


        [Route("api/estimates")]
        [HttpGet]
        public EstimatesViewModel Get(string projectId)
        {
            if (!_cacheManager.KeyExist(projectId))
            {
                return null;
            }

            var estimatesViewModel = new EstimatesViewModel();
            var es = _cacheManager.Get<Estimates>(projectId);

            if (es.EstimateList.Count > 0)
            {
                foreach (var item in es.EstimateList)
                {
                    var u = _userLogic.Get(item.UserId);
                    var data = new EstimateViewModel
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

        [Route("api/estimatesShowCard")]
        [HttpGet]
        public void ShowCard(string projectId)
        {
            if (_cacheManager.KeyExist(projectId))
            {
                var es = _cacheManager.Get<Estimates>(projectId);
                es.IsShow = true;
            }
        }

        [Route("api/estimatesIsClear")]
        [HttpGet]
        public bool IsCleared(string projectId)
        {
            return !_cacheManager.KeyExist(projectId);
        }
    }
}
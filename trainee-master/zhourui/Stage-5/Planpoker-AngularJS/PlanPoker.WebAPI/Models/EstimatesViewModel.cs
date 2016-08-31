using System.Collections.Generic;

namespace PlanPoker.WebAPI.Models
{
    public class EstimatesViewModel
    {
        public EstimatesViewModel()
        {
            Estimates = new List<EstimateViewModel>();
            IsShow = false;
        }

        public List<EstimateViewModel> Estimates { get; set; }

        public bool IsShow { get; set; }
    }
}
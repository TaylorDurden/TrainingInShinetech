using System.Collections.Generic;

namespace PlanPoker.WebAPI.Models
{
    public class EstimatesViewModel
    {
        public EstimatesViewModel()
        {
            EstimateViewModel = new List<EstimateViewModel>();
        }

        public List<EstimateViewModel> EstimateViewModel { get; set; }
        public bool IsShow { get; set; }
    }
}
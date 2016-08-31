using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

namespace PlanPoker.WebAPI.Models
{
    public class Estimates
    {
        public Estimates()
        {
            EstimateList = new List<Estimate>();
            IsShow = false;
        }

        public List<Estimate> EstimateList { get; set; }

        public bool IsShow { get; set; }

    }
}
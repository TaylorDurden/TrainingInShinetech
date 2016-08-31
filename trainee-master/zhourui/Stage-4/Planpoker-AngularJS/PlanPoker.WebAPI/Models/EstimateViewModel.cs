namespace PlanPoker.WebAPI.Models
{
    public class EstimateViewModel
    {
        public string ProjectId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string SelectedPoker { get; set; }
    }
}
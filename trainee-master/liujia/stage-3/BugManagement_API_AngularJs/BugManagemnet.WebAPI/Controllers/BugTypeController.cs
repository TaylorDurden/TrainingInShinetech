using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BugManagement.Logic.ILogic;
using BugManagemnet.WebAPI.ModelExchange;
using BugManagemnet.WebAPI.Models;

namespace BugManagemnet.WebAPI.Controllers
{
    public class BugTypeController : ApiController
    {

        private readonly IBugTypeLogic _bugTypeLogic;

        public BugTypeController(IBugTypeLogic bugTypeLogic)
        {
            _bugTypeLogic = bugTypeLogic;
        }

        [System.Web.Mvc.Route("api/bugType")]
        [System.Web.Mvc.HttpGet]
        public List<BugTypeViewModel> GetAllBugType()
        {
            List<BugTypeViewModel> listModel = null;
            var listLogicModel = _bugTypeLogic.GetAll();
            if (listLogicModel.Any())
            {
                listModel = listLogicModel.Select(logicModel => logicModel.ConvertToBugTypeViewModel()).ToList();
            }
            return listModel;
        }
    }
}
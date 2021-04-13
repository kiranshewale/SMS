using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SMSCore.Models;
using SMSCore.Services.ShowroomVehiclesService;
using SMSCore.Services.VehiclesMasterService;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehiclesMasterService _vehicleMasterService;
        private readonly IShowroomVehiclesService _showroomVehiclesService;

        public HomeController(ILogger<HomeController> logger,
            IVehiclesMasterService vehicleMasterService,
            IShowroomVehiclesService showroomVehiclesService)
        {
            _logger = logger;
            _vehicleMasterService = vehicleMasterService;
            _showroomVehiclesService = showroomVehiclesService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new DashboardModel();
            model.TotalVehicles=await _vehicleMasterService.GetVehiclesStockAsync();
            model.AvaillableVehicles = await _vehicleMasterService.GetAvaillableVehiclesStockAsync();
            model.AllotedVehicles = await _vehicleMasterService.GetAllotedVehiclesStockAsync();
            model.DelievredVehicles = 0;
            model.ShowroomVehicles = await _showroomVehiclesService.GetShowroomVehiclesStockAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

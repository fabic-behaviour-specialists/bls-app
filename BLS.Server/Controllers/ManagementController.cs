using BLS.Cloud.Models;
using BLS.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BLS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<ManagementController> _logger;

        public ManagementController(ILogger<ManagementController> logger,
                               IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        [HttpPost]
        [Route("management/syncuserdata")]
        public async Task<SyncData> SyncUserData(string json)
        {
            var rawdata = JsonConvert.DeserializeObject(json) ?? throw new BadHttpRequestException("Invalid Body");
            SyncData data = (SyncData)rawdata;

            if (string.IsNullOrWhiteSpace(data.UserID))
            {
                throw new BadHttpRequestException("Invalid UserID");
            }

            if (data.Scales.Count > 0)
            {
                await _databaseService.UpdateBehaviourScalesAsync(data.Scales);
            }
            if (data.ScaleItems.Count > 0)
            {
                await _databaseService.UpdateBehaviourScaleItemsAsync(data.ScaleItems);
            }
            if (data.Charts.Count > 0)
            {
                await _databaseService.UpdateIChooseChartsAsync(data.Charts);
            }
            if (data.ChartItems.Count > 0)
            {
                await _databaseService.UpdateIChooseChartItemsAsync(data.ChartItems);
            }

            var behaviourScales = await _databaseService.GetUserBehaviourScalesAsync(data.UserID);
            var behaviourScaleItems = await _databaseService.GetUserBehaviourScaleItemsAsync(data.UserID);
            var ichooseCharts = await _databaseService.GetUserIChooseChartsAsync(data.UserID);
            var ichooseChartItems = await _databaseService.GetUserIChooseChartItemsAsync(data.UserID);

            SyncData serverData = new SyncData();
            serverData.UserID = data.UserID;
            serverData.Scales = behaviourScales.ToList();
            serverData.ScaleItems = behaviourScaleItems.ToList();
            serverData.Charts = ichooseCharts.ToList();
            serverData.ChartItems = ichooseChartItems.ToList();

            return serverData;

        }

        public class SyncData
        {
            public string UserID { get; set; }
            public List<IChooseChart> Charts { get; set; }
            public List<IChooseChartItem> ChartItems { get; set; }
            public List<BehaviourScale> Scales { get; set; }
            public List<BehaviourScaleItem> ScaleItems { get; set; }
        }
    }
}

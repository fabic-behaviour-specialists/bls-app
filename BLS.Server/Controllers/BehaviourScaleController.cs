using BLS.Cloud.Models;
using BLS.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BLS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BehaviourScaleController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<BehaviourScaleController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BehaviourScaleController(ILogger<BehaviourScaleController> logger, IDatabaseService databaseService, IWebHostEnvironment environment)
        {
            _logger = logger;
            _databaseService = databaseService;
            _hostingEnvironment = environment;
        }

        [HttpGet()]
        [Route("report")]
        public async Task<byte[]?> Post()
        {
            try
            {
                string id = HttpContext.Request.Headers["id"].ToString();
                string rawJSON = string.Empty;
                char[] result;
                StringBuilder builder = new StringBuilder();

                using (var inputStream = new StreamReader(HttpContext.Request.Body))
                {
                    result = new char[inputStream.BaseStream.Length];
                    await inputStream.ReadAsync(result, 0, (int)inputStream.BaseStream.Length);
                }
                foreach (char c in result)
                {
                    if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                    {
                        builder.Append(c);
                    }
                }
                rawJSON = builder.ToString();

                BehaviourScaleReport? scale = null;
                if (rawJSON.Length > 0)
                {
                    try
                    {
                        scale = JsonConvert.DeserializeObject<BehaviourScaleReport>(rawJSON);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Deserialisation error");
                    }
                }

                List<BehaviourScaleItem> items = new List<BehaviourScaleItem>();

                if (scale != null)
                {
                    items = scale.Items;

                    BodyLifeSkillsChart chart = new BodyLifeSkillsChart();
                    chart.lblTitle.Text = scale.Name;

                    List<BehaviourScaleItem> lifeItems5 = items.Where(x => x.BehaviourScaleLevel == 5 && x.BehaviourScaleType == 1).ToList();
                    List<BehaviourScaleItem> bodyItems5 = items.Where(x => x.BehaviourScaleLevel == 5 && x.BehaviourScaleType == 0).ToList();
                    List<BehaviourScaleItem> lifeItems4 = items.Where(x => x.BehaviourScaleLevel == 4 && x.BehaviourScaleType == 1).ToList();
                    List<BehaviourScaleItem> bodyItems4 = items.Where(x => x.BehaviourScaleLevel == 4 && x.BehaviourScaleType == 0).ToList();
                    List<BehaviourScaleItem> lifeItems3 = items.Where(x => x.BehaviourScaleLevel == 3 && x.BehaviourScaleType == 1).ToList();
                    List<BehaviourScaleItem> bodyItems3 = items.Where(x => x.BehaviourScaleLevel == 3 && x.BehaviourScaleType == 0).ToList();
                    List<BehaviourScaleItem> lifeItems2 = items.Where(x => x.BehaviourScaleLevel == 2 && x.BehaviourScaleType == 1).ToList();
                    List<BehaviourScaleItem> bodyItems2 = items.Where(x => x.BehaviourScaleLevel == 2 && x.BehaviourScaleType == 0).ToList();
                    List<BehaviourScaleItem> lifeItems1 = items.Where(x => x.BehaviourScaleLevel == 1 && x.BehaviourScaleType == 1).ToList();
                    List<BehaviourScaleItem> bodyItems1 = items.Where(x => x.BehaviourScaleLevel == 1 && x.BehaviourScaleType == 0).ToList();

                    BodyLifeSkillsChartItem chartItem5 = new BodyLifeSkillsChartItem(_hostingEnvironment, 5);
                    BodyLifeSkillsChartItem chartItem4 = new BodyLifeSkillsChartItem(_hostingEnvironment, 4);
                    BodyLifeSkillsChartItem chartItem3 = new BodyLifeSkillsChartItem(_hostingEnvironment, 3);
                    BodyLifeSkillsChartItem chartItem2 = new BodyLifeSkillsChartItem(_hostingEnvironment, 2);
                    BodyLifeSkillsChartItem chartItem1 = new BodyLifeSkillsChartItem(_hostingEnvironment, 1);

                    foreach (BehaviourScaleItem item in lifeItems5)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem5.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in bodyItems5)
                        //  if (!doNotIncludeList.Contains(item.Id))
                        chartItem5.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in lifeItems4)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem4.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in bodyItems4)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem4.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in lifeItems3)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem3.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in bodyItems3)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem3.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in lifeItems2)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem2.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in bodyItems2)
                        //  if (!doNotIncludeList.Contains(item.Id))
                        chartItem2.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in lifeItems1)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem1.AddChartItemText(item);
                    foreach (BehaviourScaleItem item in bodyItems1)
                        // if (!doNotIncludeList.Contains(item.Id))
                        chartItem1.AddChartItemText(item);

                    int height = 0;
                    chart.AddBodyLifeSkillsChartItem(chartItem5, height);
                    height += chartItem5.Height;
                    chart.AddBodyLifeSkillsChartItem(chartItem4, height);
                    height += chartItem4.Height;
                    chart.AddBodyLifeSkillsChartItem(chartItem3, height);
                    height += chartItem3.Height;
                    chart.AddBodyLifeSkillsChartItem(chartItem2, height);
                    height += chartItem2.Height;
                    chart.AddBodyLifeSkillsChartItem(chartItem1, height);

                    string filename = @"\" + DateTime.Now.ToFileTime().ToString() + ".pdf";
                    string filepath = Path.Combine(_hostingEnvironment.WebRootPath, "~/Exports");
                    chart.ExportToPdf(filepath + filename);

                    FileStream fileStream = new FileStream(filepath + filename, FileMode.Open);
                    BinaryReader reader = new BinaryReader(fileStream);
                    Byte[] array = reader.ReadBytes(Convert.ToInt32(fileStream.Length));
                    var stream = new MemoryStream(array);
                    fileStream.Close();

                    return array;

                    // processing the stream.

                    //var result = new HttpResponseMessage(HttpStatusCode.OK)
                    //{
                    //    Content = new ByteArrayContent(stream.ToArray())
                    //};

                    //Exception exp = new Exception(result.Content.ToString());

                    //result.Content.Headers.ContentDisposition =
                    //    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    //    {
                    //        FileName = "export.pdf"
                    //    };
                    //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    //return result;
                }
            }
            catch (Exception ex)
            {
                var err = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message + Environment.NewLine + ex.StackTrace)
                };
                _logger.LogError(ex, "Internal server error");
                throw new HttpRequestException(HttpRequestError.Unknown);
            }
            return null;
        }
    }
}

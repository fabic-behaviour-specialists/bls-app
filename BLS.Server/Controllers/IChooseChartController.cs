using BLS.Cloud.Models;
using BLS.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BLS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IChooseChartController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<IChooseChartController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IChooseChartController(ILogger<IChooseChartController> logger,
                               IDatabaseService databaseService, IWebHostEnvironment environment)
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
                using (var inputStream = new StreamReader(HttpContext.Request.Body))
                {
                    rawJSON = inputStream.ReadToEnd();
                }

                IChooseChartReport? chart = null;
                if (rawJSON.Length > 0)
                {
                    try
                    {
                        chart = JsonConvert.DeserializeObject<IChooseChartReport>(rawJSON);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Deserialisation error");
                    }
                }

                List<IChooseChartItemReport> items = new List<IChooseChartItemReport>();

                if (chart != null)
                {
                    items = chart.Items;
                    BLS.Cloud.Reports.IChooseChartReport report = new BLS.Cloud.Reports.IChooseChartReport();
                    report.lblReportTitle.Text = chart.Name;

                    List<IChooseChartItemReport> option1Behaviour = items.Where(x => x.ChartOption == 0 && x.ChartType == 0).ToList();
                    List<IChooseChartItemReport> option1Outcome = items.Where(x => x.ChartOption == 0 & x.ChartType == 1).ToList();
                    List<IChooseChartItemReport> option2Behaviour = items.Where(x => x.ChartOption == 1 && x.ChartType == 0).ToList();
                    List<IChooseChartItemReport> option2Outcome = items.Where(x => x.ChartOption == 1 && x.ChartType == 1).ToList();

                    IChooseChartReportItem1 reportItem1 = new IChooseChartReportItem1();
                    IChooseChartReportItem2 reportItem2 = new IChooseChartReportItem2();

                    foreach (IChooseChartItemReport item in option1Behaviour)
                        // if (!doNotIncludeList.Contains(item.Id))
                        reportItem1.AddChartItemText(item);
                    foreach (IChooseChartItemReport item in option2Behaviour)
                        // if (!doNotIncludeList.Contains(item.Id))
                        reportItem1.AddChartItemText(item);
                    foreach (IChooseChartItemReport item in option1Outcome)
                        // if (!doNotIncludeList.Contains(item.Id))
                        reportItem2.AddChartItemText(item);
                    foreach (IChooseChartItemReport item in option2Outcome)
                        //  if (!doNotIncludeList.Contains(item.Id))
                        reportItem2.AddChartItemText(item);

                    report.AddChartItem1(reportItem1);
                    report.AddChartItem2(reportItem2);

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

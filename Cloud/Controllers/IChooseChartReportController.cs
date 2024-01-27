using BodyLifeSkillsPlatform.Data.Helpers;
using BodyLifeSkillsPlatform.Data.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Fabic.Cloud.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class IChooseChartReportController : ApiController
    {
        // GET api/values
        public string Get()
        {

            return "Hello World!";
        }

        // POST api/values
        public HttpResponseMessage Post()
        {
            try
            {
                if (ActionContext.Request.Headers.Contains("access_token"))
                {
                    string access_token = ActionContext.Request.Headers.GetValues("access_token").ElementAtOrDefault(0);
                    var token = (JwtSecurityToken)AccountController.Auth0Controller._SecurityController._TokenHandler.ReadToken(access_token);
                    if (IsValidUser(token))
                    {
                        string id = ActionContext.Request.Headers.GetValues("id").FirstOrDefault().ToString();
                        string itemsRaw = ActionContext.Request.Content.ReadAsStringAsync().Result;
                        Data.Models.IChooseChartReport chart = null;
                        if (itemsRaw.Length > 0)
                            try
                            {
                                chart = JsonConvert.DeserializeObject<Data.Models.IChooseChartReport>(itemsRaw);
                            }
                            catch (Exception ex)
                            {
                                ex.HandleBLSException();
                            }

                        List<IChooseChartItemReport> items = new List<IChooseChartItemReport>();

                        if (chart != null)
                        {
                            items = chart.Items;
                            Data.Reports.IChooseChartReport report = new Data.Reports.IChooseChartReport();
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

                            string filename = "/" + DateTime.Now.ToFileTime().ToString() + ".pdf";
                            string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/Exports");
                            report.ExportToPdf(filepath + filename);

                            FileStream fileStream = new FileStream(filepath + filename, FileMode.Open);
                            BinaryReader reader = new BinaryReader(fileStream);
                            Byte[] array = reader.ReadBytes(Convert.ToInt32(fileStream.Length));
                            var stream = new MemoryStream(array);
                            fileStream.Close();

                            // processing the stream.

                            var result = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ByteArrayContent(stream.ToArray())
                            };

                            result.Content.Headers.ContentDisposition =
                                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = "export.pdf"
                                };
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/octet-stream");
                            return result;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
            return null;
        }

        private static bool IsValidUser(JwtSecurityToken token)
        {
            if (token == null)
                return false;
            var audience = token.Audiences.FirstOrDefault();
            if (!audience.Equals(AccountController.Auth0Controller._SecurityController._ClientID))
                return false;
            if (!token.Issuer.Equals(AccountController.Auth0Controller._SecurityController._Issuer) && !token.Issuer.Equals($"https://{AccountController.Auth0Controller._SecurityController._Domain}/"))
                return false;
            if (token.ValidTo.AddMinutes(5) < DateTime.Now)
                return false;
            return true;
        }
    }
}

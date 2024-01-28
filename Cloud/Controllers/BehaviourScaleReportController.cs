using BLS.Cloud.Helpers;
using BLS.Cloud.Models;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class BehaviourScaleReportController : ApiController
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
                //if (ActionContext.Request.Headers.Contains("access_token"))
                {
                    //string access_token = ActionContext.Request.Headers.GetValues("access_token").ElementAtOrDefault(0);
                    // var token = (JwtSecurityToken)BLS.Cloud.Controllers.AccountController.Auth0Controller._SecurityController._TokenHandler.ReadToken(access_token);
                    // if (IsValidUser(token))
                    {
                        string id = ActionContext.Request.Headers.GetValues("id").FirstOrDefault().ToString();
                        string itemsRaw = ActionContext.Request.Content.ReadAsStringAsync().Result;
                        // List<string> doNotIncludeList = new List<string>();
                        BehaviourScaleReport scale = null;
                        if (itemsRaw.Length > 0)
                            try
                            {
                                scale = JsonConvert.DeserializeObject<BehaviourScaleReport>(itemsRaw);
                            }
                            catch (Exception ex)
                            {
                                ex.HandleBLSException();
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

                            BodyLifeSkillsChartItem chartItem5 = new BodyLifeSkillsChartItem(5);
                            BodyLifeSkillsChartItem chartItem4 = new BodyLifeSkillsChartItem(4);
                            BodyLifeSkillsChartItem chartItem3 = new BodyLifeSkillsChartItem(3);
                            BodyLifeSkillsChartItem chartItem2 = new BodyLifeSkillsChartItem(2);
                            BodyLifeSkillsChartItem chartItem1 = new BodyLifeSkillsChartItem(1);

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
                            string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/Exports");
                            chart.ExportToPdf(filepath + filename);

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

                            Exception exp = new Exception(result.Content.ToString());

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
                var err = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message + Environment.NewLine + ex.StackTrace)
                };
                ex.HandleBLSException();
                return err;
            }
            return null;
        }

        public static bool IsValidUser(JwtSecurityToken token)
        {
            if (token == null)
                return false;
            var audience = token.Audiences.FirstOrDefault();
            if (!audience.Equals(AccountController.Auth0Controller._SecurityController._ClientID))
                return false;
            if (!token.Issuer.Equals(AccountController.Auth0Controller._SecurityController._Issuer) && !token.Issuer.Equals($"https://{AccountController.Auth0Controller._SecurityController._Domain}/"))
                return false;
            //if (token.ValidTo.AddMinutes(5) < DateTime.Now)
            //    return false;
            return true;
        }
    }
}

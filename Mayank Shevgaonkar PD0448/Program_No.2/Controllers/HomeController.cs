using Newtonsoft.Json;
using Program_No._2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Program_No._2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            List<Country> objCountry = new List<Country>();
            CountryModel model = new CountryModel();
            objCountry = model.GetCountries();
            return View(new Venue { Countries = objCountry });
        }
        [HttpGet, ActionName("GetEventVenuesList")]
        public JsonResult GetEventVenuesList(string SearchText)
        {
            string placeApiUrl = ConfigurationManager.AppSettings["GooglePlaceAPIUrl"];

            try
            {
                placeApiUrl = placeApiUrl.Replace("{0}", SearchText);
                placeApiUrl = placeApiUrl.Replace("{1}", ConfigurationManager.AppSettings["GooglePlaceAPIKey"]);

                var result = new System.Net.WebClient().DownloadString(placeApiUrl);
                var Jsonobject = JsonConvert.DeserializeObject<RootObject>(result);

                List<Prediction> list = Jsonobject.predictions;

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, ActionName("GetVenueDetailsByPlace")]
        public JsonResult GetVenueDetailsByPlace(string placeId)
        {
            string placeDetailsApi = ConfigurationManager.AppSettings["GooglePlaceDetailsAPIUrl"];
            try
            {
                Venue ven = new Venue();
                placeDetailsApi = placeDetailsApi.Replace("{0}", placeId);
                placeDetailsApi = placeDetailsApi.Replace("{1}", ConfigurationManager.AppSettings["GooglePlaceAPIKey"]);

                var result = new System.Net.WebClient().DownloadString(placeDetailsApi);

                var xmlElm = XElement.Parse(result);
                ven = GetAllVenueDetails(xmlElm);

                return Json(ven, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>  
        /// This method is creted to get the place details from xml  
        /// </summary>  
        /// <param name="xmlElm"></param>  
        /// <returns></returns>  
        private Venue GetAllVenueDetails(XElement xmlElm)
        {
            Venue ven = new Venue();
            List<string> c = new List<string>();
            List<string> d = new List<string>();
            //get the status of download xml file  
            var status = (from elm in xmlElm.Descendants()
                          where
                              elm.Name == "status"
                          select elm).FirstOrDefault();

            //if download xml file status is ok  
            if (status.Value.ToLower() == "ok")
            {

                //get the address_component element  
                var addressResult = (from elm in xmlElm.Descendants()
                                     where
                                         elm.Name == "address_component"
                                     select elm);
                //get the location element  
                var locationResult = (from elm in xmlElm.Descendants()
                                      where
                                          elm.Name == "location"
                                      select elm);

                foreach (XElement item in locationResult)
                {
                    ven.Latitude = float.Parse(item.Elements().Where(e => e.Name.LocalName == "lat").FirstOrDefault().Value);
                    ven.Longitude = float.Parse(item.Elements().Where(e => e.Name.LocalName == "lng").FirstOrDefault().Value);
                }
                //loop through for each address_component  
                foreach (XElement element in addressResult)
                {
                    string type = element.Elements().Where(e => e.Name.LocalName == "type").FirstOrDefault().Value;

                    if (type.ToLower().Trim() == "locality") //if type is locality the get the locality  
                    {
                        ven.City = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                    }
                    else
                    {
                        if (type.ToLower().Trim() == "administrative_area_level_2" && string.IsNullOrEmpty(ven.City))
                        {
                            ven.City = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                        }
                    }
                    if (type.ToLower().Trim() == "administrative_area_level_1") //if type is locality the get the locality  
                    {
                        ven.State = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                    }
                    if (type.ToLower().Trim() == "country") //if type is locality the get the locality  
                    {
                        ven.Country = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                        ven.CountryCode = element.Elements().Where(e => e.Name.LocalName == "short_name").Single().Value;
                        if (ven.Country == "United States") { ven.Country = "USA"; }
                    }
                    if (type.ToLower().Trim() == "postal_code") //if type is locality the get the locality  
                    {
                        ven.ZipCode = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                    }
                }
            }
            xmlElm.RemoveAll();
            return ven;
        }

    }
}

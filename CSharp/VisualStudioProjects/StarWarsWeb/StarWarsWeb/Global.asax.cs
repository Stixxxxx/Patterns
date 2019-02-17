using StarWarsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ubs.GeoBazaAPI;

namespace StarWarsWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        UserDataContext db = new UserDataContext("workstation id=StarWars.mssql.somee.com;packet size=4096;user id=Stixxxxx_SQLLogin_1;pwd=ppx8wsux8v;data source=StarWars.mssql.somee.com;persist security info=False;initial catalog=StarWars");

        protected void Application_Start()
        {
            //Database.SetInitializer(new BookDbInitializer());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // обработка события BeginRequest
        protected void Application_BeginRequest()
        {
           
        }
        // обработка события AuthenticateRequest
        protected void Application_AuthenticateRequest()
        {
            
        }
        // обработка события PreRequestHandlerExecute
        protected void Application_PreRequestHandlerExecute()
        {
            var userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string coordinates = "";
            var location = DefineLocation(ip, ref coordinates);
            
            //Database.SetInitializer<UserDataContext>(null);
            
                //User user = new User() { ip = ip, country = location, browser = userAgent, time = DateTime.Now};
                //db.Users.Add(user);
                //db.SaveChanges();
            

        }

        protected void Application_End()
        {
            db.Dispose();
        }

        // определяем местоположение, обращаясь к API и базе данных
        protected string DefineLocation(string IP, ref string coordinates)
        {
            GeoBazaAPI geo = new GeoBazaAPI(Server.MapPath("~/App_Data/geobaza.dat"));
            string result = "Не определено";
            // получаем географию по ip
            List<IPLocation> locList = geo.GetLocationByIP(IP);
            if (locList != null && locList.Count != 0 && locList[0].ID != -1)
            {
                IPLocation country = GetCountry(locList);

                if (country != null)
                    result = country.ISOID + ", " + country.NameRU + ", " + locList[0].NameRU + ", долгота: " + locList[0].Longitude + ", долгота: " + locList[0].Latitude;
                else
                    result = locList[0].NameRU + ", долгота: " + locList[0].Longitude + ", долгота: " + locList[0].Latitude;
                coordinates = locList[0].Latitude + ", " + locList[0].Longitude;
            }

            return result;
        }
        // определяем страну
        private IPLocation GetCountry(List<IPLocation> locList)
        {
            for (int i = 0; i < locList.Count; i++)
            {
                if (locList[i].Type == LocationType.Country)
                    return locList[i];
            }
            return null;
        }
    }
}

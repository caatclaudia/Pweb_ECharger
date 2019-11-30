namespace ECharger.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ECharger.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ECharger.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ECharger.Models.ApplicationDbContext";
        }

        protected override void Seed(ECharger.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            string URL = "https://api.openchargemap.io/v3/poi/?output=json&countrycode=PT&maxresults=200&latitude=40.2056400&longitude=-8.4195";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.ContentType = "application/json; charset=utf-8";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var chargingStationsInfoJson = reader.ReadToEnd();
                    var chargingStationsInfo = JsonConvert.DeserializeObject<List<ChargingStationInfo>>(chargingStationsInfoJson);
                    List<ChargingStation> chagingStations = new List<ChargingStation>();
                    var i = 0;
                    var companyID = context.Users.Find("420cb643-a40c-46b1-891e-46aa144a2c40").Id;
                    Random random = new Random();
                    string operatorInfo;
                    foreach (var chargingStationInfo in chargingStationsInfo)
                    {
                        if (chargingStationInfo.AddressInfo.Town == null)
                        {
                            continue;
                        }

                        if (chargingStationInfo.OperatorInfo == null)
                        {
                            operatorInfo = "Mobi E";
                        }
                        else
                        {
                            operatorInfo = chargingStationInfo.OperatorInfo.Title;
                        }

                        ChargingStation chargingStation = new ChargingStation
                        {
                            Name = "Charging Station " + i,
                            StreetName = chargingStationInfo.AddressInfo.AddressLine1,
                            City = chargingStationInfo.AddressInfo.Town,
                            Operator = operatorInfo,
                            Latitude = chargingStationInfo.AddressInfo.Latitude,
                            Longitude = chargingStationInfo.AddressInfo.Longitude,
                            PricePerMinute = random.NextDouble(),
                            CompanyID = companyID
                        };
                        context.ChargingStations.AddOrUpdate(chargingStation);
                        i++;
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}

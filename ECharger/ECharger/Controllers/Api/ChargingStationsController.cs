using AutoMapper;
using ECharger.Dtos;
using ECharger.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECharger.Controllers.Api
{
    public class ChargingStationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/ChargingStations/{latitude}/{longitude}")]
        public IHttpActionResult Get(int latitude, int longitude)
        {
            var chargingStationsDtos = db.ChargingStations.ToList().Select(Mapper.Map<ChargingStation, ChargingStationDto>)
                                        .OrderBy(c => distanceBetweenTwoCoordinates(latitude, longitude, c.Latitude, c.Longitude));

            return Ok(chargingStationsDtos);
        }
        
        private double distanceBetweenTwoCoordinates(int userLatitude, int userLongitude, double stationLatitude, double stationLongitude)
        {
            var userCoord = new GeoCoordinate(userLatitude, userLongitude);
            var stationCoord = new GeoCoordinate(stationLatitude, stationLongitude);

            return userCoord.GetDistanceTo(stationCoord);
        }
    }
}

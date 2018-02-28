using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezginZeplin
{
    class City
    {
        public double lng;
        public double lat;
        public int plate;
        public int altitude;

        public City(){}

        public City(double lat, double lng, int plate, int altitude)
        {
            this.lat = lat;
            this.lng = lng;
            this.plate = plate;
            this.altitude = altitude;
        }

        public void writeCity()
        {
            Console.WriteLine("LAT: " + this.lat + " LONG: " + this.lng + " PLATE: " + this.plate + " ALT: " + this.altitude);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezginZeplin
{
    class City
    {
        double lat;
        double lng;
        int plaka;
        int rakim; //buzlu olsun

        public City(){}

        public City(double lat, double lng, int plaka, int rakim)
        {
            this.lat = lat;
            this.lng = lng;
            this.plaka = plaka;
            this.rakim = rakim;
        }
    }
}

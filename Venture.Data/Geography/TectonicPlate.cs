using System;
using System.Collections.Generic;
using System.Text;

namespace Venture.Data.Geography
{
    public class TectonicPlate
    {
	    private static Random _random = new Random();

	    public Tuple<double, double> Center { get; }
		public byte Id { get; }

	    public TectonicPlate(byte id)
	    {
		    Id = id;

		    var lat = _random.Next(-90, 90);
		    if (lat == 0) lat++;
		    var lon = _random.Next(-180, 180);
		    Center = new Tuple<double, double>(lat, lon);

		}
	}
}

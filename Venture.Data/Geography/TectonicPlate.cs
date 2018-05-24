using System;

namespace Venture.Data.Geography
{
    public class TectonicPlate
    {
	    private static Random _random = new Random();

	    public Tuple<double, double> Center { get; }
		public byte Id { get; }

	    public TectonicPlate(byte id, double? latitude = null, double? longitude = null)
	    {
		    Id = id;

			latitude = latitude  ?? _random.Next(-89, 91);
			longitude = longitude ?? _random.Next(-180, 180);

		    Center = new Tuple<double, double>(latitude.Value, longitude.Value);
		}
	}
}

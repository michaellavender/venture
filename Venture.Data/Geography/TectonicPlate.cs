using System;
using System.Collections.Generic;

namespace Venture.Data.Geography
{
    public class TectonicPlate
    {
	    private static readonly Random _random = new Random();

	    public Tuple<double, double> Center { get; }
		public byte Id { get; }
		public int Direction { get; }
	    public IList<GridUnit> PlateUnits { get; }

	    public TectonicPlate(byte id, double? latitude = null, double? longitude = null)
	    {
		    Id = id;

			latitude = latitude  ?? _random.Next(-89, 91);
			longitude = longitude ?? _random.Next(-180, 180);

		    Direction = _random.NextDouble() >= 0.5
			    ? 90 + _random.Next(-30, 31)
			    : -90 + _random.Next(-30, 31);

			Center = new Tuple<double, double>(latitude.Value, longitude.Value);

		    PlateUnits = new List<GridUnit>();
		}
	}
}

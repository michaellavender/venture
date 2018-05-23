using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Spatial;

namespace Venture.Data.Geography
{
    public class GridUnit
    {
	    public const double ArcDistance = 69.172;
	    public const int DegreesOfArc = 4;
		private static double radiansCvt = (Math.PI / 180.0);

		public bool IsLand { get; set; }

	    public GeographyPoint Location { get; set; }

	    public double TopPctArc => Location.Latitude > 0
		    ? Math.Cos(Location.Latitude * radiansCvt)
		    : Math.Cos((Math.Abs(Location.Latitude) - DegreesOfArc) * radiansCvt);

	    public double TopWidth => TopPctArc * ArcDistance * DegreesOfArc;

		public double BottomPctArc => Location.Latitude > 0
			? Math.Cos((Location.Latitude - DegreesOfArc)* radiansCvt)
			: Math.Cos(Math.Abs(Location.Latitude * radiansCvt));

	    public double BottomWidth => BottomPctArc * ArcDistance * DegreesOfArc;

	    public bool IsNeighbor(GridUnit gu)
	    {
		    return Math.Abs(Location.Latitude - gu.Location.Latitude) <= DegreesOfArc
				   && Math.Abs(Location.Longitude - gu.Location.Longitude) <= DegreesOfArc;
	    }
    }
}

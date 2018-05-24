using System;
using System.Collections.Generic;
using System.Linq;

namespace Venture.Data.Geography
{
    public class GridUnit
    {
	    public const double ArcDistance = 69.172;
		private static double radiansCvt = (Math.PI / 180.0);
	    private Tuple<double, double> _key;
	    private List<Tuple<double, double>> _neighborKeys;
	    private IDictionary<Tuple<double, double>, double> _distances;

		public GridUnit(double latitude, double longitude)
	    {
		    Latitude = latitude;
		    Longitude = longitude;
			Key = new Tuple<double, double>(Latitude, Longitude);

		    _distances = new Dictionary<Tuple<double, double>, double>();
		}

		public byte? PlateId { get; set; }
		public bool IsPlateBorder { get; set; }
		public bool IsLand { get; set; }

		public double Longitude { get; }
	    public double Latitude { get; }
	    public double Elevation { get; set; }
		public Tuple<double, double> Key { get; }

	    public IReadOnlyList<Tuple<double, double>> NeighborKeys
	    {
		    get
		    {
			    if (_neighborKeys == null)
			    {
				    _neighborKeys = new List<Tuple<double, double>>();
				    for (var latIdx = -1; latIdx <= 1; latIdx++)
				    {
					    var lat = Latitude + latIdx;
					    var longitude = Longitude;

					    var wrapPole = false;
					    if (lat < -89)
					    {
						    lat = -89;
						    wrapPole = true;
					    }
						else if (lat > 90)
					    {
						    lat = 90;
						    wrapPole = true;
						}

					    if (wrapPole)
					    {
						    if (longitude < 0)
							    longitude = 180 + longitude;
						    else
							    longitude = -180 + longitude;
					    }

					    for (var lonIdx = -1; lonIdx <= 1; lonIdx++)
					    {
							// Ignore this point (it's not a neighbor of itself)
						    if (latIdx == 0 && lonIdx == 0) continue;

						    var lon = longitude + lonIdx;
						    if (lon > 179)
							    lon = -180;
							else if (lon < -180)
							    lon = 179;

							_neighborKeys.Add(new Tuple<double, double>(lat, lon));
					    }
				    }
			    }

			    return _neighborKeys;
		    }
	    }


		public void SetNeighbors(IDictionary<Tuple<double, double>, GridUnit> worldUnits)
		{
			Neighbors = NeighborKeys.Select(key => worldUnits[key])
				//.Where(n => n.PlateId == null)
				.ToList();
		}

		public IReadOnlyList<GridUnit> Neighbors { get; set; }

	    public double TopPctArc => Latitude > 0
		    ? Math.Cos(Latitude * radiansCvt)
		    : Math.Cos((Math.Abs(Latitude) - 1) * radiansCvt);

	    public double TopWidth => TopPctArc * ArcDistance;

		public double BottomPctArc => Latitude > 0
			? Math.Cos((Latitude - 1)* radiansCvt)
			: Math.Cos(Math.Abs(Latitude * radiansCvt));

	    public double BottomWidth => BottomPctArc * ArcDistance;

	    public bool IsNeighbor(GridUnit gu)
	    {
		    return NeighborKeys.Contains(gu.Key);
	    }

	    public double Distance(Tuple<double, double> position)
	    {
		    return Distance(position.Item1, position.Item2);
	    }

	    public double Distance(double latitude, double longitude)
	    {
		    var key = new Tuple<double, double>(latitude, longitude);

		    if (_distances.TryGetValue(key, out var dist))
			    return dist;

		    //dist = Math.Sqrt(Math.Pow(Longitude - longitude, 2) + Math.Pow(Latitude - latitude, 2));

		    var rlat1 = Math.PI * Latitude / 180;
		    var rlat2 = Math.PI * latitude / 180;
		    var theta = Longitude - longitude;
		    var rtheta = Math.PI * theta / 180;
	        dist =
		        Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
		        Math.Cos(rlat2) * Math.Cos(rtheta);
	        dist = Math.Acos(dist);
	        dist = dist * 180 / Math.PI;

		    _distances[key] = dist;
		    return dist;
	        //return  dist * 60 * 1.1515;
	    }
	}
}
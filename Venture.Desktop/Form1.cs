using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Venture.Data.Geography;

namespace Venture.Desktop
{
	public partial class Form1 : Form
	{
		private World _world;

		private IDictionary<Color, Brush> _brushes;
		private IList<Color> _plateColors = new List<Color>();

		private Pen _blackPen;


		public Form1()
		{
			_brushes = new Dictionary<Color, Brush>();
			foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
			{
				var c = Color.FromKnownColor(kc);
				_brushes[c] = new SolidBrush(c);
			}

			_blackPen = new Pen(Color.Black);
			_plateColors.Add(Color.Red);
			_plateColors.Add(Color.Blue);
			_plateColors.Add(Color.Purple);
			_plateColors.Add(Color.Yellow);
			_plateColors.Add(Color.Green);
			_plateColors.Add(Color.Aqua);
			_plateColors.Add(Color.Bisque);
			_plateColors.Add(Color.BurlyWood);
			_plateColors.Add(Color.Plum);
			_plateColors.Add(Color.Magenta);
			_plateColors.Add(Color.Orange);
			_plateColors.Add(Color.Tan);

			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			if (_world == null) return;

			var centerX = e.ClipRectangle.Width / 2;
			var centerY = e.ClipRectangle.Height / 2;

			var hdegSize = (e.ClipRectangle.Width / 360);
			var vdegSize = (e.ClipRectangle.Height / 180);
			//var gu = _world.GridUnits.FirstOrDefault(u =>
			//	u.Location.Longitude == 0 && u.Location.Latitude == 1);
			//DrawGU(e, gu);
			//gu = _world.GridUnits.FirstOrDefault(u =>
			//	u.Location.Longitude == 88 && u.Location.Latitude == 1);
			//DrawGU(e, gu);
			//gu = _world.GridUnits.FirstOrDefault(u =>
			//	u.Location.Longitude == -88 && u.Location.Latitude == -17);
			//DrawGU(e, gu);

			foreach (var gu in _world.GridUnits.Values)
			{
				DrawGU(e, gu);
			}
		}

		private void DrawGU(PaintEventArgs e, GridUnit gu)
		{
			if (gu.PlateId == null) return;

			//if (!gu.IsOnFault) return;
			var centerX = e.ClipRectangle.Width / 2;
			var centerY = e.ClipRectangle.Height / 2;

			var hdegSize = (e.ClipRectangle.Width / 360);
			var vdegSize = (e.ClipRectangle.Height / 180);

			var yOffset = gu.Latitude * vdegSize;
			var xOffset = gu.Longitude * hdegSize;

			var xMultiplier = gu.TopPctArc;
			xMultiplier = 1;

			var rect = new Rectangle(
				(int)(centerX + (xOffset * xMultiplier)),
				(int)(centerY - yOffset),
				(int)(hdegSize * xMultiplier),
				(vdegSize));


			//e.Graphics.FillRectangle(_brushes[_plateColors[gu.PlateId ?? 0]], rect);

			//var col = Color.FromArgb((int)(gu.Elevation ?? 0), (int)(gu.Elevation ?? 0), (int)(gu.Elevation ?? 0));
			//var elevationBrush = new SolidBrush(col);

			var brush = gu.CrustType == CrustType.Unknown
			? _brushes[Color.White]
			: gu.IsOnFault
				? _brushes[Color.Black]
				: gu.CrustType == CrustType.Oceanic
					? _brushes[Color.Blue]
					: _brushes[Color.Green];

			e.Graphics.FillRectangle(brush, rect);

			//e.Graphics.DrawRectangle(_blackPen, rect);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_world = new World();
			pictureBox1.Invalidate();
		}
	}
}

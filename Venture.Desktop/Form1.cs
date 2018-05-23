using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Venture.Data.Geography;

namespace Venture.Desktop
{
	public partial class Form1 : Form
	{
		private World _world;
		private Brush _green;
		private Brush _blue;

		public Form1()
		{
			_green = new SolidBrush(Color.Green);
			_blue = new SolidBrush(Color.Blue);

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

			var hdegSize = (e.ClipRectangle.Width / 360) * GridUnit.DegreesOfArc;
			var vdegSize = (e.ClipRectangle.Height / 180) * GridUnit.DegreesOfArc;

			//var gu = _world.GridUnits.FirstOrDefault(u =>
			//	u.Location.Longitude == 0 && u.Location.Latitude == 1);
			//DrawGU(e, gu);
			//gu = _world.GridUnits.FirstOrDefault(u =>
			//	u.Location.Longitude == 88 && u.Location.Latitude == 1);
			//DrawGU(e, gu);
			//gu = _world.GridUnits.FirstOrDefault(u =>
			//	u.Location.Longitude == -88 && u.Location.Latitude == -17);
			//DrawGU(e, gu);

			foreach (var gu in _world.GridUnits)
			{
				DrawGU(e, gu);
				/*
				var topWidth = gu.TopPctArc * hdegSize;
				var topX = gu.Location.Longitude * topWidth;
				var bottomWidth = gu.BottomPctArc * hdegSize;
				var bottomX = gu.Location.Longitude * bottomWidth;
				var topY = gu.Location.Latitude > 0
					? gu.Location.Latitude * vdegSize
					: gu.Location.Latitude - 1 * vdegSize;
				var bottomY = gu.Location.Latitude > 0
					? gu.Location.Latitude - 1 * vdegSize
					: gu.Location.Latitude * vdegSize;

				var tl = new Point((int)(centerX + topX), (int)(centerY + topY));
				var tr = new Point((int)(centerX + topX + hdegSize), (int)(centerY + topY));
				var bl = new Point((int)(centerX + bottomX), (int)(centerY + bottomY));
				var br = new Point((int)(centerX + bottomX + hdegSize), (int)(centerY + bottomY));

				e.Graphics.DrawPolygon(gu.IsLand ? green : blue, new [] {tl, tr, bl, br});
				*/

				/*
				var rect = new Rectangle(
					(int)(gu.Location.Longitude * hdegSize),
					(int)(gu.Location.Latitude * vdegSize),
					1,
					1);

				e.Graphics.DrawRectangle(blue, rect);
				*/
			}
		}

		private void DrawGU(PaintEventArgs e, GridUnit gu)
		{
			var centerX = e.ClipRectangle.Width / 2;
			var centerY = e.ClipRectangle.Height / 2;

			var hdegSize = (e.ClipRectangle.Width / 360);
			var vdegSize = (e.ClipRectangle.Height / 180);

			var rect = new Rectangle(
				(int)(centerX + (gu.Location.Longitude * hdegSize * gu.TopPctArc)),
				(int)(centerY + (gu.Location.Latitude * vdegSize)),
				(int)(hdegSize * gu.TopPctArc * GridUnit.DegreesOfArc),
				vdegSize * GridUnit.DegreesOfArc);

			e.Graphics.FillRectangle(gu.IsLand ? _green : _blue, rect);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_world = new World();
			pictureBox1.Invalidate();
		}
	}
}

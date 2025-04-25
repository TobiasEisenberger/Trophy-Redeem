using System.Windows;
using System.Windows.Shapes;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.components.collision
{

    public class Collider : Component
    {

        public double Length { get; private set; }
        public Point Start { get; private set; }

        public Collider(Polyline shape)
        {
            Init(shape);
        }

        public Collider(Line shape)
        {
            Init(shape);
        }

        private void Init(Shape shape)
        {
            elementList.Add(shape);
            double length = 0.0;
            Point start = new Point();

            if (shape is Line)
            {
                var line = (Line)shape;
                start.X = line.X1; start.Y = line.Y1;
                length = (new Point(line.X1, line.Y1) - new Point(line.X2, line.Y2)).Length;
            }

            if (shape is Polyline)
            {
                var polyline = (Polyline)shape;
                var points = polyline.Points;
                start = points[0];
                for (int i = 0; i < points.Count - 1; i++)
                {
                    length += (points[i + 1] - points[i]).Length;
                }
            }

            Length = length;
            Start = start;
        }

    }
}

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Trophy_Redeem.src.components.collision
{

    public abstract class CollisionLayer
    {

        public readonly List<Collider> ground = new List<Collider>();
        public readonly List<Collider> wall = new List<Collider>();
        public readonly List<Collider> obstacle = new List<Collider>();
        private bool isDebugRun = false;

        protected CollisionLayer(bool debugRun)
        {
            isDebugRun = debugRun;
        }

        protected abstract void BuildGroundCollider();
        protected abstract void BuildWallCollider();
        

        protected Line BuildLine(Point p1, Point p2, Brush? stroke)
        {
            return new Line()
            {
                X1 = p1.X,
                X2 = p2.X,
                Y1 = p1.Y,
                Y2 = p2.Y,
                Stroke = isDebugRun ? stroke : null,
            };
        }

        protected Polyline BuildPolyline(PointCollection points, Brush? stroke)
        {
            return new Polyline()
            {
                Points = points,
                Stroke = isDebugRun ? stroke : null,
            };
        }

    }

}

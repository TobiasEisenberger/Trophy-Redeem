using System.Windows.Media;
using System.Collections.Generic;
using System.Windows;

namespace Trophy_Redeem.src.components.collision
{

    public enum CollisionDetail
    {
        None,
        Left,
        Right,
        Top,
    }

    public class CollisionDetector
    {

        CollisionLayer layer;

        public CollisionDetector(CollisionLayer layer)
        {
            this.layer = layer;
        }

        public static CollisionDetail CollidesWithDetail(Rect source, Rect target)
        {
            var collisionDetail = CollisionDetail.None;
            var intersection = source;
            intersection.Intersect(target);
            if (!intersection.IsEmpty)
            {
                if (source.Left < target.Left)
                {
                    collisionDetail = CollisionDetail.Left;
                }
                if (source.Right > target.Right)
                {
                    collisionDetail = CollisionDetail.Right;
                }
                // Collision on top counts only for 20% of target height
                if (intersection.Top == target.Top && intersection.Height <= (target.Height * 0.2))
                {
                    collisionDetail = CollisionDetail.Top;
                }
            }
            return collisionDetail;
        }

        public static bool Collides(Geometry geometry, Collider collider)
        {
            var shape = collider.GetElements()[0];
            var hitTest = shape.RenderedGeometry.FillContainsWithDetail(geometry);
            return hitTest == IntersectionDetail.Intersects;
        }

        public List<Collider> CollidesWithGround(Geometry geometry)
        {
            return Collides(layer.ground, geometry);
        }

        public List<Collider> CollidesWithWall(Geometry geometry)
        {
            return Collides(layer.wall, geometry);
        }

        private List<Collider> Collides(List<Collider> colliders, Geometry geometry)
        {
            List<Collider> collisionHit = new List<Collider>();
            foreach (var collider in colliders)
            {
                var shape = collider.GetElements()[0];
                var hitTest = shape.RenderedGeometry.FillContainsWithDetail(geometry);
                if (hitTest == IntersectionDetail.Intersects)
                {
                    collisionHit.Add(collider);
                }
            }
            return collisionHit;
        }

    }
}

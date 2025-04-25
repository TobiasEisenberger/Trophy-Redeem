using System.Windows;
using System.Windows.Media;

namespace Trophy_Redeem.src.components.collision
{
    public class LevelThreeCollisionLayer : CollisionLayer
    {

        public LevelThreeCollisionLayer(bool debugRun = false) : base(debugRun)
        {
            BuildGroundCollider();
            BuildWallCollider();
            BuildObstacleCollider();
        }

        protected override void BuildGroundCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            ground.Add(new Collider(BuildLine(new Point(0, 192), new Point(96, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(96, 224), new Point(112, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(112, 256), new Point(128, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(128, 272), new Point(288, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(144, 192), new Point(224, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(224, 176), new Point(288, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(320, 272), new Point(368, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(336, 144), new Point(448, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(368, 256), new Point(384, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(384, 240), new Point(512, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(496, 160), new Point(576, 160), stroke)));
            ground.Add(new Collider(BuildLine(new Point(512, 224), new Point(576, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(576, 256), new Point(592, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(592, 288), new Point(656, 288), stroke)));
            ground.Add(new Collider(BuildLine(new Point(608, 192), new Point(688, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(688, 272), new Point(816, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(720, 176), new Point(784, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(784, 192), new Point(832, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(800, 144), new Point(848, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(816, 256), new Point(848, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(848, 240), new Point(912, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(864, 144), new Point(960, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(912, 288), new Point(1040, 288), stroke)));
            ground.Add(new Collider(BuildLine(new Point(944, 224), new Point(960, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(960, 208), new Point(1040, 208), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1056, 288), new Point(1072, 288), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1072, 272), new Point(1088, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1072, 192), new Point(1120, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1088, 256), new Point(1136, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1136, 240), new Point(1152, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1152, 224), new Point(1216, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1216, 256), new Point(1440, 256), stroke)));
        }

        protected override void BuildWallCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            wall.Add(new Collider(BuildLine(new Point(96, 194), new Point(96, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(112, 226), new Point(112, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(128, 272), new Point(128, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(144, 196), new Point(144, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(144, 224), new Point(288, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(128, 260), new Point(128, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(224, 192), new Point(224, 180), stroke)));
            wall.Add(new Collider(BuildLine(new Point(288, 224), new Point(288, 184), stroke)));
            wall.Add(new Collider(BuildLine(new Point(288, 276), new Point(288, 320), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(320, 276),
                new Point(320, 278),
                new Point(324, 282),
                new Point(336, 282),
                new Point(336, 320),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(336, 148),
                new Point(336, 176),
                new Point(384, 176),
                new Point(384, 192),
                new Point(430, 192),
                new Point(430, 176),
                new Point(448, 176),
                new Point(448, 148),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(368, 272), new Point(368, 260), stroke)));
            wall.Add(new Collider(BuildLine(new Point(384, 256), new Point(384, 242), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(496, 162),
                new Point(496, 166),
                new Point(500, 170),
                new Point(572, 170),
                new Point(576, 166),
                new Point(576, 162),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(512, 240), new Point(512, 226), stroke)));
            wall.Add(new Collider(BuildLine(new Point(576, 228), new Point(576, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(592, 260), new Point(592, 288), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(608, 196),
                new Point(608, 224),
                new Point(640, 224),
                new Point(640, 240),
                new Point(670, 240),
                new Point(670, 224),
                new Point(688, 224),
                new Point(688, 196),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(656, 292), new Point(656, 320), stroke)));
            wall.Add(new Collider(BuildLine(new Point(688, 276), new Point(688, 320), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(720, 180),
                new Point(720, 182),
                new Point(724, 186),
                new Point(736, 186),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(736, 186), new Point(736, 208), stroke)));
            wall.Add(new Collider(BuildLine(new Point(736, 208), new Point(784, 208), stroke)));
            wall.Add(new Collider(BuildLine(new Point(784, 192), new Point(784, 180), stroke)));
            wall.Add(new Collider(BuildLine(new Point(784, 208), new Point(784, 202), stroke)));
            wall.Add(new Collider(BuildLine(new Point(784, 202), new Point(832, 202), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(800, 148),
                new Point(800, 150),
                new Point(804, 154),
                new Point(844, 154),
                new Point(848, 150),
                new Point(848, 148),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(816, 272), new Point(816, 258), stroke)));
            wall.Add(new Collider(BuildLine(new Point(832, 202), new Point(832, 196), stroke)));
            wall.Add(new Collider(BuildLine(new Point(848, 256), new Point(848, 242), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(864, 148),
                new Point(864, 176),
                new Point(960, 176),
                new Point(960, 148),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(912, 288), new Point(912, 244), stroke)));
            wall.Add(new Collider(BuildLine(new Point(944, 228), new Point(944, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(944, 256), new Point(992, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(960, 224), new Point(960, 212), stroke)));
            wall.Add(new Collider(BuildLine(new Point(992, 256), new Point(992, 240), stroke)));
            wall.Add(new Collider(BuildLine(new Point(992, 240), new Point(1040, 240), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1038, 298), new Point(1038, 320), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1040, 240), new Point(1040, 212), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1058, 298), new Point(1058, 320), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1072, 288), new Point(1072, 274), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(1072, 196),
                new Point(1072, 198),
                new Point(1076, 202),
                new Point(1116, 202),
                new Point(1120, 198),
                new Point(1120, 196),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(1088, 272), new Point(1088, 260), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1136, 256), new Point(1136, 242), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1152, 240), new Point(1152, 228), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1216, 228), new Point(1216, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(0, 80), new Point(80, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(80, 80), new Point(80, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(80, 96), new Point(160, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(160, 96), new Point(160, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(160, 64), new Point(224, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(224, 64), new Point(224, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(224, 80), new Point(272, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(272, 80), new Point(272, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(272, 64), new Point(384, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(384, 64), new Point(384, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(384, 80), new Point(464, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(464, 80), new Point(464, 48), stroke)));
            wall.Add(new Collider(BuildLine(new Point(464, 48), new Point(512, 48), stroke)));
            wall.Add(new Collider(BuildLine(new Point(512, 48), new Point(512, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(512, 128), new Point(560, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(560, 128), new Point(560, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(560, 80), new Point(608, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(608, 80), new Point(608, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(608, 64), new Point(736, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(736, 64), new Point(736, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(736, 80), new Point(784, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(784, 80), new Point(784, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(784, 64), new Point(864, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(864, 64), new Point(864, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(864, 80), new Point(912, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(912, 80), new Point(912, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(912, 64), new Point(1008, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1008, 64), new Point(1008, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1008, 80), new Point(1024, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1024, 80), new Point(1024, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1024, 96), new Point(1040, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1040, 96), new Point(1040, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1040, 128), new Point(1088, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1088, 128), new Point(1088, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1088, 80), new Point(1104, 80), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1104, 80), new Point(1104, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1104, 64), new Point(1184, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1184, 64), new Point(1184, 112), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1184, 112), new Point(1216, 112), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1216, 112), new Point(1216, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1216, 64), new Point(1232, 64), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1232, 64), new Point(1232, 48), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1232, 48), new Point(1296, 48), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1296, 48), new Point(1296, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1296, 96), new Point(1376, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1376, 96), new Point(1376, 112), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1376, 112), new Point(1440, 112), stroke)));
        }

        protected void BuildObstacleCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));

            obstacle.Add(new Collider(BuildLine(new Point(192, 224), new Point(192, 236), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(192, 236), new Point(239, 236), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(239, 236), new Point(239, 224), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(288, 320), new Point(288, 308), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(288, 308), new Point(336, 308), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(336, 308), new Point(336, 320), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(336, 145), new Point(324, 145), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(324, 145), new Point(324, 175), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(324, 175), new Point(336, 175), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(385, 192), new Point(385, 206), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(385, 206), new Point(415, 206), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(415, 206), new Point(415, 192), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(400, 146), new Point(400, 132), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(400, 132), new Point(432, 132), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(432, 132), new Point(432, 146), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(450, 241), new Point(450, 228), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(450, 228), new Point(464, 228), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(464, 228), new Point(464, 241), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(512, 96), new Point(500, 96), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(500, 96), new Point(500, 128), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(500, 128), new Point(512, 128), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(592, 288), new Point(592, 276), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(592, 276), new Point(608, 276), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(608, 276), new Point(608, 288), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(656, 320), new Point(656, 308), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(656, 308), new Point(688, 308), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(688, 308), new Point(688, 320), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(656, 194), new Point(656, 180), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(656, 180), new Point(672, 180), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(672, 180), new Point(672, 194), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(721, 273), new Point(721, 260), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(721, 260), new Point(736, 260), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(736, 260), new Point(736, 273), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(753, 209), new Point(753, 221), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(753, 221), new Point(782, 221), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(782, 221), new Point(782, 209), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(833, 145), new Point(833, 132), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(833, 132), new Point(848, 132), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(848, 132), new Point(848, 145), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(866, 241), new Point(866, 228), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(866, 228), new Point(880, 228), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(880, 228), new Point(880, 241), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(881, 145), new Point(881, 132), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(881, 132), new Point(912, 132), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(912, 132), new Point(912, 145), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(994, 210), new Point(994, 196), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(994, 196), new Point(1024, 196), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1024, 196), new Point(1024, 210), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(1009, 241), new Point(1009, 253), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1009, 253), new Point(1023, 253), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1023, 253), new Point(1023, 241), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(1040, 320), new Point(1040, 308), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1040, 308), new Point(1056, 308), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1056, 308), new Point(1056, 320), stroke)));

            obstacle.Add(new Collider(BuildLine(new Point(1074, 273), new Point(1074, 260), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1074, 260), new Point(1089, 260), stroke)));
            obstacle.Add(new Collider(BuildLine(new Point(1089, 260), new Point(1089, 273), stroke)));
        }

    }
}

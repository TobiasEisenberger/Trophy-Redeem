using System.Windows;
using System.Windows.Media;

namespace Trophy_Redeem.src.components.collision
{

    public class LevelOneCollisionLayer : CollisionLayer
    {

        public Collider CanyonCollider { get; set; }

        public LevelOneCollisionLayer(bool debugRun = false) : base(debugRun)
        {
            BuildGroundCollider();
            BuildWallCollider();
            BuildCanyonCollider();
        }

        protected override void BuildGroundCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            ground.Add(new Collider(BuildLine(new Point(0, 144), new Point(32, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(32, 224), new Point(64, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(64, 240), new Point(176, 240), stroke)));
            ground.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(28, 148),
                new Point(64, 112),
                new Point(96, 112),
            }, stroke)));
            ground.Add(new Collider(BuildLine(new Point(176, 144), new Point(208, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(208, 272), new Point(272, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(224, 192), new Point(256, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(240, 112), new Point(272, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(288, 240), new Point(336, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(272, 256), new Point(288, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(304, 176), new Point(320, 176), stroke)));
            ground.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(318, 178),
                new Point(336, 160),
                new Point(384, 160),
            }, stroke)));
            ground.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(320, 80),
                new Point(336, 80),
                new Point(368, 64),
                new Point(384, 64),
                new Point(416, 80),
                new Point(480, 80),
            }, stroke)));
            ground.Add(new Collider(BuildLine(new Point(384, 176), new Point(400, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(400, 208), new Point(422, 230), stroke)));
            ground.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(416, 224),
                new Point(496, 224),
                new Point(564, 260),
            }, stroke)));
            ground.Add(new Collider(BuildLine(new Point(528, 80), new Point(560, 80), stroke)));
            ground.Add(new Collider(BuildLine(new Point(560, 256), new Point(608, 256), stroke)));
            ground.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(604, 260),
                new Point(704, 208),
                new Point(720, 208),
            }, stroke)));
            ground.Add(new Collider(BuildLine(new Point(624, 96), new Point(656, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(704, 64), new Point(736, 64), stroke)));
            ground.Add(new Collider(BuildLine(new Point(720, 192), new Point(752, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(752, 176), new Point(768, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(768, 160), new Point(864, 160), stroke)));
            ground.Add(new Collider(BuildLine(new Point(768, 96), new Point(800, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(864, 176), new Point(896, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(944, 176), new Point(992, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1040, 144), new Point(1056, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1056, 128), new Point(1104, 128), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1104, 144), new Point(1120, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1152, 176), new Point(1200, 176), stroke)));
            ground.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(1232, 176),
                new Point(1280, 176),
                new Point(1312, 160),
                new Point(1328, 160),
                new Point(1360, 176),
                new Point(1440, 176),
            }, stroke)));
        }

        protected override void BuildWallCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(96, 112),
                new Point(96, 150.75),
                new Point(56.56, 192.13),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(56.56, 192.13), new Point(31.2, 192), stroke)));
            wall.Add(new Collider(BuildLine(new Point(31.2, 192), new Point(31.2, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(64, 226), new Point(64, 240), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(176, 146.55),
                new Point(176, 165.65),
                new Point(183, 175.55),
                new Point(200, 175.55),
                new Point(208, 165.65),
                new Point(208, 146.55),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(176, 240), new Point(176, 280), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(240, 0),
                new Point(240, 6),
                new Point(264, 32),
                new Point(296, 32),
                new Point(320, 6),
                new Point(320, 0),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(320, 82),
                new Point(320, 102),
                new Point(327, 112),
                new Point(470, 112),
                new Point(479, 102),
                new Point(479, 82),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(224, 194),
                new Point(224, 214),
                new Point(232, 224),
                new Point(248, 224),
                new Point(256, 214),
                new Point(256, 194),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(240, 114),
                new Point(240, 134),
                new Point(248, 144),
                new Point(264, 144),
                new Point(272, 134),
                new Point(272, 114),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(272, 272), new Point(272, 258), stroke)));
            wall.Add(new Collider(BuildLine(new Point(288, 256), new Point(288, 242), stroke)));
            wall.Add(new Collider(BuildLine(new Point(336, 240), new Point(336, 208), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(336, 208),
                new Point(312, 208),
                new Point(304, 198),
                new Point(304, 178),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(384, 176), new Point(384, 162), stroke)));
            wall.Add(new Collider(BuildLine(new Point(400, 178), new Point(400, 208), stroke)));
            wall.Add(new Collider(BuildLine(new Point(752, 192), new Point(752, 178), stroke)));
            wall.Add(new Collider(BuildLine(new Point(768, 176), new Point(768, 162), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(528, 82),
                new Point(528, 102),
                new Point(536, 112),
                new Point(552, 112),
                new Point(560, 102),
                new Point(560, 82),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(624, 98),
                new Point(624, 118),
                new Point(632, 128),
                new Point(648, 128),
                new Point(656, 118),
                new Point(656, 98),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(704, 66),
                new Point(704, 86),
                new Point(712, 96),
                new Point(728, 96),
                new Point(736, 86),
                new Point(736, 66),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(720, 208), new Point(720, 194), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(768, 98),
                new Point(768, 118),
                new Point(776, 128),
                new Point(792, 128),
                new Point(800, 118),
                new Point(800, 98),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(864, 176), new Point(864, 162), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(896, 178),
                new Point(896, 262),
                new Point(886, 272),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(944, 178),
                new Point(944, 214),
                new Point(952, 224),
                new Point(984, 224),
                new Point(992, 214),
                new Point(992, 178),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(1040, 146),
                new Point(1040, 166),
                new Point(1078, 208),
                new Point(1096, 208),
                new Point(1120, 182),
                new Point(1120, 146),
            }, stroke)));
            wall.Add(new Collider(BuildLine(new Point(1056, 144), new Point(1056, 130), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1104, 144), new Point(1104, 130), stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(1152, 178),
                new Point(1152, 262),
                new Point(1160, 272),
                new Point(1192, 272),
                new Point(1200, 262),
                new Point(1200, 178),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(1216, 0),
                new Point(1216, 6),
                new Point(1240, 32),
                new Point(1264, 32),
                new Point(1264, 38),
                new Point(1320, 96),
                new Point(1356, 96),
                new Point(1370, 78),
                new Point(1440, 78),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(1232, 178),
                new Point(1232, 198),
                new Point(1240, 208),
                new Point(1248, 208),
                new Point(1248, 230),
                new Point(1272, 256),
            }, stroke)));
            wall.Add(new Collider(BuildPolyline(new PointCollection()
            {
                new Point(816, 0),
                new Point(816, 6),
                new Point(856, 46),
                new Point(880, 48),
                new Point(920, 94),
                new Point(968, 96),
                new Point(982, 80),
                new Point(1000, 78),
                new Point(1008, 70),
                new Point(1008, 48),
                new Point(1024, 38),
                new Point(1024, 32),
                new Point(1046, 32),
                new Point(1056, 22),
                new Point(1080, 16),
                new Point(1088, 6),
                new Point(1088, 0),
            }, stroke)));
        }

        private void BuildCanyonCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            CanyonCollider = new Collider(BuildPolyline(new PointCollection()
            {
                new Point(0, 282),
                new Point(872, 282),
                new Point(942, 254),
                new Point(996, 274),
                new Point(1026, 260),
                new Point(1062, 262),
                new Point(1072, 276),
                new Point(1178, 280),
                new Point(1258, 256),
                new Point(1318, 272),
                new Point(1346, 256),
                new Point(1384, 262),
                new Point(1392, 278),
                new Point(1440, 280),
            }, stroke));
        }

    }
}

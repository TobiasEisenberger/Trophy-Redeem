using System.Windows;
using System.Windows.Media;

namespace Trophy_Redeem.src.components.collision
{

    public class LevelTwoCollisionLayer : CollisionLayer
    {

        public Collider CanyonCollider { get; set; }

        public LevelTwoCollisionLayer(bool debugRun = false) : base(debugRun)
        {
            BuildGroundCollider();
            BuildWallCollider();
            BuildCanyonCollider();
        }

        protected override void BuildGroundCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            ground.Add(new Collider(BuildLine(new Point(0, 224), new Point(32, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(32, 240), new Point(112, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(46, 144), new Point(80, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(80, 128), new Point(96, 128), stroke)));
            ground.Add(new Collider(BuildLine(new Point(96, 112), new Point(160, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(112, 208), new Point(160, 208), stroke)));
            ground.Add(new Collider(BuildLine(new Point(160, 224), new Point(192, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(192, 256), new Point(320, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(208, 160), new Point(224, 160), stroke)));
            ground.Add(new Collider(BuildLine(new Point(224, 144), new Point(272, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(288, 176), new Point(304, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(304, 112), new Point(336, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(352, 240), new Point(384, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(368, 304), new Point(384, 304), stroke)));
            ground.Add(new Collider(BuildLine(new Point(368, 192), new Point(432, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(368, 96), new Point(400, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(400, 80), new Point(448, 80), stroke)));
            ground.Add(new Collider(BuildLine(new Point(464, 272), new Point(528, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(464, 208), new Point(480, 208), stroke)));
            ground.Add(new Collider(BuildLine(new Point(480, 192), new Point(496, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(496, 176), new Point(528, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(528, 288), new Point(544, 288), stroke)));
            ground.Add(new Collider(BuildLine(new Point(528, 192), new Point(560, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(528, 128), new Point(544, 128), stroke)));
            ground.Add(new Collider(BuildLine(new Point(544, 112), new Point(656, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(672, 112), new Point(704, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(576, 272), new Point(592, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(592, 240), new Point(606, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(608, 224), new Point(720, 224), stroke)));
            ground.Add(new Collider(BuildLine(new Point(656, 96), new Point(672, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(718, 256), new Point(736, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(736, 272), new Point(800, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(752, 192), new Point(768, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(752, 64), new Point(816, 64), stroke)));
            ground.Add(new Collider(BuildLine(new Point(768, 176), new Point(848, 176), stroke)));
            ground.Add(new Collider(BuildLine(new Point(800, 288), new Point(848, 288), stroke)));
            ground.Add(new Collider(BuildLine(new Point(816, 96), new Point(880, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(848, 192), new Point(880, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(880, 208), new Point(896, 208), stroke)));
            ground.Add(new Collider(BuildLine(new Point(880, 64), new Point(896, 64), stroke)));
            ground.Add(new Collider(BuildLine(new Point(896, 272), new Point(912, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(912, 256), new Point(928, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(928, 240), new Point(1024, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(960, 160), new Point(976, 160), stroke)));
            ground.Add(new Collider(BuildLine(new Point(976, 144), new Point(992, 144), stroke)));
            ground.Add(new Collider(BuildLine(new Point(992, 128), new Point(1056, 128), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1072, 272), new Point(1088, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1088, 160), new Point(1104, 160), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1088, 256), new Point(1104, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1104, 240), new Point(1120, 240), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1120, 208), new Point(1296, 208), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1120, 112), new Point(1200, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1200, 96), new Point(1280, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1296, 192), new Point(1312, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1312, 256), new Point(1328, 256), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1328, 272), new Point(1344, 272), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1344, 112), new Point(1376, 112), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1360, 192), new Point(1424, 192), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1376, 96), new Point(1408, 96), stroke)));
            ground.Add(new Collider(BuildLine(new Point(1424, 176), new Point(1440, 176), stroke)));
        }

        protected override void BuildWallCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(0, 255, 0));

            wall.Add(new Collider(BuildLine(new Point(32, 224), new Point(32, 240), stroke)));

            wall.Add(new Collider(BuildLine(new Point(48, 145), new Point(50, 161), stroke)));
            wall.Add(new Collider(BuildLine(new Point(50, 161), new Point(60, 174), stroke)));
            wall.Add(new Collider(BuildLine(new Point(60, 174), new Point(76, 174), stroke)));
            wall.Add(new Collider(BuildLine(new Point(76, 174), new Point(86, 172), stroke)));
            wall.Add(new Collider(BuildLine(new Point(86, 172), new Point(111, 142), stroke)));
            wall.Add(new Collider(BuildLine(new Point(111, 142), new Point(139, 143), stroke)));
            wall.Add(new Collider(BuildLine(new Point(139, 143), new Point(157, 132), stroke)));
            wall.Add(new Collider(BuildLine(new Point(157, 132), new Point(160, 113), stroke)));

            wall.Add(new Collider(BuildLine(new Point(80, 128), new Point(80, 144), stroke)));
            wall.Add(new Collider(BuildLine(new Point(96, 112), new Point(96, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(112, 208), new Point(112, 240), stroke)));
            wall.Add(new Collider(BuildLine(new Point(160, 208), new Point(160, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(192, 224), new Point(192, 256), stroke)));

            wall.Add(new Collider(BuildLine(new Point(208, 161), new Point(210, 178), stroke)));
            wall.Add(new Collider(BuildLine(new Point(210, 178), new Point(220, 190), stroke)));
            wall.Add(new Collider(BuildLine(new Point(220, 190), new Point(261, 190), stroke)));
            wall.Add(new Collider(BuildLine(new Point(261, 190), new Point(271, 176), stroke)));
            wall.Add(new Collider(BuildLine(new Point(271, 176), new Point(272, 144), stroke)));

            wall.Add(new Collider(BuildLine(new Point(224, 145), new Point(226, 160), stroke)));

            wall.Add(new Collider(BuildLine(new Point(288, 176), new Point(288, 192), stroke)));
            wall.Add(new Collider(BuildLine(new Point(288, 192), new Point(304, 192), stroke)));
            wall.Add(new Collider(BuildLine(new Point(304, 192), new Point(304, 176), stroke)));

            wall.Add(new Collider(BuildLine(new Point(304, 112), new Point(304, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(304, 128), new Point(336, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(336, 128), new Point(336, 112), stroke)));

            wall.Add(new Collider(BuildLine(new Point(320, 260), new Point(320, 320), stroke)));

            wall.Add(new Collider(BuildLine(new Point(352, 240), new Point(354, 257), stroke)));
            wall.Add(new Collider(BuildLine(new Point(354, 257), new Point(380, 286), stroke)));
            wall.Add(new Collider(BuildLine(new Point(354, 257), new Point(380, 286), stroke)));
            wall.Add(new Collider(BuildLine(new Point(380, 286), new Point(386, 286), stroke)));
            wall.Add(new Collider(BuildLine(new Point(386, 286), new Point(386, 304), stroke)));

            wall.Add(new Collider(BuildLine(new Point(368, 305), new Point(369, 319), stroke)));

            wall.Add(new Collider(BuildLine(new Point(368, 193), new Point(370, 209), stroke)));
            wall.Add(new Collider(BuildLine(new Point(370, 209), new Point(380, 221), stroke)));
            wall.Add(new Collider(BuildLine(new Point(380, 221), new Point(386, 222), stroke)));
            wall.Add(new Collider(BuildLine(new Point(386, 222), new Point(387, 239), stroke)));

            wall.Add(new Collider(BuildLine(new Point(432, 193), new Point(431, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(431, 224), new Point(422, 236), stroke)));
            wall.Add(new Collider(BuildLine(new Point(422, 236), new Point(415, 236), stroke)));
            wall.Add(new Collider(BuildLine(new Point(415, 236), new Point(415, 319), stroke)));

            wall.Add(new Collider(BuildLine(new Point(368, 97), new Point(370, 114), stroke)));
            wall.Add(new Collider(BuildLine(new Point(370, 114), new Point(395, 139), stroke)));
            wall.Add(new Collider(BuildLine(new Point(395, 139), new Point(438, 140), stroke)));
            wall.Add(new Collider(BuildLine(new Point(438, 140), new Point(447, 127), stroke)));
            wall.Add(new Collider(BuildLine(new Point(447, 127), new Point(447, 81), stroke)));

            wall.Add(new Collider(BuildLine(new Point(400, 81), new Point(402, 96), stroke)));

            wall.Add(new Collider(BuildLine(new Point(466, 209), new Point(466, 226), stroke)));
            wall.Add(new Collider(BuildLine(new Point(466, 226), new Point(475, 237), stroke)));
            wall.Add(new Collider(BuildLine(new Point(475, 237), new Point(509, 237), stroke)));
            wall.Add(new Collider(BuildLine(new Point(509, 237), new Point(518, 236), stroke)));
            wall.Add(new Collider(BuildLine(new Point(518, 236), new Point(527, 222), stroke)));
            wall.Add(new Collider(BuildLine(new Point(527, 222), new Point(541, 222), stroke)));
            wall.Add(new Collider(BuildLine(new Point(541, 222), new Point(551, 219), stroke)));
            wall.Add(new Collider(BuildLine(new Point(551, 219), new Point(559, 207), stroke)));
            wall.Add(new Collider(BuildLine(new Point(559, 207), new Point(560, 193), stroke)));

            wall.Add(new Collider(BuildLine(new Point(464, 274), new Point(466, 289), stroke)));
            wall.Add(new Collider(BuildLine(new Point(466, 289), new Point(476, 301), stroke)));
            wall.Add(new Collider(BuildLine(new Point(476, 301), new Point(482, 301), stroke)));
            wall.Add(new Collider(BuildLine(new Point(482, 301), new Point(483, 319), stroke)));

            wall.Add(new Collider(BuildLine(new Point(480, 192), new Point(482, 208), stroke)));
            wall.Add(new Collider(BuildLine(new Point(496, 176), new Point(498, 192), stroke)));
            wall.Add(new Collider(BuildLine(new Point(528, 176), new Point(527, 192), stroke)));

            wall.Add(new Collider(BuildLine(new Point(528, 273), new Point(528, 288), stroke)));
            wall.Add(new Collider(BuildLine(new Point(544, 290), new Point(544, 319), stroke)));

            wall.Add(new Collider(BuildLine(new Point(528, 128), new Point(528, 144), stroke)));
            wall.Add(new Collider(BuildLine(new Point(528, 144), new Point(592, 144), stroke)));
            wall.Add(new Collider(BuildLine(new Point(592, 144), new Point(592, 128), stroke)));

            wall.Add(new Collider(BuildLine(new Point(544, 112), new Point(544, 126), stroke)));

            wall.Add(new Collider(BuildLine(new Point(576, 272), new Point(578, 289), stroke)));
            wall.Add(new Collider(BuildLine(new Point(578, 289), new Point(588, 302), stroke)));
            wall.Add(new Collider(BuildLine(new Point(588, 302), new Point(594, 302), stroke)));
            wall.Add(new Collider(BuildLine(new Point(594, 302), new Point(594, 320), stroke)));

            wall.Add(new Collider(BuildLine(new Point(592, 240), new Point(594, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(608, 224), new Point(610, 240), stroke)));

            wall.Add(new Collider(BuildLine(new Point(642, 117), new Point(642, 130), stroke)));
            wall.Add(new Collider(BuildLine(new Point(642, 130), new Point(651, 141), stroke)));
            wall.Add(new Collider(BuildLine(new Point(651, 141), new Point(684, 142), stroke)));
            wall.Add(new Collider(BuildLine(new Point(684, 142), new Point(694, 139), stroke)));
            wall.Add(new Collider(BuildLine(new Point(694, 139), new Point(703, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(703, 128), new Point(703, 113), stroke)));

            wall.Add(new Collider(BuildLine(new Point(656, 96), new Point(656, 112), stroke)));
            wall.Add(new Collider(BuildLine(new Point(672, 96), new Point(672, 112), stroke)));

            wall.Add(new Collider(BuildLine(new Point(720, 224), new Point(720, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(736, 256), new Point(735, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(800, 272), new Point(799, 289), stroke)));
            wall.Add(new Collider(BuildLine(new Point(848, 288), new Point(847, 320), stroke)));

            wall.Add(new Collider(BuildLine(new Point(752, 64), new Point(754, 81), stroke)));
            wall.Add(new Collider(BuildLine(new Point(754, 81), new Point(764, 94), stroke)));
            wall.Add(new Collider(BuildLine(new Point(764, 94), new Point(786, 94), stroke)));
            wall.Add(new Collider(BuildLine(new Point(786, 94), new Point(812, 126), stroke)));
            wall.Add(new Collider(BuildLine(new Point(812, 126), new Point(876, 126), stroke)));
            wall.Add(new Collider(BuildLine(new Point(816, 64), new Point(815, 96), stroke)));
            wall.Add(new Collider(BuildLine(new Point(876, 126), new Point(886, 124), stroke)));
            wall.Add(new Collider(BuildLine(new Point(886, 124), new Point(895, 112), stroke)));
            wall.Add(new Collider(BuildLine(new Point(895, 112), new Point(896, 64), stroke)));

            wall.Add(new Collider(BuildLine(new Point(768, 176), new Point(770, 192), stroke)));
            wall.Add(new Collider(BuildLine(new Point(848, 176), new Point(847, 192), stroke)));
            wall.Add(new Collider(BuildLine(new Point(880, 192), new Point(879, 208), stroke)));

            wall.Add(new Collider(BuildLine(new Point(880, 95), new Point(880, 64), stroke)));

            wall.Add(new Collider(BuildLine(new Point(752, 192), new Point(754, 210), stroke)));
            wall.Add(new Collider(BuildLine(new Point(754, 210), new Point(764, 221), stroke)));
            wall.Add(new Collider(BuildLine(new Point(764, 221), new Point(802, 222), stroke)));
            wall.Add(new Collider(BuildLine(new Point(802, 222), new Point(812, 237), stroke)));
            wall.Add(new Collider(BuildLine(new Point(812, 237), new Point(876, 239), stroke)));
            wall.Add(new Collider(BuildLine(new Point(876, 239), new Point(891, 231), stroke)));
            wall.Add(new Collider(BuildLine(new Point(891, 231), new Point(895, 223), stroke)));
            wall.Add(new Collider(BuildLine(new Point(895, 223), new Point(896, 209), stroke)));

            wall.Add(new Collider(BuildLine(new Point(896, 272), new Point(899, 290), stroke)));
            wall.Add(new Collider(BuildLine(new Point(899, 290), new Point(908, 302), stroke)));
            wall.Add(new Collider(BuildLine(new Point(908, 302), new Point(914, 302), stroke)));
            wall.Add(new Collider(BuildLine(new Point(914, 302), new Point(914, 319), stroke)));

            wall.Add(new Collider(BuildLine(new Point(912, 256), new Point(913, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(928, 240), new Point(930, 256), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1024, 240), new Point(1023, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1023, 256), new Point(1014, 268), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1014, 268), new Point(1007, 268), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1007, 268), new Point(1007, 320), stroke)));

            wall.Add(new Collider(BuildLine(new Point(960, 160), new Point(961, 171), stroke)));
            wall.Add(new Collider(BuildLine(new Point(961, 171), new Point(972, 190), stroke)));
            wall.Add(new Collider(BuildLine(new Point(972, 190), new Point(987, 191), stroke)));
            wall.Add(new Collider(BuildLine(new Point(987, 191), new Point(998, 188), stroke)));
            wall.Add(new Collider(BuildLine(new Point(998, 188), new Point(1023, 160), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1023, 160), new Point(1046, 155), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1046, 155), new Point(1055, 144), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1055, 144), new Point(1056, 128), stroke)));

            wall.Add(new Collider(BuildLine(new Point(976, 144), new Point(978, 160), stroke)));
            wall.Add(new Collider(BuildLine(new Point(992, 128), new Point(994, 144), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1088, 160), new Point(1088, 176), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1088, 176), new Point(1104, 176), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1104, 176), new Point(1104, 160), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1088, 256), new Point(1090, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1104, 240), new Point(1106, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1120, 209), new Point(1123, 240), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1072, 272), new Point(1074, 288), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1074, 288), new Point(1074, 288), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1074, 288), new Point(1084, 301), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1084, 301), new Point(1106, 301), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1106, 301), new Point(1106, 320), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1120, 112), new Point(1122, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1122, 128), new Point(1148, 158), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1148, 158), new Point(1164, 158), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1164, 158), new Point(1180, 150), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1180, 150), new Point(1186, 141), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1186, 141), new Point(1212, 143), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1212, 143), new Point(1227, 135), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1227, 135), new Point(1232, 125), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1232, 125), new Point(1261, 126), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1261, 126), new Point(1275, 119), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1275, 119), new Point(1279, 114), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1279, 114), new Point(1280, 96), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1200, 96), new Point(1202, 112), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1296, 192), new Point(1296, 208), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1312, 192), new Point(1312, 208), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1312, 208), new Point(1311, 256), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1328, 256), new Point(1327, 272), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1344, 272), new Point(1343, 320), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1376, 96), new Point(1378, 112), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1344, 112), new Point(1346, 129), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1346, 129), new Point(1356, 141), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1356, 141), new Point(1388, 142), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1388, 142), new Point(1398, 139), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1398, 139), new Point(1407, 128), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1407, 128), new Point(1408, 96), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1360, 192), new Point(1362, 210), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1362, 210), new Point(1372, 222), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1372, 222), new Point(1393, 224), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1393, 224), new Point(1410, 240), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1410, 240), new Point(1411, 259), stroke)));
            wall.Add(new Collider(BuildLine(new Point(1411, 259), new Point(1440, 288), stroke)));

            wall.Add(new Collider(BuildLine(new Point(1424, 176), new Point(1426, 192), stroke)));
        }

        private void BuildCanyonCollider()
        {
            var stroke = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            CanyonCollider = new Collider(BuildLine(new Point(0, 314), new Point(1440, 314), stroke));
        }

    }
}

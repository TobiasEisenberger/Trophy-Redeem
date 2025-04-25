using System;
using System.Windows.Media.Imaging;

namespace Trophy_Redeem.src.components.animation
{
    public readonly struct AnimationSprite
    {

        public readonly int frames;
        public readonly int rows;
        public readonly int columns;
        public readonly TimeSpan delay;
        public readonly BitmapImage source;

        public AnimationSprite(BitmapImage source, int frames, TimeSpan delay, int rows, int columns)
        {
            this.source = source;
            this.frames = frames;
            this.delay = delay;
            this.rows = rows;
            this.columns = columns;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Trophy_Redeem.src.graphics
{
    public abstract class Component
    {

        protected List<Shape> elementList;

        public Component()
        {
            elementList = new List<Shape>();
        }

        public List<Shape> GetElements()
        {
            return elementList;
        }

        protected virtual List<BitmapImage> FrameList(string path, int frames, bool appendFirstFrame = false)
        {
            List<BitmapImage> frameList = new List<BitmapImage>();
            for (int i = 0; i < frames; i++)
            {
                frameList.Add(new BitmapImage(new Uri($"src/assets/{path}/frame_{i % frames}.png", UriKind.Relative)));
            }

            if (appendFirstFrame && frameList.Count > 0)
            {
                frameList.Add(frameList[0]);
            }

            return frameList;
        }

    }
}

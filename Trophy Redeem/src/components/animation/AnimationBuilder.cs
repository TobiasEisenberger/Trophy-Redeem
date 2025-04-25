using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Collections.Generic;
using System;

namespace Trophy_Redeem.src.components.animation
{
    internal class AnimationBuilder
    {

        public static ObjectAnimationUsingKeyFrames BuildAnimation(AnimationSprite sprite)
        {
            var animation = new ObjectAnimationUsingKeyFrames();
            var keyFrames = new ObjectKeyFrameCollection();

            int frameWidth = sprite.source.PixelWidth / sprite.columns;
            int frameHeight = sprite.source.PixelHeight / sprite.rows;
            int processedFrames = 0;
            for (int i = 0; i < sprite.rows; i++)
            {
                int framesInRow = sprite.columns;
                if (i == sprite.rows - 1 && sprite.frames % sprite.columns != 0) // Last row is not completely filled
                {
                    framesInRow = sprite.frames % sprite.columns;
                }
                for (int k = 0; k < framesInRow; k++)
                {
                    var frame = new DiscreteObjectKeyFrame();
                    frame.Value = new ImageBrush(new CroppedBitmap(sprite.source, new Int32Rect(k * frameWidth, i * frameHeight, frameWidth, frameHeight)));
                    frame.KeyTime = KeyTime.FromTimeSpan(sprite.delay.Multiply(processedFrames));
                    keyFrames.Add(frame);
                    processedFrames++;
                }
            }

            animation.KeyFrames = keyFrames;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(sprite.frames * sprite.delay.Milliseconds));
            return animation;
        }

        public static ObjectAnimationUsingKeyFrames BuildAnimation(List<BitmapImage> frames, TimeSpan delay)
        {
            var animation = new ObjectAnimationUsingKeyFrames();
            var keyFrames = new ObjectKeyFrameCollection();

            for (int i = 0; i < frames.Count; i++)
            {
                var frame = new DiscreteObjectKeyFrame();
                frame.Value = new ImageBrush(frames[i]);
                frame.KeyTime = KeyTime.FromTimeSpan(delay.Multiply(i));
                keyFrames.Add(frame);
            }
            animation.KeyFrames = keyFrames;

            return animation;
        }

    }
}

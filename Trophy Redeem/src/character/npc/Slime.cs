using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Trophy_Redeem.src.components.animation;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.character.npc
{

    internal class Slime : Component
    {

        public bool Transformed { get; private set; } = false;

        ClockController idleController;
        ClockController moveController;
        ClockController transformController;

        // Do not change proportions unless bounding boxes are updated as well
        double width = 216;
        double height = 120;
        
        public Slime()
        {
            BuildComponent();
        }

        private void BuildComponent()
        {
            var slime = new Rectangle()
            {
                Width = width,
                Height = height
            };

            var idleClock = BuildIdleAnimation().CreateClock();
            idleController = idleClock.Controller;
            slime.ApplyAnimationClock(Shape.FillProperty, idleClock, HandoffBehavior.Compose);

            var moveClock = BuildMoveAnimation().CreateClock();
            moveController = moveClock.Controller;
            slime.ApplyAnimationClock(Shape.FillProperty, moveClock, HandoffBehavior.Compose);

            var transformClock = BuildTransformAnimation().CreateClock();
            transformController = transformClock.Controller;
            transformController.Clock.Completed += (sender, args) => { Transformed = true; };
            slime.ApplyAnimationClock(Shape.FillProperty, transformClock, HandoffBehavior.Compose);

            slime.Fill = new ImageBrush(new BitmapImage(new Uri("src/assets/character/slime/idle/frame_0.png", UriKind.Relative)));

            elementList.Add(slime);
        }

        public void Idle()
        {
            if (!Transformed)
            {
                moveController.Stop();
                transformController.Stop();
                if (idleController.Clock.CurrentState != ClockState.Active)
                {
                    idleController.Begin();
                }
            }
        }

        public void Move()
        {
            if (!Transformed)
            {
                idleController.Stop();
                transformController.Stop();
                if (moveController.Clock.CurrentState != ClockState.Active)
                {
                    moveController.Begin();
                }
            }
        }

        public void Transform()
        {
            if (!Transformed)
            {
                moveController.Stop();
                idleController.Stop();
                if (transformController.Clock.CurrentState != ClockState.Active)
                {
                    transformController.Begin();
                }
            }
        }

        public Rect GetActualBoundingBox()
        {
            var boundingBox = new Rect(new Point(96, 102), new Size(19, 18));
            return boundingBox;
        }

        private ObjectAnimationUsingKeyFrames BuildIdleAnimation()
        {
            var frameList = FrameList("idle", 6);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(150));
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildMoveAnimation()
        {
            var frameList = FrameList("move", 8);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(80));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildTransformAnimation()
        {
            var frameList = FrameList("transform", 32);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.BeginTime = null;

            return animation;
        }

        protected override List<BitmapImage> FrameList(string folder, int frames, bool appendFirstFrame = false)
        {
            return base.FrameList("character/slime/" + folder, frames, appendFirstFrame);
        }

    }
}

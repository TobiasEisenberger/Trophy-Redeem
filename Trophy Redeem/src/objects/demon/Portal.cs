using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Windows.Shapes;
using Trophy_Redeem.src.graphics;
using System.Windows.Media.Animation;
using System.Windows;
using Trophy_Redeem.src.components.animation;
using System.Collections.Generic;

namespace Trophy_Redeem.src.objects.demon
{
    internal class Portal : Component
    {

        public bool IsSpawned { get; private set; } = false;

        ClockController spawnController;
        ClockController activateController;

        public Portal()
        {
            BuildComponent();
        }

        private void BuildComponent()
        {
            var portal = new Rectangle()
            {
                Width = 27,
                Height = 43,
                Opacity = 0,
            };

            portal.Fill = new ImageBrush(new BitmapImage(new Uri("src/assets/objects/portal/active/frame_0.png", UriKind.Relative)));

            var spawnClock = BuildSpawnAnimation().CreateClock();
            spawnController = spawnClock.Controller;
            spawnController.Clock.Completed += (sender, args) => { IsSpawned = true; };
            portal.ApplyAnimationClock(Shape.OpacityProperty, spawnClock, HandoffBehavior.Compose);

            var ativateClock = BuildActivateAnimation().CreateClock();
            activateController = ativateClock.Controller;
            portal.ApplyAnimationClock(Shape.FillProperty, ativateClock, HandoffBehavior.Compose);

            elementList.Add(portal);
        }

        public void Spawn()
        {
            if (!IsSpawned && spawnController.Clock.CurrentState != ClockState.Active)
            {
                spawnController.Begin();
            }
        }

        public void Activate()
        {
            if (IsSpawned && activateController.Clock.CurrentState != ClockState.Active)
            {
                activateController.Begin();
            }
        }

        private DoubleAnimation BuildSpawnAnimation()
        {
            var animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(1)));
            animation.BeginTime = null;

            return animation;
        }
        
        private ObjectAnimationUsingKeyFrames BuildActivateAnimation()
        {
            var frameList = FrameList("active", 6, true);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.RepeatBehavior = new RepeatBehavior(4);
            animation.BeginTime = null;

            return animation;
        }

        protected override List<BitmapImage> FrameList(string folder, int frames, bool appendFirstFrame = false)
        {
            return base.FrameList("objects/portal/" + folder, frames, appendFirstFrame);
        }

    }
}

using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Windows.Shapes;
using Trophy_Redeem.src.graphics;
using System.Windows.Media.Animation;
using Trophy_Redeem.src.components.animation;
using System.Collections.Generic;
using System.Windows;

namespace Trophy_Redeem.src.objects.demon
{
    internal class Projectile : Component
    {

        public bool IsSpawned { get; private set; } = false;
        public bool IsExploding { get; private set; } = false;
        public bool IsExploded { get; private set; } = false;

        ClockController spawnController;
        ClockController idleController;
        ClockController explodeController;

        public Projectile()
        {
            BuildComponent();
        }

        private void BuildComponent()
        {
            var projectile = new Rectangle()
            {
                Width = 16,
                Height = 16,
                Opacity = 0,
            };

            var spawnClock = BuildSpawnAnimation().CreateClock();
            spawnController = spawnClock.Controller;
            spawnController.Clock.Completed += (sender, args) => { IsSpawned = true; Idle(); };
            projectile.ApplyAnimationClock(Shape.OpacityProperty, spawnClock, HandoffBehavior.Compose);

            var idleClock = BuildIdleAnimation().CreateClock();
            idleController = idleClock.Controller;
            projectile.ApplyAnimationClock(Shape.FillProperty, idleClock, HandoffBehavior.Compose);

            var explodeClock = BuildExplodeAnimation().CreateClock();
            explodeController = explodeClock.Controller;
            explodeController.Clock.Completed += (sender, args) => { IsExploding = false; IsExploded = true; Destroy(); };
            projectile.ApplyAnimationClock(Shape.FillProperty, explodeClock, HandoffBehavior.Compose);

            projectile.Fill = new ImageBrush(new BitmapImage(new Uri("src/assets/objects/projectile/idle/frame_0.png", UriKind.Relative)));

            elementList.Add(projectile);
        }

        public void Spawn()
        {
            if (spawnController.Clock.CurrentState != ClockState.Active)
            {
                spawnController.Begin();
            }
        }

        public void Idle()
        {
            if (idleController.Clock.CurrentState != ClockState.Active)
            {
                idleController.Begin();
            }
        }

        public void Explode()
        {
            idleController.Stop();
            if (explodeController.Clock.CurrentState != ClockState.Active)
            {
                IsExploding = true;
                explodeController.Begin();
            }
        }

        private DoubleAnimation BuildSpawnAnimation()
        {
            var animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(1)));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildIdleAnimation()
        {
            var frameList = FrameList("idle", 3);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(200));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildExplodeAnimation()
        {
            var frameList = FrameList("explode", 11);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.BeginTime = null;

            return animation;
        }

        protected override List<BitmapImage> FrameList(string folder, int frames, bool appendFirstFrame = false)
        {
            return base.FrameList("objects/projectile/" + folder, frames, appendFirstFrame);
        }

        private void Destroy()
        {
            spawnController.Remove();
            idleController.Remove();
            explodeController.Remove();
        }

    }
}

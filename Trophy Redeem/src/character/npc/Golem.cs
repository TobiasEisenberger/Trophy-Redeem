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

    internal class Golem : Component
    {

        public bool IsDying { get; private set; } = false;
        public bool IsDead { get; private set; } = false;
        public bool IsCrushed { get; private set; } = false;
        public Vector LookDirection { get; private set; } = new Vector(1 , 0);
        public double AttentionRadius { get; set; }

        ClockController idleController;
        ClockController runController;
        ClockController crushDeathController;
        ClockController deathController;

        double width = 16;
        double height = 24;

        public Golem(double attentionRadius)
        {
            BuildComponent();
            AttentionRadius = attentionRadius;
        }

        private void BuildComponent()
        {
            var enemy = new Rectangle()
            {
                Width = width,
                Height = height
            };

            var idleClock = BuildIdleAnimation().CreateClock();
            idleController = idleClock.Controller;
            enemy.ApplyAnimationClock(Shape.FillProperty, idleClock, HandoffBehavior.Compose);

            var runClock = BuildRunAnimation().CreateClock();
            runController = runClock.Controller;
            enemy.ApplyAnimationClock(Shape.FillProperty, runClock, HandoffBehavior.Compose);

            var crushDeathClock = BuildCrushDeathAnimation().CreateClock();
            crushDeathController = crushDeathClock.Controller;
            crushDeathController.Clock.Completed += (sender, args) => 
            {
                IsDead = true; IsDying = false; IsCrushed = true; Destroy(); 
            };
            enemy.ApplyAnimationClock(Shape.HeightProperty, crushDeathClock, HandoffBehavior.Compose);

            var deathClock = BuildDeathAnimation().CreateClock();
            deathController = deathClock.Controller;
            deathController.Clock.Completed += (sender, args) =>
            {
                IsDead = true; IsDying = false; Destroy();
            };
            enemy.ApplyAnimationClock(Shape.OpacityProperty, deathClock, HandoffBehavior.Compose);

            enemy.Fill = new ImageBrush(new BitmapImage(new Uri("src/assets/character/golem/idle/frame_0.png", UriKind.Relative)));
            enemy.RenderTransform = new ScaleTransform();
            enemy.RenderTransformOrigin = new Point(0.5, 0.5);

            elementList.Add(enemy);
        }

        public void Turn(Vector lookDirection)
        {
            var lookDirectionTransform = (ScaleTransform)GetElements()[0].RenderTransform;
            lookDirectionTransform.ScaleX = Math.Abs(lookDirection.X) / lookDirection.X;
            LookDirection = new Vector(lookDirectionTransform.ScaleX, 0);
        }

        public void Idle()
        {
            runController.Stop();
            if (idleController.Clock.CurrentState != ClockState.Active)
            {
                idleController.Begin();
            }
        }

        public void Run()
        {
            idleController.Stop();
            if (runController.Clock.CurrentState != ClockState.Active)
            {
                runController.Begin();
            }
        }

        public bool IsRunning()
        {
            return runController.Clock.CurrentState == ClockState.Active;
        }

        public void CrushDeath()
        {
            runController.Stop();
            idleController.Stop();
            if (crushDeathController.Clock.CurrentState != ClockState.Active)
            {
                IsDying = true;
                crushDeathController.Begin();
            }
        }

        public void Death()
        {
            runController.Stop();
            idleController.Stop();
            if (deathController.Clock.CurrentState != ClockState.Active)
            {
                IsDying = true;
                deathController.Begin();
            }
        }

        private ObjectAnimationUsingKeyFrames BuildIdleAnimation()
        {
            var frameList = FrameList("idle", 4);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildRunAnimation()
        {
            var frameList = FrameList("run", 4);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.BeginTime = null;

            return animation;
        }

        private DoubleAnimation BuildCrushDeathAnimation()
        {
            var animation = new DoubleAnimation(height/4, new Duration(TimeSpan.FromMilliseconds(100)));
            animation.BeginTime = null;

            return animation;
        }

        private DoubleAnimation BuildDeathAnimation()
        {
            var animation = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(1)));
            animation.BeginTime = null;

            return animation;
        }

        protected override List<BitmapImage> FrameList(string folder, int frames, bool appendFirstFrame = true)
        {
            return base.FrameList("character/golem/" + folder, frames, appendFirstFrame);
        }

        private void Destroy()
        {
            idleController.Remove();
            runController.Remove();
            deathController.Remove();
        }

    }
}

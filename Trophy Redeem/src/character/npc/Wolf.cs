using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Trophy_Redeem.src.components.animation;
using Trophy_Redeem.src.graphics;

enum WolfType
{
    Black,
    White,
    Gray,
    Brown
}

namespace Trophy_Redeem.src.character.npc
{
    internal class Wolf : Component
    {

        public Vector LookDirection { get; private set; } = new Vector(-1, 0);
        public double AttentionRadius { get; set; }
        public bool IsChasing { get; set; } = false;
        public bool IsAttacking { get; set; } = false;
        public bool IsDying { get; private set; } = false;
        public bool IsDead { get; private set; } = false;

        ClockController idleController;
        ClockController wakeupController;
        ClockController runController;
        ClockController attackController;
        ClockController deathController;

        string type;
        bool isAwake = false;
        int width = 48;
        int height = 32;

        public Wolf(WolfType type, double attentionRadius)
        {
            this.type = type.ToString().ToLower();
            BuildComponent();
            AttentionRadius = attentionRadius;
        }

        private void BuildComponent()
        {
            var enemy = new Rectangle()
            {
                Width = 24,
                Height = 16
            };

            var sprite = new BitmapImage(new Uri($"src/assets/character/wolf/death/{type}-sprite.png", UriKind.Relative));
            var cropRect = new Int32Rect(144, 32, width, height);
            enemy.Fill = new ImageBrush(new CroppedBitmap(sprite, cropRect));

            var idleClock = BuildIdleAnimation().CreateClock();
            idleController = idleClock.Controller;
            enemy.ApplyAnimationClock(Shape.FillProperty, idleClock, HandoffBehavior.Compose);

            var wakeupClock = BuildWakeupAnimation().CreateClock();
            wakeupController = wakeupClock.Controller;
            wakeupController.Clock.Completed += (sender, args) => { isAwake = true; };
            enemy.ApplyAnimationClock(Shape.FillProperty, wakeupClock, HandoffBehavior.Compose);

            var runClock = BuildRunAnimation().CreateClock();
            runController = runClock.Controller;
            enemy.ApplyAnimationClock(Shape.FillProperty, runClock, HandoffBehavior.Compose);

            var attackClock = BuildAttackAnimation().CreateClock();
            attackController = attackClock.Controller;
            attackController.Clock.Completed += (sender, args) => { IsAttacking = false; };
            enemy.ApplyAnimationClock(Shape.FillProperty, attackClock, HandoffBehavior.Compose);

            var deathClock = BuildDeathAnimation().CreateClock();
            deathController = deathClock.Controller;
            deathController.Clock.Completed += (sender, args) => { IsDying = false; IsDead = true; Destroy(); };
            enemy.ApplyAnimationClock(Shape.FillProperty, deathClock, HandoffBehavior.Compose);

            enemy.RenderTransform = new ScaleTransform();
            enemy.RenderTransformOrigin = new Point(0.5, 0.5);

            elementList.Add(enemy);
        }

        public void Wakeup()
        {
            if (!IsAwake() && wakeupController.Clock.CurrentState != ClockState.Active)
            {
                wakeupController.Begin();
            }
        }

        public void Turn(Vector lookDirection)
        {
            var lookDirectionTransform = (ScaleTransform)GetElements()[0].RenderTransform;
            lookDirectionTransform.ScaleX = Math.Abs(lookDirection.X) / lookDirection.X;
            LookDirection = new Vector(lookDirectionTransform.ScaleX, 0);
        }

        public void Run()
        {
            if (IsAwake())
            {
                idleController.Stop();
                wakeupController.Stop();
                attackController.Stop();
                if (runController.Clock.CurrentState != ClockState.Active)
                {
                    runController.Begin();
                }
            }
        }

        public void Idle()
        {
            if (IsAwake())
            {
                runController.Stop();
                wakeupController.Stop();
                attackController.Stop();
                if (idleController.Clock.CurrentState != ClockState.Active)
                {
                    idleController.Begin();
                }
            }
        }

        public void Attack()
        {
            if (IsAwake())
            {
                runController.Stop();
                wakeupController.Stop();
                idleController.Stop();
                if (attackController.Clock.CurrentState != ClockState.Active)
                {
                    IsAttacking = true;
                    attackController.Begin();
                }
            }
        }

        public void Die()
        {
            idleController.Stop();
            wakeupController.Stop();
            runController.Stop();
            attackController.Stop();
            if (deathController.Clock.CurrentState != ClockState.Active)
            {
                IsDying = true;
                deathController.Begin();
            }
        }

        public bool IsAwake()
        {
            return isAwake;
        }

        public bool IsRunning()
        {
            return runController.Clock.CurrentState == ClockState.Active;
        }

        private ObjectAnimationUsingKeyFrames BuildWakeupAnimation()
        {
            AnimationSprite sprite = new AnimationSprite(LoadSprite("wakeup"), 8, TimeSpan.FromMilliseconds(100), 2, 4);
            var animation = AnimationBuilder.BuildAnimation(sprite);
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildIdleAnimation()
        {
            AnimationSprite sprite = new AnimationSprite(LoadSprite("idle"), 4, TimeSpan.FromMilliseconds(200), 1, 4);
            var animation = AnimationBuilder.BuildAnimation(sprite);
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildRunAnimation()
        {
            AnimationSprite sprite = new AnimationSprite(LoadSprite("run"), 6, TimeSpan.FromMilliseconds(150), 2, 4);
            var animation = AnimationBuilder.BuildAnimation(sprite);
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildAttackAnimation()
        {
            AnimationSprite sprite = new AnimationSprite(LoadSprite("attack-FX"), 7, TimeSpan.FromMilliseconds(100), 2, 4);
            var animation = AnimationBuilder.BuildAnimation(sprite);
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildDeathAnimation()
        {
            AnimationSprite sprite = new AnimationSprite(LoadSprite("death"), 8, TimeSpan.FromMilliseconds(60), 2, 4);
            var animation = AnimationBuilder.BuildAnimation(sprite);
            animation.BeginTime = null;

            return animation;
        }

        private BitmapImage LoadSprite(string folder)
        {
            return new BitmapImage(new Uri($"src/assets/character/wolf/{folder}/{type}-sprite.png", UriKind.Relative));
        }

        private void Destroy()
        {
            idleController.Remove();
            wakeupController.Remove();
            runController.Remove();
            attackController.Remove();
        }

    }
}

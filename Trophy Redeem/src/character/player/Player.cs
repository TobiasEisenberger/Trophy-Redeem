using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Trophy_Redeem.src.components.animation;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.character.player
{
    internal class Player : Component
    {

        // Indicates if player was hit by enemy, any animations are prevented until hurt animation is over
        public bool HitStop { get; set; } = false;
        // Indicates if player was hit by enemy and is currently under protection of damage
        public bool HitProtect { get; private set; } = false;
        public Vector LookDirection { get; private set; } = new Vector(1, 0);

        public bool IsDying { get; private set; } = false;
        public bool IsDead { get; private set; } = false;

        public event EventHandler OnDamage;

        ClockController idleController;
        ClockController runController;
        ClockController jumpController;
        ClockController attackController;
        ClockController hurtController;
        ClockController hitProtectController;
        ClockController deathController;

        bool isJumping = false;
        bool isAttacking = false;

        double width = 16;
        double height = 24;

        public SoundPlayer run = new SoundPlayer(@"src\assets\Sounds\run_sound.wav");        
        public Player()
        {
            BuildPlayerObject();
        }

        private void BuildPlayerObject()
        {
            Rectangle rect = new Rectangle()
            {
                Width = width,
                Height = height,       
            };

            var idleClock = BuildIdleAnimation().CreateClock();
            idleController = idleClock.Controller;
            rect.ApplyAnimationClock(Shape.FillProperty, idleClock, HandoffBehavior.Compose);

            var runClock = BuildRunAnimation().CreateClock();
            runController = runClock.Controller;
            rect.ApplyAnimationClock(Shape.FillProperty, runClock, HandoffBehavior.Compose);

            var jumpClock = BuildJumpAnimation().CreateClock();
            jumpController = jumpClock.Controller;
            jumpController.Clock.Completed += (sender, args) => { isJumping = false; };
            rect.ApplyAnimationClock(Shape.FillProperty, jumpClock, HandoffBehavior.Compose);

            var attackClock = BuildAttackAnimation().CreateClock();
            attackController = attackClock.Controller;
            attackController.Clock.Completed += (sender, args) => { isAttacking = false; };
            rect.ApplyAnimationClock(Shape.FillProperty, attackClock, HandoffBehavior.Compose);

            var hurtClock = BuildHurtAnimation().CreateClock();
            hurtController = hurtClock.Controller;
            hurtController.Clock.Completed += (sender, args) => { HitStop = false; hitProtectController.Begin(); };
            rect.ApplyAnimationClock(Shape.FillProperty, hurtClock, HandoffBehavior.Compose);

            var hitProtectClock = BuildHitProtectAnimation().CreateClock();
            hitProtectController = hitProtectClock.Controller;
            hitProtectController.Clock.Completed += (sender, args) => { HitProtect = false; };
            rect.ApplyAnimationClock(Shape.OpacityProperty, hitProtectClock, HandoffBehavior.Compose);

            var deathClock = BuildDeathAnimation().CreateClock();
            deathController = deathClock.Controller;
            deathController.Clock.Completed += (sender, args) => { IsDying = false; IsDead = true; Destroy(); };
            rect.ApplyAnimationClock(Shape.FillProperty, deathClock, HandoffBehavior.Compose);

            rect.Fill = new ImageBrush(new BitmapImage(new Uri("src/assets/character/player/idle/frame_0.png", UriKind.Relative)));
            rect.RenderTransform = new ScaleTransform();
            rect.RenderTransformOrigin = new Point(0.5, 0.5);

            elementList.Add(rect);
        }

        public void Idle()
        {
            if (!IsJumping() && !IsAttacking() && !HitStop)
            {               
                runController.Stop();
                jumpController.Stop();
                attackController.Stop();
                hurtController.Stop();
                if (idleController.Clock.CurrentState != ClockState.Active)
                {
                    idleController.Begin();
                    run.Stop();
                }
            }
        }

        public void Run()
        {
            if (!IsJumping() && !IsAttacking() && !HitStop)
            {                
                idleController.Stop();
                jumpController.Stop();
                attackController.Stop();
                hurtController.Stop();
                if (runController.Clock.CurrentState != ClockState.Active)
                {
                    runController.Begin();
                    run.PlayLooping();                   
                }
            }
        }


        public void Jump()
        {
            if (!IsAttacking() && !HitStop)
            {
                idleController.Stop();
                runController.Stop();
                attackController.Stop();
                hurtController.Stop();
                if (jumpController.Clock.CurrentState != ClockState.Active)
                {
                    SoundPlayer jump = new SoundPlayer(@"src\assets\Sounds\Jump2.wav");
                    jump.Play();
                    jumpController.Begin();
                    isJumping = true;
                }
            }
        }

        public void Attack()
        {
            if (!IsJumping() && !HitStop)
            {
                idleController.Stop();
                runController.Stop();
                jumpController.Stop();
                hurtController.Stop();
                if (attackController.Clock.CurrentState != ClockState.Active)
                {
                    attackController.Begin();
                    isAttacking = true;
                }
            }
        }

        public void Hurt()
        {
            if (hurtController.Clock.CurrentState != ClockState.Active && hitProtectController.Clock.CurrentState != ClockState.Active)
            {
                idleController.Stop();
                runController.Stop();
                jumpController.Stop();
                attackController.Stop();
                HitStop = true;
                HitProtect = true;
                // Reset jumping, attacking because after animation cancle they wouldn't reset automatically
                isJumping = false;
                isAttacking = false;
                hurtController.Begin();
                OnDamage(this, EventArgs.Empty);
                run.Stop();
            }
        }

        public void Die()
        {
            idleController.Stop();
            runController.Stop();
            jumpController.Stop();
            attackController.Stop();
            hurtController.Stop();
            hitProtectController.Stop();
            if (deathController.Clock.CurrentState != ClockState.Active)
            {
                IsDying = true;
                deathController.Begin();
            }
            run.Stop();
        }

        public bool IsJumping()
        {
            return isJumping;
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        public void Turn(Vector lookDirection)
        {
            if (!IsAttacking() && !IsDead)
            {
                var lookDirectionTransform = (ScaleTransform)GetElements()[0].RenderTransform;
                lookDirectionTransform.ScaleX = Math.Abs(lookDirection.X) / lookDirection.X;
                LookDirection = new Vector(lookDirectionTransform.ScaleX, 0);
            }
        }

        public PathGeometry GetAttackRangeSemiCircle(Point playerPos)
        {
            SweepDirection direction = LookDirection.X > 0 ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
            Point startPoint = playerPos + new Vector(width / 2, 0);
            Point endPoint = startPoint + new Vector(0, height);
            var semiCircleGeometry = new PathGeometry();
            var semiCircleSegments = new PathSegmentCollection() { new ArcSegment(new Point(endPoint.X, endPoint.Y), new Size(10, 10), 0, false, direction, true) };
            semiCircleGeometry.Figures.Add(new PathFigure()
            {
                StartPoint = startPoint,
                Segments = semiCircleSegments,
            });
            return semiCircleGeometry;
        }

        private ObjectAnimationUsingKeyFrames BuildIdleAnimation()
        {
            var frameList = FrameList("idle", 7);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildRunAnimation()
        {
            var frameList = FrameList("run", 7);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildJumpAnimation()
        {
            var frameList = FrameList("jump", 7);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildAttackAnimation()
        {
            var frameList = FrameList("attack", 7);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(50));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildHurtAnimation()
        {
            var frameList = FrameList("hurt", 7);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.BeginTime = null;

            return animation;
        }

        private DoubleAnimation BuildHitProtectAnimation()
        {
            var animation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(100)));
            animation.BeginTime = null;
            animation.AutoReverse = true;
            animation.RepeatBehavior = new RepeatBehavior(8);

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildDeathAnimation()
        {
            var frameList = FrameList("die", 7, false);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.BeginTime = null;

            return animation;
        }
        
        protected override List<BitmapImage> FrameList(string folder, int frames, bool appendFirstFrame = true)
        {
            return base.FrameList("character/player/" + folder, frames, appendFirstFrame);
        }

        private void Destroy()
        {
            idleController.Remove();
            runController.Remove();
            jumpController.Remove();
            attackController.Remove();
            hurtController.Remove();
            hitProtectController.Remove();
        }

    }
}

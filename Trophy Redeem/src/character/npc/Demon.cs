using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Trophy_Redeem.src.components;
using Trophy_Redeem.src.components.animation;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.character.npc
{
    internal class Demon : Component
    {

        public Vector LookDirection { get; private set; } = new Vector(-1, 0);
        public bool IsAttacking { get; private set; } = false;
        public bool IsDying { get; private set; } = false;
        public bool IsDead { get; private set; } = false;

        public bool HitProtect { get; private set; } = false;

        public event EventHandler OnSmashFinish;

        public HealthBar HealthBar { get; private set; }

        ClockController idleController;
        ClockController walkController;
        ClockController cleaveController;
        ClockController fireBreathController;
        ClockController smashController;
        ClockController castSpellController;
        ClockController hurtController;
        ClockController deathController;

        double width = 216;
        double height = 120;

        public Demon(int health)
        {
            BuildComponent();
            HealthBar = new HealthBar(GetActualBoundingBox().Width, health);
        }

        private void BuildComponent()
        {
            var demon = new Rectangle()
            {
                Width = width,
                Height = height
            };

            var idleClock = BuildIdleAnimation().CreateClock();
            idleController = idleClock.Controller;
            demon.ApplyAnimationClock(Shape.FillProperty, idleClock, HandoffBehavior.Compose);

            var walkClock = BuildWalkAnimation().CreateClock();
            walkController = walkClock.Controller;
            demon.ApplyAnimationClock(Shape.FillProperty, walkClock, HandoffBehavior.Compose);

            var cleaveClock = BuildCleaveAnimation().CreateClock();
            cleaveController = cleaveClock.Controller;
            cleaveController.Clock.Completed += (sender, args) => { IsAttacking = false; };
            demon.ApplyAnimationClock(Shape.FillProperty, cleaveClock, HandoffBehavior.Compose);

            var fireBreathClock = BuildFireBreathAnimation().CreateClock();
            fireBreathController = fireBreathClock.Controller;
            fireBreathController.Clock.Completed += (sender, args) => { IsAttacking = false; };
            demon.ApplyAnimationClock(Shape.FillProperty, fireBreathClock, HandoffBehavior.Compose);

            var smashClock = BuildSmashAnimation().CreateClock();
            smashController = smashClock.Controller;
            smashController.Clock.Completed += (sender, args) => { IsAttacking = false; OnSmashFinish(this, EventArgs.Empty); };
            demon.ApplyAnimationClock(Shape.FillProperty, smashClock, HandoffBehavior.Compose);

            var castSpellClock = BuildCastSpellAnimation().CreateClock();
            castSpellController = castSpellClock.Controller;
            castSpellController.Clock.Completed += (sender, args) => { IsAttacking = false; };
            demon.ApplyAnimationClock(Shape.FillProperty, castSpellClock, HandoffBehavior.Compose);

            var hurtClock = BuildHurtAnimation().CreateClock();
            hurtController = hurtClock.Controller;
            hurtController.Clock.Completed += (sender, args) => { HitProtect = false; };
            demon.ApplyAnimationClock(Shape.OpacityProperty, hurtClock, HandoffBehavior.Compose);

            var deathClock = BuildDeathAnimation().CreateClock();
            deathController = deathClock.Controller;
            deathController.Clock.Completed += (sender, args) => { IsDying = false; IsDead = true; Destroy(); };
            demon.ApplyAnimationClock(Shape.FillProperty, deathClock, HandoffBehavior.Compose);

            demon.Fill = new ImageBrush(new BitmapImage(new Uri("src/assets/character/demon/idle/frame_0.png", UriKind.Relative)));
            demon.RenderTransform = new ScaleTransform();
            demon.RenderTransformOrigin = new Point(0.5, 0.5);

            elementList.Add(demon);
        }

        public void Idle()
        {
            walkController.Stop();
            cleaveController.Stop();
            fireBreathController.Stop();
            smashController.Stop();
            castSpellController.Stop();
            if (idleController.Clock.CurrentState != ClockState.Active)
            {
                idleController.Begin();
            }
        }

        public void Walk()
        {
            idleController.Stop();
            cleaveController.Stop();
            fireBreathController.Stop();
            smashController.Stop();
            castSpellController.Stop();
            if (walkController.Clock.CurrentState != ClockState.Active)
            {
                walkController.Begin();
            }
        }

        public void Cleave()
        {
            idleController.Stop();
            walkController.Stop();
            fireBreathController.Stop();
            smashController.Stop();
            castSpellController.Stop();
            if (cleaveController.Clock.CurrentState != ClockState.Active)
            {
                IsAttacking = true;
                cleaveController.Begin();
            }
        }

        public void FireBreath()
        {
            idleController.Stop();
            walkController.Stop();
            cleaveController.Stop();
            smashController.Stop();
            castSpellController.Stop();
            if (fireBreathController.Clock.CurrentState != ClockState.Active)
            {
                IsAttacking = true;
                fireBreathController.Begin();
            }
        }

        public void Smash()
        {
            idleController.Stop();
            walkController.Stop();
            cleaveController.Stop();
            fireBreathController.Stop();
            castSpellController.Stop();
            if (smashController.Clock.CurrentState != ClockState.Active)
            {
                IsAttacking = true;
                smashController.Begin();
            }
        }

        public void CastSpell()
        {
            idleController.Stop();
            walkController.Stop();
            cleaveController.Stop();
            smashController.Stop();
            fireBreathController.Stop();
            if (castSpellController.Clock.CurrentState != ClockState.Active)
            {
                IsAttacking = true;
                castSpellController.Begin();
            }
        }

        public void Hurt()
        {
            if (hurtController.Clock.CurrentState != ClockState.Active)
            {
                HitProtect = true;
                hurtController.Begin();
            }
        }

        public void Death()
        {
            idleController.Stop();
            walkController.Stop();
            cleaveController.Stop();
            fireBreathController.Stop();
            smashController.Stop();
            castSpellController.Stop();
            hurtController.Stop();
            if (deathController.Clock.CurrentState != ClockState.Active)
            {
                IsDying = true;
                deathController.Begin();
            }
        }

        public void Turn(Vector lookDirection)
        {
            var lookDirectionTransform = (ScaleTransform)GetElements()[0].RenderTransform;
            lookDirectionTransform.ScaleX = Math.Abs(lookDirection.X) / lookDirection.X;
            LookDirection = new Vector(lookDirectionTransform.ScaleX, 0);
        }

        public Rect GetActualBoundingBox()
        {
            return new Rect(new Point(75, 49), new Size(68, 71));
        }

        public Rect GetCleaveRange()
        {
            Point topLeft = new Point(22, 93);
            if (LookDirection.X < 0)
                topLeft = new Point(158, 93);

            return new Rect(topLeft, new Size(36, 27));
        }

        public Rect GetCleaveHitbox()
        {
            var clock = cleaveController.Clock;
            if (clock.CurrentProgress >= 0.6 && clock.CurrentProgress <= 0.8)
            {
                return GetCleaveRange(); // Hitbox occurs when knife hits the ground
            }

            return Rect.Empty;
        }

        public Rect GetSmashHitbox()
        {
            var clock = smashController.Clock;
            if (clock.CurrentProgress >= 0.6 && clock.CurrentProgress <= 0.75)
            {
                return new Rect(new Point(91, 95), new Size(43, 25));
            }

            return Rect.Empty;
        }

        public Rect GetFireBreathRange()
        {
            Point topLeft = new Point(17, 51);
            if (LookDirection.X < 0)
                topLeft = new Point(124, 51);

            return new Rect(topLeft, new Size(75, 69));
        }

        public Rect GetFireBreathHitbox()
        {
            Point topLeft = new Point(17, 51);
            if (LookDirection.X < 0)
                topLeft = new Point(124, 51);

            var hitbox = new Rect(topLeft, new Size(75, 17));

            var clock = fireBreathController.Clock;
            if (clock.CurrentProgress <= 0.34 || clock.CurrentProgress >= 0.8)
                return Rect.Empty; // No fire in animation

            // Move hitbox down, when fire animation  proceeds
            if (clock.CurrentProgress >= 0.55 && clock.CurrentProgress < 0.65)
            {
                hitbox.Offset(0, 17);
            } else if (clock.CurrentProgress >= 0.65 && clock.CurrentProgress < 0.75)
            {
                hitbox.Offset(0, 34);
            } else if (clock.CurrentProgress >= 0.75)
            {
                hitbox.Offset(0, 51);
            }
            return hitbox;
        }

        private ObjectAnimationUsingKeyFrames BuildIdleAnimation()
        {
            var frameList = FrameList("idle", 6);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(200));
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildWalkAnimation()
        {
            var frameList = FrameList("walk", 12, true);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(90));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildCleaveAnimation()
        {
            var frameList = FrameList("cleave", 15);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(80));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildFireBreathAnimation()
        {
            var frameList = FrameList("fire-breath", 21);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(90));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildSmashAnimation()
        {
            var frameList = FrameList("smash", 18);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(150));
            animation.BeginTime = null;

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildCastSpellAnimation()
        {
            var frameList = FrameList("cast-spell", 6, true);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(100));
            animation.RepeatBehavior = new RepeatBehavior(4);
            animation.BeginTime = null;

            return animation;
        }

        private DoubleAnimation BuildHurtAnimation()
        {
            var animation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(150)));
            animation.BeginTime = null;
            animation.AutoReverse = true;
            animation.RepeatBehavior = new RepeatBehavior(4);

            return animation;
        }

        private ObjectAnimationUsingKeyFrames BuildDeathAnimation()
        {
            var frameList = FrameList("death", 22);
            var animation = AnimationBuilder.BuildAnimation(frameList, TimeSpan.FromMilliseconds(200));
            animation.BeginTime = null;

            return animation;
        }

        protected override List<BitmapImage> FrameList(string folder, int frames, bool appendFirstFrame = false)
        {
            return base.FrameList("character/demon/" + folder, frames, appendFirstFrame);
        }

        private void Destroy()
        {
            idleController.Remove();
            walkController.Remove();
            cleaveController.Remove();
            fireBreathController.Remove();
            smashController.Remove();
            castSpellController.Remove();
            hurtController.Remove();
        }

    }
}

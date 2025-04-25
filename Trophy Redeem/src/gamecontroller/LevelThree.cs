using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using Trophy_Redeem.src.character.player;
using Trophy_Redeem.src.graphics;
using Trophy_Redeem.src.maps;
using Trophy_Redeem.src.character.npc;
using Trophy_Redeem.src.components;
using Trophy_Redeem.src.components.collision;
using System.Windows.Shapes;
using System.Windows.Media;
using Trophy_Redeem.src.objects.demon;
using System.Diagnostics;

namespace Trophy_Redeem.src.gamecontroller
{

    enum SlimeFightState
    {
        Idling,
        Escaping,
        Transforming,
        Defeated,
    }

    enum DemonFightState
    {
        Idling,
        Cleaving,
        Burning,
        Smashing,
        Casting,
    }

    public class LevelThree : GameController
    {

        GameObject slime;
        GameObject demon;
        Stopwatch cleaveCoolDown = new Stopwatch();
        Stopwatch fireBreathCoolDown = new Stopwatch();
        Stopwatch smashCooldown = new Stopwatch();
        Stopwatch castingCooldown = new Stopwatch();

        SlimeFightState slimeState = SlimeFightState.Idling;
        DemonFightState demonState = DemonFightState.Idling;
        Point slimePosFirst = new Point(1280, 256);
        Point slimePosSecond = new Point(1344, 256);
        Point slimeDestination;

        Collider leftArenaBoundary = new Collider(new Line() { X1 = 1216, Y1 = 256, X2 = 1216, Y2 = 176});
        Collider rightArenaBoundary = new Collider(new Line() { X1 = 1432, Y1 = 192, X2 = 1432, Y2 = 256});
        Portal portalLeft = new Portal();
        Portal portalRight = new Portal();
        List<GameObject> projectiles = new List<GameObject>();
        PointCollection projectileDestinations = new PointCollection() 
        {
            new Point(1264, 128),
            new Point(1296, 128),
            new Point(1328, 128),
            new Point(1360, 128),
        };

        public LevelThree(SaveGame saveGame) : base(saveGame)
        {
            scaleX = 3.5;
            scaleY = 3.5;
            gravity = 500;
            tiledMap = new CustomTiledMap("level3");

            collisionLayer = new LevelThreeCollisionLayer();
            collisionDetector = new CollisionDetector(collisionLayer);

            var player = new GameObject(new Player(), 100, 38) { Health = 3 };
            InitPlayer(player, new Point(0, 192));

            slime = new GameObject(new Slime(), 0, 0);
            var slimeHitbox = ((Slime)slime.component).GetActualBoundingBox();
            Canvas.SetLeft(slime.GetVisualComponent(), 1280 - slimeHitbox.Left);
            Canvas.SetTop(slime.GetVisualComponent(), 256 - slime.GetVisualComponent().Height);
            slimeDestination = slimePosFirst;

            var demonComponent = new Demon(5);
            demonComponent.OnSmashFinish += ActivateProjectiles;
            demon = new GameObject(demonComponent, 16, 64) { Health = demonComponent.HealthBar.HealthPoints };

            Canvas.SetLeft(portalLeft.GetElements()[0], 1216 - portalRight.GetElements()[0].Width);
            Canvas.SetTop(portalLeft.GetElements()[0], 224 - portalRight.GetElements()[0].Height);

            Canvas.SetLeft(portalRight.GetElements()[0], 1440 - portalRight.GetElements()[0].Width);
            Canvas.SetTop(portalRight.GetElements()[0], 256 - portalRight.GetElements()[0].Height);
        }

        public override void GameLoop(object? sender, EventArgs e)
        {
            base.GameLoop(sender, e);
            ControlPlayer();

            HandleObstacleDeath();

            var demonComponent = (Demon)demon.component;
            if (demonComponent.IsDead)
                return;

            if (slimeState != SlimeFightState.Defeated)
            {
                HandleSlimeFight();
            }
            else
            {    
                if (!demonComponent.IsDying)
                    HandleDemonFight();
            }
        }

        private void HandleObstacleDeath()
        {
            var playerVisual = player.GetVisualComponent();
            var playerHitbox = new RectangleGeometry(new Rect(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height));
            var playerComponent = (Player)player.component;

            foreach (var obstacle in collisionLayer.obstacle)
            {
                if (CollisionDetector.Collides(playerHitbox, obstacle) && !playerComponent.HitStop)
                {
                    playerComponent.Hurt();
                }
            }
        }

        private void HandleSlimeFight()
        {
            var slimeComponent = (Slime)slime.component;

            if (slimeState == SlimeFightState.Idling)
                UpdateSlimeState();
            if (slimeState == SlimeFightState.Escaping)
                UpdateSlimePos();
            if (slimeState == SlimeFightState.Transforming)
                SlimeTransform();

            if (slimeComponent.Transformed)
            {
                Canvas.SetLeft(demon.GetVisualComponent(), Canvas.GetLeft(slime.GetVisualComponent()));
                Canvas.SetTop(demon.GetVisualComponent(), Canvas.GetTop(slime.GetVisualComponent()));
                ScheduledGarbageComponents.Add(slime.component);
                ScheduledNewComponents.Add(demon.component);
                slimeState = SlimeFightState.Defeated;
                ((Player)player.component).HitStop = false;
                collisionLayer.wall.Add(leftArenaBoundary);
                collisionLayer.wall.Add(rightArenaBoundary);
                ScheduledNewComponents.Add(leftArenaBoundary);
                ScheduledNewComponents.Add(rightArenaBoundary);
                ScheduledNewComponents.Add(rightArenaBoundary);

                var demonComponent = (Demon)demon.component;
                var healthBar = demonComponent.HealthBar;
                Canvas.SetLeft(healthBar.GetElements()[0], Canvas.GetLeft(demon.GetVisualComponent()) + demonComponent.HealthBar.Size);
                Canvas.SetTop(healthBar.GetElements()[0], Canvas.GetTop(demon.GetVisualComponent()) + 20);
                ScheduledNewComponents.Add(demonComponent.HealthBar);
            }
        }

        private void UpdateSlimeState()
        {
            var player = this.player.GetVisualComponent();
            var playerComponent = (Player)this.player.component;
            Point playerPos = new Point(Canvas.GetLeft(player), Canvas.GetTop(player));
            var playerHitbox = new Rect(playerPos, new Size(player.Width, player.Height));

            var slime = (Slime)this.slime.component;
            var slimeVisual = this.slime.GetVisualComponent();
            var slimeHitbox = slime.GetActualBoundingBox();
            slimeHitbox.Offset(Canvas.GetLeft(slimeVisual), Canvas.GetTop(slimeVisual));

            var hitTest = CollisionDetector.CollidesWithDetail(playerHitbox, slimeHitbox);
            if (hitTest == CollisionDetail.None)
            {
                slime.Idle();
                slimeState = SlimeFightState.Idling;
                return;
            }
            if (this.player.State == GameObjectState.Falling && hitTest == CollisionDetail.Top)
            {
                slime.Transform();
                slimeState = SlimeFightState.Transforming;
                this.player.Reset();
                playerComponent.HitStop = true;
                this.player.State = GameObjectState.Jumping;
                return;
            }
            slime.Move();
            slimeState = SlimeFightState.Escaping;
            slimeDestination = slimeHitbox.Left == slimePosFirst.X ? slimePosSecond : slimePosFirst;
        }

        private void UpdateSlimePos()
        {
            var slime = (Slime)this.slime.component;
            var slimeVisual = this.slime.GetVisualComponent();
            var slimeHitbox = slime.GetActualBoundingBox();
            slimeHitbox.Offset(Canvas.GetLeft(slimeVisual), Canvas.GetTop(slimeVisual));

            if (slimeHitbox.Left != slimeDestination.X)
            {
                int direction = (slimeDestination.X - slimeHitbox.Left) >= 0 ? 1 : -1;
                double unitsX = 2 * direction;
                Canvas.SetLeft(slimeVisual, slimeHitbox.Left + unitsX - slime.GetActualBoundingBox().X);
                return;
            }
            slimeState = SlimeFightState.Idling;
        }

        private void SlimeTransform()
        {
            int farSideDirection = slimeDestination.Equals(slimePosFirst) ? 1 : -1;
            if (player.State == GameObjectState.Grounded)
            {
                var playerComponent = (Player)player.component;
                playerComponent.Turn(new Vector(-farSideDirection, 0));
                portalLeft.Spawn();
                portalRight.Spawn();
                return;
            }

            var playerVisual = player.GetVisualComponent();
            double unitsX = player.velocity * elapsedTime.TotalSeconds * farSideDirection;
            var newLeft = Canvas.GetLeft(playerVisual) + unitsX;

            var playerHitbox = new RectangleGeometry(new Rect(newLeft, Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height));
            if (CollisionDetector.Collides(playerHitbox, rightArenaBoundary) || CollisionDetector.Collides(playerHitbox, leftArenaBoundary))
            {
                newLeft -= unitsX;
            }

            Canvas.SetLeft(playerVisual, newLeft);
            moveMapWithPlayer(playerVisual, newLeft, 0);
        }

        private void HandleDemonFight()
        {
            var playerComponent = (Player)player.component;
            var demonComponent = (Demon)demon.component;

            if (demon.Health == 0)
            {
                demonComponent.Death();
                ScheduledGarbageComponents.Add(portalLeft);
                ScheduledGarbageComponents.Add(portalRight);
                ScheduledGarbageComponents.Add(demonComponent.HealthBar);

                foreach (var projectile in projectiles)
                {
                    ((Projectile)projectile.component).Explode();
                }

                collisionLayer.wall.Remove(leftArenaBoundary);
                collisionLayer.wall.Remove(rightArenaBoundary);
                return;
            }

            MoveDemon();
            ResetDemonCooldowns();
            
            if (!playerComponent.HitStop && !demonComponent.IsAttacking)
                DemonAttack();

            if (demonComponent.IsAttacking)
                DemonFightHitDetect();

            if (projectiles.Count > 0)
            {
                HandleProjectiles();
            }

            if (playerComponent.IsAttacking() && !playerComponent.HitProtect && !demonComponent.HitProtect)
            {
                HandlePlayerAttack();
            }
        }

        private void MoveDemon()
        {
            var demonComponent = (Demon)demon.component;
            if (demonComponent.IsAttacking)
                return;

            var playerVisual = player.GetVisualComponent();
            var playerHitbox = new RectangleGeometry(new Rect(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height));

            var demonVisual = demon.GetVisualComponent();
            var demonHitbox = demonComponent.GetActualBoundingBox();
            demonHitbox.Offset(Canvas.GetLeft(demonVisual), Canvas.GetTop(demonVisual));

            var hitTest = playerHitbox.FillContainsWithDetail(new RectangleGeometry(demonHitbox));
            if (hitTest == IntersectionDetail.Empty)
            {
                Vector playerDirection = new Vector(playerHitbox.Rect.Left - demonHitbox.Left, 0);
                demonComponent.Turn(-playerDirection);
                demonComponent.Walk();
                Canvas.SetLeft(demonVisual, Canvas.GetLeft(demonVisual) + demon.velocity * Math.Sign(playerDirection.X) * elapsedTime.TotalSeconds);

                var healthBar = demonComponent.HealthBar.GetElements()[0];
                Canvas.SetLeft(healthBar, Canvas.GetLeft(demonVisual) + demonHitbox.Width);
                return;
            }
            demonComponent.Idle();
        }

        private void ResetDemonCooldowns()
        {
            if (cleaveCoolDown.IsRunning && cleaveCoolDown.Elapsed.TotalSeconds >= 4)
            {
                cleaveCoolDown.Reset();
            }
            if (fireBreathCoolDown.IsRunning && fireBreathCoolDown.Elapsed.TotalSeconds >= 7)
            {
                fireBreathCoolDown.Reset();
            }
            if (smashCooldown.IsRunning && smashCooldown.Elapsed.TotalSeconds >= 6)
            {
                smashCooldown.Reset();
            }
            if (castingCooldown.IsRunning && castingCooldown.Elapsed.TotalSeconds >= 15)
            {
                castingCooldown.Reset();
            }
        }

        private void DemonAttack()
        {
            var playerVisual = player.GetVisualComponent();
            var playerHitbox = new RectangleGeometry(new Rect(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height));

            var demonVisual = demon.GetVisualComponent();
            var demonComponent = (Demon)demon.component;

            var smashRange = demonComponent.GetActualBoundingBox();
            smashRange.Offset(Canvas.GetLeft(demonVisual), Canvas.GetTop(demonVisual));
            var meleeRange = demonComponent.GetCleaveRange();
            meleeRange.Offset(Canvas.GetLeft(demonVisual), Canvas.GetTop(demonVisual));
            var fireRange = demonComponent.GetFireBreathRange();
            fireRange.Offset(Canvas.GetLeft(demonVisual), Canvas.GetTop(demonVisual));

            var playerCenter = new Point(playerHitbox.Rect.Location.X + playerVisual.Width/2, playerHitbox.Rect.Location.Y + playerVisual.Height/2);
            if (!cleaveCoolDown.IsRunning && new RectangleGeometry(meleeRange).FillContains(playerCenter))
            {
                demonState = DemonFightState.Cleaving;
                demonComponent.Cleave();
                cleaveCoolDown.Start();
            } else if (!fireBreathCoolDown.IsRunning && new RectangleGeometry(fireRange).FillContains(playerCenter))
            {
                demonState = DemonFightState.Burning;
                demonComponent.FireBreath();
                fireBreathCoolDown.Start();
            } else if (!smashCooldown.IsRunning && new RectangleGeometry(smashRange).FillContains(playerCenter))
            {
                demonState = DemonFightState.Smashing;
                demonComponent.Smash();
                smashCooldown.Start();
                if (projectiles.Count > 0)
                {
                    fireBreathCoolDown.Restart();
                }
            } else if (!castingCooldown.IsRunning && projectiles.Count == 0)
            {
                demonState = DemonFightState.Casting;
                demonComponent.CastSpell();
                portalLeft.Activate();
                portalRight.Activate();
                SpawnProjectiles(portalLeft);
                SpawnProjectiles(portalRight);
                castingCooldown.Start();
            }
        }

        private void SpawnProjectiles(Portal portal)
        {
            var portalVisual = portal.GetElements()[0];
            for (int i = 0; i < projectileDestinations.Count / 2; i++)
            {
                var projectile = new Projectile();
                var projectileVisual = projectile.GetElements()[0];
                Canvas.SetLeft(projectileVisual, Canvas.GetLeft(portalVisual) - projectileVisual.Width / 2 + portalVisual.Width / 2);
                Canvas.SetTop(projectileVisual, Canvas.GetTop(portalVisual) + i * portalVisual.Height / 3);
                projectiles.Add(new GameObject(projectile, 50, 0));
                ScheduledNewComponents.Add(projectile);
                projectile.Spawn();
            }
        }

        private void HandleProjectiles()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];
                var projectileComponent = (Projectile)projectile.component;
                var projectileVisual = projectile.GetVisualComponent();

                if (projectileComponent.IsExploding)
                    continue;

                if (projectileComponent.IsExploded)
                {
                    projectiles.Remove(projectile);
                    ScheduledGarbageComponents.Add(projectileComponent);
                    continue;
                }

                double left;
                double top;
                projectile.Update(elapsedTime);
                if (projectile.State == GameObjectState.Falling)
                {
                    left = Canvas.GetLeft(projectileVisual);
                    top = Canvas.GetTop(projectileVisual) + Physics.FallingHeight(gravity, projectile.FallingTime) - Physics.FallingHeight(gravity, projectile.FallingTime.Subtract(elapsedTime));
                }
                else
                {
                    Vector destination = projectileDestinations[i] - new Point(Canvas.GetLeft(projectileVisual), Canvas.GetTop(projectileVisual));
                    left = Canvas.GetLeft(projectileVisual) + Math.Sign(destination.X) * projectile.velocity * elapsedTime.TotalSeconds;
                    top = Canvas.GetTop(projectileVisual) + Math.Sign(destination.Y) * projectile.velocity * elapsedTime.TotalSeconds;
                    if (destination.Length <= 5)
                    {
                        left = projectileDestinations[i].X;
                        top = projectileDestinations[i].Y;
                    }
                }

                Canvas.SetLeft(projectileVisual, left);
                Canvas.SetTop(projectileVisual, top);
                DetectProjectileHit(projectile);
            }
        }

        private void DetectProjectileHit(GameObject projectile)
        {
            var playerComponent = (Player)player.component;
            var playerVisual = player.GetVisualComponent();
            var playerHitbox = new RectangleGeometry(new Rect(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height));

            var projectileComponent = (Projectile)projectile.component;
            var projectileVisual = projectile.GetVisualComponent();
            var projectileHitbox = new RectangleGeometry(new Rect(Canvas.GetLeft(projectileVisual), Canvas.GetTop(projectileVisual), projectileVisual.Width, projectileVisual.Height));

            var playerHitTest = playerHitbox.FillContainsWithDetail(projectileHitbox);
            if (playerHitTest != IntersectionDetail.Empty)
            {
                playerComponent.Hurt();
                projectileComponent.Explode();
            }

            var groundHitbox = new RectangleGeometry(new Rect(1216, 256, 224, 64));
            var groundHitTest = groundHitbox.FillContainsWithDetail(projectileHitbox);
            if (groundHitTest != IntersectionDetail.Empty)
            {
                projectileComponent.Explode();
            }
        }

        public void ActivateProjectiles(object sender, EventArgs args)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                var projectile = projectiles[i];
                var projectileVisual = projectile.GetVisualComponent();
                if (Canvas.GetTop(projectileVisual) == projectileDestinations[0].Y)
                {
                    projectile.State = GameObjectState.Falling;
                }
            }
        }

        private void DemonFightHitDetect()
        {
            var demonVisual = demon.GetVisualComponent();
            var playerVisual = player.GetVisualComponent();
            var playerHitbox = new RectangleGeometry(new Rect(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height));
            Rect attackHitbox = Rect.Empty;

            var demonComponent = (Demon)demon.component;
            if (demonState == DemonFightState.Cleaving)
            {
                attackHitbox = demonComponent.GetCleaveHitbox();
            } else if (demonState == DemonFightState.Burning)
            {
                attackHitbox = demonComponent.GetFireBreathHitbox();
            } else if (demonState == DemonFightState.Smashing)
            {
                attackHitbox = demonComponent.GetSmashHitbox();
            }

            if (attackHitbox.IsEmpty)
                return;

            attackHitbox.Offset(Canvas.GetLeft(demonVisual), Canvas.GetTop(demonVisual));
            var hitTest = new RectangleGeometry(attackHitbox).FillContainsWithDetail(playerHitbox);
            if (hitTest != IntersectionDetail.Empty)
            {
                var playerComponent = (Player)player.component;
                playerComponent.Hurt();
            }
        }

        private void HandlePlayerAttack()
        {
            var playerComponent = (Player)player.component;
            var playerVisual = player.GetVisualComponent();
            var playerPos = new Point(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual));

            var playerHitbox = new Rect(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual), playerVisual.Width, playerVisual.Height);

            var demonComponent = (Demon)demon.component;
            var demonVisual = demon.GetVisualComponent();
            var demonHitbox = demonComponent.GetActualBoundingBox();
            demonHitbox.Offset(Canvas.GetLeft(demonVisual), Canvas.GetTop(demonVisual));

            var swordAttackSemiCircle = playerComponent.GetAttackRangeSemiCircle(playerPos);
            var hitTest = swordAttackSemiCircle.FillContainsWithDetail(new RectangleGeometry(demonHitbox));
            if (hitTest == IntersectionDetail.Empty)
                return;

            bool wasAttackFromBehind = false;
            if (playerHitbox.Left < demonHitbox.Left + demonHitbox.Width / 2 && demonComponent.LookDirection.X == -1)
            {
                wasAttackFromBehind = true;
            } else if (playerHitbox.Left > demonHitbox.Left + demonHitbox.Width / 2 && demonComponent.LookDirection.X == 1)
            {
                wasAttackFromBehind = true;
            }

            if (wasAttackFromBehind)
            {
                demonComponent.Hurt();
                demonComponent.HealthBar.Reduce();
                demon.Health--;
            }
        }

        public override List<Component> GetComponents()
        {
            List<Component> components = base.GetComponents();
            components.Add(slime.component);
            components.Add(portalLeft);
            components.Add(portalRight);

            foreach (var collider in collisionLayer.obstacle)
            {
                components.Add(collider);
            }

            return components;
        }

    }
}

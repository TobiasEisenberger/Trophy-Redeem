using System;
using System.Windows;
using System.Collections.Generic;
using Trophy_Redeem.src.character.player;
using Trophy_Redeem.src.graphics;
using Trophy_Redeem.src.maps;
using Trophy_Redeem.src.components.collision;
using Trophy_Redeem.src.components;
using System.Windows.Controls;
using Trophy_Redeem.src.character.npc;
using System.Windows.Media;
using System.Linq;
using System.Diagnostics;

namespace Trophy_Redeem.src.gamecontroller
{

    public class LevelTwo : GameController
    {

        List<GameObject> enemies;
        Rect finishArea = new Rect(new Point(1376, 149), new Size(64, 59));
        Random random = new Random();
        Stopwatch slowCooldown = new Stopwatch();
        const int SlowAmount = 50;

        public LevelTwo(SaveGame saveGame) : base(saveGame)
        {
            gravity = 500;
            scaleX = 3.5;
            scaleY = 3.5;
            collisionLayer = new LevelTwoCollisionLayer();
            collisionDetector = new CollisionDetector(collisionLayer);

            tiledMap = new CustomTiledMap("level2");

            var player = new GameObject(new Player(), 100, 48) { Health = 3 };
            InitPlayer(player, new Point(0, 224));

            enemies = new List<GameObject>();
            SpawnEnemy(new Point(624, 112));
            SpawnEnemy(new Point(272, 256));
            SpawnEnemy(new Point(672, 224));
            SpawnEnemy(new Point(816, 176));
            SpawnEnemy(new Point(856, 96));
            SpawnEnemy(new Point(1225, 208));
            SpawnEnemy(new Point(1248, 96));
        }

        public override void GameLoop(object? sender, EventArgs e)
        {
            base.GameLoop(sender, e);
            ControlPlayer();

            CheckLevelFinish();
            HandleCanyonDeath();

            foreach (var wolf in enemies.ToList())
            {
                var wolfComponent = (Wolf)wolf.component;
                if (wolfComponent.IsDying || wolfComponent.IsDead)
                {
                    enemies.Remove(wolf);
                    continue;
                }

                if (!wolfComponent.IsAttacking)
                    MoveWolf(wolf);

                ResetDebuff();
                HandleFight(wolf);
            }
        }

        private void CheckLevelFinish()
        {
            var playerHitbox = player.GetVisualComponent().RenderedGeometry.Bounds;
            playerHitbox.Offset(Canvas.GetLeft(player.GetVisualComponent()), Canvas.GetTop(player.GetVisualComponent()));
            if (new RectangleGeometry(finishArea).FillContains(new RectangleGeometry(playerHitbox)))
            {
                Finish();
            }
        }

        private void HandleCanyonDeath()
        {
            var playerHitbox = player.GetVisualComponent().RenderedGeometry.Bounds;
            playerHitbox.Offset(Canvas.GetLeft(player.GetVisualComponent()), Canvas.GetTop(player.GetVisualComponent()));
            if (CollisionDetector.Collides(new RectangleGeometry(playerHitbox), ((LevelTwoCollisionLayer)collisionLayer).CanyonCollider))
            {
                player.GetVisualComponent().Opacity = 0;
                ((Player)player.component).Die();
                if (InGameOverlay != null)
                {
                    for (int i = 0; i < player.Health; i++)
                    {
                        InGameOverlay.RemoveHeart();
                    }
                }
            }
        }

        private void MoveWolf(GameObject wolf)
        {
            var wolfComponent = (Wolf)wolf.component;
            var wolfVisual = wolf.GetVisualComponent();
            Point wolfPos = new Point(Canvas.GetLeft(wolfVisual), Canvas.GetTop(wolfVisual));

            var player = this.player.GetVisualComponent();
            Point playerPos = new Point(Canvas.GetLeft(player), Canvas.GetTop(player));
            Vector playerDirection = playerPos - wolfPos;

            wolfComponent.IsChasing = false;
            if (playerDirection.Length <= wolfComponent.AttentionRadius)
            {
                wolfComponent.Wakeup();
                if (wolfComponent.IsAwake())
                {
                    wolfComponent.Turn(-playerDirection);
                    if (playerPos.X < wolfPos.X || playerPos.X > wolfPos.X + wolfVisual.Width)
                    {
                        wolfComponent.IsChasing = true;
                        double newLeft = wolf.velocity * elapsedTime.TotalSeconds * Math.Sign(playerDirection.X);

                        var wolfHitbox = new RectangleGeometry(new Rect(wolfPos.X, wolfPos.Y, wolfVisual.Width, wolfVisual.Height));
                        foreach (var collider in collisionDetector.CollidesWithGround(wolfHitbox))
                        {
                            var shape = collider.GetElements()[0];
                            bool isOnLeftGroundEdge = wolfPos.X < collider.Start.X;
                            bool isOnRightGroundEdge = wolfPos.X + wolfVisual.Width > collider.Start.X + shape.RenderedGeometry.Bounds.Width;
                            if ((isOnLeftGroundEdge && wolfComponent.LookDirection.X == 1) || (isOnRightGroundEdge && wolfComponent.LookDirection.X == -1))
                            {
                                newLeft = 0;
                                wolfComponent.IsChasing = false;
                            }
                        }

                        Canvas.SetLeft(wolfVisual, Canvas.GetLeft(wolfVisual) + newLeft);
                    }
                    
                }
            }

            if (wolfComponent.IsChasing)
            {
                wolfComponent.Run();
            } else
            {
                wolfComponent.Idle();
            }
        }

        private void HandleFight(GameObject wolf)
        {
            var playerComponent = (Player)player.component;
            var playerVisual = player.GetVisualComponent();
            Point playerPos = new Point(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual));
            var playerHitbox = new RectangleGeometry(new Rect(playerPos.X, playerPos.Y, playerVisual.Width, playerVisual.Height));

            var wolfComponent = (Wolf)wolf.component;
            var wolfVisual = wolf.GetVisualComponent();
            Point wolfPos = new Point(Canvas.GetLeft(wolfVisual), Canvas.GetTop(wolfVisual));
            var wolfHitbox = new RectangleGeometry(new Rect(wolfPos.X, wolfPos.Y, wolfVisual.Width, wolfVisual.Height));

            if (playerComponent.IsAttacking())
            {
                var swordAttackSemiCircle = playerComponent.GetAttackRangeSemiCircle(playerPos);

                var swordAttackGolemIntersection = swordAttackSemiCircle.FillContainsWithDetail(wolfHitbox);
                if (swordAttackGolemIntersection == IntersectionDetail.Intersects)
                {
                    wolfComponent.Die();
                    return;
                }
            }

            var hitTest = playerHitbox.FillContainsWithDetail(wolfHitbox);
            if (hitTest != IntersectionDetail.Empty && !playerComponent.HitProtect)
            {
                wolfComponent.Attack();
                ((Player)player.component).Hurt();
                ApplySlowDebuff();
            }
        }

        private void ResetDebuff()
        {
            if (slowCooldown.IsRunning && slowCooldown.Elapsed.TotalSeconds >= 3)
            {
                player.velocity += SlowAmount;
                slowCooldown.Reset();
            }
        }

        private void ApplySlowDebuff()
        {
            if (!slowCooldown.IsRunning)
            {
                player.velocity -= SlowAmount;
                slowCooldown.Start();
            }
        }

        private void SpawnEnemy(Point spawnpoint)
        {
            Array values = Enum.GetValues(typeof(WolfType));
            var wolf = new GameObject(new Wolf((WolfType)values.GetValue(random.Next(values.Length)), 100), 50, 0);
            Canvas.SetLeft(wolf.GetVisualComponent(), spawnpoint.X);
            Canvas.SetTop(wolf.GetVisualComponent(), spawnpoint.Y - wolf.GetVisualComponent().Height);

            enemies.Add(wolf);
        }

        public override List<Component> GetComponents()
        {
            List<Component> components = base.GetComponents();
            foreach (GameObject wolf in enemies)
            {
                components.Add(wolf.component);
            }
            components.Add(((LevelTwoCollisionLayer)collisionLayer).CanyonCollider);
            return components;
        }

    }

}

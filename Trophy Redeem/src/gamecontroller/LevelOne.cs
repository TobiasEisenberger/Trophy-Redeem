using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Trophy_Redeem.src.character.player;
using Trophy_Redeem.src.graphics;
using Trophy_Redeem.src.maps;
using Trophy_Redeem.src.character.npc;
using Trophy_Redeem.src.components.collision;
using Trophy_Redeem.src.components;
using System.Linq;

namespace Trophy_Redeem.src.gamecontroller
{

    public class LevelOne : GameController 
    {

        List<GameObject> enemies;
        Rect finishArea = new Rect(new Point(1360, 96), new Size(80, 96));
                
        public LevelOne(SaveGame saveGame) : base(saveGame)
        {
            gravity = 500;
            scaleX = 3.5;
            scaleY = 3.5;
            collisionLayer = new LevelOneCollisionLayer(false);
            collisionDetector = new CollisionDetector(collisionLayer);
            
            tiledMap = new CustomTiledMap("Jungle_level");
            
            var player = new GameObject(new Player(), 100, 48) { Health = 3 };
            InitPlayer(player, new Point(0, 144));
                        
            enemies = new List<GameObject>();
            SpawnEnemy(new GameObject(new Golem(64), 50, 0), new Point(48, 224));
            SpawnEnemy(new GameObject(new Golem(64), 50, 0), new Point(320, 240));
            SpawnEnemy(new GameObject(new Golem(64), 60, 0), new Point(432, 80));
            SpawnEnemy(new GameObject(new Golem(200), 70, 0), new Point(576, 256));
            SpawnEnemy(new GameObject(new Golem(48), 70, 0), new Point(624, 96));
            SpawnEnemy(new GameObject(new Golem(48), 50, 0), new Point(736, 192));
            SpawnEnemy(new GameObject(new Golem(48), 50, 0), new Point(832, 160));
            SpawnEnemy(new GameObject(new Golem(32), 60, 0), new Point(1088, 128));
        }

        public override void GameLoop(object? sender, EventArgs e)
        {
            base.GameLoop(sender, e);
            if (((Player)player.component).IsDead)
            {
                player.Health = 0; // Player hurt method is not called when falling in canyon --> manually set health to zero
            }

            ControlPlayer();
            CheckLevelFinish();
            HandleCanyonDeath();

            foreach (GameObject golem in enemies.ToList())
            {
                var golemComponent = (Golem)golem.component;
                if (golemComponent.IsDead)
                {
                    enemies.Remove(golem);
                    if (golemComponent.IsCrushed)
                        InterpolateCrushedGolemYPos(golem);
                    else
                        ScheduledGarbageComponents.Add(golemComponent);
                    continue;
                }
                UpdateEnemyPos(golem);
                HandleFight(golem);
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
            if (CollisionDetector.Collides(new RectangleGeometry(playerHitbox), ((LevelOneCollisionLayer)collisionLayer).CanyonCollider))
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

        private void SpawnEnemy(GameObject golem, Point spawnpoint)
        {
            Canvas.SetLeft(golem.GetVisualComponent(), spawnpoint.X);
            Canvas.SetTop(golem.GetVisualComponent(), spawnpoint.Y - golem.GetVisualComponent().Height);

            enemies.Add(golem);
        }

        private void HandleFight(GameObject golem)
        {
            var playerVisual = player.GetVisualComponent();
            var playerComponent = (Player)player.component;
            var playerPos = new Point(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual));
            var playerRect = new Rect(playerPos, new Size(playerVisual.Width, playerVisual.Height));

            var golemVisual = golem.GetVisualComponent();
            var golemComponent = (Golem)golem.component;
            var golemPos = new Point(Canvas.GetLeft(golemVisual), Canvas.GetTop(golemVisual));
            var golemRect = new Rect(golemPos, new Size(golemVisual.Width, golemVisual.Height));
            
            if (playerComponent.IsAttacking())
            {
                var swordAttackSemiCircle = playerComponent.GetAttackRangeSemiCircle(playerPos);
                var golemHitbox = new RectangleGeometry(golemRect);

                var swordAttackGolemIntersection = swordAttackSemiCircle.FillContainsWithDetail(golemHitbox);
                if (swordAttackGolemIntersection == IntersectionDetail.Intersects)
                {
                    golemComponent.Death();
                }
            }

            CollisionDetail hit = CollisionDetector.CollidesWithDetail(playerRect, golemRect);
            if (hit == CollisionDetail.Top && player.State == GameObjectState.Falling && golem.State != GameObjectState.Falling)
            {
                player.Reset();
                golemComponent.CrushDeath();
            }

            if (golemComponent.IsDying)
            {
                if (golem.LastGroundPos != null && golem.State != GameObjectState.Falling)
                {
                    InterpolateCrushedGolemYPos(golem);
                }
                return;
            }

            if ((hit == CollisionDetail.Left || hit == CollisionDetail.Right) && !playerComponent.HitProtect)
            {
                playerComponent.Hurt();                                
            }
        }

        private void UpdateEnemyPos(GameObject golem)
        {
            var golemComponent = (Golem)golem.component;
            if (golemComponent.IsDying)
                return;

            var golemVisual = golem.GetVisualComponent();

            var playerVisual = player.GetVisualComponent();
            Point playerPos = new Point(Canvas.GetLeft(playerVisual), Canvas.GetTop(playerVisual));

            Point golemPos = new Point(Canvas.GetLeft(golemVisual), Canvas.GetTop(golemVisual));
            Vector playerDirection = (playerPos - golemPos);

            if (playerDirection.Length >= golemComponent.AttentionRadius && !golemComponent.IsRunning())
            {
                golemComponent.Turn(playerDirection);
                golemComponent.Idle();
                golem.Reset();
                return;
            }

            golemComponent.Run();

            double unitsX = golem.velocity * golemComponent.LookDirection.X * elapsedTime.TotalSeconds;
            double unitsY = 0;

            golem.Update(elapsedTime);
            TimeSpan prevFallingTime = golem.FallingTime.Subtract(elapsedTime);
            if (golem.State == GameObjectState.Falling)
            {
                unitsY = Physics.FallingHeight(gravity, golem.FallingTime) - Physics.FallingHeight(gravity, prevFallingTime);
            }

            var newLeft = Canvas.GetLeft(golemVisual) + unitsX;
            var newTop = Canvas.GetTop(golemVisual) + unitsY;
            
            golem.State = GameObjectState.Falling;
            foreach (var collider in collisionDetector.CollidesWithGround(new RectangleGeometry(new Rect(newLeft, newTop, golemVisual.Width, golemVisual.Height))))
            {
                var shape = collider.GetElements()[0];
                if (Canvas.GetLeft(golemVisual) >= collider.Start.X + shape.RenderedGeometry.Bounds.Width || Canvas.GetLeft(golemVisual) + golemVisual.Width <= collider.Start.X)
                {
                    // Golem is either on right or left side of ground -> undo x movement
                    newLeft += (-1) * unitsX;
                    golemComponent.Turn(-golemComponent.LookDirection);
                }
                else
                {
                    // Golem is on top of ground -> set golem on top of ground
                    golem.State = GameObjectState.Grounded;
                    Point point, tangent;
                    double progress = (Canvas.GetLeft(golemVisual) + golemVisual.Width - collider.Start.X) / collider.Length;
                    shape.RenderedGeometry.GetFlattenedPathGeometry().GetPointAtFractionLength(progress, out point, out tangent);
                    newTop = point.Y - golemVisual.Height;
                    golem.LastGroundPos = point.Y;
                }
            }

            foreach (var collider in collisionDetector.CollidesWithWall(new RectangleGeometry(new Rect(newLeft, newTop, golemVisual.Width, golemVisual.Height))))
            {
                newLeft += (-1) * unitsX;
                golemComponent.Turn(-golemComponent.LookDirection);
            }

            if ((golemVisual.Height + newTop) > tiledMap.height)
            {
                golemComponent.Death();
            }

            Canvas.SetLeft(golemVisual, newLeft);
            Canvas.SetTop(golemVisual, newTop);
        }

        /*
        "Crushing" happens by changing the height of the golem --> Golem loses contact to ground
        The correct countermeasure is moving the golem down by the value of the created space after shrinking
         */
        private void InterpolateCrushedGolemYPos(GameObject golem)
        {
            if (golem.LastGroundPos != null)
            {
                Canvas.SetTop(golem.GetVisualComponent(), (double)golem.LastGroundPos - golem.GetVisualComponent().Height);
            }
        }

        public override List<Component> GetComponents()
        {
            List<Component> components = base.GetComponents();
            foreach (GameObject golem in enemies)
            {
                components.Add(golem.component);
            }
            components.Add(((LevelOneCollisionLayer)collisionLayer).CanyonCollider);

            return components;
        }

    }

}

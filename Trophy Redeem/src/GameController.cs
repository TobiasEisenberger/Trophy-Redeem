using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using Trophy_Redeem.src.character.player;
using Trophy_Redeem.src.components.collision;
using Trophy_Redeem.src.components;
using Trophy_Redeem.src.graphics;
using Trophy_Redeem.src.maps;
using System.Windows.Shapes;
using Trophy_Redeem.src.model;
using Trophy_Redeem.src.gamecontroller;
using System.Media;

namespace Trophy_Redeem.src
{
    public class GameController
    {

        public event EventHandler OnFinish;

        public GameState State { get; private set; }
        public PlayerHealthState HealthState { get; protected set; }
        public SaveGame SaveGameState { get; set; }
        public InGameOverlay? InGameOverlay;
        public Levelselect? Levelselect;

        public List<Component> ScheduledNewComponents { get; private set; } = new List<Component>();
        public List<Component> ScheduledGarbageComponents { get; private set; } = new List<Component>();

        protected long lastTicks;
        protected TimeSpan elapsedTime;
        protected CollisionLayer collisionLayer;
        protected CollisionDetector collisionDetector;
        protected CustomTiledMap tiledMap;
        protected GameObject player;
        protected double gravity;
        protected double scaleY;
        protected double scaleX;        

        TransformGroup transformGroup = new TransformGroup();
        ScaleTransform scaleTrans = new ScaleTransform();
        TranslateTransform translateTrans = new TranslateTransform();
        double screenWidth = 1;
        double screenHeight = 1;

        protected GameController(SaveGame saveGame)
        {
            transformGroup.Children.Add(translateTrans);
            transformGroup.Children.Add(scaleTrans);
            SaveGameState = saveGame;
            State = GameState.Running;
        }

        public virtual TransformGroup GetTransformations()
        {
            if (transformGroup == null)
            {
                return new TransformGroup();
            }
            return transformGroup;
        }

        public virtual void CanvasLoaded(double actualHeight, double actualWidth)
        {
            scaleTrans.ScaleX *= scaleX;
            scaleTrans.ScaleY *= scaleY;

            translateTrans.X = 0;
            translateTrans.Y = 0;

            screenHeight = actualHeight;
            screenWidth = actualWidth;
        }

        public void OnPlayerDamage(object sender, EventArgs args)
        {
            player.Health--;
            if (player.Health == 0)
            {
                HealthState = PlayerHealthState.Dead;
                var playerComponent = (Player)player.component;
                playerComponent.Die();
            }
            if (InGameOverlay != null)
            {
                InGameOverlay.RemoveHeart();
            }
        }

        protected void InitPlayer(GameObject player, Point spawn)
        {
            this.player = player;
            ((Player)player.component).OnDamage += OnPlayerDamage;

            if (SaveGameState.PlayerHealth != null)
            {
                player.Health = SaveGameState.PlayerHealth.Value;
                if (player.Health == 0)
                    ((Player)player.component).Die();
            }

            Canvas.SetLeft(player.GetVisualComponent(), spawn.X);
            Canvas.SetTop(player.GetVisualComponent(), spawn.Y - player.GetVisualComponent().Height);
        }

        protected void ControlPlayer()
        {
            var player = (Player)this.player.component;
            var playerVisual = this.player.GetVisualComponent();

            double unitsY = 0;
            double unitsX = 0;

            if (Keyboard.IsKeyDown(Key.A))
            {
                unitsX = -this.player.velocity * elapsedTime.TotalSeconds;
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                unitsX = this.player.velocity * elapsedTime.TotalSeconds;
            }

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                player.Attack();
            }

            if (unitsX != 0 && !player.HitStop)
            {
                player.Turn(new Vector(unitsX, unitsY));
                player.Run();
            }
            else
            {
                player.Idle();
            }

            if (player.IsAttacking() || player.HitStop)
            {
                unitsX = 0;
            }

            this.player.Update(elapsedTime);
            if (this.player.State != GameObjectState.Grounded)
            {
                unitsY = Physics.FallingHeight(gravity, this.player.FallingTime) - Physics.FallingHeight(gravity, this.player.FallingTime.Subtract(elapsedTime));
            }

            if (Keyboard.IsKeyDown(Key.Space) && this.player.State == GameObjectState.Grounded && !player.HitStop)
            {
                player.Jump();
                this.player.State = GameObjectState.Jumping;
            }

            if (this.player.State == GameObjectState.Jumping)
            {                
                double jumpVelocity = Physics.JumpVelocity(gravity, this.player.jumpHeight);
                unitsY += Physics.JumpHeight(jumpVelocity, this.player.FallingTime) - Physics.JumpHeight(jumpVelocity, this.player.FallingTime.Subtract(elapsedTime));
                if (unitsY > 0)
                {
                    this.player.State = GameObjectState.Falling;
                }
            }

            var newTop = Canvas.GetTop(playerVisual) + unitsY;
            var newLeft = Canvas.GetLeft(playerVisual) + unitsX;

            if (this.player.State != GameObjectState.Jumping)
            {
                this.player.State = GameObjectState.Falling;
            }

            foreach (var collider in collisionDetector.CollidesWithGround(new RectangleGeometry(new Rect(newLeft, newTop, playerVisual.Width, playerVisual.Height))))
            {
                var shape = collider.GetElements()[0];
                if (Canvas.GetLeft(playerVisual) >= collider.Start.X + shape.RenderedGeometry.Bounds.Width || Canvas.GetLeft(playerVisual) + playerVisual.Width <= collider.Start.X)
                {
                    // Player is either on right or left side of ground -> undo x movement
                    newLeft -= unitsX;
                }
                else
                {
                    this.player.State = GameObjectState.Grounded;
                    // Player is on top/under -> set player on top of ground
                    Point point, tangent;
                    double progress = (Canvas.GetLeft(playerVisual) + playerVisual.Width - collider.Start.X) / collider.Length;
                    shape.RenderedGeometry.GetFlattenedPathGeometry().GetPointAtFractionLength(progress, out point, out tangent);
                    newTop = point.Y - playerVisual.Height;
                }
            }

            foreach (var collider in collisionDetector.CollidesWithWall(new RectangleGeometry(new Rect(newLeft, newTop, playerVisual.Width, playerVisual.Height))))
            {
                if (this.player.State == GameObjectState.Jumping)
                {
                    this.player.State = GameObjectState.Falling;
                    newTop = Canvas.GetTop(playerVisual);
                }
                newLeft = Canvas.GetLeft(playerVisual);
            }

            moveMapWithPlayer(playerVisual, unitsX, unitsY);
            if (!(-newTop > 0 || newTop + playerVisual.ActualHeight > tiledMap.height))
            {
                Canvas.SetTop(playerVisual, newTop);
            }

            if (!(-newLeft > 0 || newLeft + playerVisual.ActualWidth > tiledMap.width) && !player.IsDying && !player.IsDead)
            {
                Canvas.SetLeft(playerVisual, newLeft);
            }
        }

        protected void moveMapWithPlayer(Shape player, double unitsX, double unitsY)
        {
            if (scaleTrans == null || translateTrans == null)
                return;

            var xMax = screenWidth;
            var yMax = screenHeight;

            var xNull = translateTrans.X;
            var yNull = translateTrans.Y;

            if (Canvas.GetTop(player) + yNull <= yMax * 0.2 / scaleY && unitsY < 0)
            {
                translateTrans.Y += -unitsY * 1;
            }

            if (Canvas.GetTop(player) + player.ActualHeight + yNull >= yMax * 0.80 / scaleY && unitsY > 0)
            {
                translateTrans.Y += -unitsY * 1;
            }

            if (Canvas.GetLeft(player) + xNull <= xMax * 0.2 / scaleX && unitsX < 0)
            {
                translateTrans.X += -unitsX * 1;
            }

            if (Canvas.GetLeft(player) + player.ActualWidth + xNull >= xMax * 0.80 / scaleX && unitsX > 0)
            {
                translateTrans.X += -unitsX * 1;
            }

            if (translateTrans.X > 0)
            {
                translateTrans.X = 0;
            }
            if (translateTrans.Y > 0)
            {
                translateTrans.Y = 0;
            }
            if (-translateTrans.X > tiledMap.width - screenWidth / scaleX)
            {
                translateTrans.X = -(tiledMap.width - screenWidth / scaleX);
            }

            if (-translateTrans.Y > tiledMap.height - screenHeight / scaleY)
            {
                translateTrans.Y = -(tiledMap.height - screenHeight / scaleY);
            }
        }

        public virtual List<Component> GetComponents()
        {
            List<Component> components = new List<Component>
            {
                tiledMap,
                player.component,
            };

            foreach (var collider in collisionLayer.ground)
            {
                components.Add(collider);
            }

            foreach (var collider in collisionLayer.wall)
            {
                components.Add(collider);
            }

            return components;
        }

        public virtual void GameLoop(object? sender, EventArgs e)
        {
            RenderingEventArgs renderingArgs = (RenderingEventArgs)e;
            long ticks = renderingArgs.RenderingTime.Ticks;
            if (lastTicks == -1) lastTicks = ticks;
            elapsedTime = TimeSpan.FromTicks(ticks - lastTicks);
            lastTicks = ticks;

            UpdateSaveGame();
        }

        protected void Finish()
        {
            if (State != GameState.Finished)
            {
                State = GameState.Finished;
                player.Health = 3;
                UpdateSaveGame();

                if (InGameOverlay != null)
                    InGameOverlay.Update(SaveGameState);
                if (OnFinish != null)
                    OnFinish(this, EventArgs.Empty);
            }
        }

        private void UpdateSaveGame()
        {
            SaveGameState.PlayerHealth = player.Health;

            if (State == GameState.Finished)
            {
                if (GetType() == typeof(LevelOne) && SaveGameState.HighestFinishedLevel == null)
                {
                    SaveGameState.HighestFinishedLevel = GetType().Name;
                }
                else if (GetType() == typeof(LevelTwo) && SaveGameState.HighestFinishedLevel != typeof(LevelThree).GetType().Name)
                {
                    SaveGameState.HighestFinishedLevel = GetType().Name;
                }
                else if (GetType() == typeof(LevelThree))
                {
                    SaveGameState.HighestFinishedLevel = GetType().Name;
                }
            }
        }

    }
}

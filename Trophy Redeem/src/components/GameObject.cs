using System;
using System.Windows.Shapes;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.components
{

    public enum GameObjectState
    {
        Grounded,
        Jumping,
        Falling
    }

    public class GameObject
    {

        public readonly Component component;
        public double velocity;
        public readonly double jumpHeight;
        
        public int Health { get; set; }

        public TimeSpan FallingTime { get; private set; }
        public double? LastGroundPos { get; set; }

        TimeSpan elapsedTimeLastUpdate;
        GameObjectState state = GameObjectState.Grounded;

        public GameObject(Component component, double velocity, double jumpHeight)
        {
            this.component = component;
            this.velocity = velocity;
            this.jumpHeight = jumpHeight;
            elapsedTimeLastUpdate = TimeSpan.Zero;
            FallingTime = TimeSpan.Zero;
        }

        public GameObjectState State 
        { 
            get { return state; }
            set 
            {
                OnStateChange(state, value);
                state = value; 
            }
        }

        public void Update(TimeSpan elapsedTime)
        {
            elapsedTimeLastUpdate = TimeSpan.FromSeconds(elapsedTime.TotalSeconds);
            if (state == GameObjectState.Falling || state == GameObjectState.Jumping)
            {
                FallingTime += elapsedTimeLastUpdate;
            }
        }

        public void Reset()
        {
            elapsedTimeLastUpdate = TimeSpan.Zero;
            FallingTime = TimeSpan.Zero;
        }

        public Shape GetVisualComponent()
        {
            return component.GetElements()[0];
        }

        private void OnStateChange(GameObjectState oldState, GameObjectState newState)
        {
            if (newState == GameObjectState.Jumping && FallingTime.TotalSeconds == 0)
            {
                FallingTime += elapsedTimeLastUpdate;
            }

            if (newState == GameObjectState.Grounded)
            {
                FallingTime = TimeSpan.Zero;
            }

            if (oldState == GameObjectState.Jumping && newState == GameObjectState.Falling)
            {
                FallingTime = TimeSpan.Zero;
            }
        }

    }
}

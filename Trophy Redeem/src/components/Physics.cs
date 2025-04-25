using System;

namespace Trophy_Redeem.src.components
{
    public static class Physics
    {

        public static double JumpVelocity(double gravity, double jumpHeight)
        {
            return Math.Sqrt(2 * gravity * jumpHeight);
        }

        public static double FallingHeight(double gravity, TimeSpan elapsedTime)
        {
            return 0.5 * gravity * Math.Pow(elapsedTime.TotalSeconds, 2);
        }

        public static double JumpHeight(double jumpVelocity, TimeSpan elapsedTime)
        {
            return -jumpVelocity * elapsedTime.TotalSeconds;
        }

    }
}

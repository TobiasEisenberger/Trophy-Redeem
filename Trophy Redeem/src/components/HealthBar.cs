using System.Windows.Media;
using System.Windows.Shapes;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem.src.components
{
    internal class HealthBar : Component
    {

        public int HealthPoints { get; private set; }
        public int CurrentHealthPoints { get; private set; }
        
        public double Size { get; private set; }

        public HealthBar(double size, int healthPoints)
        {
            Size = size;
            HealthPoints = healthPoints;
            CurrentHealthPoints = healthPoints;
            var healthBar = new Rectangle();
            healthBar.Width = size;
            healthBar.Height = 2;
            healthBar.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            healthBar.RadiusX = 2;
            healthBar.RadiusY = 2;
            elementList.Add(healthBar);
        }

        public void Reduce()
        {
            CurrentHealthPoints--;
            var healthBar = GetElements()[0];
            healthBar.Width = Size * (CurrentHealthPoints / (double)HealthPoints);
        }

    }
}

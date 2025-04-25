using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Trophy_Redeem.src.components;

namespace Trophy_Redeem.src.model
{
    /// <summary>
    /// Interaktionslogik für InGameOverlay.xaml
    /// </summary>
    public partial class InGameOverlay : UserControl
    {
        public InGameOverlay()
        {
            InitializeComponent();
        }

        public void Update(SaveGame saveGame)
        {
            Reset();
            if (saveGame.PlayerHealth.HasValue)
            {
                var hearts = healthBar.Children.OfType<Image>();
                if (saveGame.PlayerHealth < hearts.Count())
                {
                    int removeCount = hearts.Count() - (int)saveGame.PlayerHealth;
                    foreach (var heart in hearts.Reverse())
                    {
                        heart.Opacity = 0;
                        removeCount--;
                        if (removeCount == 0)
                            return;
                    }
                }
            }
        }

        public void Reset()
        {
            var hearts = healthBar.Children.OfType<Image>();
            foreach (Image heart in hearts)
            {
                heart.Opacity = 1;
                heart.BeginAnimation(OpacityProperty, null);
            }
        }

        public void RemoveHeart()
        {
            var hearts = healthBar.Children.OfType<Image>().Where(image => image.Opacity == 1);
            if (hearts.Count() > 0)
            {
                hearts.Last().BeginAnimation(OpacityProperty, BuildHeartRemoveAnimation());
            }
        }

        private DoubleAnimation BuildHeartRemoveAnimation()
        {
            var animation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(300)));
            return animation;
        }

    }
}

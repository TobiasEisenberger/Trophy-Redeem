using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Trophy_Redeem.src.components;
using Trophy_Redeem.src.gamecontroller;

namespace Trophy_Redeem
{
    /// <summary>
    /// Interaktionslogik für Levelselect.xaml
    /// </summary>
    public partial class Levelselect : UserControl
    {

        SaveGame saveGameState;

        public Levelselect(SaveGame saveGameState)
        {
            InitializeComponent();

            this.saveGameState = saveGameState;
            UnlockLevel();
        }

        public event EventHandler Level1BtnClick;
        public event EventHandler Level2BtnClick;
        public event EventHandler Level3BtnClick;

        public void Reset(SaveGame saveGameState)
        {
            this.saveGameState = saveGameState;
            level2_btn.IsEnabled = false;
            level2_btn.Background = Brushes.Red;
            level3_btn.IsEnabled = false;
            level3_btn.Background = Brushes.Red;
            UnlockLevel();
        }

        public void UnlockLevel()
        {
            if (saveGameState.HighestFinishedLevel == typeof(LevelOne).Name)
            {
                level2_btn.IsEnabled = true;
                level2_btn.Background = Brushes.Green;
            } else if (saveGameState.HighestFinishedLevel == typeof(LevelTwo).Name || saveGameState.HighestFinishedLevel == typeof(LevelThree).Name)
            {
                level2_btn.IsEnabled = true;
                level2_btn.Background = Brushes.Green;
                level3_btn.IsEnabled = true;
                level3_btn.Background = Brushes.Green;
            }
        }

        private void level1_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Level1BtnClick != null)
            {
                Level1BtnClick(this, EventArgs.Empty);
            }

        }

        private void level2_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Level2BtnClick != null)
            {
                Level2BtnClick(this, EventArgs.Empty);
            }
        }

        private void level3_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Level3BtnClick != null)
            {
                Level3BtnClick(this, EventArgs.Empty);
            }
        }

    }
}

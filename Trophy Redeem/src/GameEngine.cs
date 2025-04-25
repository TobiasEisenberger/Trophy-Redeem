using Microsoft.Win32;
using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Trophy_Redeem.src.components;
using Trophy_Redeem.src.gamecontroller;
using Trophy_Redeem.src.model;

namespace Trophy_Redeem.src
{

    internal class GameEngine
    {
        // Main Window Controls
        ContentControl mainContent;
        ContentControl overlayContent;

        // Komponenten zum Wiederverwenden
        Menu menu;
        Levelselect levelselect;
        PauseMenu pausemenu;
        InGameOverlay inGameOverlay = new InGameOverlay();
        RenderingCanvas renderingCanvas;
        DeathMenu deathmenu;
        //protected GameObject player;
        public PlayerHealthState Healthstate{ get; }

        // Spielstand
        SaveGame saveGame;
                
        MediaPlayer title = new MediaPlayer();

        public GameEngine(ref ContentControl mainContent, ref ContentControl overlayContent)
        {
            // Neuen Spielstand erstellen
            saveGame = new SaveGame();

            this.mainContent = mainContent;
            this.overlayContent = overlayContent;

            // Initialize UI
            menu = new Menu();
            levelselect = new Levelselect(saveGame);
            pausemenu = new PauseMenu();
            deathmenu = new DeathMenu();

            this.overlayContent.Content = menu;            
            mainContent.Focusable = true;
            Keyboard.AddKeyDownHandler(mainContent, HandleInput);

            // Hookup Ui Events
            menu.NewBtnClick += NewGame;
            menu.StartBtnClick += StartGame;
            menu.LoadBtnClick += LoadGame;
            levelselect.Level1BtnClick += StartLevel1;
            levelselect.Level2BtnClick += StartLevel2;
            levelselect.Level3BtnClick += StartLevel3;            
            pausemenu.ResumeBtnClick += ResumeGame;
            pausemenu.BackBtnClick += BackToMenu;
            pausemenu.SaveBtnClick += SaveGame;


            title.Open(new Uri(@"src\assets\Sounds\Title.wav", UriKind.Relative));
            title.Volume = 0.8;
            title.Play();            
        }

        public void NewGame(object sender, EventArgs args)
        {
            saveGame = new SaveGame();
            inGameOverlay.Reset();
            StartGame(sender, args);
        }

        public void StartGame(object sender, EventArgs args)
        {
            overlayContent.Content = levelselect;
            levelselect.Reset(saveGame);
            mainContent.Focus();
        }

        public void StartLevel1(object sender, EventArgs args)
        {
            overlayContent.Content = inGameOverlay;
            RenderNewGameController(new LevelOne(saveGame) { InGameOverlay = inGameOverlay });
            mainContent.Focus();
            title.Stop();            
        }

        public void StartLevel2(object sender, EventArgs args)
        {
            overlayContent.Content = inGameOverlay;
            RenderNewGameController(new LevelTwo(saveGame) { InGameOverlay = inGameOverlay });
            mainContent.Focus();            
            title.Stop();
        }

        public void StartLevel3(object sender, EventArgs args)
        {
            overlayContent.Content = inGameOverlay;
            RenderNewGameController(new LevelThree(saveGame) { InGameOverlay = inGameOverlay });
            mainContent.Focus();   
            title.Stop();
        }

        public void ResumeGame(object sender, EventArgs args)
        {
            pausemenu.Close();
            overlayContent.Content = inGameOverlay;
            mainContent.Focus();
            title.Stop();
        }

        public void BackToMenu(object sender, EventArgs args)
        {
            overlayContent.Content = menu;
            menu.EnableContinueButton();
        }
        
        public void HandleInput(object sender, KeyEventArgs e)
        {           
            if (e.Key == Key.Escape)
            {                
                pausemenu.Open();
                overlayContent.Content = pausemenu;
                mainContent.Focus();                
                title.Play();
            }
        }

        public void RenderNewGameController(GameController gameController)
        {
            if (renderingCanvas != null)
            {
                CompositionTarget.Rendering -= renderingCanvas.GameLoop;
            }
            gameController.OnFinish += StartGame;
            renderingCanvas = new RenderingCanvas(gameController);
            mainContent.Content = renderingCanvas;

            CompositionTarget.Rendering += renderingCanvas.GameLoop;
        }

        public void LoadGame(object sender, EventArgs args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            openFileDialog.Filter = "Game Saves (*.game)|*.game";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    if (fileContent != null)
                    {
                        var newGameData = JsonSerializer.Deserialize<SaveGame>(fileContent);
                        if (newGameData != null)
                        {
                            saveGame = newGameData;
                            inGameOverlay.Update(saveGame);
                            levelselect.Reset(saveGame);
                            menu.EnableContinueButton();
                        }
                    }
                }
            }
        }

        public void SaveGame(object sender, EventArgs args)
        {            
            string fileName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\save.game";
            int count = 1;
            while (File.Exists(fileName))
            {
                fileName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\save" + count + ".game";
                count++;
            }
            string jsonString = JsonSerializer.Serialize(saveGame);
            File.WriteAllText(fileName, jsonString);
        }

    }
}

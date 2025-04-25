using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using Trophy_Redeem.src;
using Trophy_Redeem.src.graphics;

namespace Trophy_Redeem
{
    /// <summary>
    /// Interaktionslogik für Scene.xaml
    /// </summary>
    public partial class RenderingCanvas : UserControl
    {

        Renderer renderer;
        Viewport viewport;
        GameController currentGameController;

        public RenderingCanvas(GameController gameController)
        {
            InitializeComponent();

            currentGameController = gameController;
            viewport = new Viewport(canvas);
            renderer = new Renderer(viewport);

            var gameComponents = gameController.GetComponents();

            gameComponents.ForEach(x =>
            {
                renderer.Render(x);
            });
            canvas.RenderTransform = gameController.GetTransformations();
        }

        public void GameLoop(object? sender, EventArgs e)
        {
            currentGameController.GameLoop(sender, e);
            RemoveUnusedComponents();
            RenderScheduledComponents();
        }

        private void RemoveUnusedComponents()
        {
            foreach (Component component in currentGameController.ScheduledGarbageComponents.ToList())
            {
                foreach (Shape shape in component.GetElements())
                {
                    viewport.Remove(shape);
                }
                currentGameController.ScheduledGarbageComponents.Remove(component);
            }
        }

        private void RenderScheduledComponents()
        {
            foreach (Component component in currentGameController.ScheduledNewComponents.ToList())
            {
                renderer.Render(component);
                currentGameController.ScheduledNewComponents.Remove(component);
            }
        }

        private void canvas_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            currentGameController.CanvasLoaded(canvas.ActualHeight, canvas.ActualWidth);
        }

    }

}

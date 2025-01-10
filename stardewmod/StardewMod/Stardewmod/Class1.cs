using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZoomMod
{
    public class ModEntry : Mod
    {
        private Texture2D zoomInButtonTexture;
        private Texture2D zoomOutButtonTexture;
        private Rectangle zoomInButtonBounds;
        private Rectangle zoomOutButtonBounds;

        public override void Entry(IModHelper helper)
        {
            // Cargar las texturas de los botones
            zoomInButtonTexture = helper.ModContent.Load<Texture2D>("assets/zoom_in.png");
            zoomOutButtonTexture = helper.ModContent.Load<Texture2D>("assets/zoom_out.png");

            // Registrar eventos
            helper.Events.Display.RenderedHud += OnRenderedHud;
            helper.Events.Input.ButtonPressed += OnButtonPressed;

            // Definir las posiciones de los botones
            zoomInButtonBounds = new Rectangle(10, 10, 48, 48); // Cambia las coordenadas y tamaño si es necesario
            zoomOutButtonBounds = new Rectangle(70, 10, 48, 48);
        }

        private void OnRenderedHud(object sender, RenderedHudEventArgs e)
        {
            // Dibujar los botones en la pantalla
            var spriteBatch = e.SpriteBatch;
            spriteBatch.Draw(zoomInButtonTexture, zoomInButtonBounds, Color.White);
            spriteBatch.Draw(zoomOutButtonTexture, zoomOutButtonBounds, Color.White);
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // Detectar clic en los botones
            if (e.Button == SButton.MouseLeft)
            {
                var mouseX = Game1.getMouseX();
                var mouseY = Game1.getMouseY();

                if (zoomInButtonBounds.Contains(mouseX, mouseY))
                {
                    Game1.options.zoomLevel = Math.Min(Game1.options.zoomLevel + 0.1f, 2f); // Máximo zoom in
                }
                else if (zoomOutButtonBounds.Contains(mouseX, mouseY))
                {
                    Game1.options.zoomLevel = Math.Max(Game1.options.zoomLevel - 0.1f, 0.5f); // Máximo zoom out
                }
            }
        }
    }
}

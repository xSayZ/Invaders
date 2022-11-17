using System.Collections.Generic;
using SFML.Graphics;

namespace Invader_Omexamination
{
    public class AssetManager
    {
        private readonly Dictionary<string, Texture> _textures;
        private readonly Dictionary<string, Font> _fonts;

        public AssetManager()
        {
            _textures = new Dictionary<string, Texture>();
            _fonts = new Dictionary<string, Font>();
        }

        public Texture LoadTexture(string name)
        {
            if (_textures.TryGetValue(name, out Texture found))
            {
                return found;
            }
            
            string fileName = $"assets/textures/{name}.png";
            Texture texture = new Texture(fileName);
            _textures.Add(name, texture);
            return texture;
        }

        public Font LoadFont(string name)
        {
            if (_fonts.TryGetValue(name, out Font found))
            {
                return found;
            }
            
            string fileName = $"assets/fonts/{name}.ttf";
            Font font = new Font(fileName);
            _fonts.Add(name, font);
            return font;
        }
    }
}
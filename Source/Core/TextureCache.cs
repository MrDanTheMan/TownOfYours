using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TownOfYours.Core.Data;

namespace TownOfYours.Core
{
    public class TextureCache
    {
        private static TextureCache m_instance;
        private readonly Dictionary<string, Texture2D> m_textureMap;

        /// <summary>
        /// Gets debug texture
        /// </summary>
        public Texture DebugTexture
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        private TextureCache()
        {
            m_textureMap = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Access to global instance
        /// </summary>
        public static TextureCache Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new TextureCache();
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Safely loads all textures content
        /// </summary>
        /// <param name="manifest"></param>
        public void LoadTextureResources(AssetManifest manifest, ContentManager content)
        {
            Debug.Print("TextureCache: Loading resources ... !");

            foreach (string textureName in manifest.Textures)
            {
                try
                {
                    Texture2D texture = content.Load<Texture2D>(string.Format("Textures/{0}", textureName));
                    m_textureMap[textureName] = texture;
                    if (textureName == "DebugTexture_02")
                    {
                        DebugTexture = texture;
                    }

                    Debug.Print(string.Format("TextureCache: Loading texture resource: {0}", textureName));
                }
                catch
                {
                    Debug.Print(string.Format("TextureCache: Failed to load texture: {0}", textureName));
                }
            }

            Debug.Assert(DebugTexture != null, "No debug texture found in the texture resources !");
            Debug.Print("TextureCache: Done !");
        }

        /// <summary>
        /// Fetches texture resource
        /// </summary>
        /// <param name="textureName">Resource name</param>
        /// <returns></returns>
        public Texture2D GetTexture(string textureName)
        {
            Debug.Assert(m_textureMap.ContainsKey(textureName), "Texture resource not found !");
            return m_textureMap[textureName];
        }
    }
}

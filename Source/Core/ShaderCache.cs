using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TownOfYours.Core.Data;

namespace TownOfYours.Core
{
    public class ShaderCache
    {
        private static ShaderCache m_instance;
        private readonly Dictionary<string, Effect> m_shaderMap;

        /// <summary>
        /// Ctor
        /// </summary>
        private ShaderCache()
        {
            m_shaderMap = new Dictionary<string, Effect>();
        }

        /// <summary>
        /// Access to global instance of the shader cache object
        /// </summary>
        public static ShaderCache Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ShaderCache();
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Gets handle to the boing shader
        /// Boing shader is used for rendering if given renderable has invalid shader / material setup
        /// to indicate that there is an issue
        /// </summary>
        public Effect BoingShader
        {
            get;
            private set;
        }

        /// <summary>
        /// Safely loads all shaders content
        /// </summary>
        /// <param name="manifest"></param>
        public void LoadShaderResources(AssetManifest manifest, ContentManager content)
        {
            Debug.Print("ShaderCache: Loading resources ... !");

            foreach (string shaderName in manifest.Shaders)
            {
                try
                {
                    Effect shader = content.Load<Effect>(string.Format("Shaders/{0}", shaderName));
                    m_shaderMap[shaderName] = shader;
                    Debug.Print(string.Format("ShaderCache: Loading shader resource: {0}", shaderName));
                    if (shaderName == "Boing")
                    {
                        BoingShader = shader;
                    }
                }
                catch
                {
                    Debug.Print(string.Format("ShaderCache: Failed to load shader: {0}", shaderName));
                }
            }

            Debug.Assert(BoingShader != null, "No boing shader found in the shader resources !");
            Debug.Print("ShaderCache: Done !");
        }

        /// <summary>
        /// Fetches shader resource
        /// </summary>
        /// <param name="shaderName">Resource name</param>
        /// <returns></returns>
        public Effect GetShader(string shaderName)
        {
            Debug.Assert(m_shaderMap.ContainsKey(shaderName), "Shader resource not found !");
            return m_shaderMap[shaderName];
        }
    }
}

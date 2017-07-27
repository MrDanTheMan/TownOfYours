using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace TownOfYours.Core.Data
{
    public class AssetManifest
    {
        public List<string> Shaders
        {
            get;
            private set;
        }

        public List<string> Textures
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        private AssetManifest()
        {
            Shaders = new List<string>();
            Textures = new List<string>();
        }

        /// <summary>
        /// Parses asset manifest file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static AssetManifest Read(Stream stream)
        {
            AssetManifest output = new AssetManifest();
            XDocument xDoc;

            try
            {
                xDoc = XDocument.Load(stream);
                if (xDoc.Root == null)
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }

            foreach (XElement assetElement in xDoc.Root.Descendants())
            {
                switch (assetElement.Name.LocalName.ToLower())
                {
                    case "shader":
                        string shaderName = ParseNameAttribute(assetElement);
                        if (shaderName != null)
                        {
                            output.Shaders.Add(shaderName);
                        }
                        break;
                    case "texture":
                        string textureName = ParseNameAttribute(assetElement);
                        if(textureName != null)
                        {
                            output.Textures.Add(textureName);
                        }
                        break;
                    default:
                        continue;
                }
            }

            return output;
        }

        /// <summary>
        /// Parse name attribute value from given xml element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static string ParseNameAttribute(XElement element)
        {
            XAttribute attr = element.Attribute("name");
            string val = attr?.Value;

            if (string.IsNullOrEmpty(val))
            {
                return null;
            }

            return val;
        }
    }
}

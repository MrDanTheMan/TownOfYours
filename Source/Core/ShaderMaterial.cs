using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace TownOfYours.Core
{
    public class ShaderMaterial
    {
        public static ShaderMaterial None = new ShaderMaterial();
        private readonly ShaderPropertyBase[] m_overrides;
        /// <summary>
        /// Gets this material shader
        /// </summary>
        public Effect Shader
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="shader"></param>
        public ShaderMaterial (Effect shader)
        {
            Debug.Assert(shader != null, "Shader handle is null !");
            Shader = shader;
            m_overrides = ResolveOverrideParameters(shader);
        }

        /// <summary>
        /// private ctor
        /// </summary>
        private ShaderMaterial()
        {
            m_overrides = new ShaderPropertyBase[0];
        }

        /// <summary>
        /// Get shader proeprty override value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void GetValue<T>(string propertyName, ref T value)
        {
            for (int i = 0; i < m_overrides.Length; i++)
            {
                ShaderPropertyBase thisProperty = m_overrides[i];
                if (propertyName == thisProperty.Name)
                {
                    Debug.Assert(thisProperty is ShaderProperty<T>, "Shader proeprty type miss match !");
                    value = (thisProperty as ShaderProperty<T>).Value;
                }
            }
        }

        /// <summary>
        /// Set shader property override value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetValue<T>(string propertyName, T value)
        {
            for (int i = 0; i < m_overrides.Length; i++)
            {
                ShaderPropertyBase thisProperty = m_overrides[i];
                if (propertyName == thisProperty.Name)
                {
                    Debug.Assert(thisProperty is ShaderProperty<T>, "Shader proeprty type miss match !");
                    (thisProperty as ShaderProperty<T>).Value = value;
                    return;
                }
            }
        }

        /// <summary>
        /// Updates shader parameters with the overriden values
        /// </summary>
        public void PushShaderOverrides()
        {
            foreach (ShaderPropertyBase property in m_overrides)
            {
                EffectParameter shaderParam = Shader.Parameters[property.Name];
                Debug.Assert(shaderParam != null, "Shader paramter is null !");

                switch (shaderParam.ParameterType)
                {
                    case EffectParameterType.Bool:
                        Debug.Assert(property is ShaderProperty<bool>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<bool>).Value);
                        break;
                    case EffectParameterType.Int32:
                        Debug.Assert(property is ShaderProperty<int>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<int>).Value);
                        break;
                    case EffectParameterType.Single:
                        Debug.Assert(property is ShaderProperty<Single>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<Single>).Value);
                        break;
                    case EffectParameterType.Texture:
                        Debug.Assert(property is ShaderProperty<Texture>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<Texture>).Value);
                        break;
                    case EffectParameterType.Texture2D:
                        Debug.Assert(property is ShaderProperty<Texture2D>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<Texture2D>).Value ?? TextureCache.Instance.DebugTexture);
                        break;
                    case EffectParameterType.Texture3D:
                        Debug.Assert(property is ShaderProperty<Texture3D>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<Texture3D>).Value);
                        break;
                    case EffectParameterType.TextureCube:
                        Debug.Assert(property is ShaderProperty<TextureCube>, "Invalid shader override value type !");
                        shaderParam.SetValue((property as ShaderProperty<TextureCube>).Value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Resolves overridable parameters from given shader
        /// </summary>
        /// <param name="shader"></param>
        /// <returns>Array of overridable shader properties</returns>
        private static ShaderPropertyBase[] ResolveOverrideParameters(Effect shader)
        {
            List<ShaderPropertyBase> overrides = new List<ShaderPropertyBase>();
            foreach (EffectParameter param in shader.Parameters)
            {
                if (!param.Name.StartsWith("STD_"))
                {
                    ShaderPropertyBase thisOverride = null;
                    switch (param.ParameterType)
                    {
                        case EffectParameterType.Bool:
                            thisOverride = new ShaderProperty<bool>(param.Name, false);
                            break;
                        case EffectParameterType.Int32:
                            thisOverride = new ShaderProperty<int>(param.Name, 0);
                            break;
                        case EffectParameterType.Single:
                            thisOverride = new ShaderProperty<Single>(param.Name, 0);
                            break;
                        case EffectParameterType.Texture:
                            thisOverride = new ShaderProperty<Texture>(param.Name, null);
                            break;
                        case EffectParameterType.Texture2D:
                            thisOverride = new ShaderProperty<Texture2D>(param.Name, null);
                            break;
                        case EffectParameterType.Texture3D:
                            thisOverride = new ShaderProperty<Texture3D>(param.Name, null);
                            break;
                        case EffectParameterType.TextureCube:
                            thisOverride = new ShaderProperty<TextureCube>(param.Name, null);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (thisOverride != null)
                    {
                        overrides.Add(thisOverride);
                    }
                }
            }

            return overrides.ToArray();
        }

        /// <summary>
        /// checks if given material is in invalid state
        /// </summary>
        /// <param name="material"></param>
        /// <returns>True if material setup is invalid</returns>
        public static bool IsInvalid(ShaderMaterial material)
        {
            if (material == null ||
                material == ShaderMaterial.None ||
                material.Shader == null)
            {
                return true;
            }

            return false;
        }
    }
}

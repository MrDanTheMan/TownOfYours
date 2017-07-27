namespace TownOfYours.Core
{
    public class ShaderProperty<T> : ShaderPropertyBase
    {
        /// <summary>
        /// Gets or sets this parameter value
        /// </summary>
        public T Value
        {
            get;
            set;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ShaderProperty()
        {
        }

        /// <summary>
        /// Alternative ctor
        /// </summary>
        /// <param name="propertyName">Shader porperty name</param>
        /// <param name="propertyValue">Shader property value</param>
        public ShaderProperty (string propertyName, T propertyValue)
        {
            Name = propertyName;
            Value = propertyValue;
        }
    }
}

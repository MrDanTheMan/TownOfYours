namespace TownOfYours.Core
{
    public class RenderStates
    {
        /// <summary>
        /// Gets or sets visibility render state
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets draw priority state
        /// </summary>
        public int DrawOder
        {
            get;
            set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public RenderStates()
        {
            Visible = true;
            DrawOder = 0;
        }
    }
}

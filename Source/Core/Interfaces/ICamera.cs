using Microsoft.Xna.Framework;

namespace TownOfYours.Core.Interfaces
{
    /// <summary>
    /// Event arguments data set for camera events
    /// </summary>
    public struct CameraEventArgs
    {
        public int m_width;
        public int m_height;
    }

    /// <summary>
    /// Event handler signature
    /// </summary>
    /// <param name="sender">Camera handle</param>
    /// <param name="args">Camera event arguments</param>
    public delegate void CameraEventHandler(ICamera sender, CameraEventArgs args);

    /// <summary>
    /// Camera interface encaupsulates all the nacasserry components needed to
    /// work with Renderer class
    /// </summary>
    public interface ICamera
    {
        event CameraEventHandler CameraResize;

        /// <summary>
        /// Gets or sets this camera view matrix
        /// </summary>
        Matrix ViewMatrix
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this camera projection matrix
        /// </summary>
        Matrix Projection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets this camera transform
        /// </summary>
        TransformMatrix Transform
        {
            get;
            set;
        }
    }
}
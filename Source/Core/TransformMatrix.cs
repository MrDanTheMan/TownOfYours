using Microsoft.Xna.Framework;

namespace TownOfYours.Core
{
    public class TransformMatrix
    {
        private Matrix m_transform;
        private Matrix m_rotation;
        private Matrix m_scale;
        private Matrix m_translation;
        private bool m_dirty;

        /// <summary>
        /// Gets this transform translation 
        /// </summary>
        public Vector3 Translation
        {
            get { return m_translation.Translation; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public TransformMatrix()
        {
            m_transform = Matrix.Identity;
            m_rotation = Matrix.Identity;
            m_scale = Matrix.Identity;
            m_translation = Matrix.Identity;
        }

        /// <summary>
        /// Get this transform composite matrix
        /// </summary>
        public Matrix Transform
        {
            get
            {
                if (m_dirty)
                {
                    ComputeComposite();
                }

                return m_transform;
            }
        }

        /// <summary>
        /// Translate this transform
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector3 translation)
        {
            m_translation.Translation += translation;
            m_dirty = true;
        }

        /// <summary>
        /// Sets this transform translation
        /// </summary>
        /// <param name="translation"></param>
        public void SetTranslation(Vector3 translation)
        {
            m_translation.Translation = translation;
            m_dirty = true;
        }

        /// <summary>
        /// Scale this trasnform
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector3 scale)
        {
            m_scale.Scale *= scale;
            m_dirty = true;
        }

        /// <summary>
        /// Scale this transform
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(float scale)
        {
            m_scale.Scale *= scale;
            m_dirty = true;
        }

        /// <summary>
        /// Set this transform scale
        /// </summary>
        /// <param name="scale"></param>
        public void SetScale(Vector3 scale)
        {
            m_scale.Scale = scale;
            m_dirty = true;
        }

        /// <summary>
        /// Set this transform scale
        /// </summary>
        /// <param name="scale"></param>
        public void SetScale(float scale)
        {
            m_scale.Scale = new Vector3(scale);
            m_dirty = true;
        }

        /// <summary>
        /// Rotate this transform
        /// </summary>
        /// <param name="rotation"></param>
        public void Rotate(Vector3 rotation)
        {
            Matrix x = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            Matrix y = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            Matrix z = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));

            m_rotation = (x * y * z) * m_rotation;
            m_dirty = true;
        }

        /// <summary>
        /// Set this transform rotation
        /// </summary>
        /// <param name="rotation"></param>
        public void SetRotation(Vector3 rotation)
        {
            Matrix x = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            Matrix y = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            Matrix z = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));

            m_rotation = x*y*z;
            m_dirty = true;
        }

        /// <summary>
        /// Recompute this trasnform composite matrix value
        /// </summary>
        private void ComputeComposite()
        {
            m_transform = m_translation * m_rotation * m_scale;
            m_dirty = false;
        }
    }
}

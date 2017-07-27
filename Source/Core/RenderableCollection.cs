using System.Collections.Generic;
using System.Collections.ObjectModel;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.Core
{
    public class RenderableCollection
    {
        private List<IRenderable> m_internalRenderables;

        /// <summary>
        /// Gets readonly list of renderables
        /// </summary>
        public ReadOnlyCollection<IRenderable> Renderables
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public RenderableCollection()
        {
            m_internalRenderables = new List<IRenderable>();
            Renderables = new ReadOnlyCollection<IRenderable>(m_internalRenderables.AsReadOnly());
        }

        /// <summary>
        /// Gets size of this collection
        /// </summary>
        public int Count
        {
            get { return m_internalRenderables.Count; }
        }

        /// <summary>
        /// Adds renderable to this collection
        /// </summary>
        /// <param name="renderable"></param>
        public void Add(IRenderable renderable)
        {
            m_internalRenderables.Add(renderable);
        }

        /// <summary>
        /// Removes renderbale from this collection
        /// </summary>
        /// <param name="renderable"></param>
        public void Remove(IRenderable renderable)
        {
            m_internalRenderables.Remove(renderable);
        }

        
    }
}

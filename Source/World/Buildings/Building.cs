using System;
using TownOfYours.Core;
using TownOfYours.Utilities;

namespace TownOfYours.World.Buildings
{
    public abstract class Building
    {
        private GridLocation m_location;

        /// <summary>
        /// Gets this building grid location
        /// </summary>
        public GridLocation Location
        {
            get { return m_location; }
            set
            {
                UpdateLocationHandler(m_location, value);
                m_location = value;
                OnLocationChanged(value);
            }
        }

        /// <summary>
        /// Gets or sets this building name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets this building GUID
        /// </summary>
        public Guid GUID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the render quad
        /// </summary>
        public Quad RenderQuad
        {
            get;
            protected set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        protected Building()
        {
            RenderQuad = new Quad(GlobalSettings.TILE_SIZE, PIVOT_MODE.DEFAULT);
            RenderQuad.States.DrawOder = GlobalSettings.BUILDING_LAYER_ORDER;
            RenderQuad.Material = new ShaderMaterial(ShaderCache.Instance.GetShader("Diff1"));
            GUID = Guid.NewGuid();
            Name = "";
            Location = new GridLocation();
        }

        /// <summary>
        /// Handles lcoation changed of this building
        /// </summary>
        /// <param name="location">Updated location</param>
        protected virtual void OnLocationChanged(GridLocation location)
        {
            if (RenderQuad != null)
            {
                RenderQuad.Transform.SetTranslation(Location.WorldSpace);
            }
        }

        private void UpdateLocationHandler(GridLocation oldHandle, GridLocation newHandle)
        {
            if(oldHandle != null)
            {
                oldHandle.LocationChanged -= OnLocationChanged;
            }

            if(newHandle != null)
            {
                newHandle.LocationChanged += OnLocationChanged;
            }
        }
    }
}

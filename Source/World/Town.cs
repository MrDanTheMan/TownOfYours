using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.Xna.Framework;
using TownOfYours.World.Buildings;
using TownOfYours.Core;
using TownOfYours.Core.Interfaces;
using TownOfYours.Utilities;

namespace TownOfYours.World
{
    public class Town
    {
        private ObservableCollection<Building> m_buildings;
        private RenderableCollection m_renderableQueue;

        public enum CONTROLLER
        {
            AI,
            PLAYER,
        }

        /// <summary>
        /// Gets or sets what controlls this towen in game
        /// </summary>
        public CONTROLLER ControllerType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the grid location of this town, that is also the location of town hall
        /// </summary>
        public GridLocation Location
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets this town color
        /// </summary>
        public Color TownColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets collection of town tiles 
        /// </summary>
        public ObservableCollection<TownTile> Tiles
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets list of buildings in this town
        /// </summary>
        public ObservableCollection<Building> Buildings
        {
            get { return m_buildings; }
            private set { m_buildings = value; }
        }

        /// <summary>
        /// Gets readonly collection of renderables
        /// </summary>
        public ReadOnlyCollection<IRenderable> Renderables
        {
            get { return m_renderableQueue.Renderables; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public Town(Tile tile)
        {            
            m_renderableQueue = new RenderableCollection();
            Location = new GridLocation(tile.Column, tile.Row);
            Buildings = new ObservableCollection<Building>();
            Tiles = new ObservableCollection<TownTile>();
            Buildings.CollectionChanged += OnBuildingsChanged;
            Tiles.CollectionChanged += OnTilesChanged;

            Buildings.Add(BuildingFactory.Create(BuildingFactory.BUILDING_TYPE.TownHall, Location.Location));
            Tiles.Add(new TownTile(tile));
            foreach (Tile nTile in tile.Neihgbours)
            {
                Tiles.Add(new TownTile(nTile));
            }
        }

        /// <summary>
        /// Handles changes made to the buildings collection
        /// Currently only supports removal and addition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="notifyCollectionChangedEventArgs"></param>
        private void OnBuildingsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Building thisBuilding in args.NewItems)
                {
                    m_renderableQueue.Add(thisBuilding.RenderQuad);
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Building thisBuilding in args.OldItems)
                {
                    m_renderableQueue.Remove(thisBuilding.RenderQuad);
                }
            }
        }

        /// <summary>
        /// Handles changes made to the tonw tiles collection
        /// Currently only supports removal and addition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTilesChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TownTile tile in args.NewItems)
                {
                    m_renderableQueue.Add(tile.RenderQuad);
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TownTile tile in args.OldItems)
                {
                    m_renderableQueue.Remove(tile.RenderQuad);
                }
            }
        }
    }
}

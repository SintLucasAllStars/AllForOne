using UnityEngine;

namespace MechanicFever
{
    public class Map : Singleton<Map>
    {
        [SerializeField]
        private int _sizeX = 0, _sizeZ = 0;
        public int SizeX => _sizeX;
        public int SizeZ => _sizeZ;

        [SerializeField]
        private MapX[] _grid = new MapX[0];
        public MapX[] Grid => _grid;

        private Tile[,] _tiles;

        [SerializeField]
        private GameObject _tile;

        [SerializeField]
        private GameObject[] _obstacles;

        public void SetMap(MapX[] map) => _grid = map;

        public int lastTab = 0;

        private void Start()
        {
            _tiles = new Tile[_sizeX, _sizeZ];
            for (int x = 0; x < _sizeX; x++)
            {
                for (int z = 0; z < _sizeZ; z++)
                {
                    switch (_grid[x].Columns[z].CollisionType)
                    {
                        case CollisionType.None:
                            _tiles[x, z] = Instantiate(_tile, new Vector3(x, 0, z), Quaternion.identity, this.transform).GetComponent<Tile>();
                            break;
                        case CollisionType.Obstacle:
                            _tiles[x, z] = Instantiate(_obstacles[_grid[x].Columns[z].Prop], new Vector3(x, 0, z), Quaternion.identity, this.transform).GetComponent<Tile>();
                            break;
                        case CollisionType.Occupied:
                            break;
                    }
                }
            }
            Screen.fullScreen = false;
        }

        public Node GetNode(int x, int z)
        {
            if (x < 0 || x > _sizeX - 1 || z < 0 || z > _sizeZ - 1)
            {
                if (x < 0)
                    x = 0;
                if (x > _sizeX - 1)
                    x = _sizeX - 1;
                if (z < 0)
                    z = 0;
                if (z > _sizeZ - 1)
                    z = _sizeZ - 1;
            }

            return Grid[x].Columns[z];
        }

        public void OccupyNode(Node node, PlayerUnit unit)
        {
            Node n = GetNode(node.X, node.Z);
            _tiles[n.X, n.Z].Occupy(unit);
            _grid[n.X].Columns[n.Z].SetCollisionType(CollisionType.Occupied);
        }

        public void ResetOldNode(int x, int y) => Grid[x].Columns[y].SetCollisionType(CollisionType.None);

        public bool IsValidNode(int x, int z)
        {
            //WALL DETECTION
            if (Grid[x].Columns[z].GetCollisionType() == CollisionType.Obstacle)
                return false;

            //UNIT DETECTION
            if (Grid[x].Columns[z].GetCollisionType() == CollisionType.Occupied)
                return false;

            return true;
        }
    }

    //Wanted to make this a struct, but then it cannot be nullable :/
    [System.Serializable]
    public class MapX
    {
        public Node[] Columns;
    }
}
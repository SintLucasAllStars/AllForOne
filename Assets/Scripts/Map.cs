using UnityEngine;

public class Map : Singleton<Map>
{
    [SerializeField]
    private int _sizeX = 0, _sizeZ = 0;
    public int SizeX => _sizeX;
    public int SizeZ => _sizeZ;

    [SerializeField]
    private MapX[] _grid = new MapX[0];
    public MapX[] Grid => _grid;

    public GameObject Tile, RedTile;

    public void SetMap(MapX[] map) => _grid = map;

    private void Start()
    {
        for (int x = 0; x < _sizeX; x++)
        {
            for (int z = 0; z < _sizeZ; z++)
            {
                Tile tile;
                if (Grid[x].Columns[z].CollisionType == CollisionType.None)
                    tile = Instantiate(Tile, new Vector3(x, 0, z), Quaternion.identity).GetComponent<Tile>();
                else
                    tile = Instantiate(RedTile, new Vector3(x, 0, z), Quaternion.identity).GetComponent<Tile>();
            }
        }
        Screen.fullScreen = false;
    }
    public Node GetNode(int x, int y)
    {
        if (x < 0 || x > _sizeX - 1 || y < 0 || y > _sizeZ - 1)
        {
            if (x < 0)
                x = 0;
            if (x > _sizeX - 1)
                x = _sizeX - 1;
            if (y < 0)
                y = 0;
            if (y > _sizeZ - 1)
                y = _sizeZ - 1;
        }

        return Grid[x].Columns[y];
    }

    public void OccupieNode(int x, int y)
    {
        Node n = GetNode(x, y);
        n.SetCollisionType(CollisionType.Occupied);
    }

    public void ResetOldNode(int x, int y) => Grid[x].Columns[y].SetCollisionType(CollisionType.None);

    public bool IsValidNode(int x, int y)
    {
        //BORDERS
        if (x == _sizeX - 1 || y == _sizeZ - 1 || y == 0 || x == 0)
            return false;

        //WALL DETECTION
        if (Grid[x].Columns[y].GetCollisionType() == CollisionType.Obstacle)
            return false;

        //UNIT DETECTION
        if (Grid[x].Columns[y].GetCollisionType() == CollisionType.Occupied)
            return false;

        return true;
    }
}

[System.Serializable]
public class MapX
{
    public Node[] Columns;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Prefab;

    public int width = 6;
    public int height = 6;
    float offset = 1.05f;
    // Start is called before the first frame update
    void Start()
    {
        CreateQuadTileMap();
    }

    void CreateQuadTileMap()
    {
        for(int x =0; x <= width; x++)
        {
            for(int z = 0; z <= width; z++)
            {
                GameObject T = Instantiate(Prefab);
                T.transform.position = new Vector3(x * offset, 0, z * offset);
                T.transform.parent = transform;
                //if (CheckOdd(z))
                //{
                //    T.GetComponent<MeshRenderer>().material.color = Color.red;
                //}
                //else
                //{
                //    T.GetComponent<MeshRenderer>().material.color = Color.white;
                //}
                T.name = x.ToString() + ", " + z.ToString(); 
            }
        }
    }
    bool CheckOdd(int value)
    {
        return value % 2 != 0;
    }
}

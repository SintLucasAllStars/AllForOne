using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public List<GameObject> m_P1Units;
    public List<GameObject> m_P2Units;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlacementMode(bool state)
    {
        UnitPlacement up = this.gameObject.GetComponent<UnitPlacement>();
        if (up == null)
        {
            Debug.Log("There is no UnitPlacement on this object!");
        }
        else
        {
            up.enabled = state;
        }
    }
}

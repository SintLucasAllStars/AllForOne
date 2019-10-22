using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour 
{
    
    public StatList stats = new StatList();
    public GameObject redMarker;
    public GameObject blueMarker;
    public bool active = false;


    

    
    // Start is called before the first frame update
    void Start()
    {
        AssignMarkers();
        stats.DebugStats();
    }

    public void AssignMarkers()
    {
        redMarker = gameObject.transform.GetChild(0).gameObject;
        blueMarker = gameObject.transform.GetChild(1).gameObject;
        redMarker.SetActive(false);
        blueMarker.SetActive(false);
    }

    public void ActivateMarker(bool markerActive)
    {
        if (markerActive == true)
        {
            if (stats.team == "Red")
            {
                redMarker.SetActive(true);
            }
            else
            {
                blueMarker.SetActive(true);
            }  
        }
        else
        {
            if (stats.team == "Red")
            {
                redMarker.SetActive(false);
            }
            else
            {
                blueMarker.SetActive(false);
            }  
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

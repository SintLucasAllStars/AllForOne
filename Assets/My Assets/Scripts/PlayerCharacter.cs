using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour 
{
    
    public StatList stats = new StatList();
    


    

    
    // Start is called before the first frame update
    void Start()
    {
        stats.DebugStats();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    
}

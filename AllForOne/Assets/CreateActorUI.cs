using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateActorUI : MonoBehaviour
{
    public Text healthT,speedT,defenseT,strenghtT;
    public Text cost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cost.text = "Cost : " + CreateActor.instance.cost.ToString();   
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateActorUI : MonoBehaviour
{
    public Text points;
    public Text cost;
    public Text currentPlayer;
    void Start()
    {
        
    }

    void Update()
    {
        cost.text = "Cost : " + CreateActor.instance.cost.ToString();
        currentPlayer.text = GameManager.instance.curPlayer.name + "'s turn";
        points.text = "Points: "+ GameManager.instance.curPlayer.points.ToString();
    }

}

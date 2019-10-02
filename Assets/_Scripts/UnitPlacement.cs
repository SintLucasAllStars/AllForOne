using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPlacement : MonoBehaviour
{
    public Text points, cost, currentPlayer;
    public Slider healthSlider, strengthSlider, speedSlider, defenceSlider;
    public Image background;
    public Button ready, place;
    
    //Disable PlayerController
    public GameObject [] playerObj;
 //Select player 1 or 2;
    private int player;
    private int currentPoints, currentCost;
    private int[] value;
    
    //Players ready
    private bool canStart;
    
 //   public GameManager unitPrefab;

 private void Awake()
 {
     //SetValues
     currentPoints = 100;
     currentCost = 5;
     Debug.Log(currentCost);
     //Start
     Statup();
     
 }

 private void Statup()
 {
     DisableOther();
     
     DisplaySliders();
     DisplayButtons();
     DisplayBackground();
     setValues(currentPoints, currentCost);
     
     

     SelectPlayer(player);
     player = 0;
     value = new int[4];
     
     for (int i = 0; i < value.Length; i++)
     {
         value[i] = 0;
     }

 }

 private void DisableOther()
 {
     playerObj = GameObject.FindGameObjectsWithTag("Player");
     playerObj[0].gameObject.SetActive(false);
 }

 private void setValues(int points, int cost)
 {
     this.cost.text = cost + "";
     this.points.text = points + "";
 }

 private void DisplaySliders()
 {
     healthSlider.gameObject.SetActive(true);
     strengthSlider.gameObject.SetActive(true);
     speedSlider.gameObject.SetActive(true);
     defenceSlider.gameObject.SetActive(true);
 }

 private void DisplayButtons()
 {
     ready.gameObject.SetActive(true);
     place.gameObject.SetActive(true);
 }

 private void DisplayBackground()
 {
     background.gameObject.SetActive(true);
 }

 private void Update()
 {
     int thisValue;
     int thisValue2;
     int thisValue3;
     
     if (player == 0 && currentPoints >= 100)
     {
         currentPlayer.text = "Player" + 1;
         value[0] = ((int) healthSlider.value);
         value[1] = ((int) strengthSlider.value);
         value[2] = ((int) speedSlider.value);
         value[3] = ((int) defenceSlider.value);
         thisValue = value[0] + value[1] + value[2] + value[3];
         thisValue2 = currentCost * thisValue;
         thisValue3 = currentPoints - currentCost;
         points.text = thisValue3.ToString();
         cost.text = thisValue2.ToString();
     }
     else if (player == 1 && currentPoints >= 100)
     {
         currentPlayer.text = "Player" + 2;
         value[0] = ((int) healthSlider.value);
         value[1] = ((int) strengthSlider.value);
         value[2] = ((int) speedSlider.value);
         value[3] = ((int) defenceSlider.value);
         currentCost = currentCost * value[0] + value[1] + value[2] + value [3];
         cost.text = currentCost + ""; 
     }
 
     if (currentPoints <= 0)
     {
       DisableSliders();
     }
 }

 private void DisableSliders()
 {
     healthSlider.gameObject.SetActive(false);
     strengthSlider.gameObject.SetActive(false);
     speedSlider.gameObject.SetActive(false);
     defenceSlider.gameObject.SetActive(false);
 }


 private void SelectPlayer(int playernum)
 {
     playernum = player;
     if (playernum == 0)
     {
         player = 0;
     }
     else if (playernum == 1)
     {
         player = 1;
     }
 }
}

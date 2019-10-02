using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitPlacement : MonoBehaviour
{
    public Text points, cost;
    public Slider healthSlider, strengthSlider, speedSlider, defenceSlider;
    public Image background;
    public Button ready, place;
    
    //Disable PlayerController
    public GameObject [] playerObj;
 //Select player 1 or 2;
    private int player;
    private int currentPoints, currentCost;
    
    //Player ready
    private bool canStart;
    
 //   public GameManager unitPrefab;

 private void Awake()
 {
     //SetValues
     currentPoints = 100;
     currentCost = 0;
     
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

 }

 private void DisableOther()
 {
     playerObj = GameObject.FindGameObjectsWithTag("Player");
     playerObj[0].gameObject.SetActive(false);
 }

 private void setValues(int points, int cost)
 {
     this.cost.text = cost+ "";
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
}

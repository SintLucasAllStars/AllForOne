using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreator : MonoBehaviour
{
    public Slider hSlider;
    public Slider spSlider;
    public Slider stSlider;
    public Slider dSlider;
    
    private int unitCost;

    void Start()
    {
        //hSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        //spSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        //stSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        //dSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }
    
    void Update()
    {
        
    }
    
    private void ValueChangeCheck()
    {
        print("h: " + hSlider.value);
        print("sp: " + spSlider.value);
        print("st: " + stSlider.value);
        print("d: " + dSlider.value);
    }

    public void PrintUnitValues()
    {
        print(CalculateCost());
    }

    private int CalculateCost()
    {
        int h = map(hSlider.value, 1, 100, 3, 30);
        int sp = map(spSlider.value, 1, 100, 3, 30);
        int st = map(stSlider.value, 1, 100, 2, 20);
        int d = map(dSlider.value, 1, 100, 2, 20);
        cost = Int(Mathf.Round(h + sp + st + d));
        return cost;
    }

}

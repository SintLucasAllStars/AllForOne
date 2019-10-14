using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    //
    //selectionUi  Value holders
    private int selectedClass = -1; // -1 = no class
    private string charName = "";
    private int uiCharHp = 1; // 1 = 1% of 100
    private int uiCharStr = 1;
    private int uiCharSpd = 1;
    private int uiCharDef = 1;
    
    //
    private GameManager gm;
    public List<GameObject> ui;
    public List<GameObject> statUi;

    public List<GameObject> charCreateUi;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    public void SelectionDefaults()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private StatList CreateStatList()
    {
        string team = gm.turnHolder;
        
        //
        string name = charCreateUi[0].GetComponent<TextMeshProUGUI>().text;
        int Hp = int.Parse(charCreateUi[1].GetComponent<TextMeshProUGUI>().text);
        int Str = int.Parse(charCreateUi[2].GetComponent<TextMeshProUGUI>().text);
        int Spd = int.Parse(charCreateUi[3].GetComponent<TextMeshProUGUI>().text);
        int Def = int.Parse(charCreateUi[4].GetComponent<TextMeshProUGUI>().text);
        StatList statInstance = new StatList(team, name, Hp, Str, Spd, Def);
        return statInstance;
    }

    //button
    public void SelectClass(int charClass)
    {
        
    }

    public void CreatePlayerCharacter(int charClass)
    {
        StatList flatStats = CreateStatList();

        GameObject prefab = gm.charPrefabs[charClass];
        prefab.GetComponent<PlayerCharacter>().stats = flatStats;
        
        
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        
        gm.AddUnitToTeam(instance);
    }

    public void SelectPrefab()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WarriorCreation : MonoBehaviour
{
    public Slider[] sliders;
    public TMP_Text costText;
    public GameObject panelContainer;
    public GameObject warriorPrefab;

    private Player curPlayer;
    private TileScript tile;
    private Vector3 warriorStartPos;

    int warriorCost = 0;

    public void Start()
    {
        warriorStartPos = new Vector3(0, 0.25f, 0);
    }

    public void OpenWarriorCreator(TileScript a_Tile, Player a_Player)
    {
        this.curPlayer = a_Player;
        this.tile = a_Tile;
        OnValueChange();
        panelContainer.SetActive(true);
    }

    public void OnValueChange()
    {
        warriorCost = 0;

        for (int i = 0; i < sliders.Length; i++)
            warriorCost += (int)sliders[i].value;

        costText.text = "Cost: " + warriorCost.ToString();
    }

    public void CreateWarrior()
    {
        Warrior createdWarrior = new Warrior((int)sliders[0].value, (int)sliders[1].value, (int)sliders[2].value, (int)sliders[3].value, curPlayer);
        GameObject spawnedWarrior = Instantiate(warriorPrefab, (tile.transform.position + tile.GetPosition() + warriorStartPos), Quaternion.identity);
        spawnedWarrior.GetComponent<Actor>().SetWarrior(createdWarrior);

        tile.AddNewWarrior(spawnedWarrior.GetComponent<Actor>());
        curPlayer.RemoveWarriorCost(warriorCost);
        curPlayer.AddWarrior(spawnedWarrior.GetComponent<Actor>());

        panelContainer.SetActive(false);
    }
}

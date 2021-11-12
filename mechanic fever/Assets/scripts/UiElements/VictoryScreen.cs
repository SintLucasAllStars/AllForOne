using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class VictoryScreen : MonoBehaviour
{
    public GameObject survivingUnits;
    private Transform[] spawnPoints;
    public Text matchTimeText;

    private void Start()
    {
        Init();
        GameManager.gameManager.OnReset.AddListener(Init);
    }

    private void Init()
    {
        SetMatchTime();

        loadInSpawnPoints();

        spawnSurvivingUnits();
    }

    private void loadInSpawnPoints()
    {
        spawnPoints = new Transform[survivingUnits.transform.childCount];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = survivingUnits.transform.GetChild(i);
        }
    }

    private void spawnSurvivingUnits()
    {
        List<GameObject> units = GameManager.gameManager.winningPlayer.GetUnitList();

        int i = 0;
        foreach (GameObject unit in units)
        {
            Animator unitAnimator = unit.GetComponent<Animator>();
            unit.GetComponent<Rigidbody>().useGravity = false;
            unit.transform.position = spawnPoints[i].position;
            unit.transform.rotation = spawnPoints[i].rotation;
            unitAnimator.SetInteger("RandomVictory", Random.Range(0, 3));
            unitAnimator.SetBool("Victory", true);
            i++;
        }
    }

    private void SetMatchTime()
    {
        matchTimeText.text = GameManager.gameManager.GetMatchTime().ToString("00:00:00");
    }

    public void returnToMainMenu()
    {
        List<GameObject> units = GameManager.gameManager.winningPlayer.GetUnitList();

        foreach (GameObject unit in units)
        {
            Destroy(unit);
        }

        GameManager.gameManager.LoadLevel(0);
    }
}

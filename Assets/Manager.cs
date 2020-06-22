using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public List<Humanoid> humanoids = new List<Humanoid>();
    public GameObject normalPrefab;
    public GameObject weakerPrefab;
    public GameObject strongerPrefab;
    public Vector3 tilePos;
    public Text turn;
    public Text p1Money;
    public Text p2Money;
    public Text Version;
    public Text cantAfford;
    public int p1Points;
    public int p2Points;
    public bool ableToBuy;

    private string humanoidVersion;
    private bool p1Turn;
    private int humanoidNumber;
    void Start()
    {
        ableToBuy = true;
        humanoidNumber = 0;
        p1Points = 100;
        p2Points = 100;
        p1Turn = true;

        p1Money.text = p1Points.ToString();
        p2Money.text = p2Points.ToString();
        turn.text = "Player one's turn";
        humanoidVersion = "normal";
        Version.text = humanoidVersion;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    tilePos = hit.transform.position;
                    Bought();
                    hit.collider.tag = "Used";
                }
            }
        }
        if (p2Points == 0 && p1Points == 0)
        {
            ableToBuy = false;
            turn.text = "GAME STARTING OVER 99999999 hours";
            return;
        }
        if (p1Points == 0)
        {
            p1Turn = false;
            turn.text = "Player two's turn";
        }
        if (p2Points == 0)
        {
            p1Turn = true;
            turn.text = "Player one's turn";

        }
    }

    private void Create_New_Humanoid(string name, string team)
    {
        if(humanoidVersion == "normal")
        {
            Humanoid humanoid = new Humanoid(name, team, 100, 25, 10, 2f);
            humanoids.Add(humanoid);
            GameObject temp = Instantiate(GetVersion(), tilePos, Quaternion.identity);
            temp.GetComponent<Character>().Assign_values(humanoid);
        }
        if (humanoidVersion == "weaker")
        {
            Humanoid humanoid = new Humanoid(name, team, 50, 10, 5, 4f);
            humanoids.Add(humanoid);
            GameObject temp = Instantiate(GetVersion(), tilePos, Quaternion.identity);
            temp.GetComponent<Character>().Assign_values(humanoid);
        }
        if (humanoidVersion == "stronger")
        {
            Humanoid humanoid = new Humanoid(name, team, 300, 50, 50, 1f);
            humanoids.Add(humanoid);
            GameObject temp = Instantiate(GetVersion(), tilePos, Quaternion.identity);
            temp.GetComponent<Character>().Assign_values(humanoid);
        }
    }

    public void Affordable()
    {
        int p1FakePoints = p1Points;
        int p2FakePoints = p2Points;
        if (p1Turn)
        {
            if (humanoidVersion == "normal")
            {
                p1FakePoints -= 20;
                if (p1FakePoints < 0)
                {
                    ableToBuy = false;
                    cantAfford.text = "CANNOT AFFORD";
                }
                else
                {
                    ableToBuy = true;
                    cantAfford.text = "";
                }
            }
            if (humanoidVersion == "weaker")
            {
                p1FakePoints -= 10;
                if (p1FakePoints < 0)
                {
                    ableToBuy = false;
                    cantAfford.text = "CANNOT AFFORD";
                }
                else
                {
                    ableToBuy = true;
                    cantAfford.text = "";
                }
            }
            if (humanoidVersion == "stronger")
            {
                p1FakePoints -= 50;
                if (p1FakePoints < 0)
                {
                    ableToBuy = false;
                    cantAfford.text = "CANNOT AFFORD";
                }
                else
                {
                    ableToBuy = true;
                    cantAfford.text = "";
                }
            }
        }

        if (!p1Turn)
        {
            if (humanoidVersion == "normal")
            {
                p2FakePoints -= 20;
                if (p2FakePoints < 0)
                {
                    ableToBuy = false;
                    cantAfford.text = "CANNOT AFFORD";
                }
                else
                {
                    ableToBuy = true;
                    cantAfford.text = "";
                }
            }
            if (humanoidVersion == "weaker")
            {
                p2FakePoints -= 10;
                if (p2FakePoints < 0)
                {
                    ableToBuy = false;
                    cantAfford.text = "CANNOT AFFORD";
                }
                else
                {
                    ableToBuy = true;
                    cantAfford.text = "";
                }
            }
            if (humanoidVersion == "stronger")
            {
                p2FakePoints -= 50;
                if (p2FakePoints < 0)
                {
                    ableToBuy = false;
                    cantAfford.text = "CANNOT AFFORD";
                }
                else
                {
                    ableToBuy = true;
                    cantAfford.text = "";
                }
            }
        }
    }
    public void Bought()
    {
        Affordable();
        if (ableToBuy)
        {
            humanoidNumber += 1;
            string humanoid_name = string.Format("Humanoid{0}", humanoidNumber);
            if (p1Turn)
            {
                turn.text = "Player two's turn";
                if(humanoidVersion == "weaker")
                    p1Points -= 10;
                if(humanoidVersion == "normal")
                    p1Points -= 20;
                if(humanoidVersion == "stronger")
                    p1Points -= 50;
                Create_New_Humanoid(humanoid_name, "p1");
                p1Money.text = p1Points.ToString();
                Debug.Log(p1Money);
                p1Turn = false;
                return;
            }
            if(!p1Turn)
            {
                turn.text = "Player one's turn";
                if (humanoidVersion == "weaker")
                    p2Points -= 10;
                if (humanoidVersion == "normal")
                    p2Points -= 20;
                if (humanoidVersion == "stronger")
                    p2Points -= 50;
                Create_New_Humanoid(humanoid_name, "p2");
                p2Money.text = p2Points.ToString();
                p1Turn = true;
                return;
            }
        }
    }
    public void SetVersion(string s)
    {
        humanoidVersion = s;
        Version.text = humanoidVersion;
    }
    public GameObject GetVersion()
    {
        if (humanoidVersion == "normal")
            return normalPrefab;
        if (humanoidVersion == "stronger")
            return strongerPrefab;
        if (humanoidVersion == "weaker")
            return weakerPrefab;
        return normalPrefab;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class CreatePlayer : MonoBehaviour
{
    public InputField nameText;
    private BasePlayer newPlayer;
    public string PlayerName;

    public GameObject inputField;
    public string curPlayerName;
    public GameObject charName;

    public Text strengthText;
    public Text healthText;
    public Text speedText;
    public Text defenseText;

    private int pointsToSpend = 90;
    public Text pointsText;

    public Animator animator;

    public float playTime = 1.5f;
    public float pauseTime = 1.5f;
    public AudioSource melee;
    public AudioSource ocarina;

    public GameObject canvas;

    void Start()
    {
        newPlayer = new BasePlayer();
        strengthText.text = 1.ToString();
        healthText.text = 1.ToString();
        speedText.text = 1.ToString();
        defenseText.text = 1.ToString();

        pointsText.text = 90.ToString();
    }

    void Update()
    {
        curPlayerName = inputField.GetComponent<Text>().text;
        charName.GetComponent<TextMeshProUGUI>().text = curPlayerName;

        if(pointsToSpend == 0)
        {
            Debug.Log("boohoo");
        }
    }

    public void CreateNewPlayer()
    {
        GameInfo.PlayerName = nameText.text;

        GameInfo.Strength = newPlayer.Strength;
        GameInfo.Health = newPlayer.Health;
        GameInfo.Speed = newPlayer.Speed;
        GameInfo.Defense = newPlayer.Defense;

        SaveInfo.SaveAll();

        //SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void UpdateUI()
    {
        strengthText.text = newPlayer.Strength.ToString();
        healthText.text = newPlayer.Health.ToString();
        speedText.text = newPlayer.Speed.ToString();
        defenseText.text = newPlayer.Defense.ToString();
        pointsText.text = pointsToSpend.ToString();
    }

    public void SetStrength(int amount)
    {
        if(amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Strength += amount;
            pointsToSpend -= 2;
            UpdateUI();

        }

        else if ( amount < 1)
        {
            newPlayer.Strength += amount;
            pointsToSpend += 2;
            UpdateUI();
        }
    }

    public void SetHealth(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Health += amount;
            pointsToSpend -= 3;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Health += amount;
            pointsToSpend += 3;
            UpdateUI();
        }
    }

    public void SetSpeed(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Speed += amount;
            pointsToSpend -= 3;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Speed += amount;
            pointsToSpend += 3;
            UpdateUI();
        }
    }

    public void SetDefense(int amount)
    {
        if (amount > 0 && pointsToSpend > 0)
        {
            newPlayer.Defense += amount;
            pointsToSpend -= 2;
            UpdateUI();

        }

        else if (amount < 1)
        {
            newPlayer.Defense += amount;
            pointsToSpend += 2;
            UpdateUI();
        }
    }

    public void LoadStuff()
    {
        LoadInfo.LoadAll();
        //SceneManager.LoadScene(0);

        canvas.SetActive(false);
    }

    public void Animations()
    {
        animator.SetTrigger("onBuyPress");
        //melee.Stop();
        //ocarina.Play();   
        StartCoroutine("SwitchSound");
    }

    IEnumerator SwitchSound()
    {
        melee.Pause();
        ocarina.Play();
        yield return new WaitForSeconds(1.5f);
        melee.UnPause();
        yield return null;
    }
}

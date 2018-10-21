using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPanel : MonoBehaviour
{
    public RawImage AdrenalineImage;
    public RawImage TimeMachineImage;
    public RawImage RageImage;
    public RawImage Panel;

    public Text AdrenalineText;
    public Text TimeMachineText;
    public Text RageText;

    public Text AdrenalineTimeText;
    public Text TimeMachineTimeText;
    public Text RageTimeText;


    private int _currentlyAtPosition = 0;

    private float _currentPowerUpTime;
    
    

    public void SetTexts()
    {
        AdrenalineText.text = "x" + PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().GetCurrentAmountOfAdrenaline();
        TimeMachineText.text = "x" + PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().GetCurrentAmountOfTimeMachine();
        RageText.text = "x" + PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().GetCurrentAmountOfRage();

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ! GameManager.Instance.PowerUpActive())
        {
            StopAllCoroutines();
            switch (_currentlyAtPosition)
            {
                case 0:
                    StartCoroutine(LerpTowardsTarget(TimeMachineImage.transform.position));
                    _currentlyAtPosition = 1;
                    break;
                case 1:
                    StartCoroutine(LerpTowardsTarget(RageImage.transform.position));
                    _currentlyAtPosition = 2;
                    break;
                case 2:
                    StartCoroutine(LerpTowardsTarget(AdrenalineImage.transform.position));
                    _currentlyAtPosition = 0;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && !GameManager.Instance.PowerUpActive())
        {
            switch (_currentlyAtPosition)
            {
                //adrenaline
                case 0:
                    if (PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().GetCurrentAmountOfAdrenaline() > 0)
                    {
                        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().ActivatePowerUp(PowerUp.PowerUpEnum.Adrenaline);
                    }
                    else
                    {
                        Debug.Log("No adrenaline");
                    }
                    break;
                //time machine
                case 1:
                    if (PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().GetCurrentAmountOfTimeMachine() > 0)
                    {
                        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().ActivatePowerUp(PowerUp.PowerUpEnum.TimeMachine);
                       
                    }
                    else
                    {
                        Debug.Log("No time machine");
                    }
                    
                    break;
                //rage
                case 2:
                    if (PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().GetCurrentAmountOfRage() > 0)
                    {
                        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().ActivatePowerUp(PowerUp.PowerUpEnum.Rage);
                    }
                    else
                    {
                        Debug.Log("No rage");
                    }
               
                    break;
            }
        }

        if (_currentPowerUpTime > 0f)
        {
            _currentPowerUpTime -= Time.deltaTime;
            
            switch (_currentlyAtPosition)
            {
                 case 0:
                     AdrenalineTimeText.text = _currentPowerUpTime.ToString("00");
                        break;
                case 1:
                    TimeMachineTimeText.text = _currentPowerUpTime.ToString("00");

                    break;
                case 2:
                    RageTimeText.text = _currentPowerUpTime.ToString("00");

                    break;
            }
        }

       
    }

    public void SetTime(float time)
    {
        _currentPowerUpTime = time;
        switch (_currentlyAtPosition)
        {
            case 0:
                AdrenalineTimeText.gameObject.SetActive(true);
                break;
            case 1:
                TimeMachineTimeText.gameObject.SetActive(true);
                break;
            case 2:
                RageTimeText.gameObject.SetActive(true);
                break;
        }
        
    }

    public void PowerUpFinished()
    {
        RageTimeText.gameObject.SetActive(false);
        TimeMachineTimeText.gameObject.SetActive(false);
        AdrenalineTimeText.gameObject.SetActive(false);



    }


    private IEnumerator LerpTowardsTarget(Vector3 target)
    {
        Vector3 actualTarget = new Vector3(Panel.transform.position.x, target.y, target.z);
        bool moving = true;
        while (moving)
        {
            Panel.transform.position = Vector3.Lerp(Panel.transform.position, actualTarget, 5f * Time.deltaTime);
            
            if (Vector3.Distance(Panel.transform.position, actualTarget) < 0.1f)
            {
                moving = false;
            }

            yield return null;
        }
    }
}
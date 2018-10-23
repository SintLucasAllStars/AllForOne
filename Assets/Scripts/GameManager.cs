using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using MathExt;
using Players;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool ChoosingCharacters = true;
    [SerializeField] private CharacterPanel _characterPanel;
    private CameraMovement _cameraMovement;

    private bool _inSelectionState;

    [SerializeField] private Text _timeLeftText;

    private float _timeLeft;

    private bool _waitingForCoroutine;

    [HideInInspector] public bool TimerActive;

    [SerializeField] private Slider _healthSlider;

    [SerializeField] private Material _resetMaterial;


    public bool FriendlyFire = false;

    [SerializeField] private PowerUpPanel _powerUpPanel;

    public ColorRanges MyColorRanges;

    private bool _powerUpActive = false;

    private bool _pauseTimer;

    private List<Color> PlayerOneColors = new List<Color>();
    private List<Color> PlayerTwoColors = new List<Color>();
    

    protected override void Awake()
    {
        base.Awake();
        _cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
    }

    private void Start()
    {
        ResetMaterial();
        //TimerActive = true;
        _healthSlider.gameObject.SetActive(false);
        _timeLeft = 10f;
        InitializeColorLists();
    }

    private void InitializeColorLists()
    {
        PlayerOneColors = MyColorRanges.PlayerOne.ToList();
        PlayerTwoColors = MyColorRanges.PlayerTwo.ToList();
    }

    public Color GetRandomColor(int playerNumber)
    {
        if (playerNumber == 1)
        {
            var randomColor1 = PlayerOneColors.GetRandomElement_List();
            PlayerOneColors.Remove(randomColor1);
            return randomColor1;
        }

        var randomColor2 = PlayerTwoColors.GetRandomElement_List();
        PlayerTwoColors.Remove(randomColor2);
        return randomColor2;

    }
    
    
    

        
    private void ResetMaterial()
    {
        _resetMaterial.color = Color.white;
    }

    public void CharacterPlaced()
    {
        if (PlayerManager.Instance.SetCurrentlyActivePlayerSelection())
        {
            _characterPanel.OnNewCharacter();
        }
        else
        {
            _waitingForCoroutine = true;
            PlayerManager.Instance.SetRandomPlayer();
            _cameraMovement.CameraSlerp(_cameraMovement.TopView, false);
        }
    }

    public bool InSelectionState()
    {
        return _inSelectionState;
    }

    public bool PowerUpActive()
    {
        return _powerUpActive;
    }

    public void CameraCoroutineFinished(bool setParent)
    {
        if (_waitingForCoroutine && !setParent)
        {
            _inSelectionState = true;
            _waitingForCoroutine = false;
        }
        else if (_waitingForCoroutine && setParent)
        {

            if (!PlayerManager.Instance.GetCurrentlyActiveCharacter().Fortified)
            {
                TimerActive = true;
                PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.EnableUserControl();
            }
            else
            {
                PlayerManager.Instance.GetCurrentlyActiveCharacter().EndFortify();
                StartCoroutine(IEDelayedEnableCharCall());
            }

        }
    }

    private void TurnFinished(bool fortify)
    {
        _healthSlider.gameObject.SetActive(false);
        if (PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.OnFloor() && !fortify)
        {
            PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.DisableUserControl();
            _cameraMovement.CameraSlerp(_cameraMovement.TopView, false);
        }
        else if (fortify)
        {
            PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.DisableUserControl();
            StartCoroutine(IEDelayedCameraCall());

        }
        else
        {
            PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.Die();
            StartCoroutine(IEDelayedCameraCall());

        }
        _timeLeft = 10.0f;
        _timeLeftText.text = _timeLeft.ToString("F");
        PlayerManager.Instance.SetCurrentlyActivePlayer();  

    }

    public void SetCameraMovement(Transform transformLocal, bool setParent)
    {
        _cameraMovement.CameraSlerp(transformLocal, setParent);
        _waitingForCoroutine = setParent;
        _inSelectionState = false;
    }

    public void EnableHealthSlider()
    {
        _healthSlider.gameObject.SetActive(true);
        _healthSlider.value = PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().Health;
    }

    public void ResetPowerUpPanelTexts()
    {
        _powerUpPanel.SetTexts();
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        StartCoroutine(IEPowerUp(powerUp));
    }

    private IEnumerator IEPowerUp(PowerUp powerUp)
    {
        _powerUpActive = true;
        _pauseTimer = powerUp.FreezeTime;
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().AddSpeed(powerUp.SpeedBoost);
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().AddStrength(powerUp.StrengthBoost);
        _powerUpPanel.SetTime(powerUp.TimeLength);
        yield return new WaitForSeconds(powerUp.TimeLength);
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().AddSpeed(-powerUp.SpeedBoost);
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().AddStrength(-powerUp.StrengthBoost);
        _powerUpActive = false;
        _pauseTimer = false;
        _powerUpPanel.PowerUpFinished();


    }

    private IEnumerator IEDelayedCameraCall()
    {
        yield return  new  WaitForSeconds(3);
        _cameraMovement.CameraSlerp(_cameraMovement.TopView, false);
    }

    private IEnumerator IEDelayedEnableCharCall()
    {
        yield return new WaitForSeconds(3);
        TimerActive = true;
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.EnableUserControl();
        
    }

    public void Fortify()
    {
        TimerActive = false;
        _timeLeft = 0;
        _timeLeftText.text = _timeLeft.ToString("F");
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().Fortify();
        TurnFinished(true);
    }
    

    private void Update()
    {
        if (TimerActive && !_pauseTimer)
        {
            _timeLeft -= Time.deltaTime;
            _healthSlider.value = PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().Health;
            if (_timeLeft < 0.0f)
            {
                TimerActive = false;


                _timeLeft = 0.0f;
                TurnFinished(false);
            }

            _timeLeftText.text = _timeLeft.ToString("F");
        }
    }
}


[Serializable]
public class ColorRanges
{
    public Color[] PlayerOne;
    public Color[] PlayerTwo;
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
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


    private bool _powerUpActive = false;

    private bool _pauseTimer;
    

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
        _timeLeft = 1000f;
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
            TimerActive = true;
            PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.EnableUserControl();
        }
    }

    private void TurnFinished()
    {
        PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.DisableUserControl();
//        if (currentCharacter + 1 < PlayerManager.Instance.GetCurrentlyActivePlayer().Characters.Count())
//        {
//            PlayerManager.Instance.GetCurrentlyActivePlayer().CurrentlyActiveCharacter++;
//        }
//        else
//        {
//            PlayerManager.Instance.GetCurrentlyActivePlayer().CurrentlyActiveCharacter =
//        }
        PlayerManager.Instance.SetCurrentlyActivePlayer();
        _timeLeft = 10.0f;
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
    
    
    

    private void Update()
    {
        if (TimerActive && !_pauseTimer)
        {
            _timeLeft -= Time.deltaTime;
            _healthSlider.value = PlayerManager.Instance.GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().Health;
            if (_timeLeft < 0.0f)
            {
                TimerActive = false;

                _healthSlider.gameObject.SetActive(false);
                _cameraMovement.CameraSlerp(_cameraMovement.TopView, false);
                _timeLeft = 0.0f;
                TurnFinished();
            }

            _timeLeftText.text = _timeLeft.ToString("F");
        }
    }
}

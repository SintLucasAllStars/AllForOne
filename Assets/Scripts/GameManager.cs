using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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

    protected override void Awake()
    {
        base.Awake();
        _cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
    }

    private void Start()
    {
        //TimerActive = true;
        _timeLeft = 10f;
    }

    public void CharacterPlaced()
    {
        if (PlayerManager.Instance.SetCurrentlyActivePlayer())
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

    public void CameraCoroutineFinished(bool setParent)
    {
        if (_waitingForCoroutine && !setParent)
        {
            _inSelectionState = true;
            //TimerActive = true;
//            int currentCharacter = PlayerManager.Instance.GetCurrentlyActivePlayer().CurrentlyActiveCharacter;
//            _cameraMovement.CameraSlerp(PlayerManager.Instance.GetCurrentlyActivePlayer().Characters[currentCharacter ].MyCharacterMono.CameraTransform);
//            if (currentCharacter  < PlayerManager.Instance.GetCurrentlyActivePlayer().Characters.Count -1)
//            {
//                PlayerManager.Instance.GetCurrentlyActivePlayer().CurrentlyActiveCharacter++;
//            }
//            else
//            {
//                Debug.Log("NewTurn");
//            }
            _waitingForCoroutine = false;
        }
        else if (_waitingForCoroutine && setParent)
        {
            TimerActive = true;
        }
    }

    public void SetCameraMovement(Transform transformLocal, bool setParent)
    {
        _cameraMovement.CameraSlerp(transformLocal, setParent);
        _waitingForCoroutine = setParent;
        _inSelectionState = false;
    }

    private void Update()
    {
        if (TimerActive)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0.0f)
            {
                TimerActive = false;
                int currentCharacter = PlayerManager.Instance.GetCurrentlyActivePlayer().CurrentlyActiveCharacter;
                PlayerManager.Instance.GetCurrentlyActivePlayer().Characters[currentCharacter].MyCharacterMono.DisableUserControl();
                _cameraMovement.CameraSlerp(_cameraMovement.TopView, false);
                _timeLeft = 0.0f;
            }

            _timeLeftText.text = _timeLeft.ToString("F");
        }
    }
}
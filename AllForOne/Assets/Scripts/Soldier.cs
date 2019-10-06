using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private Transform _barrle;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private LayerMask _RooflayerMask;

    [HideInInspector]
    public bool _timeFreeze;

    [SerializeField]
    private List<GameObject> _looks;

    [Header("Materials")]   
    [SerializeField]
    private List<Material> _teamMaterials;

    [SerializeField]
    private MeshRenderer _material;

    [SerializeField]
    private List<Color> _teamColors;

    [SerializeField]
    private Image _image;

    [Header("Animator")]
    [SerializeField]
    private Animator _animator;

    [Header("Camera")]
    [SerializeField]
    private Transform _cameraPivot;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _cameraSpeed;

    [SerializeField]
    private float _cameraRange;

    [Header("UI")]
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private Text _pickupWeaponText;

    [SerializeField]
    private Slider _healthSlider;

    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private Text _powerupText;

    [Header("Stats")]
    [SerializeField]
    private float _defaultWalkSpeed;

    [SerializeField]
    private float _defaultRunSpeed;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private List<GameObject> _weaponModels;


    public float _health = 1;
    public float _strenght = 1;
    public float _speed = 1;
    public float _defense = 1;
    public TeamEnum _teamEnum;

    public bool _isFortified;

    public int _playTime = 10;
    private bool _controled;

    private float _currentSpeed;
    private float _speedBuff = 10;

    private string _weaponAnimationName;
    private float _maxHealth;
    private float _weaponDamage;
    private float _weaponSpeed;
    private float _weaponRange;
    private bool _onCooldown;

    private Weapon _weaponInReatch;
    private PowerUps _PowerupInReatch;
    private List<PowerUps> _powerups = new List<PowerUps>();

    private void Start()
    {
        _looks[Random.Range(0, _looks.Count)].SetActive(true);
        _weaponAnimationName = "Punch";
        _weaponDamage = 1;
        _weaponSpeed = 10;
        _weaponRange = 0;
        _currentHealth *= _health;
        _material.material = _teamMaterials[(int)_teamEnum];
        _image.color = _teamColors[(int)_teamEnum];
        _maxHealth = _currentHealth;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _currentHealth;
    }

    public void ControleUnit()
    {
        _isFortified = false;
        _shield.SetActive(false);
        _currentSpeed = _defaultWalkSpeed;
        _camera.enabled = true;
        _controled = true;
        _canvas.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(Timer());
    }

    private void FixedUpdate()
    {
        if (!_controled)
        {
            return;
        }
        CameraManagement();
        AnimationManagement();

        if (Input.GetMouseButton(0))
        {
            TriggerAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isFortified = true;
            _shield.SetActive(true);
            PlaytimeEnd();
        }

        _powerupText.text = "";
        for (int i = 0; i < _powerups.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                PowerUps powerup = _powerups[i];
                powerup.UsePowerUp(this);
                if (_powerups.Count == 1)
                {
                    _powerups.Clear();
                }
                else
                {
                    _powerups.Remove(powerup);
                }
                return;
            }

            _powerupText.text += (i + 1) +": " + _powerups[i]._name + "\n";
        }

            if (Input.GetKeyDown(KeyCode.E))
        {
            if (_weaponInReatch != null)
            {
                SelectWeapon(_weaponInReatch);
            }
            if (_PowerupInReatch != null)
            {
                AddPowerup(_PowerupInReatch);
            }
        }


        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {

            _animator.SetInteger("Speed", 0);
            return;
        }
        MovementManagement();

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Input.GetAxisRaw("Horizontal") == 0)
        {
            _currentSpeed = _defaultRunSpeed;
            _animator.SetInteger("Speed", 2);
            if (Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("Jump", true);
            }
            else
            {
                _animator.SetBool("Jump", false);
            }
        }
        else
        {
            _currentSpeed = _defaultWalkSpeed;
            _animator.SetInteger("Speed", 1);
        }
    }
    private void CameraManagement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        _cameraPivot.eulerAngles += new Vector3(-mouseY, 0, 0) * _cameraSpeed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, mouseX, 0) * _cameraSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(_cameraPivot.position, -_cameraPivot.forward, out hit, _cameraRange))
        {
            _camera.transform.position = hit.point;
        }
        else
        {
            _camera.transform.position = _cameraPivot.position - _cameraPivot.transform.forward * _cameraRange;
        }
        _camera.transform.LookAt(_cameraPivot);
    }

    private void MovementManagement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (_currentSpeed * ((_speed / _speedBuff) + 1)) * Time.deltaTime);
    }

    private void AnimationManagement()
    {
        _animator.SetInteger("Horizontal", Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")));
        _animator.SetInteger("Vertical", Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));
    }


    private void SelectWeapon(Weapon weapon)
    {
        _weaponAnimationName = weapon._animationName;
        _weaponDamage = weapon._damage;
        _weaponSpeed = weapon._speed;
        _weaponRange = weapon._range;


        for (int i = 0; i < _weaponModels.Count; i++)
        {
            _weaponModels[i].SetActive(false);

            if (weapon._weaponNumber == i)
            {
                _weaponModels[i].SetActive(true);
            }
        }

        Destroy(weapon.gameObject);
        _pickupWeaponText.text = "";
        _weaponInReatch = null;
    }

    private void AddPowerup(PowerUps powerup)
    {
        _powerups.Add(powerup);
        powerup.gameObject.SetActive(false);
        _PowerupInReatch = null;
        _pickupWeaponText.text = "";
    }

    private void TriggerAttack()
    {
        if (!_onCooldown)
        {
            _onCooldown = true;
            _animator.Play(_weaponAnimationName);
        }
    }

    public void Attack()
    {
        Invoke("WeaponReset", (1 / _weaponSpeed) * 5);

        RaycastHit cameraHit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out cameraHit, Mathf.Infinity))
        {
            _barrle.LookAt(cameraHit.point);
        }

        RaycastHit weaponHit;
        Debug.DrawRay(_barrle.position, _barrle.forward, Color.red, 5);
        if (Physics.Raycast(_barrle.position, _barrle.forward, out weaponHit, _weaponRange + 1))
        {
            Soldier soldier = weaponHit.collider.GetComponent<Soldier>();
            if (soldier == null || soldier._teamEnum == _teamEnum)
            {
                return;
            }
            if (soldier._isFortified)
            {
                soldier.TakeDamage((_weaponDamage * _strenght) / soldier._defense);
                return;
            }
            soldier.TakeDamage(_weaponDamage * _strenght);
        }
    }

    private void WeaponReset()
    {
        _onCooldown = false;
    }

    private void PlaytimeEnd()
    {
        if (!_controled)
        {
            return;
        }
        _camera.enabled = false;
        _controled = false;
        _canvas.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        GameControler.Instance.EndControlingUnit();

        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.up, out hit, Mathf.Infinity, _RooflayerMask))
        {
            if (hit.collider.GetComponent<Roof>() != null)
            {
                return;
            }
        }
        Die();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage / _defense;
        if (_currentHealth <= 0)
        {
            Die();
        }
        _healthSlider.value = _currentHealth;
    }

    public void Die()
    {
        if (_teamEnum == TeamEnum.TeamRed)
        {
            GameControler.Instance._RedSoldiers--;
            if (GameControler.Instance._RedSoldiers == 0)
            {
                GameControler.Instance.Victory(TeamEnum.TeamBlue);
            }
        }
        else
        {
            GameControler.Instance._BlueSoldiers--;
            if (GameControler.Instance._BlueSoldiers == 0)
            {
                GameControler.Instance.Victory(TeamEnum.TeamRed);
            }
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null)
        {
            _pickupWeaponText.text = "Press E to puck up a " + weapon._weaponName;
             _weaponInReatch = weapon;
        }
        PowerUps powerup = other.GetComponent<PowerUps>();
        if (powerup != null)
        {
            _pickupWeaponText.text = "Press E to puck up a " + powerup._name;
            _PowerupInReatch = powerup;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Weapon>() != null)
        {
            _pickupWeaponText.text = "";
            _weaponInReatch = null;
        }
        if (other.GetComponent<PowerUps>() != null)
        {
            _pickupWeaponText.text = "";
            _PowerupInReatch = null;
        }
    }

    private IEnumerator Timer()
    {
        int time = _playTime;
        _timerText.text = "TIME: " + time;
        for (int i = 0; i < _playTime;)
        {
            if (!_controled)
            {
                i = _playTime;
                yield return null;
            }

                yield return new WaitForSeconds(1);
            if (!_timeFreeze)
            {
                i++;
                time--;
                _timerText.text = "TIME: " + time;
            }
        }
        PlaytimeEnd();
    }
}


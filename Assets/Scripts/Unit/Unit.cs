using UnityEngine;
using IExtraFunctions;
using System;

[RequireComponent(typeof(CharacterController))]
public class Unit : MonoBehaviour
{
    public int health;
    public int strength;
    public float speed;
    public int defense;

    private Vector3 spawnLocation;

    public static Color[] colors = new Color[2] { new Color(255, 0, 0), new Color(0, 0, 255) };

    public int cost = 10;

    public Weapon weapon;

    public PowerUp powerUp;

    //movement
    private CharacterController characterController;
    private float JumpForce;

    public Unit(int _health, int _strength, float _speed, int _defense, Weapon.TypeOfWeapon weapon)
    {
        this.health = _health;
        this.strength = _strength;
        this.speed = _speed;
        this.defense = _defense;
        this.weapon = WeaponController.WeaponDict[weapon];
    }

    private void Start()
    {
        characterController = this.gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        InputController();
    }

    private void InputController()
    {
        if (characterController.isGrounded)
        {
            var gravity = 20.0f;
            var jumpHeight = 8.0f;
            var moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.LeftShift))
                speed *= 1.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = jumpHeight;
            }

            moveDir *= speed;

            //add the gravity
            moveDir.y -= gravity * Time.deltaTime;
            characterController.Move(moveDir * Time.deltaTime);
        }
    }

    public void PickUpPowerUp(PowerUp _powerUp) { this.powerUp = _powerUp; }

    public bool UsePowerUp()
    {
        var baseValeu = 0f;
        if (powerUp != null)
        {
            switch (powerUp.typeOfPowerUp)
            {
                case PowerUp.TypeOfPowerUp.speed:
                    baseValeu = this.speed;
                    this.speed /= powerUp.value;
                    Timer.Wait(powerUp.duration, () => { this.speed = baseValeu; });
                    break;
                case PowerUp.TypeOfPowerUp.strength:
                    baseValeu = this.strength;
                    this.strength /= powerUp.value;
                    Timer.Wait(powerUp.duration, () => { this.strength = (int)baseValeu; });
                    break;
                case PowerUp.TypeOfPowerUp.counter:
                    var secondsToWait = powerUp.value;
                    GameController.instance.HaltCountDown(secondsToWait);
                    break;
            }
            return true;
        }
        else
            return false;

    }

    public void Fortify() { }

    public static int Cost(int _health, int _strength, int _speed, int _defense)
    {
        return (_health * 3) + (_speed * 3) + (_strength * 2) + (_defense * 2);
    }

    public void SetColor(bool _isPlayerOne)
    {
        var mat = this.gameObject.GetComponent<Material>();
        if (_isPlayerOne)
        {
            mat.color = colors[0];
        }
        else
        {
            mat.color = colors[1];
        }
    }
}
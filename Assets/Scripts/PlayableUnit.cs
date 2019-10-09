using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableUnit : MonoBehaviour
{
    public Player _player;

    public int _health;
    public int _strength;
    public int _speed;
    public int _defense;

    public GameObject _thirdPersonCamera;

    private bool isActive = false;

    private Animator _animator;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;

            PlayerManager.Instance._topDownCamera.enabled = !isActive;
            _thirdPersonCamera.SetActive(isActive);
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        Color characterColor = Color.black;
        if (_player._playerID == 1)
        {
            characterColor = new Color(1, Random.Range(0f, 0.25f), Random.Range(0f, 0.25f));
        }
        else if (_player._playerID == 2)
        {
            characterColor = new Color(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), 1);
        }

        List<Renderer> renderers = new List<Renderer>();
        renderers.AddRange(GetComponentsInChildren<Renderer>());
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = characterColor;
        }

        _thirdPersonCamera = GetComponentInChildren<Camera>().gameObject;
        _thirdPersonCamera.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {

        }
    }
}

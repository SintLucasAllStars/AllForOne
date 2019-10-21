﻿using UnityEngine;

namespace MechanicFever
{
    public class Rocket : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField]
        private GameObject _explosion;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Start() => _rigidbody.AddForce(transform.forward * 1000);

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(_explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
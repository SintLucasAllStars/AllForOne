using TMPro;
using UnityEngine;
using System;

namespace MechanicFever
{
    public class Notifier : Singleton<Notifier>
    {
        private Animator _animator = null;

        [SerializeField]
        private TextMeshProUGUI _notificationText = null, _buttonText = null;

        public delegate void OnUnderstoodPress();
        public static OnUnderstoodPress Understood;

        private void Start() => _animator = GetComponentInChildren<Animator>();

        public void ShowNotification(string text, string buttonText = "Understood")
        {
            _notificationText.text = text;
            _buttonText.text = buttonText;
            OpenModal(true);
        }

        public void OpenModal(bool isEnabled) => _animator.SetBool("isEnabled", isEnabled);

        public void Press()
        {
            if (Understood == null)
                return;

            Understood();

            Delegate[] subscribers = Understood.GetInvocationList();
            for (int i = 0; i < subscribers.Length; i++)
            {
                Understood -= subscribers[i] as OnUnderstoodPress;
            }
        }

        private void OnEnable() => NetworkManager.Error += ShowNotification;

        private void OnDisable() => NetworkManager.Error -= ShowNotification;
    }
}
using UnityEngine;
using TMPro;

namespace AllForOne
{
    public class Notifier : Singleton<Notifier>
    {
        private Animator _animator;

        [SerializeField]
        private TextMeshProUGUI _notificationText;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void ShowNotification(string text)
        {
            _notificationText.text = text;
            OpenModal(true);
        }

        public void OpenModal(bool isEnabled) => _animator.SetBool("isEnabled", isEnabled);

        private void OnEnable()
        {
            NetworkManager.ConnectionFailed += ShowNotification;
            NetworkManager.Disconnect += ShowNotification;
        }

        private void OnDisable()
        {
            NetworkManager.ConnectionFailed -= ShowNotification;
            NetworkManager.Disconnect -= ShowNotification;
        }
    }
}
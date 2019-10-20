using UnityEngine;

namespace AllForOne
{
    public class ConnectButton : Button
    {
        public override void Press()
        {
            NetworkManager.ConnectionSuccessful += Debugs;
            NetworkManager.ConnectionFailed += Debugs2;
            NetworkManager.Instance.Connect();
        }

        private void Debugs()
        {
            Debug.Log("Connected");
        }

        private void Debugs2(string reason)
        {
            Debug.Log("Not Connected: " + reason);
        }

        void OnDisable()
        {
            NetworkManager.ConnectionSuccessful -= Debugs;
            NetworkManager.ConnectionFailed -= Debugs2;
        }
    }
}
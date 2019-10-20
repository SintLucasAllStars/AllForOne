using UnityEngine;

namespace AllForOne
{
    public class ConnectButton : Button
    {
        public override void Press()
        {
            NetworkManager.ConnectionSuccessful += Debugs;
            NetworkManager.Instance.Connect();
        }

        private void Debugs() => SceneManager.Instance.LoadScene(1);
    }
}
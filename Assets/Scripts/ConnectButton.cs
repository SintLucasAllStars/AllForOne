namespace MechanicFever
{
    public class ConnectButton : Button
    {
        public override void Press() => NetworkManager.Instance.Connect();

        private void LoadLevel() => SceneManager.Instance.LoadScene(1);

        private void OnEnable() => NetworkManager.ConnectionSuccessful += LoadLevel;

        private void OnDisable() => NetworkManager.ConnectionSuccessful -= LoadLevel;
    }
}
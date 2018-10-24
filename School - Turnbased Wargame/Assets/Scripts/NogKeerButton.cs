using UnityEngine;
using UnityEngine.SceneManagement;

public class NogKeerButton : MonoBehaviour {

	public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

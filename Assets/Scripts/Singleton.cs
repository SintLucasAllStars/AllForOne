using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance = null;

    [SerializeField]
    private bool _dontDestroy = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = gameObject.GetComponent<T>();
        else if (Instance.GetInstanceID() != GetInstanceID())
            Destroy(this);

        if (_dontDestroy)
            DontDestroyOnLoad(this.gameObject);
    }
}
using UnityEngine;
using TMPro;

public class UiScript : MonoBehaviour
{
    public TextMeshProUGUI username;
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        username.text = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(mainCamera.transform);
    }
}

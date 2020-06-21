using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [Inject] private IUnitEditorManager _unitEditor;
    private Vector3 movement = Vector3.zero;

    private void Start()
    {
        _unitEditor.onEditorToggled += OnUnitEditorToggled;
        enabled = false;
    }

    private void OnUnitEditorToggled(bool newState)
    {
        enabled = !newState;
    }

    private void Update()
    {
        movement = Vector3.zero;
        GetInput(KeyCode.A, Vector3.left);
        GetInput(KeyCode.W, Vector3.forward);
        GetInput(KeyCode.S, Vector3.back);
        GetInput(KeyCode.D, Vector3.right);
        
        ApplyMovement();
    }

    private void GetInput(KeyCode input, Vector3 dir)
    {
        if (Input.GetKey(input))
            movement += dir;
    }

    private void ApplyMovement()
    {
        transform.Translate(movement * (speed * Time.deltaTime));
    }
}
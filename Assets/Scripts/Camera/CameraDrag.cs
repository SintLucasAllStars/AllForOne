using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private float zoom = 80f;
    private Vector3 dragOrigin;
    private bool scrolling;

    private void Awake()
    {
        Debug.LogError("TODO:\n1. ADD A ZOOM OPTION TO THE CAMERA.\n2. Fix ui looks.\n3.Add fight option.");
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
        
        transform.Translate(move, Space.World);
    }
}
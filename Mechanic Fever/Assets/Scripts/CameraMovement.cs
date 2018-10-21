using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float topDownHeight;
    [SerializeField] private Vector3 topDownRotation;

    [SerializeField] private float travelTime;
    [SerializeField] private GameObject roof;

    bool move = true;

    // Use this for initialization
    void Start()
    {
        //Subscribe to event when first round has started.
        GameManager.instance.StartRound += StartRound;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
            Move();

        //Debug
        if(Input.GetKeyDown(KeyCode.Escape))
            GameManager.instance.RunEvent(false);
    }

    void StartRound()
    {
        GameManager.instance.EndRound += EndRound;
        GameManager.instance.StartRound -= StartRound;
    }

    void EndRound()
    {
        transform.parent.parent.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().enabled = false;
        transform.parent = null;
        StartCoroutine(MoveToward(new Vector3(transform.position.x, topDownHeight, transform.position.z), Quaternion.Euler(topDownRotation), null, true));
        roof.SetActive(false);
    }

    void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 right = Vector3.right * movement.x * speed * Time.deltaTime;
        Vector3 forward = Vector3.forward * movement.y * speed * Time.deltaTime;

        transform.position += right + forward;
    }

    public void FlyTowardCharacter(Character character)
    {
        transform.parent = character.pivot;
        StartCoroutine(MoveToward(character.cameraLocation, Quaternion.Euler(Vector3.zero), character, false));
    }

    public IEnumerator MoveToward(Vector3 target, Quaternion rotationTarget, Character character, bool move)
    {
        this.move = false;
        Vector3 currentPosition = transform.localPosition;
        Quaternion currentRotation = transform.rotation;

        float elapsedTime = 0.0f;
        while(elapsedTime < travelTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            float currentValue = Mathf.Clamp01(elapsedTime / travelTime);
            transform.localPosition = Vector3.Lerp(currentPosition, target, currentValue);
            transform.localRotation = Quaternion.Lerp(currentRotation, rotationTarget, currentValue);
        }

        this.move = move;
        roof.SetActive(!move);

        if(!move)
        {
            character.ActivateCharacter(true);
            GetComponentInParent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().enabled = true;
            GameManager.instance.RunEvent(true);
        }
    }
}

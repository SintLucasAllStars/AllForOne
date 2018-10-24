using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameManager gameManager;
    public Transform characterPostion;

    public GameObject inGamePlayer = null;
    public float panSpeed = 40f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    private Vector3 offset;

    public float scrollSpeed = 20f;
    public float minHeight = 10f;
    public float maxHeight = 40f;

    private float speed = 10f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.playerList[gameManager.playerID].IsSelected)
        {
            inGamePlayer = FindObjectOfType<GameObject>();
            gameManager.playerList[gameManager.playerID].IsSelected = false;
            characterPostion.position = gameManager.playerList[gameManager.playerID].NodePosition;
            Vector3 tempVector = new Vector3(characterPostion.position.x, characterPostion.position.y + 1.8f, characterPostion.position.z - 0.6f);
            characterPostion.position = tempVector;
            characterPostion.rotation = Quaternion.Euler(30, 0, 0);
            transform.position = Vector3.Lerp(transform.position, characterPostion.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, characterPostion.transform.rotation, speed * Time.deltaTime);
            offset = new Vector3(characterPostion.position.x, characterPostion.position.y + 1.8f, characterPostion.position.z - 0.6f);
            Debug.Log(string.Format("X = {0} Y = {1}", gameManager.playerList[gameManager.playerID].NodePosition.x, gameManager.playerList[gameManager.playerID].NodePosition.y));
        }
        else if (!gameManager.playerList[gameManager.playerID].IsSelected && gameManager.playerList[gameManager.playerID].NodePosition.x == 0)
        {
            Vector3 pos = transform.position;

            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.z += panSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.y <= panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);
            pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

            transform.position = pos;
        }
    }

    private void LateUpdate()
    {
        if (inGamePlayer != null)
        {
            Vector3 tempVector = new Vector3(inGamePlayer.transform.position.x, inGamePlayer.transform.position.y + 1f, inGamePlayer.transform.position.z - 0.6f);
            transform.position = tempVector;
            transform.rotation = Quaternion.Euler(20, 0, 0);
        }
    }
}

    
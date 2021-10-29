using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelecter : MonoBehaviour
{
    public Vector3 chameraOffset;
    private Quaternion oldRotation;
    private Vector3 oldPosition;

    private RaycastHit rayHit;

    public Transform cameraBase;

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.turnManager.currentGameMode == TurnManager.GameMode.action)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit);

                if (rayHit.collider.CompareTag($"{TurnManager.turnManager.GetCurrentTurn()}Owned"))
                {
                    StartCoroutine(PositionCamera(rayHit.collider.gameObject));
                }
            }
        }
    }

    private IEnumerator PositionCamera(GameObject character)
    {
        TurnManager.turnManager.controllingCamera = false;

        oldRotation = transform.rotation;
        oldPosition = transform.position;

        while (transform.position != character.transform.position + chameraOffset && transform.rotation != character.transform.rotation)
        {
            transform.position = Vector3.Lerp(transform.position, character.transform.position + chameraOffset, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, character.transform.rotation, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }

        transform.parent = character.transform;

        selectCharacter(character);
    }

    private void selectCharacter(GameObject character)
    {
        character.GetComponent<CharacterController>().startCharacterControl(this);
    }

    public IEnumerator resetCamera()
    {
        transform.parent = null;

        while (transform.position != oldPosition && transform.rotation != oldRotation)
        {
            transform.position = Vector3.Lerp(transform.position, oldPosition, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, oldRotation, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }

        transform.parent = cameraBase;
        TurnManager.turnManager.controllingCamera = true;
    }
}

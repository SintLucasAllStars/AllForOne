using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : MonoBehaviour {
    [HideInInspector]
    public GameManager gameManager;

    public GameObject thirdPersonControler;
    public Text currentTurnText;
    public LayerMask pawnLayer;
    private HashSet<GameObject> ColoredPawnObjects = new HashSet<GameObject>();
    private Pawn selectedPawn;

    public IEnumerator InitBattle() {
        currentTurnText.gameObject.SetActive(true);
        yield return StartCoroutine(SelectPawn());
        yield return StartCoroutine(TakeControl());
    }

    private IEnumerator TakeControl() {
        selectedPawn.gameObject.GetComponentInChildren<MeshRenderer>().material.color /= 4;
        // selectedPawn.GetComponentInChildren<Rigidbody>().detectCollisions = false;
        // GameObject controler = Instantiate(thirdPersonControler);
        // controler.transform.position = selectedPawn.transform.position - Vector3.up * 0.5f;
        // selectedPawn.transform.SetParent(controler.transform);
        // CameraFollowPawn cameraFollowPawn = gameManager.camera.gameObject.GetComponent<CameraFollowPawn>();
        // cameraFollowPawn.enabled = true;
        // cameraFollowPawn.pawn = selectedPawn;
        selectedPawn.gameObject.AddComponent<DTL_ExtendedFlyCam>();
        selectedPawn.gameObject.AddComponent<PawnCombat>().battlePhase = this;
        // gameManager.camera.transform.SetParent(selectedPawn.transform);
        selectedPawn.GetComponentInChildren<Camera>().enabled = true;
        gameManager.camera.gameObject.SetActive(false);
        yield return null;
    }

    public void GuiUpdatePlayer() {
        currentTurnText.text = gameManager.currentPlayer.name;
        currentTurnText.color = gameManager.currentPlayer.color;
    }

    private IEnumerator SelectPawn() {
        while (!selectedPawn) {
            Ray ray = gameManager.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, pawnLayer)) {
                GameObject highlightedGameObject = hit.collider.gameObject;
                if (gameManager.currentPlayer == highlightedGameObject.GetComponentInParent<Pawn>().player) {
                    if (!ColoredPawnObjects.Contains(highlightedGameObject)) {
                        ColoredPawnObjects.Add(highlightedGameObject);
                        highlightedGameObject.GetComponent<MeshRenderer>().material.color /= 4;
                    }
                    // read mouse button
                    if (Input.GetMouseButtonDown(0)) {
                        print("selected");
                        selectedPawn = highlightedGameObject.GetComponentInParent<Pawn>();
                    }
                }
            }
            else {
                foreach (GameObject pawnObject in ColoredPawnObjects) {
                    pawnObject.GetComponent<MeshRenderer>().material.color =
                        pawnObject.GetComponentInParent<Pawn>().player.color;
                }

                ColoredPawnObjects.Clear();
            }


            yield return 0;
        }
    }
}
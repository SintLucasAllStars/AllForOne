using UnityEngine;

public class PawnGenerator : MonoBehaviour {
    public GameObject pawnPrefab;

    private void Start() {
        GameObject pawnInstance = Instantiate(pawnPrefab);
        pawnInstance.transform.position = Vector3.up * 0.5f;
    }
}

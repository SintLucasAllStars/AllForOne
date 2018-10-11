using UnityEngine;

public class CharactarSelector : MonoBehaviour
{

    [SerializeField] private LayerMask layer;

    private string currentTag;
    private bool selectingCharacter;

    private CameraMovement cameraMovement;
    private Camera topDownCamera;


    private void Start()
    {
        GameManager.instance.EndRound += EndRound;
        cameraMovement = GetComponent<CameraMovement>();
        topDownCamera = GetComponent<Camera>();
    }

    private void EndRound()
    {
        currentTag = GameManager.instance.GetCurrentPlayer().playerTag;
        selectingCharacter = true;
    }


    // Update is called once per frame
    private void Update()
    {
        if(!selectingCharacter)
            return;

        RaycastHit hit;
        if(Physics.Raycast(topDownCamera.ScreenPointToRay(Input.mousePosition), out hit, 200, layer))
        {
            if(hit.collider.CompareTag(currentTag))
            {
                if(Input.GetMouseButtonDown(0))
                {
                    SelectUnit(hit.collider.GetComponent<Character>());
                }
            }
        }

    }

    private void SelectUnit(Character character)
    {
        character.ActivateCharacter(true);
        cameraMovement.FlyToward(character);
        selectingCharacter = false;
    }
}

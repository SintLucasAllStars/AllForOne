using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupManager manager;
    public Movement movementScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            movementScript.PowerupTimer = 6;
            movementScript.isRunBoost = true;

            manager.Powerups.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}

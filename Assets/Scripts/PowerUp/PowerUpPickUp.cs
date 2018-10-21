using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    [SerializeField]
    public PowerUp MyPowerUp;


    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<ICharacter>();
        if (character == null) return;
        if (character.OwnedBy() != PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber) return;
        var powerUpInventory = other.GetComponent<PowerUpInventory>();
        if (powerUpInventory != null)
        {
            powerUpInventory.AddPowerUp(MyPowerUp);
            GameManager.Instance.ResetPowerUpPanelTexts();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No power up inventory found");
        }
    }
}

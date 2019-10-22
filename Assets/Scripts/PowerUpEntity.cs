using UnityEngine;

public class PowerUpEntity : MonoBehaviour
{
    private PowerUp _powerUp;

    private void Instantiate(PowerUp.Type powerUpType)
    {
        _powerUp = new PowerUp(powerUpType);
    }

    private void Start()
    {
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                Instantiate(PowerUp.Type.Adrenaline);
                break;
            case 1:
                Instantiate(PowerUp.Type.Rage);
                break;
            case 2:
                Instantiate(PowerUp.Type.TimeMachine);
                break;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.gameObject.CompareTag("Unit"))
        {
            GameManager gameManager = GameManager.GetGameManager();
            Player player = gameManager.GetCurrentPlayer();
            
            player.AddPowerUp(_powerUp);
            
            gameManager.CallPowerUpPickupEvent(_powerUp, player);
            
            Destroy(gameObject);
        }
    }
    
}
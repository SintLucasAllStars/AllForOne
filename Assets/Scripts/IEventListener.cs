public interface IEventListener
{
    void OnStateChange(GameManager.GameState oldState, GameManager.GameState newState);
    void OnPowerUpPickup(PowerUp powerUp, Player player);
}
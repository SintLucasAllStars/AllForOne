namespace Players
{
    public class Character : ICharacter
    {
        public float Health;
        public float Speed;
        public float Strength;
        public float Defence;
        public CharacterMono MyCharacterMono;
        public int OwnedByPlayer;


        public int OwnedBy()
        {
            return OwnedByPlayer;
        }

        public void SetHealth(float damage)
        {
            Health -= damage;
            MyCharacterMono.SetSliderValue();
            if (Health < 0)
            {
                MyCharacterMono.Die();
            }
        }
        
        public int GetCurrentAmountOfAdrenaline()
        {
            return MyCharacterMono.GetCurrentAmountOfAdrenaline();
        }

        public int GetCurrentAmountOfTimeMachine()
        {
            return MyCharacterMono.GetCurrentAmountOfTimeMachine();

        }

        public void AddStrength(float amountOfStrength)
        {
            Strength += amountOfStrength;
        }

        public void AddSpeed(float amountOfSpeed)
        {
            Speed += amountOfSpeed;
        }
        
        

        public int GetCurrentAmountOfRage()
        {
            return MyCharacterMono.GetCurrentAmountOfRage();
        }

        public void ActivatePowerUp(PowerUp.PowerUpEnum powerUpEnum)
        {
            MyCharacterMono.ActivatePowerUp(powerUpEnum);
        }


        public Character(float strength, float defence, float speed, float health,int ownedByPlayer)
        {
            Strength = strength;
            Defence = defence;
            Speed = speed;
            Health = health;
            OwnedByPlayer = ownedByPlayer;

        }

    }

}

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

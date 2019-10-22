using UnityEngine;

namespace MechanicFever
{
    public class Explosion : MonoBehaviour
    {
        private void Start() => Destroy(this.gameObject, 3);
    }
}

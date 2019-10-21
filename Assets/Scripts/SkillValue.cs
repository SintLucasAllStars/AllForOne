using UnityEngine;
using UnityEngine.UI;

namespace MechanicFever
{
    public class SkillValue : MonoBehaviour
    {
        private Slider _slider;

        private void Awake() => _slider = GetComponent<Slider>();

        public void AddValue(int value) => _slider.value += value;
    }
}
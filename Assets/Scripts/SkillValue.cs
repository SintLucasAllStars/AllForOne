using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllForOne
{
    public class SkillValue : MonoBehaviour
    {
        private Slider _slider;

        private void Awake() => _slider = GetComponent<Slider>();

        public void AddValue(int value) => _slider.value += value;
    }
}
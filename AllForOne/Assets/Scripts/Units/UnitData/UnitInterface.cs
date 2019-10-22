using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UnitInterface : MonoBehaviour
{
    [SerializeField] private Unit unit;

    [SerializeField] private Slider healthBar;

    #region Combat UI
    [SerializeField] private GameObject combatCanvas;
    [SerializeField] private TMP_Text damageText;
    #endregion

    // Update is called once per frame
    private void Update()
    {
        healthBar.transform.LookAt(Camera.main.transform);
    }

    /// <summary>
    /// Call this when the combat starts or ends, this opens or closes the combatCanvas.
    /// </summary>
    public void ActivateCombatCanvas(bool active)
    {
        combatCanvas.SetActive(active);
    }

    /// <summary>
    /// Set the damage on the damage text.
    /// </summary>
    public void DamageText(int damage)
    {
        damageText.text = "Damage: " + damage;
    }

    /// <summary>
    /// Set the values of the slider.
    /// </summary>
    public void SetSlider(int maxHitPoints)
    {
        healthBar.minValue = 0;
        healthBar.maxValue = maxHitPoints;
        healthBar.value = maxHitPoints;
    }

    /// <summary>
    /// Call this when HitPoints are changed in any way.
    /// </summary>
    public void UpdateSlider(int hitPoints)
    {
        healthBar.value = hitPoints;
    }
}
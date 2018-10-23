using UnityEngine;
using UnityEngine.UI;

public class UnitGameObjectInteractable : MonoBehaviour
{
    public GameObject unitGameObject;
    public int unitIndex;
    [SerializeField] Image healthImage;

    public void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            Character c = gameObject.GetComponentInParent<Character>();
            if (c != null && c.playerNormalStats != null)
            {
                OnHealthBarChange(c.currentHealth, c.playerNormalStats.health);
            }
        }
    }

    public void OnHealthBarChange (int currentHealth, int maxHealth)
    {
        healthImage.fillAmount = Mathf.Clamp(1f / maxHealth * currentHealth, 0, 1);
    }
}

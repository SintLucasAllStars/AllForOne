using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider damage_s, speed_s, range_s;

    private int damage_i, speed_i, range_i;

    public void CharacterUI()
    {
        damage_s.value = damage_i;
        speed_s.value = speed_i;
        range_s.value = range_i;
        GameController.instance.BuyUnit(damage_i, speed_i, range_i);
    }
}

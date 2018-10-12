using UnityEngine.UI;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    [SerializeField] private Image timerBackground;
    [SerializeField] private Image clockTimer;
    [SerializeField] private RectTransform clockRectTransform;

    [SerializeField] private Vector3 maxAngle;

    private Canvas canvas;

    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<Canvas>();

        GameManager.instance.StartRound += delegate { ToggleUI(true); };
        GameManager.instance.EndRound += delegate { ToggleUI(false); }; ;
        GameManager.instance.EndRound += ToggleColor;
    }

    public void SetTimer(float seconds)
    {
        float percentage = 1 - seconds / 10;

        clockRectTransform.localRotation = Quaternion.Euler(0,0, -360 * percentage);
    }

    void ToggleColor()
    {
        Color color = GameManager.instance.GetCurrentPlayer().color;
        timerBackground.color = color;
        clockTimer.color = color;
    }

    void ToggleUI(bool enable)
    {
        canvas.enabled = enable;
        clockRectTransform.localRotation = Quaternion.identity;

    }
}

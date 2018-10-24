using UnityEngine.UI;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    public static GameUi instance;

    [SerializeField] private Image timerBackground;
    [SerializeField] private Image clockTimer;
    [SerializeField] private RectTransform clockRectTransform;

    [SerializeField] private Image background;
    [SerializeField] private Text winnerText;

    public Image powerUp;

    private Canvas canvas;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

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

        clockRectTransform.localRotation = Quaternion.Euler(0, 0, -360 * percentage);
    }

    public void EndScreen(Player winner)
    {
        canvas.enabled = true;
        background.enabled = true;
        background.color = winner.color;
        winnerText.enabled = true;
        winnerText.text = winner.playerTag + winnerText.text;
    }

    void ToggleColor()
    {
        Color color = GameManager.instance.GetCurrentPlayer().color;
        Image[] images = GetComponentsInChildren<Image>(true);
        for(int i = 0; i < images.Length; i++)
        {
            images[i].color = color;
        }
    }

    void ToggleUI(bool enable)
    {
        canvas.enabled = enable;
        clockRectTransform.localRotation = Quaternion.identity;
    }
}

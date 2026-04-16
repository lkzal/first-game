using UnityEngine;
using TMPro;

public class TipUI : MonoBehaviour
{
    public static TipUI Instance;

    public TextMeshProUGUI tipText;

    void Awake()
    {
        Instance = this;
        tipText.gameObject.SetActive(false);
    }

    public void ShowTip(string text)
    {
        //tipText.text = text;
        tipText.gameObject.SetActive(true);
    }

    public void HideTip()
    {
        tipText.gameObject.SetActive(false);
    }
}
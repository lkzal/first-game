using UnityEngine;

public class SunMover : MonoBehaviour
{
    [Header("时间设置")]
    public float dayLength = 120f; // 完整一天的秒数
    public float timeOfDay = 0.25f; // 0=午夜, 0.25=早上6点, 0.5=正午, 0.75=傍晚6点

    [Header("颜色设置")]
    public Color sunriseColor = new Color(1f, 0.5f, 0.3f);
    public Color noonColor = new Color(1f, 1f, 0.9f);
    public Color sunsetColor = new Color(1f, 0.3f, 0.2f);

    private Light sunLight;
    private RenderSettings skySettings;

    void Start()
    {
        sunLight = GetComponent<Light>();
    }

    void Update()
    {
        // 时间流逝
        timeOfDay += Time.deltaTime / dayLength;
        if (timeOfDay > 1f) timeOfDay -= 1f;

        // 计算太阳角度（绕X轴旋转）
        float sunAngle = Mathf.Lerp(-90f, 270f, timeOfDay);
        transform.rotation = Quaternion.Euler(sunAngle, 45f, 0f);

        // 改变颜色
        if (timeOfDay < 0.25f) // 午夜到日出
            sunLight.color = Color.Lerp(sunsetColor, sunriseColor, timeOfDay * 4);
        else if (timeOfDay < 0.5f) // 日出到正午
            sunLight.color = Color.Lerp(sunriseColor, noonColor, (timeOfDay - 0.25f) * 4);
        else if (timeOfDay < 0.75f) // 正午到日落
            sunLight.color = Color.Lerp(noonColor, sunsetColor, (timeOfDay - 0.5f) * 4);
        else // 日落到午夜
            sunLight.color = Color.Lerp(sunsetColor, Color.black, (timeOfDay - 0.75f) * 4);

        // 正午亮度最高，日出日落减弱
        sunLight.intensity = Mathf.Clamp01(Mathf.Cos((timeOfDay - 0.5f) * Mathf.PI * 2)) * 1.5f;
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetColor("_Tint", sunLight.color);
        }
    }
}

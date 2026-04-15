using UnityEngine;

public class SunSkyGradient : MonoBehaviour
{
    [Header("太阳")]
    public float rotateSpeed = 0.5f;
    private Light sun;

    [Header("渐变颜色")]
    public Color daySky = new Color(0.3f, 0.6f, 1);
    public Color duskSky = new Color(1, 0.4f, 0.2f);
    public Color nightSky = new Color(0.1f, 0.1f, 0.3f);

    private Material skyMat;

    void Start()
    {
        sun = GetComponent<Light>();
        skyMat = RenderSettings.skybox;
    }

    void Update()
    {
        // 太阳旋转
        transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        float x = transform.eulerAngles.x;

        // 按高度渐变天空色
        if (x > 60 && x < 120)
        {
            skyMat.SetColor("_SkyTint", daySky);
            sun.intensity = 1.2f;
        }
        else if (x > 150 && x < 210)
        {
            skyMat.SetColor("_SkyTint", duskSky);
            sun.intensity = 0.6f;
        }
        else
        {
            skyMat.SetColor("_SkyTint", nightSky);
            sun.intensity = 0.1f;
        }
        DynamicGI.UpdateEnvironment();
    }
}

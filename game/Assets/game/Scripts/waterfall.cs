using UnityEngine;

public class WaterfallFlow : MonoBehaviour
{
    public float speed = 2f;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat.mainTextureOffset += new Vector2(0, -speed * Time.deltaTime);
    }
}

using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 100f; // 小地图相机高度

    void LateUpdate()
    {
        if (player == null) return;

        // 只改 XZ，保持高度不变
        Vector3 newPos = player.position;
        newPos.y = height;

        transform.position = newPos;
    }
}


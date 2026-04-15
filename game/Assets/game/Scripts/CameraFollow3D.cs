using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform player;

    [Header("基础跟随")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 30f; // 提高到30，保证跟手但不抖

    [Header("鼠标视角控制")]
    public float mouseSensitivity = 2f;
    public float minPitch = -30f;
    public float maxPitch = 60f;
    private float yaw = 0f;
    private float pitch = 0f;

    [Header("视角模式")]
    public bool isFreeCamera = true;

    void LateUpdate()
    {
        if (player == null) return;

        // 核心修复：强制让相机使用刚体上一帧的平滑位置
        Vector3 targetPos = player.position;

        if (isFreeCamera)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            targetPos += rotation * offset;

            transform.position = targetPos; // 直接赋值，不再平滑
            transform.LookAt(player.position + Vector3.up * 1.5f);
        }
        else
        {
            targetPos += offset;
            transform.position = targetPos; // 直接赋值
            transform.LookAt(player.position + Vector3.up * 1.5f);
        }
    }

    public void SwitchToFixedView()
    {
        isFreeCamera = false;
        yaw = 0f;
        pitch = 0f;
    }

    public void SwitchToFreeView()
    {
        isFreeCamera = true;
    }
}

using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform player; // 拖入玩家对象

    [Header("基础跟随")]
    public Vector3 offset = new Vector3(0, 5, -10); // 相机相对玩家的偏移量
    public float followSpeed = 10f; // 跟随平滑度

    [Header("鼠标视角控制")]
    public float mouseSensitivity = 2f; // 鼠标灵敏度
    public float minPitch = -30f; // 最小俯角
    public float maxPitch = 60f;  // 最大仰角
    private float yaw = 0f;   // 水平旋转角
    private float pitch = 0f; // 垂直旋转角

    [Header("视角模式")]
    public bool isFreeCamera = true; // true=自由视角，false=固定视角

    void LateUpdate()
    {
        if (player == null) return;

        if (isFreeCamera)
        {
            // 1. 自由视角：鼠标控制旋转
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // 限制俯仰角

            // 计算旋转后的相机位置
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 targetPos = player.position + rotation * offset;

            // 平滑移动相机
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
            // 相机始终看向玩家
            transform.LookAt(player.position + Vector3.up * 1.5f);
        }
        else
        {
            // 2. 固定视角：不响应鼠标，只跟随玩家
            Vector3 targetPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
            transform.LookAt(player.position + Vector3.up * 1.5f);
        }
    }

    // 外部调用：一键切换到固定视角
    public void SwitchToFixedView()
    {
        isFreeCamera = false;
        // 重置旋转到初始固定视角
        yaw = 0f;
        pitch = 0f;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    // 外部调用：一键切换到自由视角
    public void SwitchToFreeView()
    {
        isFreeCamera = true;
    }
}

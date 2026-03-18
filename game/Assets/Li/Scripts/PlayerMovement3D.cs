using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement3D : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 7f;

    [Header("跳跃设置")]
    public float jumpForce = 7f;
    public bool isGrounded; // 可在Inspector查看是否在地面

    private Rigidbody _rb;
    private Vector3 _moveDir;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = true;       // 使用Unity自带重力
        _rb.freezeRotation = true;   // 禁止旋转摔倒
    }

    protected virtual void Update()
    {
        // 立即获取输入
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // XZ平面移动方向
        _moveDir = new Vector3(h, 0, v).normalized;

        // 跳跃检测
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    protected virtual void FixedUpdate()
    {
        // 物理移动：保持Y轴速度，只控制XZ
        Vector3 moveVelocity = _moveDir * moveSpeed;
        _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
    }

    // 跳跃逻辑
    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
    }

    // 地面检测
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}

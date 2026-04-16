using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement3D : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 7f;

    [Header("跳跃设置")]
    public float jumpForce = 7f;
    public bool isGrounded;

    [Header("相机视角")]
    public Camera mainCamera;

    // 对话时禁止移动
    public static bool canMove = true;

    private Rigidbody _rb;
    private Vector3 _moveDir;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = true;
        _rb.freezeRotation = true;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        // 不能移动时直接跳过输入
        if (!canMove)
        {
            _moveDir = Vector3.zero;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0, v).normalized;
        _moveDir = GetCameraRelativeMoveDir(input);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    protected virtual void FixedUpdate()
    {
        Vector3 moveVelocity = _moveDir * moveSpeed;
        _rb.velocity = new Vector3(moveVelocity.x, _rb.velocity.y, moveVelocity.z);
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    protected virtual Vector3 GetCameraRelativeMoveDir(Vector3 input)
    {
        if (mainCamera == null || input.magnitude < 0.01f)
            return Vector3.zero;

        Vector3 f = mainCamera.transform.forward;
        Vector3 r = mainCamera.transform.right;
        f.y = 0; r.y = 0;
        f.Normalize(); r.Normalize();

        Vector3 dir = r * input.x + f * input.z;
        if (dir.magnitude > 0.1f)
        {
            transform.forward = dir;
        }

        return dir.normalized;
    }
}
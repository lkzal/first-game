using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;

    private CharacterController _controller;

    protected virtual void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        MoveInput();
    }

    protected virtual void MoveInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        _controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    // 外部禁用移动
    public virtual void SetCanMove(bool enable)
    {
        enabled = enable;
    }
}

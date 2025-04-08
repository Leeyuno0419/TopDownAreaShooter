using UnityEngine;

/// <summary>
/// �÷��̾��� �̵�, ���� ��ȯ, �ð��� ������ ���
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("�̵� ����")]
    public float moveSpeed = 5f;

    [Header("��������Ʈ")]
    public SpriteRenderer spriteRenderer; // �ڽĿ� �ִ� SpriteRenderer ����
    public Transform spriteTransform;     // ũ�� ������ �ڽ� Transform ����

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 originalScale;
    private float bobTimer = 0f;
    public float bobSpeed = 8f;   // ��鸲 �ӵ�
    public float bobAmount = 0.05f; // ��鸲 ũ��

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = spriteTransform.localScale;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // ���⿡ ���� �̹��� ����
        if (moveInput.x > 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x < 0)
            spriteRenderer.flipX = false;

        // �̵� �� ��鸲 �ִϸ��̼�
        if (moveInput.sqrMagnitude > 0.01f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float scaleOffset = Mathf.Sin(bobTimer) * bobAmount;
            spriteTransform.localScale = new Vector3(
                originalScale.x + scaleOffset,
                originalScale.y - scaleOffset,
                originalScale.z
            );
        }
        else
        {
            spriteTransform.localScale = originalScale; // ���� ����
            bobTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
    }
}
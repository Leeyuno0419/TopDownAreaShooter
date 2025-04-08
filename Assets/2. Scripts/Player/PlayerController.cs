using UnityEngine;

/// <summary>
/// 플레이어의 이동, 방향 전환, 시각적 연출을 담당
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;

    [Header("스프라이트")]
    public SpriteRenderer spriteRenderer; // 자식에 있는 SpriteRenderer 연결
    public Transform spriteTransform;     // 크기 조절할 자식 Transform 연결

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 originalScale;
    private float bobTimer = 0f;
    public float bobSpeed = 8f;   // 흔들림 속도
    public float bobAmount = 0.05f; // 흔들림 크기

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = spriteTransform.localScale;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 방향에 따라 이미지 반전
        if (moveInput.x > 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x < 0)
            spriteRenderer.flipX = false;

        // 이동 시 흔들림 애니메이션
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
            spriteTransform.localScale = originalScale; // 원상 복구
            bobTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
    }
}
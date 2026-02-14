using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// 플레이어의 이동 속도
    /// </summary>
    [SerializeField]
    private float moveSpeed;

    /// <summary>
    /// 이동에 필요한 리지드바디2D
    /// </summary>
    private Rigidbody2D playerRigidbody2D;

    private void Awake()
    {
        // 컴포넌트 정보 가져오기
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 플레이어 이동
        PlayerMove();
    }

    /// <summary>
    /// 플레이어를 이동시키는 함수
    /// </summary>
    private void PlayerMove()
    {
        // PlayerInput에 있는 입력값을 가져와준다.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 가져온 입력값을 정규화하여 Vector2 타입의 지역 변수에 값으로 넣어준다.
        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Rigidbody2D를 통해 실제로 움직이게 만들어준다.
        playerRigidbody2D.linearVelocity = moveDirection * moveSpeed;
    }
}

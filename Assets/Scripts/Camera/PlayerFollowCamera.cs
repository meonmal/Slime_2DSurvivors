using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    /// <summary>
    /// 카메라가 따라 다닐 플레이어
    /// </summary>
    [SerializeField]
    private Transform player;

    private void LateUpdate()
    {
        // 카메라가 플레이어를 따라다니게 만든다.
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}

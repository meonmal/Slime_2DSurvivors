using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class MapRePosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    // 플레이어 위치 기준으로 청크를 재배치하기 때문에 필요
    [SerializeField] private Transform[] chunks;
    // 3x3 = 9개 청크 (타일맵 Transform 9개)

    [Header("Chunk size (cells)")]
    [SerializeField] private Vector2Int chunkCellSize = new Vector2Int(36, 20);
    // 한 청크가 가로 36칸, 세로 20칸

    [Header("Grid cell size (world units)")]
    [SerializeField] private Vector2 gridCellSize = new Vector2(1f, 1f);
    // 타일 1칸이 월드에서 몇 유닛인지 (Grid.cellSize 값)

    [Header("Origin")]
    [SerializeField] private Vector2 originWorld = Vector2.zero;
    // 맵 기준점 (보통 0,0)

    [Header("If your chunks are center-aligned like (+18,+10) etc")]
    [SerializeField] private bool centerAligned = true;
    // 청크가 중심 기준(±18, ±10 등)으로 배치되어 있다면 true

    private Vector2 chunkWorldSize;
    // 청크 1장의 월드 크기 (ex. 36 x 20 유닛)

    private Vector2 centerOffset;
    // 중심 배치일 경우 반 청크 오프셋 (ex. 18, 10)

    private void Awake()
    {
        // 청크 한 장의 월드 크기 계산
        chunkWorldSize = new Vector2(
            chunkCellSize.x * gridCellSize.x,
            chunkCellSize.y * gridCellSize.y
        );

        // 중심 배치라면 반 청크만큼 오프셋 적용
        centerOffset = centerAligned ? (chunkWorldSize * 0.5f) : Vector2.zero;

        // 안전 체크 (3x3이 아니라면 오류 출력)
        if (chunks == null || chunks.Length != 9)
            Debug.LogError($"chunks는 9개(3x3)여야 함. 현재: {(chunks == null ? 0 : chunks.Length)}개");
    }

    private void LateUpdate()
    {
        if (player == null || chunks == null || chunks.Length != 9)
            return;

        // 플레이어 위치를 청크 좌표계로 변환
        // centerOffset을 빼는 이유: 중심 기준 배치 보정
        Vector2 p = (Vector2)player.position - originWorld - centerOffset;

        // 플레이어가 현재 속한 "청크 인덱스" 계산
        int baseX = Mathf.FloorToInt(p.x / chunkWorldSize.x);
        int baseY = Mathf.FloorToInt(p.y / chunkWorldSize.y);

        /*
         이제 3x3을 항상 플레이어 주변으로 배치한다.

         (-1,1)   (0,1)   (1,1)
         (-1,0)   (0,0)   (1,0)
         (-1,-1)  (0,-1)  (1,-1)

         baseX, baseY가 플레이어가 속한 중심 청크.
        */

        int idx = 0;

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                // 목표 위치 계산
                // 플레이어 기준 청크 + 주변 오프셋
                Vector2 targetPos2D = originWorld + centerOffset + new Vector2(
                    (baseX + x) * chunkWorldSize.x,
                    (baseY + y) * chunkWorldSize.y
                );

                // 기존 Z값은 유지
                Vector3 targetPos = new Vector3(
                    targetPos2D.x,
                    targetPos2D.y,
                    chunks[idx].position.z
                );

                // 청크를 해당 위치로 재배치
                chunks[idx].position = targetPos;

                idx++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 밤송이 오브젝트를 생성하는 클래스
/// 마우스 클릭 시 밤송이를 생성하고 클릭 방향으로 발사합니다.
/// </summary>
public class BamsongiGenerator : MonoBehaviour
{
    // 생성할 밤송이 프리팹 (Inspector에서 할당)
    public GameObject bamsongiPrefab;

    // 매 프레임마다 호출되는 메서드
    void Update()
    {
        // [구버전] 고정 방향으로 밤송이 발사 (비활성화)
        // if (Input.GetMouseButtonDown(0))
        // {
        //     GameObject bamsongi = Instantiate(bamsongiPrefab);
        //     bamsongi.GetComponent<BamsongiController>().Shoot(new Vector3(0, 250, 2000));
        // }

        // 마우스 왼쪽 클릭 시 화면의 클릭 좌표 방향으로 밤송이 생성 및 발사
        if (Input.GetMouseButtonDown(0))
        {
            // 밤송이 프리팹을 인스턴스화하여 씬에 생성
            GameObject bamsongi = Instantiate(bamsongiPrefab);

            // 마우스 클릭 위치에서 카메라 기준 레이(광선) 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            // 레이 방향을 월드 좌표로 변환
            Vector3 worldDir = ray.direction;
            
            // 정규화된 방향에 힘(2000)을 곱해 발사
            bamsongi.GetComponent<BamsongiController>().Shoot(worldDir.normalized * 2000);
        }
    }
}

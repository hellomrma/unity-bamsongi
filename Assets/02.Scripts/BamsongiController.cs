using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 밤송이 오브젝트를 제어하는 클래스
/// 발사 기능과 충돌 시 동작을 관리합니다.
/// </summary>
public class BamsongiController : MonoBehaviour
{
    // 총 누적 점수 (static: 모든 밤송이가 공유하는 변수)
    public static int totalScore = 0;

    // 게임 시작 시 호출되는 메서드
    void Start()
    {
        // 테스트용 발사 코드 (비활성화)
        // Shoot(new Vector3(0, 250, 2000));
    }

    /// <summary>
    /// 밤송이를 지정된 방향으로 발사합니다.
    /// </summary>
    /// <param name="dir">발사 방향 및 힘의 크기를 나타내는 벡터</param>
    public void Shoot(Vector3 dir)
    {
        // Rigidbody에 힘을 가해 발사
        GetComponent<Rigidbody>().AddForce(dir);
    }

    /// <summary>
    /// 다른 오브젝트와 충돌했을 때 호출되는 메서드
    /// </summary>
    /// <param name="collision">충돌 정보</param>
    private void OnCollisionEnter(Collision collision)
    {
        // 물리 시뮬레이션 비활성화 (밤송이가 박힌 상태로 고정)
        GetComponent<Rigidbody>().isKinematic = true;
        
        // 파티클 효과 재생 (충돌 이펙트)
        GetComponent<ParticleSystem>().Play();

        // 충돌한 오브젝트의 자식으로 설정 (타겟에 붙어서 함께 이동)
        transform.parent = collision.transform;

        // 충돌한 오브젝트가 타겟인지 확인하고 점수 누적
        // collision.gameObject: 밤송이와 충돌한 오브젝트 (타겟 또는 바닥 등)
        // GetComponent<TargetController>(): 해당 오브젝트에서 TargetController 스크립트를 가져옴
        // - 타겟이면 → TargetController 스크립트가 target 변수에 저장됨
        // - 바닥/벽이면 → null이 저장됨 (스크립트가 없으므로)
        TargetController target = collision.gameObject.GetComponent<TargetController>();
        
        // target이 null이 아닌지 확인 (충돌한 오브젝트가 타겟인지 확인)
        // 바닥이나 벽에 부딪혔을 때는 점수를 주면 안 되므로 조건 검사
        if (target != null)
        {
            // target.score: 해당 타겟의 점수 (TargetController에서 설정한 값)
            // totalScore +=: 총점에 더함 (totalScore = totalScore + target.score 와 같음)
            totalScore += target.score;
            
            // Console 창에 획득 점수와 총점 출력
            Debug.Log("타겟 적중! 획득 점수: " + target.score + " / 총점: " + totalScore);
        }

        // 부모 해제 코드 (비활성화)
        // transform.parent = null;

        // 충돌체 비활성화 (추가 충돌 방지)
        GetComponent<SphereCollider>().enabled = false;

        // 밤송이 오브젝트 삭제 (비활성화)
        // Destroy(gameObject, 5.0f);
    }
}

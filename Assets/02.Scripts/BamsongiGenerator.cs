using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamsongiGenerator : MonoBehaviour
{

    public GameObject bamsongiPrefab;

    // Update is called once per frame
    void Update()
    {

        // 마우스 온쪽 클릭시 밤송이 오브젝트 생성 및 날리기
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bamsongi = Instantiate(bamsongiPrefab);
            bamsongi.GetComponent<BamsongiController>().Shoot(new Vector3(0, 250, 2000));
        }
    }
}

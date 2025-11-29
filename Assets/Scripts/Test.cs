using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Awake() // 컴포넌트가 활성화되기 전에 호출
    {
        Debug.Log("Awake");
    }

    void OnEnable() // 컴포넌트가 활성화 될 때 호출
    {
        Debug.Log("OnEnable");
    }

    void Start() // 게임이 시작될 때 호출
    {
        Debug.Log("Start");
    }

    void FixedUpdate() // 물리 연산이 필요한 때 호출, 1초에 50번 호출
    {
        Debug.Log("FixedUpdate");
    }

    void Update() // 게임이 실행되는 동안 계속 호출, 1초에 60번 호출(단 환경에 따라 다르게 호출)
    {
        Debug.Log("Update");
    }

    void LateUpdate() // Update보다 나중에 호출, 후처리 및 보정 작업에 사용
    {
        Debug.Log("LateUpdate");
    }

    void OnDestroy() // 컴포넌트가 파괴될 때 호출
    {
        Debug.Log("OnDestroy");
    }

    void OnDisable() // 컴포넌트가 비활성화 될 때 호출
    {
        Debug.Log("OnDisable");
    }

    private int j = 1;
    private float f = 1.0f;
    private string s = "일";
    private bool b = true;
    private Vector2 v = new Vector2(1, 1);
    private Vector3 v3 = new Vector3(1, 1, 1);
}

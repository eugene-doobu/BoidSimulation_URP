using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageYRotate : MonoBehaviour
{
    public float speed = 0.1f;
    Transform tr;
    Action rotAction;
    

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }
    private void Start()
    {
        AddRotAction();
    }

    void Update()
    {
        rotAction();
    }

    void RotFunc()
    {
        tr.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    [ContextMenu("AddRotAction")]
    public void AddRotAction()
    {
        // RotAction의 중복실행을 막기 위해 Action 초기화
        rotAction = null;
        rotAction += RotFunc;
    }

    [ContextMenu("RemoveRotAction")]
    public void RemoveRotAction()
    {
        rotAction = null;
        rotAction += NullFunc;
    }

    // null Action 접근으로 인한 테스트 에러 방지용
    void NullFunc()
    {
        return;
    }
}

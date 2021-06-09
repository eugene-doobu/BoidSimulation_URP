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
        AddRotAction();
    }

    void Update()
    {
        rotAction();
    }

    void RotFunc()
    {
        tr.Rotate(Vector3.up * speed);
    }

    [ContextMenu("AddRotAction")]
    void AddRotAction()
    {
        // RotAction의 중복실행을 막기 위해 Action 초기화
        rotAction = null;
        rotAction += RotFunc;
    }

    [ContextMenu("RemoveRotAction")]
    void RemoveRotAction()
    {
        rotAction = null;
    }
}

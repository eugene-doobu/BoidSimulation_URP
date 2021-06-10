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
        tr.Rotate(Vector3.up * speed);
    }

    [ContextMenu("AddRotAction")]
    public void AddRotAction()
    {
        // RotAction�� �ߺ������� ���� ���� Action �ʱ�ȭ
        rotAction = null;
        rotAction += RotFunc;
    }

    [ContextMenu("RemoveRotAction")]
    public void RemoveRotAction()
    {
        rotAction = null;
        rotAction += VoidFuncs;
    }

    // null Action �������� ���� �׽�Ʈ ���� ������
    void VoidFuncs()
    {
        return;
    }
}

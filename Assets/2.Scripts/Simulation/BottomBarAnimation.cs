using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using System;

public class BottomBarAnimation : MonoBehaviour
{
    #region variables
    Transform tr;

    // group_panels part
    Transform group_panelsTr;
    Text[] panels;

    ReactiveProperty<float> selectedPos = new ReactiveProperty<float>(0);
    Transform selectedTr;
    #endregion

    #region Unity Event Funcs
    private void Awake()
    {
        // Find Objs
        tr = GetComponent<Transform>();
        group_panelsTr = tr.Find("Group_panels");
        panels = group_panelsTr.GetComponentsInChildren<Text>();
        selectedTr = tr.Find("img_selected");
    }

    void Start()
    {
        // ���� �ε��ǰ� ���� �ð����� UI�� Transfrom���� �Ҿ�����
        // ���� �ľ��� �ȵǾ� �ϴ��� ����� �Է� ��       
        // ������� ȭ�� ��ü ���� ���̿��� ù��° �г��� �߾Ӱ��� ��ġ ����
        selectedPos.Value = 0.2475f * Screen.width;

        // ��ư Ŭ���� selected mark �̵�
        selectedPos
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                BtnsEnable(false);
                selectedTr.DOMoveX(selectedPos.Value, 1f)
                    .OnComplete(() => BtnsEnable(true));
                // �÷��̾� �̵� �ִϸ��̼� �̺�Ʈ
            });
    }

    #endregion

    /// <summary>
    /// ��� ��ư���� enable�� ���� ó�����ִ� �Լ�
    /// ��ư�� ���� �����ϸ� Animation State�� �����Ƿ�, 
    /// Text�� �����Ͽ� Raycast Target�� ��Ȱ��ȭ����
    /// </summary>
    /// <param name="state"> ��ư�� ������ enable state</param>
    void BtnsEnable(bool state)
    {
        foreach (var txt in panels)
        {
            txt.raycastTarget = state;
        }
    }

    public void OnPanelSelect(Transform _tr)
    {
        selectedPos.Value = _tr.position.x;
    }
}

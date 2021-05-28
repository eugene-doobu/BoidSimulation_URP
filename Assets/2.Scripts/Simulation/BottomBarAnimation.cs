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
        // 씬이 로딩되고 일정 시간동안 UI의 Transfrom값이 불안정함
        // 원인 파악이 안되어 일단은 상수값 입력 ㅡ       
        // 상수값은 화면 전체 가로 넓이에서 첫번째 패널의 중앙값의 위치 비율
        selectedPos.Value = 0.2475f * Screen.width;

        // 버튼 클릭시 selected mark 이동
        selectedPos
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                BtnsEnable(false);
                selectedTr.DOMoveX(selectedPos.Value, 1f)
                    .OnComplete(() => BtnsEnable(true));
                // 플레이어 이동 애니메이션 이벤트
            });
    }

    #endregion

    /// <summary>
    /// 모든 버튼들의 enable을 동시 처리해주는 함수
    /// 버튼에 직접 접근하면 Animation State가 깨지므로, 
    /// Text에 접근하여 Raycast Target을 비활성화해줌
    /// </summary>
    /// <param name="state"> 버튼에 적용할 enable state</param>
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

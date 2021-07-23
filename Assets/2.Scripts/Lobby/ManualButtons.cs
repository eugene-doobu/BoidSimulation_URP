using UnityEngine;

public class ManualButtons : MonoBehaviour
{
    /// <summary>
    /// 나가기 버튼을 누른 경우
    /// UI 패널을 닫는다.
    /// </summary>
    public void OnClickExitButton()
    {
        // 부모의 모달창을 비활성화
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class ManualButtons : MonoBehaviour
{
    /// <summary>
    /// ������ ��ư�� ���� ���
    /// UI �г��� �ݴ´�.
    /// </summary>
    public void OnClickExitButton()
    {
        // �θ��� ���â�� ��Ȱ��ȭ
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}

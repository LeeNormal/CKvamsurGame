using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    // �ð�
    public float time;
    // �ð����̰� �ϴ� �ؽ�
    public Text text_Timer;

    void Start()
    {
        if (text_Timer == null)
        {
            text_Timer = GetComponent<Text>();
        }
        else { }
    }

    void Update()
    {
        TimerUI();
    }

    // �ð� ���� �Լ�
    void TimerUI()
    {
        time += Time.deltaTime;
        text_Timer.text = "�ð� : " + Mathf.Round(time);
    }

}

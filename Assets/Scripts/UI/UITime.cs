using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    // 시간
    public float time;
    // 시간보이게 하는 텍스
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

    // 시간 구현 함수
    void TimerUI()
    {
        time += Time.deltaTime;
        text_Timer.text = "시간 : " + Mathf.Round(time);
    }

}

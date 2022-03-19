using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] GameSystem gameSystem;
    [SerializeField] GameObject resultPanel;
    int timeCount;

    private void Start()
    {
        timeCount = ParamsSO.Entity.TimeLimit;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1);
            timeCount--;
            timerText.text = timeCount.ToString();
        }
        Debug.Log("タイムアップ");
        gameSystem.SetGameOverFlag(true);
        resultPanel.SetActive(true);
    }
}

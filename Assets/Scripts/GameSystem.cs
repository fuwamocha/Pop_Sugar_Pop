using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] ScoreCounter score;
    [SerializeField] BallGenerator ballGenerator;
    [SerializeField] EffectSpawner effectSpawner;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();
    [SerializeField] Text timerText;

    [SerializeField] GameObject resultPanel;
    Ball currentDraggingBall;
    int timeCount;
    bool isDragging;
    bool gameOver;

    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Main);

        timeCount = ParamsSO.Entity.TimeLimit;

        score.Add(0);

        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.InitBallCount));
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
        gameOver = true;
        resultPanel.SetActive(true);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene("Main");
    }

    void Update()
    {
        if (gameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
        else if (isDragging)
        {
            OnDragging();
        }
    }

    void OnDragBegin()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit && hit.collider.GetComponent<Ball>())
        {
            Ball ball = hit.collider.GetComponent<Ball>();

            if (ball.IsBomb())
            {
                Explosion(ball);
            }
            else
            {
                AddRemoveBall(ball);
                isDragging = true;
            }

        }
    }

    void OnDragging()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit && hit.collider.GetComponent<Ball>())
        {
            Ball ball = hit.collider.GetComponent<Ball>();

            if (ball.id == currentDraggingBall.id)
            {
                float distance = Vector2.Distance(ball.transform.position, currentDraggingBall.transform.position);

                if (distance < ParamsSO.Entity.BallDistance)
                {
                    AddRemoveBall(ball);
                }
            }

        }
    }

    void OnDragEnd()
    {
        int removeCount = removeBalls.Count;

        if (removeCount >= ParamsSO.Entity.BallMinNumber)
        {
            for (int i = 0; i < removeCount; i++)
            {
                removeBalls[i].Explosion();
            }
            score.Add(removeCount * ParamsSO.Entity.ScorePoint);

            StartCoroutine(ballGenerator.Spawns(removeCount));
            effectSpawner.Score(removeBalls[removeCount - 1].transform.position, removeCount * ParamsSO.Entity.ScorePoint);

            SoundManager.instance.PlaySE(SoundManager.SE.Destroy);
        }

        for (int i = 0; i < removeCount; i++)
        {
            removeBalls[i].transform.localScale = Vector3.one;
            removeBalls[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        removeBalls.Clear();

        isDragging = false;
    }

    void AddRemoveBall(Ball ball)
    {

        currentDraggingBall = ball;

        if (!removeBalls.Contains(ball))
        {
            SoundManager.instance.PlaySE(SoundManager.SE.Touch);

            ball.transform.localScale = Vector3.one * 1.4f;
            removeBalls.Add(ball);
        }
    }

    void Explosion(Ball bomb)
    {
        List<Ball> explosionList = new List<Ball>();
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(bomb.transform.position, ParamsSO.Entity.BombRange);

        for (int i = 0; i < hitObj.Length; i++)
        {
            Ball ball = hitObj[i].GetComponent<Ball>();
            if (ball)
            {
                explosionList.Add(ball);
            }
        }

        int removeCount = explosionList.Count;
        for (int i = 0; i < removeCount; i++)
        {
            explosionList[i].Explosion();
        }
        score.Add(removeCount * ParamsSO.Entity.ScorePoint);
        StartCoroutine(ballGenerator.Spawns(removeCount));
        effectSpawner.Score(bomb.transform.position, removeCount * ParamsSO.Entity.ScorePoint);

        SoundManager.instance.PlaySE(SoundManager.SE.Destroy);
    }

}

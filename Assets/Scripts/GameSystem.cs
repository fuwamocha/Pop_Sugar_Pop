using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] BallGenerator ballGenerator;
    [SerializeField] EffectSpawner effectSpawner;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();

    Ball currentDraggingBall;
    bool isDragging;
    public bool gameOver { get; private set; }

    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Main);

        score.Add(0);

        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.InitBallCount));
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
                Bomb bomb = hit.collider.GetComponent<Bomb>();
                int removeCount = bomb.Explosion(ball);

                score.Add(removeCount * ParamsSO.Entity.ScorePoint);
                StartCoroutine(ballGenerator.Spawns(removeCount));
                effectSpawner.Score(bomb.transform.position, removeCount * ParamsSO.Entity.ScorePoint);
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

    public void SetGameOverFlag(bool status)
    {
        gameOver = status;
    }
}

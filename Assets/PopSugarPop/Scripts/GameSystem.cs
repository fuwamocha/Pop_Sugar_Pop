using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator;
    [SerializeField] DragStatus dragStatus;

    public bool gameOver { get; private set; }

    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Main);

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
            dragStatus.OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragStatus.OnDragEnd();
        }
        else if (dragStatus.isDragging)
        {
            dragStatus.OnDragging();
        }
    }

    public void SetGameOverFlag(bool status)
    {
        gameOver = status;
    }
}

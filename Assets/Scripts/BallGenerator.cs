using System.Collections;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    // Ballを生成
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Sprite[] ballSprites;
    [SerializeField] Sprite bombSprite;

    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8f);

            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);

            int ballID = Random.Range(0, ballSprites.Length);

            if (Random.Range(0, 100) < 30) // 3％の確率でtrue
            {
                ballID = -1;
                ball.GetComponent<SpriteRenderer>().sprite = bombSprite;
            }
            else
            {
                ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID];
            }

            ball.GetComponent<Ball>().id = ballID;

            yield return new WaitForSeconds(0.04f);
        }
    }
}

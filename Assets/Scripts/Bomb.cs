using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int Explosion(Ball bomb)
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
        SoundManager.instance.PlaySE(SoundManager.SE.Destroy);

        return removeCount;
    }
}

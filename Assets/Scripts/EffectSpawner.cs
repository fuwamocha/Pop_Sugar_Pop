using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] GameObject pointEffectPrefab;
    public void Score(Vector2 position, int score)
    {
        GameObject effectObj = Instantiate(pointEffectPrefab, position, Quaternion.identity);
        PointEffect pointEffect = effectObj.GetComponent<PointEffect>();
        pointEffect.Show(score);
    }
}

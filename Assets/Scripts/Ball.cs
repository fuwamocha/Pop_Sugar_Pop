using UnityEngine;

public class Ball : MonoBehaviour
{
    public int id;
    [SerializeField] GameObject explosionPrefab;

    public void Explosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        Destroy(explosion, 0.2f);
    }

    public bool IsBomb()
    {
        return id == -1;
    }

}

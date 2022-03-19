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
        /* 
        if (id == -1)
        {
            return true;
        }
            return false; */
        return id == -1;
    }

}

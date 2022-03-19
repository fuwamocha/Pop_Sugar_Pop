using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceBGM;
    [SerializeField] AudioClip[] BGMClips;

    [SerializeField] AudioSource audioSourceSE;
    [SerializeField] AudioClip[] SEClips;

    public enum BGM
    {
        Title,
        Main
    }
    public enum SE
    {
        Touch,
        Destroy
    }

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayBGM(BGM bgm)
    {
        audioSourceBGM.clip = BGMClips[(int)bgm];
        audioSourceBGM.Play();
    }

    public void PlaySE(SE se)
    {
        audioSourceSE.PlayOneShot(SEClips[(int)se]);
    }
}

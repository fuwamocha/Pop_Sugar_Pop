using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{

    private void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Title);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}

using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] string BGM;
    [SerializeField] private bool playBGMOnStart;

    private void Start()
    {
        if(BGM.Length > 0 && playBGMOnStart)
        {
            Debug.Log("BGM played");
            AudioManager.instance.PlayBackgroundMusic(BGM);
        }

    }
    public void SoungEvent(string name)
    {
        AudioManager.instance.PlaySFX(name, transform.position);
    }
}

using UnityEngine;

public class PlayAudio : MonoBehaviour
{

    public void SoungEvent(string name)
    {
        AudioManager.instance.PlaySFX(name, transform.position);
    }
}

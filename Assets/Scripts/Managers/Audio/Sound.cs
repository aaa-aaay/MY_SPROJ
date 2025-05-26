using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSound", menuName = "MyAudio/Sound")]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public string name;
    [Range(0f, 1f)]
    public float volume;
}

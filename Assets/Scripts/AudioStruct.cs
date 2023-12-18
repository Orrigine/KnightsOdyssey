using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStruct : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClip;
    
    public void Hurt()
    {
        GetComponent<AudioSource>().PlayOneShot(_audioClip[0]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioManagerSource;
    [SerializeField] private AudioClip backgroundAudio;
    [SerializeField] private AudioClip normalStateGhostAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioManagerSource = GetComponent<AudioSource>();
        audioManagerSource.clip = backgroundAudio;
        audioManagerSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioManagerSource.isPlaying)
        {
            audioManagerSource.clip = normalStateGhostAudio;
            audioManagerSource.loop = true;
            audioManagerSource.Play();
        }
    }
}

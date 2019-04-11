using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessAudioManager : MonoBehaviour {

    public AudioClip failureSound;
    public AudioClip successSound1;
    public AudioClip successSound2;
    public AudioClip successSound3;
    public AudioClip successSound4;

    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFailureSound()
    {
        audioSource.clip = failureSound;
        audioSource.Play();
    }

    public void PlaySuccessSound()
    {
        int randomSound = Random.Range(1, 5);
        switch (randomSound)
        {
            case 1:
                audioSource.clip = successSound1;
                break;
            case 2:
                audioSource.clip = successSound2;
                break;
            case 3:
                audioSource.clip = successSound3;
                break;
            case 4:
                audioSource.clip = successSound4;
                break;
            default:
                Debug.Log("Check SuccessManager for sound clips");
                break;
        }
        audioSource.Play();
    }
}

using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioFiles")]
    public AudioClip[] song;
    private AudioSource audioS;
    int randomSong;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        randomSong = Random.Range(0, song.Length);
        StartCoroutine(GlobalAudio());
    }

    private void Update()
    {
        if (randomSong < 1)
        {
            randomSong = song.Length -1;
        }

        if (randomSong >= song.Length)
        {
            randomSong = 0;
        }
    }

    IEnumerator GlobalAudio()
    {
        yield return new WaitForEndOfFrame();

        float songLength;

        songLength = song[randomSong].length;
        audioS.clip = song[randomSong];
        audioS.Play();

        yield return new WaitForSeconds(songLength);
        audioS.Stop();
        randomSong++;
        StartCoroutine(GlobalAudio());
    }
}

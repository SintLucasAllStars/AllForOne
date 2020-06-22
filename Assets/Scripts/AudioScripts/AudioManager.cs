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
        GetRandomSong();
    }

    private void GetRandomSong()
    {
        randomSong = Random.Range(0, song.Length);
        StartCoroutine(GlobalAudio());
    }

    private void Update()
    {
        if (randomSong < 0)
        {
            randomSong = song.Length -1;
        }

        if (randomSong > 1)
        {
            randomSong = 0;
        }
    }

    IEnumerator GlobalAudio()
    {
        float songLength;

        songLength = song[randomSong].length;
        audioS.clip = song[randomSong];
        audioS.Play();

        yield return new WaitForSeconds(songLength);
        audioS.Stop();
        GetRandomSong();
    }
}

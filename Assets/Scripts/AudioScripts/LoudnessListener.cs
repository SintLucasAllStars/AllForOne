using UnityEngine;

public class LoudnessListener : MonoBehaviour
{
    private AudioSource audios;
    private float updateStep = 0.01f;
    private int sampleDataLength = 1024;
    private float currentUpdateTime = 0f;
    private float clipLoudness;
    private float[] clipSampleData;
    private float sizeFactor = 1;
    private float minSize = 0;
    private float maxSize = 0.3f;


    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];
        audios = GameObject.FindWithTag("AudioManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if(currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;

            if (audios.clip != null) 
            { 
                audios.clip.GetData(clipSampleData, audios.timeSamples); 
            }        

            foreach (var clip in clipSampleData)
            {
                clipLoudness += Mathf.Abs(clip);
            }
            clipLoudness /= sampleDataLength;

            clipLoudness *= sizeFactor;
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
        }
    }
}

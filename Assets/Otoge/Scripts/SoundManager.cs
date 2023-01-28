using UnityEngine;

public class SoundManager
{
    private AudioSource audioSource = new AudioSource();
    private AudioClip audioClip;
    
    /// <summary>
    /// 入力適用SEを作成.
    /// </summary>
    public void CreateApplySe()
    {
        int position = 0;
        int samplerate = 44100;
        float frequency = 440;

        void OnAudioRead(float[] data)
        {
            int count = 0;
            while (count < data.Length)
            {
                data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate) * 0.1f;
                position++;
                count++;
            }
        }

        void OnAudioSetPosition(int newPosition)
        {
            position = newPosition;
        }
        
        // audioClip = AudioClip.Create("test", samplerate/100, 1, samplerate/10, true, OnAudioRead, OnAudioSetPosition);
        // audioSource = GetComponent<AudioSource>();
        // audioSource.clip = audioClip;
        //audioSource.Play();
    }
}
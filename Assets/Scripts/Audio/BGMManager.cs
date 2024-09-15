using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource DayBGM;
    public AudioSource NightBGM;

    [Header("Audio Settings")]
    public int PlayVolume = 1;

    private void Start()
    {
        PlayVolume = PlayVolume/100;
    }
    private void Update()
    {
        if (TimeCycle.Instance.TimeValue % 2 == 1)
        {
            SetBGMVolume(PlayVolume, 0);
        }
        else
        {
            SetBGMVolume(0, PlayVolume);
        }
    }

    private void SetBGMVolume(int dayVolume, int nightVolume)
    {
        DayBGM.volume = dayVolume;
        NightBGM.volume = nightVolume;
    }
}

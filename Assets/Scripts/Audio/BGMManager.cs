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
        if (TimeCycle.Instance.TimeValue % 2 == 1) //Day
        {
            SetDayVolume();
        }
        else if (TimeCycle.Instance.TimeValue % 2 == 0)//Night
        {
            SetNightVolume();
        }
    }

    private void SetDayVolume()
    {
       DayBGM.volume = PlayVolume;
       NightBGM.volume = 0;
    }
    
    private void SetNightVolume()
    {
        DayBGM.volume = 0;
        NightBGM.volume = PlayVolume;
    }
}

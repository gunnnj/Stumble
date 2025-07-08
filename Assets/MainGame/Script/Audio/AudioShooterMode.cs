using UnityEngine;

public class AudioShooterMode : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    public static AudioShooterMode Instance;
    void Awake()
    {
        Instance = this;

    }
    public void PlaySFX(AudioClips clips){
        audioSource.PlayOneShot(audioClips[(int)clips]);
    }

}
public enum AudioClips{
    LaserShoot
}

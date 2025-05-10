using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

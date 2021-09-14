using UnityEngine;

public class SoundManager : MonoBehaviour
{


    public static SoundManager Instance;


    [SerializeField]
    private AudioClip backgroundMusic;

    [SerializeField]
    private AudioClip coinCollectSoundClip;


    private AudioSource _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GetComponent<AudioSource>();
        _audioManager.clip = backgroundMusic;
        _audioManager.playOnAwake = true;
        _audioManager.loop = true;
        _audioManager.Play();
        Instance = this;


    }


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void playCoinCollectSound()
    {
        _audioManager.PlayOneShot(coinCollectSoundClip);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

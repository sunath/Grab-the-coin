using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text score_text;


    [SerializeField]
    private GameObject RetryUIPanel;
    [SerializeField]
    private GameObject WonUIPanel;


    private PlayerMovement player;
    private int _hasToCollectCoins;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<PlayerMovement>();
        _hasToCollectCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        score_text.text = _hasToCollectCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsPlayerDied)
        {
            RetryUIPanel.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.CompareTag("Coin") )
        {
            _hasToCollectCoins -= 1;
            score_text.text = _hasToCollectCoins.ToString();
            Destroy(other.gameObject);
            SoundManager sound = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
            sound.playCoinCollectSound();
            if(_hasToCollectCoins <= 0)
            {
                WonUIPanel.SetActive(true);
                player.setIsPlayerWon(true);
            }
        }
    }
}

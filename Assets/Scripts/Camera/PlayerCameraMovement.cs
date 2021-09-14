using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;



    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = player.transform.position + offset;
        
    }
}

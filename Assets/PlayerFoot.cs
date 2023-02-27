using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{

    PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( player != null)
        {
            if( collision.gameObject.CompareTag("Ground"))
                player.SetJumpState(false);
        }
        else
            player = GetComponentInParent<PlayerMove>();

    }
    private void OnCollisionExit(Collision collision)
    {
        if( player != null)
        {
            if (collision.gameObject.CompareTag("Ground"))
                player.SetJumpState(true);
        }
        else
            player = GetComponentInParent<PlayerMove>();
    }
}

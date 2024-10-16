using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{

    public Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public GameObject followCamera;
    public Transform followCameraTarget;

    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;

    public float walkSpeed;
    public float frameRate;
    float idleTime;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(followCamera != null)
        {
            followCamera.transform.SetParent(followCameraTarget.transform);
            followCamera.transform.position = followCameraTarget.transform.position;
        }

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //set walk based on direction
        rigidbody.velocity = direction * walkSpeed;

        //handle direction

        HandleSpriteFlip();

        SetSprite();
    }

    void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        {
            //holding a direction.

            float playTime = Time.time - idleTime; //time since we started walking
            int totalFrames = (int)(playTime * frameRate); //total frames since we started
            int frame = totalFrames % directionSprites.Count; //corrent frame

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            //holding nothing, input is neutral
            idleTime = Time.time;
        }
    }

    void HandleSpriteFlip()
    {
        //if we're facing right, and the player holds left, flip.
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        //if we're facing left and the player hold right, flip.
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    List<Sprite> GetSpriteDirection()
    {

        List<Sprite> selectedSprites = null;

        if(direction.y > 0)//north
        {
            if(Mathf.Abs(direction.x) > 0)//east or west
            {
                selectedSprites = neSprites;
            }
            else//neutral x
            {
                selectedSprites = nSprites;
            }
        }
        else if (direction.y < 0)//south
        {
            if (Mathf.Abs(direction.x) > 0)//east or west
            {
                selectedSprites = seSprites;
            }
            else//neutral x
            {
                selectedSprites = sSprites;
            }
        }
        else //neutrol
        {
            if (Mathf.Abs(direction.x) > 0)//east or west
            {
                selectedSprites = eSprites;
            }
        }

        return selectedSprites;
    }
}

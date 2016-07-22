using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour {

	public float speed = 3;
    public float maxDistance = 5;
    private Vector2 startLocation;
    public LayerMask blockingLayer;		//Blocking layer to the object
    
    protected BoxCollider2D bCollider;
    protected Rigidbody2D rBody;
    
    //start
    void Start(){
        bCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        startLocation = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.right * Time.deltaTime * speed;
        // Did it hit the player?
        RaycastForPlayer();
        RaycastForWall();
        // Destory the arrow after a set distance have been traveled
        if (DistanceTraveled(startLocation, transform.position) > maxDistance) {
            Debug.Log("Arrow dropped by gravity");
            Destroy(this.gameObject);
        }
    }

    void RaycastForPlayer()
    {
        //Vector2 playerDirection = new Vector2(anim.GetFloat("xInput"), anim.GetFloat("yInput"));
        //Debug.Log("dir" + playerDirection);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2( -1, 0), .3f, blockingLayer);
        if (hit.transform != null)
        {

            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<Player>().TakeDamage(2);
                Destroy(this.gameObject);
            }


            hit.transform.GetComponent<Player>().TakeDamage(2);
            Destroy(this.gameObject);
        }
    }

    void RaycastForWall()
    {
        //Vector2 playerDirection = new Vector2(anim.GetFloat("xInput"), anim.GetFloat("yInput"));
        //Debug.Log("dir" + playerDirection);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector2(-1, 0), .3f, blockingLayer);
        if (hit.transform.tag == "Wall")
        {
            Debug.Log("Arrow hit a wall");
            Destroy(this.gameObject);

        }
    }

    float DistanceTraveled(Vector2 start, Vector2 end)
    {
        return Mathf.Sqrt(Mathf.Pow(start.x - end.x, 2) + Mathf.Pow(start.y - end.y, 2));
    }


}

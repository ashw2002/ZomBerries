using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    /* This script handles the pathfinding for Anderson's patrolling enemies.  
     * He has most of this already, but the main change is the if statements inside the goToPoint() function.
     * They reference the PADetection script which simply detects if the player is inside the collider of the Patrol Area.
     * It is important to note that the patrol area collider is a trigger, which prevents actual collision.
     * 
     * Another thing to note is that these public variables can be accessed and changed from the unity editor itself which
     * is why some are not assigned.  You can find thses scripts inside the inspector tab.
     * 
     * to get the reference to the player variable, you jsut drag the player object from the Hierarchy into the slot under 
     * the pathfind script attached to the Patroller object.
     * 
     */
    public Transform[] points;
    public float speed;
    private int curPoint = 0;
    private Rigidbody2D rb;
    private Transform tr;
    public GameObject player;
    public GameObject patrolArea;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        goToPoint();
    }

    //Anderson has most of this but the ifstatment will need to be implemented. Notice the second parameter determines destination
    void goToPoint()
    {
        float step = speed * Time.deltaTime;
        rb.velocity = new Vector2(0, 0);
        if(patrolArea != null)
        {
            if (patrolArea.GetComponent<PADetection>().playerIn)
            {
                tr.position = Vector2.MoveTowards(tr.position, player.GetComponent<Transform>().position, step);
            }
            else
            {
                tr.position = Vector2.MoveTowards(tr.position, points[curPoint].position, step);
            }
        }
       
        
    }

    //This makes the patroller move from point to point. Anderson already has this.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PatrolPoint"))
        {
            curPoint += 1;
            print(curPoint);
            if(curPoint >= points.Length)
            {
                curPoint = 0;
            }
        }
    }
}

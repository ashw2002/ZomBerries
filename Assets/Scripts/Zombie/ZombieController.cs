using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int speed;
    public bool alive = true;
    private Transform tr;
    private Rigidbody2D rb;

    public Transform[] patPoints;
    public int curPoint;
    private GameObject detectRadius;
    private GameObject zombieBod;
    public GameObject player;
    // Start is called before the first frame update


    private void ZombMove(Vector3 target)
    {
        Vector3 dir = target - tr.position;
        float Angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);

        float step = speed * Time.deltaTime;

        tr.position = Vector2.MoveTowards(tr.position, target, step);
    }
    void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        detectRadius = this.gameObject.GetComponent<Transform>().GetChild(1).gameObject;
        zombieBod = this.gameObject.GetComponent<Transform>().GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            zombieBod.GetComponent<Animator>().SetInteger("AnimState", 1);
            if(detectRadius.GetComponent<PADetection>().playerIn)
            {
                ZombMove(player.GetComponent<Transform>().position);
            }
            else
            {
                ZombMove(patPoints[curPoint].position);
            }
        }
    }

    
}

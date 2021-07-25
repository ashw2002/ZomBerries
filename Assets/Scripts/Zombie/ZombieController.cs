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
    private AudioSource AD;
    public AudioClip step1;
    public AudioClip step2;
    private int StepNumber = 0;


    private void controlSteps()
    {
        if (!AD.isPlaying && StepNumber == 0)
        {
            AD.clip = step1;
            AD.volume = .5f;
            AD.Play();
            StepNumber = 1;
        }
        else if (!AD.isPlaying && StepNumber == 1)
        {
            AD.clip = step2;
            AD.volume = .5f;
            AD.Play();
            StepNumber = 0;
        }
    }

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
        AD = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            controlSteps();
            zombieBod.GetComponent<Animator>().SetInteger("AnimState", 1);
            if(detectRadius.GetComponent<PADetection>().playerIn)
            {
                ZombMove(player.GetComponent<Transform>().position);
                if(Vector3.Distance(tr.position, player.GetComponent<Transform>().position) > 6)
                {
                    detectRadius.GetComponent<PADetection>().playerIn = false;
                }
            }
            else
            {
                ZombMove(patPoints[curPoint].position);
            }
        }
        else
        {
            if (Vector3.Distance(tr.position, player.GetComponent<Transform>().position) > 15)
            {
                alive = true;
            }
        }
    }

    
}

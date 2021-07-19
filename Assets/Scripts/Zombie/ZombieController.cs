using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private bool alive = true;
    private Transform tr;
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource aud;
    private GameObject blood;
    // Start is called before the first frame update

    private void ZombMove()
    {

    }
    void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
        aud = this.gameObject.GetComponent<AudioSource>();
        blood = Resources.Load<GameObject>("Prefabs/BloodSplat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            anim.SetInteger("AnimState", 2);
            Instantiate(blood, tr.position, tr.rotation);
            aud.Play();
        }
    }
}

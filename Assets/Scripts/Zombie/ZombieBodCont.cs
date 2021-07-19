using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBodCont : MonoBehaviour
{
    private GameObject pare;
    private Animator anim;
    private AudioSource aud;
    private GameObject blood;
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        pare = this.gameObject.GetComponent<Transform>().parent.gameObject;
        anim = this.gameObject.GetComponent<Animator>();
        tr = this.gameObject.GetComponent<Transform>().parent;
        aud = this.gameObject.GetComponent<AudioSource>();
        blood = Resources.Load<GameObject>("Prefabs/BloodSplat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("bullet") && pare.GetComponent<ZombieController>().alive == true)
        {
            pare.GetComponent<ZombieController>().alive = false;
            anim.SetInteger("AnimState", 2);
            Instantiate(blood, tr.position, tr.rotation);
            aud.Play();
        }
        else if (collision.CompareTag("PatrolPoint"))
        {

            pare.GetComponent<ZombieController>().curPoint += 1;
            if (pare.GetComponent<ZombieController>().curPoint >= pare.GetComponent<ZombieController>().patPoints.Length)
            {
                pare.GetComponent<ZombieController>().curPoint = 0;
            }
        }
    }
}

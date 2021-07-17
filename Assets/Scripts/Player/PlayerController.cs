using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tran;
    public int speed;
    private GameObject gun;
    private Animator an;
    public float CoolTime;
    private float Cooldown = 0;

    private void LookAt2D(Vector3 target)
    {
        //Script Found here: https://www.youtube.com/watch?v=Geb_PnF1wOk
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var Angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tran.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
    }
    private void move()
    {   if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            an.SetInteger("AnimState", 1);
        }
        else
        {
            an.SetInteger("AnimState", 0);
        }
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        tran = this.gameObject.GetComponent<Transform>();
        gun = tran.GetChild(0).gameObject;
        an = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gun.GetComponent<Animator>().SetInteger("AnimState", 0);
        if (Input.GetButtonDown("Fire1") && Cooldown >= CoolTime)
        {
            Cooldown = 0;
            Debug.Log("Fired");
            gun.GetComponent<AudioSource>().Play();
            gun.GetComponent<Animator>().SetInteger("AnimState", 1);

        }
        else
        {
            Cooldown += Time.deltaTime;
        }
        move();
        LookAt2D(Input.mousePosition);
    }
}

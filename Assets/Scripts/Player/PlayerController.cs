using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tran;
    public int speed;
    private GameObject gun;
    private Transform barrel;
    private Animator an;
    public float CoolTime;
    private float Cooldown = 0;
    public GameObject bullet;

    private void LookAt2D(Vector3 target)
    {
        //Script Found here: https://www.youtube.com/watch?v=Geb_PnF1wOk
        Vector3 dir = target - Camera.main.WorldToScreenPoint(transform.position);
        float Angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tran.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
    }
    private void move()
    {   if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            an.SetInteger("AnimState", 1);
        }
        else
        {
            an.SetInteger("AnimState", 0);
            rb.velocity = new Vector3(0, 0,0);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        tran = this.gameObject.GetComponent<Transform>();
        gun = tran.GetChild(0).gameObject;
        barrel = gun.GetComponent<Transform>().GetChild(0);
        an = this.gameObject.GetComponent<Animator>();
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        gun.GetComponent<Animator>().SetInteger("AnimState", 0);
        if (Input.GetButtonDown("Fire1") && Cooldown >= CoolTime)
        {
            Cooldown = 0;
            gun.GetComponent<AudioSource>().Play();
            Instantiate(bullet, barrel.position, tran.rotation);
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

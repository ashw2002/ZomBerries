using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject bullet;
    private int ammo = 5;
    public Text ammoCount;
    public int MaxDolls;
    private int DollCount = 0;

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
            rb.angularVelocity = 0f;
            an.SetInteger("AnimState", 1);
        }
        else
        {
            an.SetInteger("AnimState", 0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
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
        ammoCount.text = "Ammo: " + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        gun.GetComponent<Animator>().SetInteger("AnimState", 0);
        if (Input.GetButtonDown("Fire1") && Cooldown >= CoolTime && ammo > 0)
        {
            ammo -= 1;
            Cooldown = 0;
            gun.GetComponent<AudioSource>().Play();
            Instantiate(bullet, barrel.position, tran.rotation);
            gun.GetComponent<Animator>().SetInteger("AnimState", 1);
            ammoCount.text = "Ammo: " + ammo;
        }
        else
        {
            Cooldown += Time.deltaTime;
        }
        move();
        LookAt2D(Input.mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ammo"))
        {
            ammo += 1;
            Destroy(other.gameObject);
            ammoCount.text = "Ammo: " + ammo;
        }else if (other.CompareTag("Doll")){
            DollCount += 1;
            Destroy(other.gameObject);
        }
    }
}

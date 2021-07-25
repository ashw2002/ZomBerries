using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tran;
    public float speed;
    private GameObject gun;
    private Transform barrel;
    private Animator an;
    public float CoolTime;
    private float Cooldown = 0;
    private GameObject bullet;
    private int ammo = 2;
    public Text ammoCount;
    public Text message;
    public int MaxDolls;
    private int DollCount = 0;
    private float textHideTime;
    public GameObject lava;
    private AudioSource AD;
    public Image redFadeout;
    private int injury = 0;
    private float IframeTime = 1f;
    private float IframeCount = 0f;
    private bool bleedOut = false;
    private AudioSource HeartBeat;
    private bool deathPlayed = false;
    public AudioClip step1;
    public AudioClip step2;
    private int StepNumber = 0;
    public AudioClip[] Swells;

    private void controlSteps()
    {
        if (!gun.GetComponent<AudioSource>().isPlaying && StepNumber == 0)
        {
            gun.GetComponent<AudioSource>().clip = step1;
            gun.GetComponent<AudioSource>().volume = .5f;
            gun.GetComponent<AudioSource>().Play();
            StepNumber = 1;
        }else if (!gun.GetComponent<AudioSource>().isPlaying && StepNumber == 1)
        {
            gun.GetComponent<AudioSource>().clip = step2;
            gun.GetComponent<AudioSource>().volume = .5f;
            gun.GetComponent<AudioSource>().Play();
            StepNumber = 0;
        }
    }

    private void fadeOut(string Scene)
    {
        Color tempcolor = redFadeout.color;
        tempcolor.a += .2f * Time.deltaTime;
        redFadeout.color = tempcolor;
        if (redFadeout.color.a >= 1f)
        {
            SceneManager.LoadScene(Scene);
        }
    }
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
            controlSteps();
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
        AD = this.GetComponent<AudioSource>();
        HeartBeat = this.gameObject.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(injury == 2)
        {
            if (!deathPlayed)
            {
                AD.clip = Resources.Load<AudioClip>("SFX/Death");
                AD.volume = .5f;
                AD.Play();
                deathPlayed = true;
            }
            fadeOut("GameLoseScreen");
        }
        IframeCount += Time.deltaTime;
        if(injury == 1)
        {
            if (!HeartBeat.isPlaying)
            {
                HeartBeat.Play();
            }
            Color TempFade = redFadeout.color;
            Debug.Log(TempFade.a);
            if (bleedOut)
            {
                TempFade.a -= Time.deltaTime * 0.1f;
            }
            else
            {
                TempFade.a += Time.deltaTime * 0.1f;
            }
            if (TempFade.a >= .3f)
            {
                bleedOut = true;
            }
            else if(TempFade.a <= .1f)
            {
                bleedOut = false;
            }
            redFadeout.color = TempFade;
        }

        if(injury < 2)
        {
            gun.GetComponent<Animator>().SetInteger("AnimState", 0);
            if (Input.GetButtonDown("Fire1") && Cooldown >= CoolTime && ammo > 0)
            {
                ammo -= 1;
                Cooldown = 0;
                gun.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("SFX/Shotgun3");
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

            if (textHideTime > 2 && DollCount < MaxDolls)
            {
                message.text = "";
            }
            else if(textHideTime > 2 && DollCount >= MaxDolls)
            {
                message.text = "You have collected all that you need.  Return to the volcano and complete the ritual";
            }
            else
            {
                textHideTime += Time.deltaTime;
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ammo"))
        {
            ammo += 1;
            AD.clip = Resources.Load<AudioClip>("SFX/AmmoPickup");
            AD.volume = .75f;
            AD.Play();
            Destroy(other.gameObject);
            ammoCount.text = "Ammo: " + ammo;
            message.text = "Picked Up 1 Bullet";
            textHideTime = 0;
        }else if (other.CompareTag("Doll")){
            textHideTime = 0;
            DollCount += 1;
            Destroy(other.gameObject);

            AD.clip = Swells[Random.Range(0,4)];
            AD.Play();
            injury = 0;
            Color tempColor = redFadeout.color;
            tempColor.a = 0;
            redFadeout.color = tempColor;
            HeartBeat.Pause();
            if(DollCount >= MaxDolls)
            {
                message.text = "You have collected all that you need.  Return to the volcano and complete the ritual";
                lava.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
                message.text = "Picked up a doll. " + (MaxDolls - DollCount) + " left to collect";
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            fadeOut("GameWinScreen");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Transform>().parent.gameObject.CompareTag("zombie") && IframeCount >= IframeTime && collision.gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<ZombieController>().alive)
        {
            if (injury == 1)
            {
                an.SetInteger("AnimState", 2);
                
                injury = 2;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                gun.SetActive(false);

            }
            else if (injury == 0)
            {
                injury = 1;
                AD.clip = Resources.Load<AudioClip>("SFX/Hit_Hurt");
                AD.Play();
                IframeCount = 0;
            }
            Debug.Log(injury); 
        }
    }
}

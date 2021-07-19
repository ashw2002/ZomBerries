using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    public int speed;
    private int timeToDelete = 3;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        tr = this.gameObject.GetComponent<Transform>();
        float xvel = speed * Mathf.Cos(Mathf.Deg2Rad * tr.rotation.eulerAngles.z);
        float yvel = speed * Mathf.Sin(Mathf.Deg2Rad * tr.rotation.eulerAngles.z);
        rb.velocity = new Vector2(xvel, yvel);
    }
    void Update()
    {
        if(timer >= timeToDelete)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}

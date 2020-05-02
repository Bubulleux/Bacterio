using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float range;
    public int degat;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime);
        range -= Time.deltaTime;
        if (range <= 0f)
        {
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            target.GetComponent<BacteriMotor>().Degat(degat);
            Destroy(gameObject);
        }
    }

}

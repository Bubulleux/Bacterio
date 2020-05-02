using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    public int point;
    public Text pointCounter;
    void Update()
    {
        pointCounter.text = "Point : " + point;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            Destroy(other.gameObject);
            point++;
        }
    }

    private void FixedUpdate()
    {
        Vector3 _vel = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        transform.position += _vel * Time.fixedDeltaTime * speed;
    }

    
}

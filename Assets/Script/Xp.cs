using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xp : MonoBehaviour
{
    public int xpCount;
    public float size;
    public Transform gfx;
    [SerializeField]
    private Transform target;
    void Start()
    {
        gfx.localScale = Vector3.one * 0.4f * size;
        gfx.localPosition = new Vector3(0f, 0.4f * size, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist > 5f)
            {
                target = null;
                gfx.localScale = Vector3.one * 0.4f * size;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                transform.localScale = Vector3.one;
                gameObject.tag = "Food";
                return;
            }
            if (dist < 0.4f)
            {
                target.gameObject.GetComponent<BacteriMotor>().TakeXP(xpCount);
                Destroy(gameObject);
            }
            Vector3 _vel = (target.position - transform.position).normalized * Time.deltaTime * 10f;
            transform.Translate(_vel);
            transform.localScale = Vector3.one * dist / 3f;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bacterie" && target == null)
        {
            target = other.transform;
            gameObject.tag = "Untagged";
        }
    }
}

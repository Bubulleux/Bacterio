using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public Transform gfx;
    [SerializeField]
    private Transform target;
    void Start()
    {

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
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                transform.localScale = Vector3.one;
                gameObject.tag = "Heal";
                return;
            }
            if (dist < 0.4f)
            {
                target.GetComponent<BacteriMotor>().heal += 20;
                target.GetComponent<BacteriMotor>().heal = target.GetComponent<BacteriMotor>().heal > 100 ? 100 : target.GetComponent<BacteriMotor>().heal;
                Destroy(gameObject);
            }
            Vector3 _vel = (target.position - transform.position).normalized * Time.deltaTime * 10f;
            transform.Translate(_vel);
            transform.localScale = Vector3.one * dist / 3f;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bacterie" && target == null && other.gameObject.GetComponent<BacteriMotor>().heal < 100)
        {
            target = other.transform;
            gameObject.tag = "Untagged";
            Debug.Log("take");
        }
    }
}

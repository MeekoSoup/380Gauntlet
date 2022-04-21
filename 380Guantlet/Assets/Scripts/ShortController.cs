using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortController : MonoBehaviour
{
    public int health;
    private BaseEnemy _enemy;

    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
            transform.Translate(Vector3.right * Time.deltaTime * 10);

        if (Input.GetKey("a"))
            transform.Translate(Vector3.left * Time.deltaTime * 10);

        if (Input.GetKey("w"))
            transform.Translate(Vector3.forward * Time.deltaTime * 10);

        if (Input.GetKey("s"))
            transform.Translate(Vector3.back * Time.deltaTime * 10);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
            Destroy(collision.gameObject);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Take Damage"))
        {
            health -=
        }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    float speed = 40.0f;
    float timeOfLife = 1.0f;
    float damage = 1.0f;
    public DestroyableObject owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoForward();
        StartCoroutine(Dissapear());
    }

    void GoForward()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(timeOfLife);
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner.tag != collision.tag)
        {
            DestroyableObject destroyObj = collision.gameObject.GetComponent<DestroyableObject>();
            if (destroyObj)
            {
                destroyObj.LoseHealth(damage);
            }
            Destroy(this.gameObject);
        }
    }
}

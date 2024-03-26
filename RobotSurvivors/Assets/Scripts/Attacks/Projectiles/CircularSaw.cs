using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [SerializeField]float radius = 5.0f;

    public float counter = 0.0f;
    float speed = 5.0f;
    float rotationSpeed = 1000.0f;
    public float damage = 1.0f;
    public float timeOfLife = 10.0f;
    [SerializeField] public DestroyableObject owner;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dissapear());
    }

    // Update is called once per frame
    void Update()
    {
        counter = counter + speed * Time.deltaTime;
        if (owner != null)
        {
            Vector3 newPos = owner.transform.position;
            newPos.x = newPos.x + Mathf.Cos(counter) * radius;
            newPos.y = newPos.y + Mathf.Sin(counter) * radius;
            transform.position = newPos;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
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
        }
    }
}

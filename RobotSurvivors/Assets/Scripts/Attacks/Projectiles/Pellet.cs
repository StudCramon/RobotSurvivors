using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float speed = 40.0f;
    public float timeOfLife = 1.0f;
    public float damage = 1.0f;
    public DestroyableObject owner;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound(SoundNames.LASERSHOT);
        StartCoroutine(Dissapear());
    }

    // Update is called once per frame
    void Update()
    {
        GoForward();
    }

    void GoForward()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    IEnumerator Dissapear()
    {
        Debug.Log("CoroutineStarted!");
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

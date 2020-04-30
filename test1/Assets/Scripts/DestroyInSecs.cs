using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSecs : MonoBehaviour
{
    [SerializeField] float destroy = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Answers")
        {
            Destroy(gameObject);
        }
    }
}

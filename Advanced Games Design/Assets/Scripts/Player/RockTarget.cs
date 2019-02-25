using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTarget : MonoBehaviour
{
    public Material enemiesDefaultMaterial;
    [SerializeField] Material silhoutteMaterial;

    Renderer renderer;

    SphereCollider sphere;
    Collider newCollider;

    private bool trigger;

    // Start is called before the first frame update
    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (trigger && sphere != null)
        {
            if (newCollider.gameObject.tag == "Enemy")
            {
                var enemyBody = newCollider.transform.Find("Body_low");

                Debug.Log("Body found");

                renderer = enemyBody.gameObject.GetComponent<Renderer>();
                renderer.sharedMaterial = silhoutteMaterial;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        newCollider = other;
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        newCollider = null;
        trigger = false;

        if (other.gameObject.tag == "Enemy")
        {

            Debug.Log("I am an enemy");

            var enemyBody = other.transform.Find("Body_low");
            renderer = enemyBody.gameObject.GetComponent<Renderer>();
            renderer.sharedMaterial = enemiesDefaultMaterial;
        }
    }
}

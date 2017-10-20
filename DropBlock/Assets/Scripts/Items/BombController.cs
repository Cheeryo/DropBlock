using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Programmiert von Maximilian Schöberl
public class BombController : MonoBehaviour {

    [SerializeField] private GameObject explosionPrefab;
    private float delay;
    private float radius;
    private bool triggered = false;

    public void Init(float delay, float radius)
    {
        this.delay = delay;
        this.radius = radius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!triggered)
        {
            Invoke("Explode", delay);
            triggered = true;
        }
    }

    private void Explode()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, radius, Vector3.forward);
        RaycastHit[] blocks = hit.Where(o => o.collider.CompareTag("Block")).ToArray();
        RaycastHit[] players = hit.Where(o => o.collider.CompareTag("Player")).ToArray();

        List<GameObject> destroy = new List<GameObject>();

        GameObject obj = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        obj.transform.localScale = new Vector3(radius,radius,radius);
        foreach (RaycastHit h in blocks)
        {
            if (!destroy.Contains(h.collider.gameObject))
                destroy.Add(h.collider.gameObject);
        }

        foreach (RaycastHit h in players)
        {
            h.collider.GetComponent<PlayerController>().currentEnergy -= 50;
        }

        foreach (GameObject g in destroy)
            Destroy(g);

        Destroy(obj, 5f);
        Destroy(this.gameObject);
    }
}

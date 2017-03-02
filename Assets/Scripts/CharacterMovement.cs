using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour {

    public Transform target;
    public NavMeshAgent nav; 
    
    void Awake()
    {
        target = GameManager.instance.target.transform;
        nav = GetComponent<NavMeshAgent>();
    }
    
	
	void Update ()
    {
        if (transform.position != target.transform.position)
            nav.SetDestination(target.position);
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "target")
            Destroy(gameObject);
    }
}

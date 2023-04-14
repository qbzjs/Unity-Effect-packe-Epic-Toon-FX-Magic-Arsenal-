using UnityEngine;
using System.Collections;
public class Ragdoll : MonoBehaviour
{
    Animator _anim;
    Collider[] _cols;
    Rigidbody[] _rb;

    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _cols = GetComponentsInChildren<Collider>();
        _rb = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
    }
    void ToggleColliderState(bool state)
    {
        for (int i = 0; i < _cols.Length; i++)
        {
            _cols[i].enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }

    void ToggleRigidbodyState(bool state)
    {
        for (int i = 0; i < _rb.Length; i++)
        {
            _rb[i].isKinematic = state;
        }
        //GetComponent<Rigidbody>().isKinematic = !state;
    }

    internal void ToggleRagdoll(bool state)
    {
        ToggleColliderState(state);
        ToggleRigidbodyState(!state);
        _anim.enabled = !state;
    }
}

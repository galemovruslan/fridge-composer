using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToRepository : MonoBehaviour
{
    [SerializeField] private RuntimeRepository _repository;

    private void OnEnable()
    {
        _repository.AddObject(this.gameObject);
    }

    private void OnDisable()
    {
        _repository.RemoveObject(this.gameObject);
    }
}

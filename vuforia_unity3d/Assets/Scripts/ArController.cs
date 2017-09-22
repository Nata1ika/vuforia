using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArController : MonoBehaviour
{

    public void SetActive(bool show)
    {
        for (int i = 0; i < _obj.Length; i++)
        {
            _obj[i].SetActive(show);
        }
    }

    [SerializeField] GameObject[]   _obj;
}

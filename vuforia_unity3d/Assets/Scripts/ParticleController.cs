using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public void ChangeShow(bool show)
    {
        for (int i = 0; i < _show.Length; i++)
        {
            _show[i].SetActive(show);
        }
        for (int i = 0; i < _hide.Length; i++)
        {
            _hide[i].SetActive(! show);
        }
    }

    [SerializeField] GameObject[]	_show;
    [SerializeField] GameObject[]	_hide;
}

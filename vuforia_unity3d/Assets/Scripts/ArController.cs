using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArController : MonoBehaviour
{
    void Start()
    {
        _targetList = new List<string>();
        for (int i = 0; i < _target.Length; i++)
        {
            _target[i].TrackingFoundEvent += TrackingFound;
            _target[i].TrackingLostEvent += TrackingLost;
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < _target.Length; i++)
        {
            _target[i].TrackingFoundEvent -= TrackingFound;
            _target[i].TrackingLostEvent -= TrackingLost;
        }
    }

    void TrackingFound(string name)
    {
        if (! _targetList.Contains(name))
        {
            _targetList.Add(name);
        }

        if (_targetList.Count > 0 && ! _isActive)
        {
            SetActive(true);
            _isActive = true;
        }
    }

    void TrackingLost(string name)
    {
        if (_targetList.Contains(name))
        {
            _targetList.Remove(name);
        }

        if (_targetList.Count == 0 && _isActive)
        {
            SetActive(false);
            _isActive = false;
        }
    }

    public void SetActive(bool show)
    {
        for (int i = 0; i < _obj.Length; i++)
        {
            _obj[i].SetActive(show);
        }
    }

    [SerializeField] GameObject[]                               _obj;
    [SerializeField] Vuforia.DefaultTrackableEventHandler[]     _target;

    List<string>    _targetList;
    bool            _isActive = false;
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    public System.Action<string> ClickEvent;

    Vector3 mousePosition
    {
        get
        {
            #if ! UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if (Input.touchCount == 1)
            {
            Touch touch = Input.GetTouch(0);
            return new Vector3(touch.position.x, touch.position.y, 0);
            }
            else
            {
            return Vector3.zero;
            }
            #else
            return Input.mousePosition;         
            #endif
        }
    }

    bool IsPointerOverUIObject()
    {
        #if ! UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[i].fingerId))
            {
                return true;
            }
        }
        return false;
        #else
        return EventSystem.current.IsPointerOverGameObject();
        #endif
    }

    void Update ()
    {
        #if ! UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        if (IsPointerOverUIObject() || Input.touchCount > 1)
        {
        _timeStart = 0f;
        }
        else if (Input.touchCount == 1)
        {
        _timeStart += Time.deltaTime;
        _mousePosition = mousePosition;
        }
        else if (Input.touchCount == 0 && _timeStart > 0 && _timeStart < _timeMaxClick)
        {
        Click(_mousePosition);
        _timeStart = 0f;
        }
        else
        {
        _timeStart = 0f;
        }
        #else
        if (IsPointerOverUIObject() || Input.GetMouseButton(1))
        {
            _timeStart = 0f;
        }
        else if (Input.GetMouseButton(0))
        {
            _timeStart += Time.deltaTime;
        }
        else if (_timeStart > 0 && _timeStart < _timeMaxClick)
        {
            Click(mousePosition);
            _timeStart = 0f;
        }
        else
        {
            _timeStart = 0f;
        }
        #endif        
    }

    void Click(Vector3 mouse)
    {
        Ray ray = _camera.ScreenPointToRay(mouse);
        RaycastHit[] hit = Physics.RaycastAll(ray);

        if (hit != null && hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                bool hitIsOk = true;

                if (_ignoreString != null && _ignoreString.Length > 0)
                {
                    for (int j = 0; j < _ignoreString.Length; j++)
                    {
                        if (hit[i].collider.name.Contains(_ignoreString[j]))
                        {
                            hitIsOk = false;
                            break;
                        }
                    }
                }

                if (hitIsOk && _onlyString.Length > 0)
                {
                    hitIsOk = false;
                    for (int  j = 0; j < _onlyString.Length; j++)
                    {
                        if (hit[i].collider.name.Contains(_onlyString[j]))
                        {
                            hitIsOk = true;
                            break;
                        }
                    }
                }

                if (hitIsOk && ClickEvent != null)
                {
                    ClickEvent(hit[i].collider.gameObject.name);
                    return;
                }
            }
        }
    }

    [SerializeField] Camera         _camera;
    [SerializeField] string[]       _ignoreString;
    [SerializeField] string[]       _onlyString;

    float       _timeStart = 0f;
    const float _timeMaxClick = 0.4f;

    #if ! UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
    Vector3     _mousePosition;
    #endif

}
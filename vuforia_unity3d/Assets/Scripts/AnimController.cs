using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{

	void Start()
    {
        _clickController.ClickEvent += Click;
	}

    void OnDestroy()
    {
        _clickController.ClickEvent -= Click;
    }

    void Click(string name)
    {
        if (name == _colliderObjName)
        {
            _anim.Play(_animName);
            _audio.Play();
        }
    }

	
    [SerializeField] ClickController    _clickController;
    [SerializeField] string             _colliderObjName;
    [SerializeField] Animator           _anim;
    [SerializeField] string             _animName;
    [SerializeField] AudioSource        _audio;
}

﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GenericButton : MonoBehaviour
{

    [SerializeField]
    private Button _uiButton;

    [SerializeField]
	private Key _inputButton;

    [SerializeField]
    private UnityEvent _onClick;
    public UnityEvent OnClick
    {
        get { return _onClick; }
    }

    private void OnEnable()
	{
		if(_uiButton == null) return;
        _uiButton.onClick.AddListener(TriggerGenericButtonClick);
	    _uiButton.interactable = true;
    }

    private void OnDisable()
	{        
		if(_uiButton == null) return;
		_uiButton.interactable = false;
        _uiButton.onClick.RemoveListener(TriggerGenericButtonClick);
    }

    private void Update()
    {
        if (Keyboard.current[_inputButton].wasPressedThisFrame)
        {
            TriggerGenericButtonClick();
        }
    }

    public void TriggerGenericButtonClick()
	{
		Debug.Log("[GenericButton] " + gameObject.name + " triggered.");
        _onClick.Invoke();
    }

}

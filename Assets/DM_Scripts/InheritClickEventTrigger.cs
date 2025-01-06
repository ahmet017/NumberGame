/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InheritClickEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IPointerClickHandler, IEventSystemHandler
{
	private void Start()
	{
		this.curEvent = EventSystem.current;
	}

	private void Update()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.onPointDown.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this.onPointUp.Invoke();
	}

	public void OnSelect(BaseEventData eventData)
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		this.onClick.Invoke();
	}

	public UnityEvent onPointDown;

	public UnityEvent onPointUp;

	public UnityEvent onClick;

	private EventSystem curEvent;
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class testscript : MonoBehaviour {

	public Button toogleBtn;
	public Text toggleBtnTxt;
	public Image led;
	public Text diagnoticTxt;

	SerialPort portToMsg;
	bool toggleState;

	void OnEnable()
	{
		toggleState = false;
		toggleBtnTxt.text = "LED Switch";
		diagnoticTxt.text = "Debug Info...";
	}

	// Use this for initialization
	void Start ()
	{
		string[] portNames;
		portNames = System.IO.Ports.SerialPort.GetPortNames();
		string portNameExtracted = portNames[0].ToString();
		diagnoticTxt.text = portNameExtracted;

		//portToMsg = new SerialPort ("/dev/ttyUSB0", 9600, Parity.None, 8, StopBits.One);
		//portToMsg = new SerialPort ("/dev/bus/usb/003/002", 9600, Parity.None, 8, StopBits.One);
		portToMsg = new SerialPort (portNameExtracted, 9600, Parity.None, 8, StopBits.One);

		portToMsg.Open ();
		portToMsg.Write ("0");

		led.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (toggleState)
		{
			portToMsg.WriteLine("1");
		}
		else
		{
			portToMsg.WriteLine("0");
		}
	}

	void OnDisable ()
	{
		// --- Clean-up & degrade ---
		portToMsg.Write ("0");
		portToMsg.Close();
	}

	// --------------
	// Public Methods
	// --------------

	public void OnClick()
	{
		if (toggleState)
		{
			toggleState = false;
			toggleBtnTxt.text = "Off";
			led.color = Color.clear;
		}
		else
		{
			toggleState = true;
			toggleBtnTxt.text = "On";
			led.color = Color.red;
		}
	}
}

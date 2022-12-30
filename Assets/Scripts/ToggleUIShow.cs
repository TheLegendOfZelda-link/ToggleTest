using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Default
{
	public class ToggleUIShow : MonoBehaviour
	{
		public ToggleComp toggleComp;

        private Text showText;

        private void Awake()
        {
            toggleComp.OnToggleStateChanged.AddListener(UIShowStateChanged);
            toggleComp.OnToggleInputStateChanged.AddListener(UIShowToggleInputChanged);

            showText = GetComponent<Text>();
        }


        private void UIShowStateChanged(int curStateCount)
        {
            showText.text = $"CurState:\t {curStateCount}";
        }


        private void UIShowToggleInputChanged(ToggleComp.ToggleInputState inputstate)
        {
            showText.text = showText.text + $"\nInputState:\t{inputstate}";
        }
    }
}
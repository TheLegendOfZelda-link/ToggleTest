using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Default
{
	public class ToggleUIShow : MonoBehaviour
	{
		public ToggleComp toggleComp;

        private TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            toggleComp.OnToggleStateChanged.AddListener(UIShowStateChanged);

            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void UIShowStateChanged(int curStateCount)
        {
            textMeshProUGUI.text = $"CurState + {curStateCount}";
        }
    }
}
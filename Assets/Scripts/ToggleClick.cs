using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Default
{
	public class ToggleClick : MonoBehaviour
	{
        public UnityEvent OnToggleStartClicked;
        public UnityEvent OnToggleFinishClicked;
        public UnityEvent OnToggleEndClicked;

        public float clickedFinishTime = 1;

        private bool isClicked = false;

        private float clickedTime = 0;

        private void OnMouseDown()
        {
            //Debug.Log("Toggle Clicked");
            OnToggleStartClicked?.Invoke();

            isClicked = true;

            StartCoroutine(FinishClicked());
        }

        private void OnMouseUp()
        {
            isClicked= false;

            clickedTime = 0;

            OnToggleEndClicked?.Invoke();
        }

        private IEnumerator FinishClicked()
        {
            yield return new WaitForSeconds(1.0f);

            if (MathF.Abs(clickedFinishTime - clickedTime) < 0.1f)
            {
                Debug.Log("Toggle Finished");
                OnToggleFinishClicked?.Invoke();
            }
        }

        private void Update()
        {
            if (isClicked)
            {
                clickedTime += Time.deltaTime;
            }
        }
    }
}
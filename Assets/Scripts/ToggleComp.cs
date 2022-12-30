using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Default
{
    public enum ToggleType
    {
        AutoReturn,         //自动回弹
        ManualReturn,      //手动触发回弹
        TimerReturn        //计时器触发回弹
    }

    public class ToggleComp : MonoBehaviour
    {
        public ToggleType toggleType;

        //开关的点击事件
        [HideInInspector]
        public UnityEvent OnToggleClickDown;
        [HideInInspector]
        public UnityEvent OnToggleClickFinish;
        [HideInInspector]
        public UnityEvent OnToggleClickUp;

        //开关当前状态相关
        public int toggleStateNum = 1;
        private int curStateNum = 0;
        public int CurStateNum
        {
            get { return curStateNum; }
            set
            {
                if (curStateNum != value)
                {
                    curStateNum = value;
                    OnToggleStateChanged?.Invoke(curStateNum);
                }
            }
        }

        //开关的状态事件
        [HideInInspector]
        public UnityEvent<int> OnToggleStateChanged;

        //计时器相关
        public float timerLength = 1;
        private float timerTime = 0;

        private ToggleClick m_ToggleClick;

        private void Awake()
        {
            m_ToggleClick = GetComponent<ToggleClick>();

            OnToggleClickDown.AddListener(ToggleClickDown);
            OnToggleClickFinish.AddListener(ToggleClickFinish);
            OnToggleClickUp.AddListener(ToggleClickUp);

            OnToggleStateChanged.AddListener(ToggleStateChanged);

            m_ToggleClick.OnToggleStartClicked = OnToggleClickDown;
            m_ToggleClick.OnToggleFinishClicked = OnToggleClickFinish;
            m_ToggleClick.OnToggleEndClicked = OnToggleClickUp;
        }

        private void Update()
        {
            if (toggleType == ToggleType.TimerReturn && CurStateNum != 0)
            {
                if (timerTime < timerLength)
                {
                    timerTime += Time.deltaTime;
                }
                else
                {
                    timerTime = 0;
                    CurStateNum = 0;
                }
            }
        }

        private void ToggleClickDown()
        {
            switch (toggleType)
            {
                case ToggleType.AutoReturn:
                    break;
                case ToggleType.ManualReturn:
                    break;
                case ToggleType.TimerReturn:
                    break;
                default:
                    break;
            }
        }

        private void ToggleClickFinish()
        {
            CurStateNum = CurStateNum < toggleStateNum ? CurStateNum += 1 : 0;
            Debug.Log("curStateNum:\t" + CurStateNum);
        }

        private void ToggleClickUp()
        {
            switch (toggleType)
            {
                case ToggleType.AutoReturn:
                    CurStateNum = 0;
                    break;
                case ToggleType.ManualReturn:
                    break;
                case ToggleType.TimerReturn:
                    break;
                default:
                    break;
            }
        }


        private void ToggleStateChanged(int stateNum)
        {

        }
    }
}
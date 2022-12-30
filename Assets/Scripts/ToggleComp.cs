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
        public enum ToggleInputState
        {
            Idle,               //未操作时（目前无法判断回复为Idle的条件）！！
            Down,             //操作按下时
            Up,                 //操作松手时
        }

        public ToggleType toggleType;

        private ToggleInputState inputState;

        public ToggleInputState InputState
        {
            get { return inputState; }
            set
            {
                if (inputState != value)
                {
                    inputState = value;
                    OnToggleInputStateChanged?.Invoke(inputState);
                }
            }
        }

        //开关的点击事件
        [HideInInspector]
        public UnityEvent OnToggleClickDown;
        [HideInInspector]
        public UnityEvent OnToggleClickFinish;
        [HideInInspector]
        public UnityEvent OnToggleClickUp;

        //开关当前状态相关
        public int toggleStateNum = 1;
        private int curToggleStateNum = 0;
        public int CurToggleStateNum
        {
            get { return curToggleStateNum; }
            set
            {
                if (curToggleStateNum != value)
                {
                    curToggleStateNum = value;
                    OnToggleStateChanged?.Invoke(curToggleStateNum);
                }
            }
        }

        //开关输入的状态时间
        [HideInInspector]
        public UnityEvent<ToggleInputState> OnToggleInputStateChanged;

        //开关的状态事件
        [HideInInspector]
        public UnityEvent<int> OnToggleStateChanged;

        //计时器相关
        public float timerLength = 1;
        private float timerTime = 0;

        private ToggleClick m_ToggleClick;

        private Animator m_ToggleAnimator;

        private void Awake()
        {
            m_ToggleClick = GetComponent<ToggleClick>();
            m_ToggleAnimator = GetComponent<Animator>();

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
            if (toggleType == ToggleType.TimerReturn && CurToggleStateNum != 0 && inputState == ToggleInputState.Up)
            {
                if (timerTime < timerLength)
                {
                    timerTime += Time.deltaTime;
                }
                else
                {
                    timerTime = 0;
                    CurToggleStateNum = 0;
                }
            }
        }

        private void ToggleClickDown()
        {
            InputState = ToggleInputState.Down;
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
            m_ToggleAnimator.SetBool("IsClicked", true);
        }

        private void ToggleClickFinish()
        {
            CurToggleStateNum = CurToggleStateNum < toggleStateNum ? CurToggleStateNum += 1 : 0;
            Debug.Log("curStateNum:\t" + CurToggleStateNum);
        }

        private void ToggleClickUp()
        {
            InputState = ToggleInputState.Up;
            switch (toggleType)
            {
                case ToggleType.AutoReturn:
                    CurToggleStateNum = 0;
                    break;
                case ToggleType.ManualReturn:
                    break;
                case ToggleType.TimerReturn:
                    break;
                default:
                    break;
            }
            m_ToggleAnimator.SetBool("IsClicked", false);
        }


        private void ToggleStateChanged(int stateNum)
        {

        }
    }
}
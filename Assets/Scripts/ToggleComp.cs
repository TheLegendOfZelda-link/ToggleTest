using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Default
{
    public enum ToggleType
    {
        AutoReturn,         //�Զ��ص�
        ManualReturn,      //�ֶ������ص�
        TimerReturn        //��ʱ�������ص�
    }

    public class ToggleComp : MonoBehaviour
    {
        public enum ToggleInputState
        {
            Idle,               //δ����ʱ��Ŀǰ�޷��жϻظ�ΪIdle������������
            Down,             //��������ʱ
            Up,                 //��������ʱ
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

        //���صĵ���¼�
        [HideInInspector]
        public UnityEvent OnToggleClickDown;
        [HideInInspector]
        public UnityEvent OnToggleClickFinish;
        [HideInInspector]
        public UnityEvent OnToggleClickUp;

        //���ص�ǰ״̬���
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

        //���������״̬ʱ��
        [HideInInspector]
        public UnityEvent<ToggleInputState> OnToggleInputStateChanged;

        //���ص�״̬�¼�
        [HideInInspector]
        public UnityEvent<int> OnToggleStateChanged;

        //��ʱ�����
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ObhodZonPVO
{
    static class EventsHelper
    {
        public delegate void KeyDown(KeyEventArgs e);
        public static event KeyDown EventKeyDown;
        public static void OnEventKeyDown(KeyEventArgs e)
        {
            EventKeyDown.Invoke(e);
        }

        public delegate void SetPositionAgent(State st, bool animation);
        public static event SetPositionAgent EventSetPositionAgent;
        public static void OnEventSetPositionAgent(State st, bool animation)
        {
            EventSetPositionAgent.Invoke(st, animation);
        }

        public delegate void SetContentBord(int number, string str);
        public static event SetContentBord EventSetContentBord;
        public static void OnEventSetContentBord(int number, string str)
        {
            EventSetContentBord.Invoke(number, str);
        }

        public delegate void SetTextReward(string str);
        public static event SetTextReward EventSetTextReward;
        public static void OnEventSetTextReward(string str)
        {
            EventSetTextReward.Invoke(str);
        }

        public delegate void SetTextRewardLeft(string str);
        public static event SetTextRewardLeft EventSetTextRewardLeft;
        public static void OnEventSetTextRewardLeft(string str)
        {
            EventSetTextRewardLeft.Invoke(str);
        }

        public delegate void SetTextRewardLeftUp(string str);
        public static event SetTextRewardLeftUp EventSetTextRewardLeftUp;
        public static void OnEventSetTextRewardLeftUp(string str)
        {
            EventSetTextRewardLeftUp.Invoke(str);
        }

        public delegate void SetTextRewardLeftDown(string str);
        public static event SetTextRewardLeftDown EventSetTextRewardLeftDown;
        public static void OnEventSetTextRewardLeftDown(string str)
        {
            EventSetTextRewardLeftDown.Invoke(str);
        }

        public delegate void SetTextRewardUp(string str);
        public static event SetTextRewardUp EventSetTextRewardUp;
        public static void OnEventSetTextRewardUp(string str)
        {
            EventSetTextRewardUp.Invoke(str);
        }

        public delegate void SetTextRewardDown(string str);
        public static event SetTextRewardDown EventSetTextRewardDown;
        public static void OnEventSetTextRewardDown(string str)
        {
            EventSetTextRewardDown.Invoke(str);
        }

        public delegate void SetTextRewardRight(string str);
        public static event SetTextRewardRight EventSetTextRewardRight;
        public static void OnEventSetTextRewardRight(string str)
        {
            EventSetTextRewardRight.Invoke(str);
        }

        public delegate void SetTextRewardRightUp(string str);
        public static event SetTextRewardRightUp EventSetTextRewardRightUp;
        public static void OnEventSetTextRewardRightUp(string str)
        {
            EventSetTextRewardRightUp.Invoke(str);
        }

        public delegate void SetTextRewardRightDown(string str);
        public static event SetTextRewardRightDown EventSetTextRewardRightDown;
        public static void OnEventSetTextRewardRightDown(string str)
        {
            EventSetTextRewardRightDown.Invoke(str);
        }

        public delegate Alg GetAlgoritm();
        public static event GetAlgoritm EventGetAlgoritm;
        public static Alg OnEventGetAlgoritm()
        {
            return EventGetAlgoritm.Invoke();
        }

        public delegate void StartPlay();
        public static event StartPlay EventStartPlay;
        public static void OnEventStartPlay()
        {
            EventStartPlay.Invoke();
        }

        public delegate void WindowClosing();
        public static event WindowClosing EventWindowClosing;
        public static void OnEventWindowClosing()
        {
            EventWindowClosing.Invoke();
        }

        public delegate State FindState(State st);
        public static event FindState EventFindState;
        public static State OnEventFindState(State st)
        {
            return EventFindState.Invoke(st);
        }

        public delegate State MoveState(State st, Act act);
        public static event MoveState EventMoveState;
        public static State OnEventMoveState(State st, Act act)
        {
            return EventMoveState.Invoke(st, act);
        }

        public delegate State GetCurrentState();
        public static event GetCurrentState EventGetCurrentState;
        public static State OnEventGetCurrentState()
        {
            return EventGetCurrentState.Invoke();
        }

        public delegate void DrawLine(Line line);
        public static event DrawLine EventDrawLine;
        public static void OnEventDrawLine(Line line)
        {
            EventDrawLine.Invoke(line);
        }

        public delegate void InitAgent(State st);
        public static event InitAgent EventInitAgent;
        public static void OnEventInitAgent(State st)
        {
            EventInitAgent.Invoke(st);
        }

        public delegate void SetCurrentStateAgent(State st);
        public static event SetCurrentStateAgent EventSetCurrentStateAgent;
        public static void OnEventSetCurrentStateAgent(State st)
        {
            EventSetCurrentStateAgent.Invoke(st);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ObhodZonPVO
{
    class Agent
    {
        private State currentState;
        public List<Act> routeAgent;
        int maxPolicy;
        double discont;

        public Agent(int InitX, int InitY, int MaxPolicy, double Discont)
        {
            maxPolicy = MaxPolicy;
            discont = Discont;
            routeAgent = new List<Act>();
            currentState = EventsHelper.OnEventFindState(new State(InitX, InitY));
            SetPositionAgent(currentState, false);
            EventsHelper.EventGetCurrentState += GetCurrentStateAgent;
            EventsHelper.EventInitAgent += InitStateAgent;
            EventsHelper.EventSetCurrentStateAgent += SetCurrentState;
        }

        private void SetCurrentState(State st)
        {
            currentState = st;
        }

        void SetPositionAgent(State st, bool animation)
        {
            EventsHelper.OnEventSetPositionAgent(st, animation);
        }

        public void InitStateAgent(State st)
        {
            currentState = EventsHelper.OnEventFindState(st);
            SetPositionAgent(currentState, false);
        }

        State GetCurrentStateAgent()
        {
            return currentState;
        }

        public double ActionAgent(Act act, bool animation, bool move)
        {
            double reward = 0.0;
            if (currentState.X != 18 || currentState.Y != 12)
            {
                switch (act)
                {
                    case Act.LeftDown:
                        reward = currentState.GetReward(Act.LeftDown);
                        if (currentState.X != 0 && currentState.Y != 0)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X - 1), (currentState.Y - 1)));
                        break;
                    case Act.Left:
                        reward = currentState.GetReward(Act.Left);
                        if (currentState.X != 0)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X - 1), (currentState.Y)));
                        break;
                    case Act.LeftUp:
                        reward = currentState.GetReward(Act.LeftUp);
                        if (currentState.X != 0 && currentState.Y != 19)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X - 1), (currentState.Y + 1)));
                        break;
                    case Act.Up:
                        reward = currentState.GetReward(Act.Up);
                        if (currentState.Y != 19)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X), (currentState.Y + 1)));
                        break;
                    case Act.RightUp:
                        reward = currentState.GetReward(Act.RightUp);
                        if (currentState.X != 19 && currentState.Y != 19)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X + 1), (currentState.Y + 1)));
                        break;
                    case Act.Right:
                        reward = currentState.GetReward(Act.Right);
                        if (currentState.X != 19)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X + 1), currentState.Y));
                        break;
                    case Act.RightDown:
                        reward = currentState.GetReward(Act.RightDown);
                        if (currentState.X != 19 && currentState.Y != 0)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X + 1), (currentState.Y - 1)));
                        break;
                    case Act.Down:
                        reward = currentState.GetReward(Act.Down);
                        if (currentState.Y != 0)
                            currentState = EventsHelper.OnEventFindState(new State((currentState.X), (currentState.Y - 1)));
                        break;
                    default:
                        break;
                }

                if (move)
                {
                    SetPositionAgent(currentState, animation);
                    EventsHelper.OnEventSetTextReward(Convert.ToString(reward));
                    EventsHelper.OnEventSetTextRewardLeft("Left " + Convert.ToString(currentState.GetReward(Act.Left)));
                    EventsHelper.OnEventSetTextRewardLeftUp("LeftUp " + Convert.ToString(currentState.GetReward(Act.LeftUp)));
                    EventsHelper.OnEventSetTextRewardLeftDown("LeftDown " + Convert.ToString(currentState.GetReward(Act.LeftDown)));
                    EventsHelper.OnEventSetTextRewardUp("Up " + Convert.ToString(currentState.GetReward(Act.Up)));
                    EventsHelper.OnEventSetTextRewardDown("Down " + Convert.ToString(currentState.GetReward(Act.Down)));
                    EventsHelper.OnEventSetTextRewardRight("Right " + Convert.ToString(currentState.GetReward(Act.Right)));
                    EventsHelper.OnEventSetTextRewardRightUp("RightUp " + Convert.ToString(currentState.GetReward(Act.RightUp)));
                    EventsHelper.OnEventSetTextRewardRightDown("RightDown " + Convert.ToString(currentState.GetReward(Act.RightDown)));
                }
            }
            else
            {
                //reward = finishReward; - убрал ревард за действия в терминальном состоянии
            }
            return reward;
        }

        public void CreateRouteAgent(bool Vp)
        {

            routeAgent.Clear();

            State staticSt = new State(currentState.X, currentState.Y);
            for (int i = 0; i < maxPolicy; i++)
            {
                double[] arr = new double[8];
                if (Vp)
                {
                    arr[0] = currentState.GetReward(Act.LeftDown) + discont * EventsHelper.OnEventMoveState(currentState, Act.LeftDown).VpOpt;
                    arr[1] = currentState.GetReward(Act.Left) + discont * EventsHelper.OnEventMoveState(currentState, Act.Left).VpOpt;
                    arr[2] = currentState.GetReward(Act.LeftUp) + discont * EventsHelper.OnEventMoveState(currentState, Act.LeftUp).VpOpt;
                    arr[3] = currentState.GetReward(Act.Up) + discont * EventsHelper.OnEventMoveState(currentState, Act.Up).VpOpt;
                    arr[4] = currentState.GetReward(Act.RightUp) + discont * EventsHelper.OnEventMoveState(currentState, Act.RightUp).VpOpt;
                    arr[5] = currentState.GetReward(Act.Right) + discont * EventsHelper.OnEventMoveState(currentState, Act.Right).VpOpt;
                    arr[6] = currentState.GetReward(Act.RightDown) + discont * EventsHelper.OnEventMoveState(currentState, Act.RightDown).VpOpt;
                    arr[7] = currentState.GetReward(Act.Down) + discont * EventsHelper.OnEventMoveState(currentState, Act.Down).VpOpt;
                }
                else
                {
                    arr[0] = currentState.Qfunction(Act.LeftDown);
                    arr[1] = currentState.Qfunction(Act.Left);
                    arr[2] = currentState.Qfunction(Act.LeftUp);
                    arr[3] = currentState.Qfunction(Act.Up);
                    arr[4] = currentState.Qfunction(Act.RightUp);
                    arr[5] = currentState.Qfunction(Act.Right);
                    arr[6] = currentState.Qfunction(Act.RightDown);
                    arr[7] = currentState.Qfunction(Act.Down);
                }

                double maxValue = arr.Max();
                int indexMax = -1;
                List<int> indexesMax = new List<int>();
                for (int ind = 0; ind < arr.Length; ind++)
                {
                    if (Math.Abs(arr[ind] - maxValue) < 0.01)
                        indexesMax.Add(ind);
                }

                if (indexesMax.Count > 1)
                {
                    for (int j = 0; j < indexesMax.Count; j++)
                    {
                        if ((i != 0) && (routeAgent[i - 1] == (Act)indexesMax[j]))
                        {
                            indexMax = indexesMax[j];
                            break;
                        }
                        else
                        {
                            indexMax = indexesMax[j];
                        }

                    }
                }
                else
                    indexMax = indexesMax[0];

                routeAgent.Add((Act)indexMax);
                ActionAgent(routeAgent[i], false, true);
            }

            InitStateAgent(staticSt);

        }
    }
}

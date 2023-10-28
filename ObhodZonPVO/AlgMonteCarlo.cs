using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObhodZonPVO
{
    static class AlgMonteCarlo
    {
        static public void MethodMonteCarloWithIS(List<State> lstState, int MaxPolicy, double discont, int length)
        {
            Random rnd = new Random();
            //делаем рандомную стратегию
            List<PolicyState> lstPolicyCurrent = new List<PolicyState>();
            
            for (int i = 0; i < lstState.Count; i++)
                lstPolicyCurrent.Add(new PolicyState(0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125));

            for (int i = 0; i < length; i++) //генерация множества эпизодов
            {
                int rndX = rnd.Next(0, 20);
                int rndY = rnd.Next(0, 20);
                EventsHelper.OnEventInitAgent(new State(rndX, rndY));
                //Сгенерировать эпизод, следуя π: S0, A0, R1, S1, A1, R2, …, ST–1, AT–1, R
                List<State> lstStateMC = new List<State>();
                List<Act> lstActMC = new List<Act>();
                List<double> lstRewardMC = new List<double>();

                for (int k = 0; k < MaxPolicy; k++)
                {
                    State curState = EventsHelper.OnEventGetCurrentState();
                    lstStateMC.Add(curState);
                    PolicyState ps = FindPolicyState(curState, lstPolicyCurrent);
                    Act act = ArgMaximum(ps, rnd); //Act act = RandomAct(ps, rnd);
                    lstActMC.Add(act);
                    curState = EventsHelper.OnEventMoveState(curState, act);
                    EventsHelper.OnEventSetCurrentStateAgent(curState);
                    lstRewardMC.Add(curState.GetReward(act));
                }

                double income = 0;
                for (int j = (lstStateMC.Count - 1); j > 0; j--) //движение в цикле с конца
                {
                    income = income * discont + lstRewardMC[j];
                    if (!FindReplay(lstStateMC, lstActMC,j)) //lstStateMC[i] не встречается в оставшихся
                    {
                        State st = EventsHelper.OnEventFindState(lstStateMC[j]);
                        st.SetReturnsAct(lstActMC[j], income);
                    }
                }

                UpdatePolicyGreedyQ(lstState, lstPolicyCurrent);
            }

            EventsHelper.OnEventInitAgent(new State(2, 2));
        }

        static public void MethodMonteCarloOnePolicy(List<State> lstState, int MaxPolicy, double discont, int length)
        {

        }

        static public void MethodMonteCarloManyPolicy(List<State> lstState, int MaxPolicy, double discont, int length)
        {

        }

        static void UpdatePolicyGreedyQ(List<State> lstState, List<PolicyState> lstPolicyCurrent)
        {
            foreach (var item in lstState)
            {
                double[] arr = new double[8];
                arr[0] = item.Qfunction(Act.LeftDown);
                arr[1] = item.Qfunction(Act.Left);
                arr[2] = item.Qfunction(Act.LeftUp);
                arr[3] = item.Qfunction(Act.Up);
                arr[4] = item.Qfunction(Act.RightUp);
                arr[5] = item.Qfunction(Act.Right);
                arr[6] = item.Qfunction(Act.RightDown);
                arr[7] = item.Qfunction(Act.Down);

                double maxValue = arr.Max();

                List<int> indexsMax = new List<int>();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (Math.Abs(arr[i] - maxValue) < 0.01)
                        indexsMax.Add(i);
                }

                PolicyState policyCurrentState = FindPolicyState(item, lstPolicyCurrent);
                for (int i = 0; i < 8; i++)
                {
                    policyCurrentState.SetProbabilityAction((Act)i, 0.0);
                }

                double probability = 1 / (double)indexsMax.Count;
                foreach (var ind in indexsMax)
                {
                    policyCurrentState.SetProbabilityAction((Act)ind, probability);
                }
            }
        }

        static bool FindReplay(List<State> lstStateMC, List<Act> lstActMC, int number)
        {
            bool rezult = false;
            for (int i = number; i > 0; --i)
            {
                if ((lstStateMC[number].X == lstStateMC[i].X) && (lstStateMC[number].Y == lstStateMC[i].Y) && (lstActMC[number] == lstActMC[i]))
                    rezult = true;
            }
            return rezult;
        }

        static Act ArgMaximum(PolicyState ps, Random rnd)
        {
            double[] arr = new double[8];
            arr[0] = ps.GetProbabilityAction(Act.LeftDown);
            arr[1] = ps.GetProbabilityAction(Act.Left);
            arr[2] = ps.GetProbabilityAction(Act.LeftUp);
            arr[3] = ps.GetProbabilityAction(Act.Up);
            arr[4] = ps.GetProbabilityAction(Act.RightUp);
            arr[5] = ps.GetProbabilityAction(Act.Right);
            arr[6] = ps.GetProbabilityAction(Act.RightDown);
            arr[7] = ps.GetProbabilityAction(Act.Down);

            double maxValue = arr.Max();

            List<int> indexsMax = new List<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (Math.Abs(arr[i] - maxValue) < 0.01)
                    indexsMax.Add(i);
            }

            int MaxRandomIndex = indexsMax[rnd.Next(0, indexsMax.Count)];

            return (Act)MaxRandomIndex;
        }

        static Act RandomAct(PolicyState ps, Random rnd)
        {
            int rezult = rnd.Next(0, 8);
            return (Act)rezult;
        }

        static PolicyState FindPolicyState(State st, List<PolicyState> lstPolicyCurrent)
        {
            int index = st.X + st.Y * 20;
            if (index >= 0 && index < 400)
                return lstPolicyCurrent[index];
            return lstPolicyCurrent[0];
        }
    }

}

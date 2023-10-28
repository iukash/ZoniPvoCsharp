using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ObhodZonPVO
{
    static class AlgIterationPolicyDP
    {
        static public void IterationPolicyDP(List<State> lstState, double discont)
        {
            List<PolicyState> lstPolicyCurrent = new List<PolicyState>();
            List<PolicyState> lstPolicyOpt = new List<PolicyState>();
            for (int i = 0; i < lstState.Count; i++)
            {
                lstPolicyCurrent.Add(new PolicyState(0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125, 0.125));
            }

            int gg = 0;
            bool NotEquality = true;
            while (NotEquality)
            {
                lstPolicyOpt.Clear();
                foreach (var pc in lstPolicyCurrent)
                {
                    lstPolicyOpt.Add(new PolicyState(pc.GetProbabilityAction(Act.LeftDown), pc.GetProbabilityAction(Act.Left),
                                                      pc.GetProbabilityAction(Act.LeftUp), pc.GetProbabilityAction(Act.Up),
                                                      pc.GetProbabilityAction(Act.RightUp), pc.GetProbabilityAction(Act.Right),
                                                      pc.GetProbabilityAction(Act.RightDown), pc.GetProbabilityAction(Act.Down)));
                }

                UpdateVpStates(lstState, discont, lstPolicyCurrent);
                UpdateCurrentPolicy(lstState, discont, lstPolicyCurrent);
                gg++;

                NotEquality = false;
                for (int i = 0; i < lstPolicyCurrent.Count; i++)
                {
                    if(lstPolicyCurrent[i].CompareTo(lstPolicyOpt[i]) != 0)
                        NotEquality = true;
                }
            }
            var ec = gg;
        }

        static void UpdateVpStates(List<State> lstState, double discont, List<PolicyState> lstPolicyCurrent)
        {
            foreach (var item in lstState)
            {
                PolicyState pSt = FindPolicyState(item, lstPolicyCurrent);
                item.VpOpt = CountVpState(item, discont, pSt);
            }
        }

        static double CountVpState(State st, double discont, PolicyState pSt)
        {
            double rezult = 0.0;
            rezult = pSt.GetProbabilityAction(Act.LeftDown) * (st.GetReward(Act.LeftDown) + discont * EventsHelper.OnEventMoveState(st, Act.LeftDown).VpOpt) +
                     pSt.GetProbabilityAction(Act.Left) * (st.GetReward(Act.Left) + discont * EventsHelper.OnEventMoveState(st, Act.Left).VpOpt) +
                     pSt.GetProbabilityAction(Act.LeftUp) * (st.GetReward(Act.LeftUp) + discont * EventsHelper.OnEventMoveState(st, Act.LeftUp).VpOpt) +
                     pSt.GetProbabilityAction(Act.Up) * (st.GetReward(Act.Up) + discont * EventsHelper.OnEventMoveState(st, Act.Up).VpOpt) +
                     pSt.GetProbabilityAction(Act.RightUp) * (st.GetReward(Act.RightUp) + discont * EventsHelper.OnEventMoveState(st, Act.RightUp).VpOpt) +
                     pSt.GetProbabilityAction(Act.Right) * (st.GetReward(Act.Right) + discont * EventsHelper.OnEventMoveState(st, Act.Right).VpOpt) +
                     pSt.GetProbabilityAction(Act.RightDown) * (st.GetReward(Act.RightDown) + discont * EventsHelper.OnEventMoveState(st, Act.RightDown).VpOpt) +
                     pSt.GetProbabilityAction(Act.Down) * (st.GetReward(Act.Down) + discont * EventsHelper.OnEventMoveState(st, Act.Down).VpOpt);

            return rezult;
        }
        static PolicyState FindPolicyState(State st, List<PolicyState> lstPolicyCurrent)
        {
            int index = st.X + st.Y * 20;
            if (index >= 0 && index < 400)
                return lstPolicyCurrent[index];
            return lstPolicyCurrent[0];
        }

        static void UpdateCurrentPolicy(List<State> lstState, double discont, List<PolicyState> lstPolicyCurrent)
        {
            foreach (var item in lstState)
            {
                double[] arr = new double[8];
                arr[0] = item.GetReward(Act.LeftDown) + discont * EventsHelper.OnEventMoveState(item, Act.LeftDown).VpOpt;
                arr[1] = item.GetReward(Act.Left) + discont * EventsHelper.OnEventMoveState(item, Act.Left).VpOpt;
                arr[2] = item.GetReward(Act.LeftUp) + discont * EventsHelper.OnEventMoveState(item, Act.LeftUp).VpOpt;
                arr[3] = item.GetReward(Act.Up) + discont * EventsHelper.OnEventMoveState(item, Act.Up).VpOpt;
                arr[4] = item.GetReward(Act.RightUp) + discont * EventsHelper.OnEventMoveState(item, Act.RightUp).VpOpt;
                arr[5] = item.GetReward(Act.Right) + discont * EventsHelper.OnEventMoveState(item, Act.Right).VpOpt;
                arr[6] = item.GetReward(Act.RightDown) + discont * EventsHelper.OnEventMoveState(item, Act.RightDown).VpOpt;
                arr[7] = item.GetReward(Act.Down) + discont * EventsHelper.OnEventMoveState(item, Act.Down).VpOpt;

                double maxValue = arr.Max();

                List<int> indexsMax = new List<int>();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == maxValue)
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
    }
}

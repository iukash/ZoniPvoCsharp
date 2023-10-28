using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ObhodZonPVO
{
    static class AlgIterationVpStateDP
    {
        static public void IterationVpStateDP(List <State> lstState, double discont)
        {
            bool shod = false;
            while (!shod)
                shod = UpdateVpForIS(lstState, discont);

        }

        static private bool UpdateVpForIS(List<State> lstState, double discont)
        {
            bool shodimost = true;
            double Eps = 0.1;
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

                if (Math.Abs(maxValue - item.VpOpt) > Eps)
                    shodimost = false;
                item.VpOpt = maxValue;
            }
            return shodimost;
        }
    }
}

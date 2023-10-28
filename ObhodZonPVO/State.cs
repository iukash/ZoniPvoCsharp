using System;
using System.Collections.Generic;
using System.Text;

namespace ObhodZonPVO
{
    class State
    {
        public int X;
        public int Y;
        public double VpOpt;
        public double ZonePVO;
        private List<double> reward;
        private List<double> returnsAct;
        private List<int> nAct;
        public State(int x, int y)
        {
            X = x;
            Y = y;
            VpOpt = 0.0;
            ZonePVO = 0.0;
            reward = new List<double>();
            returnsAct = new List<double>();
            nAct = new List<int>();

            for (int i = 0; i < 8; i++)
            {
                reward.Add(0.0);
                returnsAct.Add(0.0);
                nAct.Add(0);
            }
        }

        public void SetReward(Act act, double value)
        {
            reward[(int)act] = value;
        }

        public double GetReward(Act act)
        {
            return reward[(int)act];
        }

        public void SetReturnsAct(Act act, double value)
        {
            returnsAct[(int)act] += value;
            nAct[(int)act]++;
        }

        public double Qfunction(Act act)
        {
            double rezult = 0.0;
            if (nAct[(int)act] == 0)
                rezult = 100;
            else
                rezult = (returnsAct[(int)act] / nAct[(int)act]);

            return rezult;
        }
    }
}

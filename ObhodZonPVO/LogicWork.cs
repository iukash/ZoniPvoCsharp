using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Globalization;

namespace ObhodZonPVO
{
    enum Act { LeftDown, Left, LeftUp, Up, RightUp, Right, RightDown, Down }
    enum Alg { DP, IterPolDP, IterStDP, MC, MCWithIS, MCOnePolicy, MCManyPolicy, TD }
    class LogicWork
    {
        Environment environment;
        Agent agent;
        Thread thPlay;
        bool AbortThread = false;
        int MaxPolicy = 1000;
        double rewardPvoIn = 30;
        double rewardPvoMove = 30;
        double finishReward = 500;
        double moveReward = 5;
        double discont = 0.7;
        int sizeX = 20;
        int sizeY = 20;

        public LogicWork()
        {
            EventsHelper.EventKeyDown += MyKeyDown;
            EventsHelper.EventStartPlay += StartPlay;
            EventsHelper.EventWindowClosing += WindowClosing;

            environment = new Environment(sizeX, sizeY, moveReward, rewardPvoIn, rewardPvoMove, finishReward, discont);
            agent = new Agent(2, 2, MaxPolicy, discont);
            UpdateLabelGridPvo(sizeX* sizeY);
        }

        private void WindowClosing()
        {
            AbortThread = true;
        }

        private void StartPlay()
        {
            bool realization = false;
            Alg currentAlg = EventsHelper.OnEventGetAlgoritm();
            bool Vp = false;
            switch (currentAlg)
            {
                case Alg.DP:
                    MessageBox.Show("Not select calculation method");
                    break;
                case Alg.IterPolDP:
                    AlgIterationPolicyDP.IterationPolicyDP(environment.lstState, discont);
                    realization = true;
                    Vp = true;
                    break;
                case Alg.IterStDP:
                    AlgIterationVpStateDP.IterationVpStateDP(environment.lstState, discont);
                    realization = true;
                    Vp = true;
                    break;
                case Alg.MC:
                    MessageBox.Show("Not select calculation method");
                    break;
                case Alg.MCWithIS:
                    AlgMonteCarlo.MethodMonteCarloWithIS(environment.lstState, MaxPolicy, discont, 10000);
                    realization = true;
                    Vp = false;
                    break;
                case Alg.MCOnePolicy:
                    MessageBox.Show("Not implemented");
                    break;
                case Alg.MCManyPolicy:
                    MessageBox.Show("Not implemented");
                    break;
                case Alg.TD:
                    MessageBox.Show("Not implemented");
                    break;
                default:
                    break;
            }

            if (realization)
            {
                UpdateLabelGridVpState(sizeX * sizeY);
                agent.CreateRouteAgent(Vp);
                RouteView.DrawRoute(agent.routeAgent, sizeY);
                //thPlay = new Thread(Play);
                //thPlay.Start();
            }
        }

        void Play()
        {
            for (int i = 0; i < MaxPolicy; i++)
            {
                Thread.Sleep(500);
                agent.ActionAgent(agent.routeAgent[i], false, true);
                if (AbortThread)
                    return;
            }
        }

        void UpdateLabelGridVpState(int count)
        {
            for (int i = 0; i < count; i++)
                EventsHelper.OnEventSetContentBord(i, Convert.ToString(environment.lstState[i].VpOpt));
        }

        void UpdateLabelGridPvo(int count)
        {
            for (int i = 0; i < count; i++)
                EventsHelper.OnEventSetContentBord(i, Convert.ToString(environment.lstState[i].ZonePVO));
        }
        private void MyKeyDown(KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.D)
                agent.ActionAgent(Act.Right, true, true);
            if (e.Key == System.Windows.Input.Key.W)
                agent.ActionAgent(Act.Up, true, true);
            if (e.Key == System.Windows.Input.Key.X)
                agent.ActionAgent(Act.Down, true, true);
            if (e.Key == System.Windows.Input.Key.A)
                agent.ActionAgent(Act.Left, true, true);
            if (e.Key == System.Windows.Input.Key.E)
                agent.ActionAgent(Act.RightUp, true, true);
            if (e.Key == System.Windows.Input.Key.C)
                agent.ActionAgent(Act.RightDown, true, true);
            if (e.Key == System.Windows.Input.Key.Z)
                agent.ActionAgent(Act.LeftDown, true, true);
            if (e.Key == System.Windows.Input.Key.Q)
                agent.ActionAgent(Act.LeftUp, true, true);
        }
    }
}

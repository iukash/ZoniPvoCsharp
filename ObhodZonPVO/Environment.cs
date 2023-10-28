using System;
using System.Collections.Generic;
using System.Text;

namespace ObhodZonPVO
{
    class Environment
    {
        public List<State> lstState;
        private int xGrid = 0;
        private int yGrid = 0;
        public Environment(int x, int y, double moveReward, double rewardPvoIn, double rewardPvoMove, double finishReward, double discont)
        {
            lstState = new List<State>();
            xGrid = x;
            yGrid = y;
            
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    lstState.Add(new State(j, i));

            CountZonePvo(500, 250);
            CountZonePvo(350, 450);

            InitRewardStates(moveReward, rewardPvoIn, rewardPvoMove, finishReward, discont);

            EventsHelper.EventFindState += FindState;
            EventsHelper.EventMoveState += MoveState;
        }
        private void CountZonePvo(int x, int y)
        {
            foreach (var item in lstState)
            {
                if (Math.Sqrt(Math.Pow(item.X * 50 - x, 2) + Math.Pow(item.Y * 50 - y, 2)) < 110)
                {
                    if (Math.Abs(item.ZonePVO) < 0.01)
                        item.ZonePVO = 0.9;
                    else
                        item.ZonePVO = 1 - (1 - 0.9) * (1 - item.ZonePVO);
                }
                else if (Math.Sqrt(Math.Pow(item.X * 50 - x, 2) + Math.Pow(item.Y * 50 - y, 2)) < 160)
                {
                    if (Math.Abs(item.ZonePVO) < 0.01)
                        item.ZonePVO = 0.5;
                    else
                        item.ZonePVO = 1 - (1 - 0.5) * (1 - item.ZonePVO);
                }
            }
        }

        private void InitRewardStates(double moveReward, double rewardPvoIn, double rewardPvoMove, double finishReward, double discont)
        {
            foreach (var item in lstState)
            {
                item.SetReward(Act.LeftDown, -1.41 * moveReward);
                item.SetReward(Act.RightDown, -1.41 * moveReward);
                item.SetReward(Act.LeftUp, -1.41 * moveReward);
                item.SetReward(Act.RightUp, -1.41 * moveReward);
                item.SetReward(Act.Down, -1.0 * moveReward);
                item.SetReward(Act.Up, -1.0 * moveReward);
                item.SetReward(Act.Left, -1.0 * moveReward);
                item.SetReward(Act.Right, -1.0 * moveReward);


                if (item.X == 0)
                {
                    item.SetReward(Act.LeftDown, -6.41 * moveReward);
                    item.SetReward(Act.LeftUp, -6.41 * moveReward);
                    item.SetReward(Act.Left, -6.0 * moveReward);
                }
                else if (item.X == 19)
                {
                    item.SetReward(Act.RightDown, -6.41 * moveReward);
                    item.SetReward(Act.RightUp, -6.41 * moveReward);
                    item.SetReward(Act.Right, -6.0 * moveReward);
                }

                if (item.Y == 0)
                {
                    item.SetReward(Act.LeftDown, -6.41 * moveReward);
                    item.SetReward(Act.RightDown, -6.41 * moveReward);
                    item.SetReward(Act.Down, -6.0 * moveReward);
                }
                else if (item.Y == 19)
                {
                    item.SetReward(Act.LeftUp, -6.41 * moveReward);
                    item.SetReward(Act.RightUp, -6.41 * moveReward);
                    item.SetReward(Act.Up, -6.0 * moveReward);
                }

                item.SetReward(Act.LeftDown, item.GetReward(Act.LeftDown) - MoveState(item, Act.LeftDown).ZonePVO * rewardPvoIn);
                item.SetReward(Act.Left, item.GetReward(Act.Left) - MoveState(item, Act.Left).ZonePVO * rewardPvoIn);
                item.SetReward(Act.LeftUp, item.GetReward(Act.LeftUp) - MoveState(item, Act.LeftUp).ZonePVO * rewardPvoIn);
                item.SetReward(Act.Up, item.GetReward(Act.Up) - MoveState(item, Act.Up).ZonePVO * rewardPvoIn);
                item.SetReward(Act.RightUp, item.GetReward(Act.RightUp) - MoveState(item, Act.RightUp).ZonePVO * rewardPvoIn);
                item.SetReward(Act.Right, item.GetReward(Act.Right) - MoveState(item, Act.Right).ZonePVO * rewardPvoIn);
                item.SetReward(Act.RightDown, item.GetReward(Act.RightDown) - MoveState(item, Act.RightDown).ZonePVO * rewardPvoIn);
                item.SetReward(Act.Down, item.GetReward(Act.Down) - MoveState(item, Act.Down).ZonePVO * rewardPvoIn);

                if (item.ZonePVO > 0.01)
                {
                    item.SetReward(Act.LeftDown, item.GetReward(Act.LeftDown) - rewardPvoMove);
                    item.SetReward(Act.Left, item.GetReward(Act.Left) - rewardPvoMove);
                    item.SetReward(Act.LeftUp, item.GetReward(Act.LeftUp) - rewardPvoMove);
                    item.SetReward(Act.Up, item.GetReward(Act.Up) - rewardPvoMove);
                    item.SetReward(Act.RightUp, item.GetReward(Act.RightUp) - rewardPvoMove);
                    item.SetReward(Act.Right, item.GetReward(Act.Right) - rewardPvoMove);
                    item.SetReward(Act.RightDown, item.GetReward(Act.RightDown) - rewardPvoMove);
                    item.SetReward(Act.Down, item.GetReward(Act.Down) - rewardPvoMove);
                }

                if (item.X == 18 && item.Y == 12)
                {
                    item.SetReward(Act.LeftDown, finishReward);
                    item.SetReward(Act.LeftUp, finishReward);
                    item.SetReward(Act.Left, finishReward);
                    item.SetReward(Act.RightDown, finishReward);
                    item.SetReward(Act.RightUp, finishReward);
                    item.SetReward(Act.Right, finishReward);
                    item.SetReward(Act.Down, finishReward);
                    item.SetReward(Act.Up, finishReward);
                }

                if (item.X == 18 && item.Y == 11)
                    item.SetReward(Act.Up, finishReward * discont);
                if (item.X == 18 && item.Y == 13)
                    item.SetReward(Act.Down, finishReward * discont);
                if (item.X == 17 && item.Y == 12)
                    item.SetReward(Act.Right, finishReward * discont);
                if (item.X == 19 && item.Y == 12)
                    item.SetReward(Act.Left, finishReward * discont);
                if (item.X == 19 && item.Y == 13)
                    item.SetReward(Act.LeftDown, finishReward * discont);
                if (item.X == 19 && item.Y == 11)
                    item.SetReward(Act.LeftUp, finishReward * discont);
                if (item.X == 17 && item.Y == 13)
                    item.SetReward(Act.RightDown, finishReward * discont);
                if (item.X == 17 && item.Y == 11)
                    item.SetReward(Act.RightUp, finishReward * discont);

            }
        }

        State FindState(State st)
        {
            int index = st.X + st.Y * 20;
            if (index >= 0 && index < xGrid * yGrid)
                return lstState[index];
            return st;
        }

        State MoveState(State st, Act act)
        {
            switch (act)
            {
                case Act.LeftDown:
                    if (st.X != 0 && st.Y != 0)
                        return FindState(new State((st.X - 1), (st.Y - 1)));
                    break;
                case Act.Left:
                    if (st.X != 0)
                        return FindState(new State((st.X - 1), (st.Y)));
                    break;
                case Act.LeftUp:
                    if (st.X != 0 && st.Y != 19)
                        return FindState(new State((st.X - 1), (st.Y + 1)));
                    break;
                case Act.Up:
                    if (st.Y != 19)
                        return FindState(new State((st.X), (st.Y + 1)));
                    break;
                case Act.RightUp:
                    if (st.X != 19 && st.Y != 19)
                        return FindState(new State((st.X + 1), (st.Y + 1)));
                    break;
                case Act.Right:
                    if (st.X != 19)
                        return FindState(new State((st.X + 1), st.Y));
                    break;
                case Act.RightDown:
                    if (st.X != 19 && st.Y != 0)
                        return FindState(new State((st.X + 1), (st.Y - 1)));
                    break;
                case Act.Down:
                    if (st.Y != 0)
                        return FindState(new State((st.X), (st.Y - 1)));
                    break;
                default:
                    break;
            }
            return st;
        }
    }
}

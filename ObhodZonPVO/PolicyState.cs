using System;
using System.Collections.Generic;
using System.Text;

namespace ObhodZonPVO
{
    class PolicyState : IComparable
    {
        private double probabilityLeftDown;
        private double probabilityLeft;
        private double probabilityLeftUp;
        private double probabilityUp;
        private double probabilityRightUp;
        private double probabilityRight;
        private double probabilityRightDown;
        private double probabilityDown;
        public PolicyState(double prLeftDown, double prLeft, double prLeftUp, double prUp, double prRightUp, double prRight, double prRightDown, double prDown)
        {
            probabilityLeftDown = prLeftDown;
            probabilityLeft = prLeft;
            probabilityLeftUp = prLeftUp;
            probabilityUp = prUp;
            probabilityRightUp = prRightUp;
            probabilityRight = prRight;
            probabilityRightDown = prRightDown;
            probabilityDown = prDown;
    }
        public void SetProbabilityAction(Act act, double value)
        {
            switch (act)
            {
                case Act.LeftDown:
                    probabilityLeftDown = value;
                    break;
                case Act.Left:
                    probabilityLeft = value;
                    break;
                case Act.LeftUp:
                    probabilityLeftUp = value;
                    break;
                case Act.Up:
                    probabilityUp = value;
                    break;
                case Act.RightUp:
                    probabilityRightUp = value;
                    break;
                case Act.Right:
                    probabilityRight = value;
                    break;
                case Act.RightDown:
                    probabilityRightDown = value;
                    break;
                case Act.Down:
                    probabilityDown = value;
                    break;
                default:
                    break;
            }
        }
        public double GetProbabilityAction(Act act)
        {
            switch (act)
            {
                case Act.LeftDown:
                    return probabilityLeftDown;
                case Act.Left:
                    return probabilityLeft;
                case Act.LeftUp:
                    return probabilityLeftUp;
                case Act.Up:
                    return probabilityUp;
                case Act.RightUp:
                    return probabilityRightUp;
                case Act.Right:
                    return probabilityRight;
                case Act.RightDown:
                    return probabilityRightDown;
                case Act.Down:
                    return probabilityDown;
                default:
                    break;
            }
            return 0.0;
        }

        public int CompareTo(object obj)
        {
            PolicyState ps = obj as PolicyState;
            if (ps != null)
                if (ps.probabilityDown == probabilityDown && ps.probabilityLeft == probabilityLeft && ps.probabilityLeftDown == probabilityLeftDown &&
                    ps.probabilityLeftUp == probabilityLeftUp && ps.probabilityRight == probabilityRight && ps.probabilityRightDown == probabilityRightDown &&
                    ps.probabilityRightUp == probabilityRightUp && ps.probabilityUp == probabilityUp)
                    return 0;
                else
                    return 1;
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}

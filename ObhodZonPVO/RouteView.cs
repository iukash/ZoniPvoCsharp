using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObhodZonPVO
{
    static class RouteView
    {
        static public void DrawRoute(List<Act> lstAct, int sizeY)
        {
            State curState = EventsHelper.OnEventGetCurrentState();

            foreach (var item in lstAct)
            {
                if (curState.X == 18 && curState.Y == 12)
                    break;

                Line line = new Line();
                line.Stroke = Brushes.Green;
                line.StrokeThickness = 8;
                line.X1 = curState.X * 50 + 25;
                line.Y1 = sizeY * 50 - (curState.Y * 50 + 25);
                curState = EventsHelper.OnEventMoveState(curState, item);
                line.X2 = curState.X * 50 + 25;
                line.Y2 = sizeY * 50 - (curState.Y * 50 + 25);
                EventsHelper.OnEventDrawLine(line);
            }
        }
    }
}

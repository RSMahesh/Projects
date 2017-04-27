
using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ServerDLL
{

    internal abstract class MouseSimulater
    {

        #region private fields
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        private static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;

        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;

        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public enum MouseSpeed { Instant, SuperSlow, Slow, Natural = 3, Fast = 5, SuperFast = 8 };

        #endregion

        #region static methods: click

        static public void Click_Left(int X, int Y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
     
        }

        static public void Click_Right(int X, int Y)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        static public void Click_DoubleClick()
        {

            int X = Cursor.Position.X;

            int Y = Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

            Thread.Sleep(150);

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

        }

        #endregion

        #region static methods: move

        static public void MoveTo(Point p)
        {

            MouseSimulater.moveTo(p);

        }

        static public void MoveTo(Point p, MouseSpeed speed)
        {

            switch (speed)
            {

                case MouseSpeed.Instant:

                    MouseSimulater.MoveTo(p);

                    break;

                default:

                    MouseSimulater.moveTo(p, speed);

                    break;

            }

        }

        static public void MoveTo(int x, int y)
        {

            MouseSimulater.MoveTo(new Point(x, y));

        }

        static public void MoveTo(int x, int y, MouseSpeed speed)
        {

            MouseSimulater.MoveTo(new Point(x, y), speed);

        }

        private static void moveTo(Point p)
        {

            Cursor.Position = p;

        }

        private static void moveTo(Point p, MouseSpeed speed)
        {

            List<Point> wayPoints = new List<Point>();

            switch (speed)
            {

                case MouseSpeed.Instant:

                    MouseSimulater.moveTo(p);

                    return;

                default:

                    getWayPoints(Cursor.Position, p, ref wayPoints, (int)speed);

                    foreach (Point waypoint in wayPoints)
                    {

                        MouseSimulater.moveTo(waypoint);

                        Thread.Sleep(5);

                    }

                    return;

            }

        }

        private static void getWayPoints(Point from, Point to, ref List<Point> points, int divider)
        {

            lock (points)
            {

                int fromX, fromY, toX, toY, distanceX, distanceY, directionX, directionY, howMany;

                double intervalX, intervalY;

                fromX = from.X;

                fromY = from.Y;

                toX = to.X;

                toY = to.Y;

                distanceX = Math.Abs(fromX - toX);

                distanceY = Math.Abs(fromY - toY);

                directionX = fromX > toX ? -1 : 1;

                directionY = fromY > toY ? -1 : 1;

                howMany = ((distanceX > distanceY) ? distanceX : distanceY) / (6 * divider);

                intervalX = 1.000 * distanceX / howMany;

                intervalY = 1.000 * distanceY / howMany;

                points.Clear();

                for (int i = 1; i <= howMany; i++)
                {

                    points.Add(new Point(

                    fromX + (int)(intervalX * i * directionX)

                    ,

                    fromY + (int)(intervalY * i * directionY)

                    ));

                }

            }

        }

        #endregion



    }

}


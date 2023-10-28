using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObhodZonPVO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BorderGrid> lstBord;
        public MainWindow()
        {
            InitializeComponent();
            RadioButtonItSt.IsChecked = true;

            lstBord = new List<BorderGrid>();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    BorderGrid bord = new BorderGrid();
                    Canvas.SetTop(bord, (19 - i)*50);
                    Canvas.SetLeft(bord, j*50);
                    //Panel.SetZIndex(bord, 0);
                    MainCanvas.Children.Add(bord);
                    lstBord.Add(bord);
                }
            }

            EventsHelper.EventSetPositionAgent += SetPositionAgent;
            EventsHelper.EventSetContentBord += SetContentBord;
            EventsHelper.EventSetTextReward += SetTextReward;
            EventsHelper.EventSetTextRewardLeft += SetTextRewardLeft;
            EventsHelper.EventSetTextRewardLeftUp += SetTextRewardLeftUp;
            EventsHelper.EventSetTextRewardLeftDown += SetTextRewardLeftDown;
            EventsHelper.EventSetTextRewardUp += SetTextRewardUp;
            EventsHelper.EventSetTextRewardDown += SetTextRewardDown;
            EventsHelper.EventSetTextRewardRight += SetTextRewardRight;
            EventsHelper.EventSetTextRewardRightUp += SetTextRewardRightUp;
            EventsHelper.EventSetTextRewardRightDown += SetTextRewardRightDown;
            EventsHelper.EventGetAlgoritm += GetAlgoritm;
            EventsHelper.EventDrawLine += DrawLine;

            LogicWork lw = new LogicWork();
        }

        private void DrawLine(Line line)
        {
            MainCanvas.Children.Add(line);
        }

        private void SetTextReward(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxReward.Text = str; })); 
        }

        private void SetTextRewardLeft(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardLeft.Text = str; }));
        }

        private void SetTextRewardLeftUp(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardLeftUp.Text = str; }));
        }

        private void SetTextRewardLeftDown(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardLeftDown.Text = str; }));
        }

        private void SetTextRewardDown(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardDown.Text = str; }));
        }

        private void SetTextRewardRight(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardRight.Text = str; }));
        }

        private void SetTextRewardRightUp(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardRightUp.Text = str; }));
        }

        private void SetTextRewardRightDown(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardRightDown.Text = str; }));
        }

        private void SetTextRewardUp(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { TextBoxRewardUp.Text = str; }));
        }

        private void SetContentBord(int number, string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { lstBord[number].SetContent(str); }));
        }

        Alg GetAlgoritm()
        {
            Alg AlgRezult = Alg.DP;

            if (RadioButtonDP.IsChecked == true)
                if (RadioButtonItPol.IsChecked == true)
                    AlgRezult = Alg.IterPolDP;
                else
                    AlgRezult = Alg.IterStDP;
            else if(RadioButtonMonteCarlo.IsChecked == true)
                if (RadioButtonWithIS.IsChecked == true)
                    AlgRezult = Alg.MCWithIS;
                else if(RadioButtonOnePolicy.IsChecked == true)
                    AlgRezult = Alg.MCOnePolicy;
                else
                    AlgRezult = Alg.MCManyPolicy;
            else if (RadioButtonTD.IsChecked == true)
                AlgRezult = Alg.TD;

            return AlgRezult;
        }

        private void SetPositionAgent(State st, bool animation)
        {
            if (animation)
            {
                double left = Canvas.GetLeft(Agent);
                int secondX = Convert.ToInt32(Math.Abs(st.X - left / 50));
                var dx = new DoubleAnimation(left, st.X * 50, new Duration(new TimeSpan(0, 0, secondX)));
                Agent.BeginAnimation(Canvas.LeftProperty, dx);

                double bottom = Canvas.GetBottom(Agent);
                int secondY = Convert.ToInt32(Math.Abs(st.Y - bottom / 50));
                var dy = new DoubleAnimation(bottom, st.Y * 50, new Duration(new TimeSpan(0, 0, secondY)));
                Agent.BeginAnimation(Canvas.BottomProperty, dy);
            }
            else
            {
                Dispatcher.BeginInvoke((Action)(() => { Canvas.SetLeft(Agent, st.X * 50); }));
                Dispatcher.BeginInvoke((Action)(() => { Canvas.SetBottom(Agent, st.Y * 50); })); 
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            EventsHelper.OnEventKeyDown(e);
        }

        private void RadioButtonDP_Checked(object sender, RoutedEventArgs e)
        {
            if(GroupBoxDP!=null)
                GroupBoxDP.IsEnabled = true;

            if (GroupBoxMC != null)
                GroupBoxMC.IsEnabled = false;
        }

        private void RadioButtonMonteCarlo_Checked(object sender, RoutedEventArgs e)
        {
            if (GroupBoxMC != null)
                GroupBoxMC.IsEnabled = true;
            GroupBoxDP.IsEnabled = false;
        }

        private void RadioButtonTD_Checked(object sender, RoutedEventArgs e)
        {
            GroupBoxDP.IsEnabled = false;
            GroupBoxMC.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EventsHelper.OnEventStartPlay();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EventsHelper.OnEventWindowClosing();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrblCNC.Glutils;

namespace GrblCNC.Controls
{
    public partial class ManualControl : UserControl
    {
        public delegate void AxisStepJogPressedDelegate(object sender, int axis, float amount);
        public event AxisStepJogPressedDelegate AxisStepJogPressed;
        public delegate void AxisContinuesJogPressedDelegate(object sender, int axis, int direction);
        public event AxisContinuesJogPressedDelegate AxisContinuesJogPressed;
        public delegate void AxisHomePressedDelegate(object sender, int axis);
        public event AxisHomePressedDelegate AxisHomePressed;
        public ManualControl()
        {
            InitializeComponent();
            comboJogStep.SelectedIndex = 0;
            Enabled = Global.GrblConnected;
            Global.GrblConnectionChanged += Global_GrblConnectionChanged;
        }

        void Global_GrblConnectionChanged(bool isConnected)
        {
            Enabled = isConnected;
        }

        public float GetSelectedJogStep()
        {
            if (comboJogStep.SelectedIndex == 0)
                return 0;
            // parse the distance value from the distance string in the combo box
            string[] vars = comboJogStep.SelectedItem.ToString().Split(' ');
            return Utils.ParseFloatInvariant(vars[0]);
        }

        private void AxisPos_click(object sender, EventArgs e)
        {
            if (comboJogStep.SelectedIndex <= 0)
                return;
            if (AxisStepJogPressed == null)
                return;
            JogButton b = (JogButton)sender;
            AxisStepJogPressed(this, b.Id, GetSelectedJogStep());
        }

        private void AxisNeg_click(object sender, EventArgs e)
        {
            if (comboJogStep.SelectedIndex == 0)
                return;
            if (AxisStepJogPressed == null)
                return;
            JogButton b = (JogButton)sender;
            AxisStepJogPressed(this, b.Id, -GetSelectedJogStep());
        }

        private void AxisHome_click(object sender, EventArgs e)
        {
            if (AxisHomePressed == null)
                return;
            JogButton b = (JogButton)sender;
            AxisHomePressed(this, b.Id);
        }

        private void jogButtTouchOff_Click(object sender, EventArgs e)
        {

        }

        private void jogButtToolTouchOff_Click(object sender, EventArgs e)
        {

        }

        private void jogButtProbe_Click(object sender, EventArgs e)
        {

        }

        private void jogButtSpindleStop_Click(object sender, EventArgs e)
        {

        }

        private void jogButtSpindleCCW_Click(object sender, EventArgs e)
        {

        }

        private void jogButtSpindleCW_Click(object sender, EventArgs e)
        {

        }

        private void AxisPos_down(object sender, MouseEventArgs e)
        {
            if (comboJogStep.SelectedIndex != 0)
                return;
            if (AxisContinuesJogPressed == null)
                return;
            JogButton b = (JogButton)sender;
            AxisContinuesJogPressed(this, b.Id, 1);
        }

        private void AxisNeg_down(object sender, MouseEventArgs e)
        {
            if (comboJogStep.SelectedIndex != 0)
                return;
            if (AxisContinuesJogPressed == null)
                return;
            JogButton b = (JogButton)sender;
            AxisContinuesJogPressed(this, b.Id, -1);
        }

        private void Axis_up(object sender, MouseEventArgs e)
        {
            if (comboJogStep.SelectedIndex != 0)
                return;
            if (AxisContinuesJogPressed == null)
                return;
            JogButton b = (JogButton)sender;
            AxisContinuesJogPressed(this, b.Id, 0);
        }

    }
}

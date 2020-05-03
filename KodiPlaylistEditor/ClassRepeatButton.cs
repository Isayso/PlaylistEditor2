/*
 *      GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007
 *
 *         This program stores the YouTube Links on local device
 *         Copyright (C) <2019>  <Github: Isayso>
 *
 *         This program is free software: you can redistribute it and/or modify
 *         it under the terms of the GNU General Public License as published by
 *         the Free Software Foundation, either version 3 of the License, or
 *         (at your option) any later version.
 *
 *         This program is distributed in the hope that it will be useful,
 *         but WITHOUT ANY WARRANTY; without even the implied warranty of
 *         MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *         GNU General Public License for more details.
 *
 *         You should have received a copy of the GNU General Public License
 *         along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System;
using System.Windows.Forms;

/// <summary>
/// A repeating button class.
/// When the mouse is held down on the button it will first wait for FirstDelay milliseconds,
/// then press the button every LoSpeedWait milliseconds until LoHiChangeTime milliseconds,
/// then press the button every HiSpeedWait milliseconds
/// </summary>
class RepeatingButton : Button
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepeatingButton"/> class.
    /// </summary>
    public RepeatingButton()
    {
        internalTimer = new Timer();
        internalTimer.Interval = FirstDelay;
        internalTimer.Tick += new EventHandler(internalTimer_Tick);
        this.MouseDown += new MouseEventHandler(RepeatingButton_MouseDown);
        this.MouseUp += new MouseEventHandler(RepeatingButton_MouseUp);
    }

    /// <summary>
    /// The delay before first repeat in milliseconds
    /// </summary>
    public int FirstDelay = 400;

    /// <summary>
    /// The delay in milliseconds between repeats before LoHiChangeTime
    /// </summary>
    public int LoSpeedWait = 250;

    /// <summary>
    /// The delay in milliseconds between repeats after LoHiChangeTime
    /// </summary>
    public int HiSpeedWait = 75;

    /// <summary>
    /// The changeover time between slow repeats and fast repeats in milliseconds
    /// </summary>
    public int LoHiChangeTime = 1000;

    private void RepeatingButton_MouseDown(object sender, MouseEventArgs e)
    {
        internalTimer.Tag = DateTime.Now;
        internalTimer.Start();
    }

    private void RepeatingButton_MouseUp(object sender, MouseEventArgs e)
    {
        internalTimer.Stop();
        internalTimer.Interval = FirstDelay;
    }

    private void internalTimer_Tick(object sender, EventArgs e)
    {
        this.OnClick(e);
        TimeSpan elapsed = DateTime.Now - ((DateTime)internalTimer.Tag);
        if (elapsed.TotalMilliseconds < LoHiChangeTime)
        {
            internalTimer.Interval = LoSpeedWait;
        }
        else
        {
            internalTimer.Interval = HiSpeedWait;
        }
    }

    private Timer internalTimer;
}
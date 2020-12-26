﻿//  MIT License
//  Copyright (c) 2018 github/isayso
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
//  files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy,
//  modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Drawing;
using System.Windows.Forms;

namespace PlaylistEditor
{
    public partial class NotificationBoxF : Form
    {
        public NotificationBoxF(Form _owner, string label, NotificationMsg message, Position pos)
        {
            InitializeComponent();

            Owner = _owner;

            color(message, pos);

            lbl.Text = label;
        }

        public void color(NotificationMsg backgcl, Position screenpos)
        {

            switch (backgcl)
            {
                case NotificationMsg.OK:
                    this.BackColor = System.Drawing.Color.DarkGreen;
                    break;

                case NotificationMsg.ERROR:
                    this.BackColor = System.Drawing.Color.DarkRed;
                    break;

                case NotificationMsg.DONE:
                    this.BackColor = System.Drawing.Color.MidnightBlue;
                    break;

            }

            switch (screenpos)
            {
                case Position.Center:
                    this.StartPosition = FormStartPosition.CenterScreen;
                    break;

                case Position.Parent:
                    this.StartPosition = FormStartPosition.Manual;

                    if (Owner != null)
                        Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                            Owner.Location.Y + Owner.Height / 2 - Height / 2);

                    //this.StartPosition = FormStartPosition.CenterParent;
                    break;
            }


        }

    }
}

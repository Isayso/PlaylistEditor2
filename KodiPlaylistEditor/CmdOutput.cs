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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistEditor
{
    //  public static MyText {private get; set; }

    public partial class CmdOutput : Form
    {
        public string myProperty { get; set; }

        //public class Form2
        //{
        //    public string myProperty { get; set; }
        //}
        //private void Form2_Load(object sender, EventArgs e)
        //{
        //    //MessageBox.Show(this.myProperty);
        //    textbox_cmdout.Text = myProperty;
        //}


        public CmdOutput()
        {
            InitializeComponent();
           // textbox_cmdout.Text = "";

        }


     
        public void textbox_cmdout_TextChanged(object sender, EventArgs e)
        {
           // textbox_cmdout.Text = myProperty;
        }
    }
}

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
using System.Windows.Forms;

namespace PlaylistEditor
{
    public partial class popup2 : Form
    {
        public popup2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }
        public void color(string backgcl)
        {

            switch (backgcl)
            {
                case "green":
                    this.BackColor = System.Drawing.Color.DarkGreen;
                    
                    break;

                case "red":
                    this.BackColor = System.Drawing.Color.DarkRed;
                   
                    break;

                case "blue":
                    this.BackColor = System.Drawing.Color.MidnightBlue;
               
                    break;

            }



        }
    }
}

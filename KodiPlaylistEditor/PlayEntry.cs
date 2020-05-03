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
using System.ComponentModel;

namespace PlaylistEditor
{
    public class PlayEntry : INotifyPropertyChanged
    {
        private string _Name, _Link; 

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayEntry(string Name, string Link)

        {
            _Name = Name;
            _Link = Link;
       
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.NotifyPropertyChanged("Name");
            }
        }
        public string Link
        {
            get { return _Link; }
            set
            {
                _Link = value;
                this.NotifyPropertyChanged("Link");
            }
        }
        private void NotifyPropertyChanged(string value)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(value));
        }
    }
}

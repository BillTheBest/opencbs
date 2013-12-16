﻿// Copyright © 2013 Open Octopus Ltd.
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
// 
// Website: http://www.opencbs.com
// Contact: contact@opencbs.com

using OpenCBS.Interface.Presenter;
using OpenCBS.Interface.View;

namespace OpenCBS.View
{
    public partial class ChangePasswordView : BaseView, IChangePasswordView
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        public void Run()
        {
            ShowDialog();
        }

        public void Stop()
        {
            Close();
        }

        public int Id { get; set; }

        public string CurrentPassword
        {
            get { return _currentPasswordTextBox.Text; }
        }

        public string NewPassword
        {
            get { return _newPasswordTextBox.Text; }
        }

        public string NewPasswordConfirmation
        {
            get { return _newPasswordConfirmationTextBox.Text; }
        }

        public void Attach(IChangePasswordPresenterCallbacks presenterCallbacks)
        {
            _okButton.Click += (sender, e) => presenterCallbacks.Ok();
            _cancelButton.Click += (sender, e) => presenterCallbacks.Cancel();
            FormClosing += (sender, e) => presenterCallbacks.Close();
        }
    }
}

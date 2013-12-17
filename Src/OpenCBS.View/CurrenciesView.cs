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

using System.Collections.Generic;
using System.Windows.Forms;
using OpenCBS.DataContract;
using OpenCBS.Interface;
using OpenCBS.Interface.Presenter;
using OpenCBS.Interface.View;

namespace OpenCBS.View
{
    public partial class CurrenciesView : BaseView, ICurrenciesView
    {
        private ICurrenciesPresenterCallbacks _presenterCallbacks;

        public CurrenciesView(ITranslator translator)
            : base(translator)
        {
            InitializeComponent();
            MdiParent = Application.OpenForms[0];
        }

        public void Run()
        {
            Show();
        }

        public void ShowCurrencies(IList<CurrencyDto> currencies)
        {
            var selectedObject = _currenciesListView.SelectedObject;
            _currenciesListView.SetObjects(currencies);
            _presenterCallbacks.ChangeSelection();
            _currenciesListView.SelectedObject = selectedObject;
        }

        public void Attach(ICurrenciesPresenterCallbacks presenterCallbacks)
        {
            _addButton.Click += (sender, e) => presenterCallbacks.Add();
            _editButton.Click += (sender, e) => presenterCallbacks.Edit();
            _deleteButton.Click += (sender, e) => presenterCallbacks.Delete();
            _showDeletedCheckBox.CheckedChanged += (sender, e) => presenterCallbacks.Refresh();
            _currenciesListView.SelectedIndexChanged += (sender, e) => presenterCallbacks.ChangeSelection();
            FormClosing += (sender, e) => presenterCallbacks.Close();
            _presenterCallbacks = presenterCallbacks;
        }

        public bool AllowAdding
        {
            get { return _addButton.Visible; }
            set { _addButton.Visible = value; }
        }

        public bool AllowEditing
        {
            get { return _editButton.Visible; }
            set { _editButton.Visible = value; }
        }

        public bool AllowDeleting
        {
            get { return _deleteButton.Visible; }
            set { _deleteButton.Visible = value; }
        }

        public bool CanEdit
        {
            get { return _editButton.Enabled; }
            set { _editButton.Enabled = value; }
        }

        public bool CanDelete
        {
            get { return _deleteButton.Enabled; }
            set { _deleteButton.Enabled = value; }
        }

        public int? SelectedCurrencyId
        {
            get
            {
                var currencyDto = (CurrencyDto) _currenciesListView.SelectedObject;
                if (currencyDto == null) return null;
                return currencyDto.Id;
            }
        }

        public bool ShowDeleted
        {
            get { return _showDeletedCheckBox.Checked; }
        }
    }
}
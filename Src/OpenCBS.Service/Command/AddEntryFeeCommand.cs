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

using OpenCBS.DataContract;
using OpenCBS.DataContract.AppEvent;
using OpenCBS.DataContract.CommandData;
using OpenCBS.Interface;
using OpenCBS.Interface.Presenter;
using OpenCBS.Interface.Service;

namespace OpenCBS.Service.Command
{
    public class AddEntryFeeCommand : ICommand<AddEntryFeeData>
    {
        private readonly IEntryFeePresenter _presenter;
        private readonly IEntryFeeService _entryFeeService;
        private readonly IApplicationController _appController;

        public AddEntryFeeCommand(IEntryFeePresenter presenter, IEntryFeeService entryFeeService, IApplicationController appController)
        {
            _presenter = presenter;
            _entryFeeService = entryFeeService;
            _appController = appController;
        }

        public void Execute(AddEntryFeeData commandData)
        {
            var result = _presenter.Get(null);
            if (result.CommandResult == CommandResult.Ok)
            {
                var id = _entryFeeService.Add(result.Data);
                _appController.Raise(new EntryFeeSavedEvent { Id = id });
            }
        }
    }
}
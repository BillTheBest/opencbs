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

using NSubstitute;
using NUnit.Framework;
using OpenCBS.DataContract;
using OpenCBS.DataContract.AppEvent;
using OpenCBS.DataContract.CommandData;
using OpenCBS.Interface;
using OpenCBS.Interface.Presenter;
using OpenCBS.Interface.Service;
using OpenCBS.Service.Command;

// ReSharper disable InconsistentNaming
namespace OpenCBS.UnitTest.Command
{
    [TestFixture]
    public class DeleteRoleCommandTest
    {
        private IConfirmationPresenter _presenter;
        private IRoleService _roleService;
        private IApplicationController _appController;
        private DeleteRoleCommand _command;

        [SetUp]
        public void SetUp()
        {
            _presenter = Substitute.For<IConfirmationPresenter>();
            _roleService = Substitute.For<IRoleService>();
            _appController = Substitute.For<IApplicationController>();
            _command = new DeleteRoleCommand(_presenter, _roleService, _appController);
        }

        [Test]
        public void Execute_NotConfirmed_DoesNotDeleteRole()
        {
            _presenter.Get(Arg.Any<string>()).Returns(CommandResult.Cancel);
            _command.Execute(new DeleteRoleData { Id = 1 });
            _roleService.DidNotReceive().Delete(Arg.Is<int>(id => id == 1));
            _appController.DidNotReceive().Raise(Arg.Is<RoleDeletedEvent>(data => data.Id == 1));
        }

        [Test]
        public void Execute_Confirmed_DeletesRole()
        {
            _presenter.Get(Arg.Any<string>()).Returns(CommandResult.Ok);
            _command.Execute(new DeleteRoleData { Id = 1});
            _roleService.Received().Delete(Arg.Is<int>(id => id == 1));
            _appController.Received().Raise(Arg.Is<RoleDeletedEvent>(data => data.Id == 1));
        }
    }
}
// ReSharper restore InconsistentNaming

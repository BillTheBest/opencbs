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
using OpenCBS.DataContract;
using OpenCBS.Interface.Presenter;

namespace OpenCBS.Interface.View
{
    public interface IExoticScheduleView : IView<IExoticSchedulePresenterCallbacks>
    {
        void Run();
        void Stop();
        void ShowNotification(Notification notification);
        void FocusItems();

        int Id { get; set; }
        string ExoticScheduleName { get; set; }
        IList<ExoticScheduleItemDto> Items { get; set; }

        ExoticScheduleItemDto SelectedItem { get; }

        bool CanMoveUp { get; set; }
        bool CanMoveDown { get; set; }
        bool CanDelete { get; set; }
    }
}

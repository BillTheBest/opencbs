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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;

namespace OpenCBS.Persistence
{
    public static class DapperExtensions
    {
        public static void Update<T>(this IDbConnection connection, T data, IList<string> exclude = null)
        {
            var tableName = GetTableName(typeof (T));

            var paramNames = GetParamNames(data);
            if (exclude != null)
                foreach (var ex in exclude)
                    paramNames.Remove(ex);

            var builder = new StringBuilder();
            builder.Append("update [").Append(tableName).Append("] set ");
            builder.AppendLine(string.Join(",", paramNames.Where(n => n != "Id").Select(p => p + "= @" + p)));
            builder.Append("where Id = @Id");

            var parameters = new DynamicParameters(data);
            connection.Execute(builder.ToString(), parameters);
        }

        public static int Insert<T>(this IDbConnection connection, T data)
        {
            var tableName = GetTableName(typeof(T));

            var o = (object) data;
            var paramNames = GetParamNames(o);
            paramNames.Remove("Id");

            var cols = string.Join(",", paramNames);
            var colsParams = string.Join(",", paramNames.Select(p => "@" + p));
            var builder = new StringBuilder();
            builder.Append("set nocount on insert ");
            builder.Append(tableName);
            builder.Append("(").Append(cols).Append(")");
            builder.Append("values(").Append(colsParams).Append(") select cast(scope_identity() as int)");
            return connection.Query<int>(builder.ToString(), o).Single();
        }

        public static void Delete<T>(this IDbConnection connection, int id)
        {
            var tableName = GetTableName(typeof(T));
            var sql = "update " + tableName + " set Deleted = 1 where Id = @Id";
            connection.Execute(sql, new { Id = id });
        }

        private static string GetTableName(Type t)
        {
            var tableName = t.Name;
            if (tableName.EndsWith("Row"))
                tableName = tableName.Substring(0, tableName.Length - 3);
            return tableName;
        }

        private static IList<string> GetParamNames(object o)
        {
            if (o is DynamicParameters)
            {
                return (o as DynamicParameters).ParameterNames.ToList();
            }

            var paramNames = new List<string>();
            foreach (var prop in o.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
            {
                paramNames.Add(prop.Name);
            }
            return paramNames;
        }
    }
}

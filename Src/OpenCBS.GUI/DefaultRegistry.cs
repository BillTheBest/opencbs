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

using System.Windows.Forms;
using OpenCBS.GUI.Command;
using OpenCBS.GUI.CommandData;
using OpenCBS.Interface;
using OpenCBS.Interface.Repository;
using OpenCBS.Persistence;
using StructureMap.Configuration.DSL;

namespace OpenCBS.GUI
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scanner =>
            {
                scanner.Assembly("OpenCBS.Service");
                scanner.Assembly("OpenCBS.Persistence");
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            });
            For<ApplicationContext>().Use<AppContext>();
            For<IApplicationController>().Singleton().Use<ApplicationController>();
            For<IEventPublisher>().Singleton().Use<EventPublisher>();

            // Factories
//            For<IPolicyFactory>().Use<PolicyFactory>();

            // Views / presenters
//            For<ILoanProductsView>().Use<LoanProductsView>();
//            For<ILoanProductsPresenter>().Use<LoanProductsPresenter>();
//            For<ILoanProductView>().Use<LoanProductView>();
//            For<IEntryFeesView>().Use<EntryFeesView>();
//            For<IEntryFeeView>().Use<EntryFeeView>();
//            For<IConfirmationView>().Use<ConfirmationView>();
//            For<ISelectEntryFeeView>().Use<SelectEntryFeeView>();
//            
//            For<ILoanProductPresenter>().Use<LoanProductPresenter>();
//            For<IEntryFeesPresenter>().Use<EntryFeesPresenter>();
//            For<IEntryFeePresenter>().Use<EntryFeePresenter>();
//            For<IConfirmationPresenter>().Use<ConfirmationPresenter>();
//            For<ISelectEntryFeePresenter>().Use<SelectEntryFeePresenter>();

            // Commands
            For<ICommand<ShowLoanProductsData>>().Use<ShowLoanProductsCommand>();
            For<ICommand<AddLoanProductData>>().Use<AddLoanProductCommand>();
            For<ICommand<EditLoanProductData>>().Use<EditLoanProductCommand>();
            For<ICommand<DeleteLoanProductData>>().Use<DeleteLoanProductCommand>();
            For<ICommand<SelectEntryFeeData>>().Use<SelectEntryFeeCommand>();

            For<ICommand<ShowEntryFeesData>>().Use<ShowEntryFeesCommand>();
            For<ICommand<AddEntryFeeData>>().Use<AddEntryFeeCommand>();
            For<ICommand<EditEntryFeeData>>().Use<EditEntryFeeCommand>();
            For<ICommand<DeleteEntryFeeData>>().Use<DeleteEntryFeeCommand>();

            For<ICommand<ChangeLanguageData>>().Use<ChangeLanguageCommand>();

            // Repositories
//            For<IPolicyRepository>().Use<PolicyRepository>();
            For<IConnectionProvider>().Use<SqlConnectionProvider>();
//            For<ILoanProductRepository>().Use<LoanProductRepository>();
//            For<ICurrencyRepository>().Use<CurrencyRepository>();
//            For<IEntryFeeRepository>().Use<EntryFeeRepository>();

            // Services
//            For<ILoanProductService>().Use<LoanProductService>();
//            For<IEntryFeeService>().Use<EntryFeeService>();

            // Validators
//            For<ILoanProductValidator>().Use<LoanProductValidator>();
//            For<IEntryFeeValidator>().Use<EntryFeeValidator>();

            For<ITranslator>().Singleton().Use<JsonTranslator>();

            RegisterInterceptor(new EventAggregatorInterceptor());
        }
    }
}

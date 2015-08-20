//------------------------------------------------------------------------------
// <copyright file="MySonarIssuesControl.xaml.cs" company="Hocoma AG">
//     Copyright (c) Hocoma AG.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.VisualStudio.Settings.Internal;

using VSSonarIntegration.Annotations;
using VSSonarIntegration.Issues.Services;

namespace VSSonarIntegration.Issues
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MySonarIssuesControl.
    /// </summary>
    public partial class MySonarIssuesControl : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySonarIssuesControl"/> class.
        /// </summary>
        public MySonarIssuesControl()
        {
            this.InitializeComponent();
            this.DataContext = this;
            issues = new ObservableCollection<SonarIssue>();
            Loaded += GetIssues;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <comment>async void because used as event handler</comment>
        internal async void GetIssues(object sender, RoutedEventArgs e)
        {
            IEnumerable<SonarIssue> result = new List<SonarIssue>();
            try
            {
                result = await Task.Factory.StartNew(() =>
                {
                    using (ISonarService service = new SonarService())
                    {
                        return service.GetIssues("fau");
                    }
                });
            }
            catch (Exception exc)
            {
                ;
            }
           

            Issues.Clear();
            foreach (var issue in result)
            {
                Issues.Add(issue);
            }
        }

        private ObservableCollection<SonarIssue> issues;

        public ObservableCollection<SonarIssue> Issues
        {
            get { return issues; }
            set
            {
                if (issues == value)
                    return;
                issues = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
}
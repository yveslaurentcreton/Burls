using Burls.Windows.Constants;
using Burls.Windows.Core;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Burls.Windows.Views
{
    /// <summary>
    /// Interaction logic for CreateProfileSelectionRuleDialog.xaml
    /// </summary>
    public partial class ProfileSelectionRuleCreateDialog : Flyout, IFlyoutView
    {
        public ProfileSelectionRuleCreateDialog()
        {
            InitializeComponent();
        }

        public string FlyoutName => FlyoutNames.ProfileSelectionRuleCreate;
    }
}

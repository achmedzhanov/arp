using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Arp.Databases;
using JetBrains.DataFlow;
using JetBrains.IDE;
using JetBrains.ProjectModel;
using JetBrains.UI.CommonControls;
using JetBrains.UI.Controls;
using JetBrains.UI.Options;
using JetBrains.UI.Options.Helpers;

namespace Arp.UI
{

    [OptionsPage("ARP", "ARP", null, ParentId = "Tools", Sequence = 1.0)]
    class ARPPage : AStackPanelOptionsPage
    {
// Fields
    private readonly IProperty<string> connectionString;
    private readonly IProperty<bool> databaseEnabled;
        private DatabaseManager manager;
        public const string PID = "UnitTesting";

    // Methods
    public ARPPage(IOptionsDialog optionsDialog) : base("ARP")
    {
        this.connectionString = new Property<string>("ConectionString");
        this.databaseEnabled = new Property<bool>("Enabled");
        var solution = optionsDialog.DataContext.GetData<ISolution>(DataConstants.SOLUTION);
        if (solution != null)
        {
            this.manager = DatabaseManager.GetInstance(solution);

            base.Bind(new PropertyBinding<string, string>(ManualProperty.Create<string>("ConectionString", this.manager.ConnectionString, delegate(string value)
            {
                this.manager.ConnectionString = value;
            }), this.connectionString, DataFlowDirection.FromSource));

            base.Bind(new PropertyBinding<bool, bool>(ManualProperty.Create<bool>("Enabled", this.manager.Enabled, delegate(bool value)
            {
                this.manager.Enabled = value;
            }), this.databaseEnabled, DataFlowDirection.FromSource));

            // TODO bind properties
            
            using (new LayoutSuspender(this))
            {
                this.InitSettings();
                return;
            }
        }
        base.Controls.Add(JetBrains.UI.Options.Helpers.Controls.CreateNoSolutionCueBanner());
    }


    private void InitSettings()
    {

        var enableDatabaseSupport = new CheckBoxDisabledNoCheck("&Enable Database Support");
        base.Controls.Add(enableDatabaseSupport);
        base.Controls.Add(JetBrains.UI.Options.Helpers.Controls.Separator.DefaultHeight);

        base.Controls.Add(new Controls.Label("Database connection string (MSSQL 2005 support only)"));
        var directoryBox = new Controls.EditBox();
        base.Controls.Add(directoryBox);

        base.AddRef(new PropertyBinding<bool, bool>(this.databaseEnabled, enableDatabaseSupport.CheckedLogicallyProperty, DataFlowDirection.BothWays));
        base.AddRef(new PropertyBinding<string, string>(this.connectionString, directoryBox.Text, DataFlowDirection.BothWays));

    }

    public override bool OnOk()
    {
        base.OnOk();

        manager.Refresh();

        return true;
    }
}
}


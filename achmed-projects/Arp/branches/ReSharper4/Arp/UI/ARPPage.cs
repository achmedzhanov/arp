using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JetBrains.DataFlow;
using JetBrains.IDE;
using JetBrains.ProjectModel;
using JetBrains.UI.Controls;
using JetBrains.UI.Options;
using JetBrains.UI.Options.Helpers;

namespace Arp.UI
{
    /*
    [OptionsPage("ARP", "ARP", "optionsPage", ParentId = "Tools", Sequence = 1.0)]
    class ARPPage : AStackPanelOptionsPage
    {
// Fields
    private readonly IProperty<string> connectionString;
    private readonly IProperty<bool> databaseEnabled;
    public const string PID = "UnitTesting";

    // Methods
    public ARPPage(IOptionsDialog optionsDialog) : base("ARP")
    {
        this.connectionString = new Property<string>("CustomFolder");
        this.databaseEnabled = new Property<bool>("UseCustomFolder");
        var solution = optionsDialog.DataContext.GetData<ISolution>(DataConstants.SOLUTION);
        if (solution != null)
        {
            this.myManager = UnitTestManager.GetInstance(solution);
            base.Bind(new PropertyBinding<string, string>(ManualProperty.Create<string>("CustomStartDirectory", this.myManager.CustomStartDirectory, delegate (string value) {
                this.myManager.CustomStartDirectory = value;
            }), this.myCustomFolder, DataFlowDirection.FromSource));
            using (new LayoutSuspender(this))
            {
                this.InitSettings();
                this.InitProviders();
                return;
            }
        }
        base.Controls.Add(Controls.CreateNoSolutionCueBanner());
    }

    private void InitProviders()
    {
        base.Controls.Add(Controls.Separator.DefaultHeight);
        base.Controls.Add(new Controls.Label("Enabled unit testing providers:"));
        this.myProvidersList.CheckBoxes = true;
        this.myProvidersList.MultiSelect = false;
        this.myProvidersList.View = View.List;
        this.myProvidersList.Dock = DockStyle.Fill;
        foreach (IUnitTestProvider provider in this.myManager.GetProviders())
        {
            ListViewItem item = this.myProvidersList.Items.Add(provider.ID);
            if (this.myManager.ProviderEnabled(provider))
            {
                item.Checked = true;
            }
        }
        base.Controls.Add(this.myProvidersList);
        base.AddRef(new PropertyBinding<bool, bool>(this.myEnableUnitTesting, WinFormsProperty.Create<bool>(this.myProvidersList, "Enabled", false), DataFlowDirection.FromSource));
    }

    private void InitSettings()
    {
        TableLayoutPanel grid;
        CheckBoxDisabledNoCheck enableUnitTesting = new CheckBoxDisabledNoCheck("&Enable Unit Testing");
        base.Controls.Add(enableUnitTesting);
        base.Controls.Add(Controls.Separator.DefaultHeight);
        CheckBoxDisabledNoCheck shadowCopy = new CheckBoxDisabledNoCheck("&Shadow-copy assemblies being tested");
        base.Controls.Add(shadowCopy);
        base.Controls.Add(new Controls.Label("Allows to rebuild the assemblies while the tests are running. If shadow-copying is causing problems when loading supplementary files, disable this option.", OptionsDialog.InnerLevelMargin));
        base.Controls.Add(Controls.Separator.DefaultHeight);
        RadioGroup<bool> rgStartupFolder = new RadioGroup<bool>();
        base.Controls.Add(rgStartupFolder);
        rgStartupFolder.FlowDirection = ((FlowLayoutPanel) this).FlowDirection.TopDown;
        rgStartupFolder.HasCheckbox.Value = false;
        rgStartupFolder.LabelText.Value = "Run tests from:";
        rgStartupFolder.AddRadioButton("&Project output folder", false);
        rgStartupFolder.AddRadioButton("Specified &folder", true);
        rgStartupFolder.RadioButtonsAdditionalMargin.Value = OptionsDialog.InnerLevelMargin;
        rgStartupFolder.Dock = DockStyle.Fill;
        rgStartupFolder.AddDependent(new Controls.Separator());
        rgStartupFolder.AddDependent(grid = new TableLayoutPanel());
        grid.AutoSize = true;
        grid.AutoSizeMode = ((Panel) this).AutoSizeMode.GrowAndShrink;
        grid.RowCount = 1;
        grid.ColumnCount = 2;
        grid.RowStyles.Insert(0, new RowStyle(SizeType.AutoSize));
        grid.ColumnStyles.Insert(0, new ColumnStyle(SizeType.Percent, 100f));
        grid.ColumnStyles.Insert(1, new ColumnStyle(SizeType.AutoSize));
        grid.Margin += OptionsDialog.InnerLevelMargin + OptionsDialog.InnerLevelMargin;
        grid.Dock = DockStyle.Fill;
        Controls.EditBox directoryBox = new Controls.EditBox();
        Controls.Button browseButton = new Controls.Button("Browse...", delegate {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = directoryBox.Text.Value;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    directoryBox.Text.Value = dialog.SelectedPath;
                }
            }
        });
        grid.Controls.Add(directoryBox);
        grid.Controls.Add(browseButton);
        base.AddRef(new PropertyBinding<bool, bool>(this.myEnableUnitTesting, enableUnitTesting.CheckedLogicallyProperty, DataFlowDirection.BothWays));
        base.AddRef(new PropertyBinding<bool, bool>(this.myEnableUnitTesting, WinFormsProperty.Create<bool>(shadowCopy, "Enabled", false), DataFlowDirection.FromSource));
        base.AddRef(new PropertyBinding<bool, bool>(this.myEnableUnitTesting, WinFormsProperty.Create<bool>(rgStartupFolder, "Enabled", false), DataFlowDirection.FromSource));
        base.AddRef(new PropertyBinding<bool, bool>(this.myEnableUnitTesting, WinFormsProperty.Create<bool>(directoryBox, "Enabled", false), DataFlowDirection.FromSource));
        base.AddRef(new PropertyBinding<bool, bool>(this.myUseShadowCopy, shadowCopy.CheckedLogicallyProperty, DataFlowDirection.BothWays));
        base.AddRef(new PropertyBinding<bool, bool>(this.myUseCustomFolder, rgStartupFolder.Value, DataFlowDirection.BothWays));
        base.AddRef(new PropertyBinding<string, string>(this.myCustomFolder, directoryBox.Text, DataFlowDirection.BothWays));
        base.AddRef(new PropertyBinding<bool, bool>(this.myUseCustomFolder, WinFormsProperty.Create<bool>(directoryBox, "Enabled", false), DataFlowDirection.FromSource));
    }

    public override bool OnOk()
    {
        base.OnOk();
        bool refresh = false;
        foreach (ListViewItem item in this.myProvidersList.Items)
        {
            IUnitTestProvider provider = this.myManager.GetProvider(item.Text);
            if ((provider != null) && (item.Checked != this.myManager.ProviderEnabled(provider)))
            {
                this.myManager.EnableProvider(provider, item.Checked);
                refresh = true;
            }
        }
        if (refresh)
        {
            this.myManager.RefreshAll();
        }
        return true;
    }
}
}*/
}

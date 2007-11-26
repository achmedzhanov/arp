using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Arp.Assertions;
using DevExpress.XtraEditors.Controls;
using JetBrains.CommonControls;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.UI;
using JetBrains.UI.Controls;
using JetBrains.UI.PopupMenu;
using JetBrains.UI.RichText;
using JetBrains.UI.TreeView;
using JetBrains.Util;
using JetBrains.Util.DataStructures.TreeModel;

namespace Arp.UnusedReferences.UI
{
    public partial class ModulesChooserPanel : Form, IConstrainableControl
    {
        private bool autoActivate;
        private TreeModelViewChecked treeView;
        private readonly UnusedReferencesSearchResult searchResults;

        public ModulesChooserPanel(UnusedReferencesSearchResult searchResults)
        {
            this.searchResults = searchResults;

            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.ContainerControl, true);
            InitializeComponent();

            InitTree();
            CheckAll();
        }



        private void CheckAll()
        {
            foreach (TreeModelViewNode viewNode in this.treeView.IterateForward(delegate {
                                                                                             return true;
            }))
            {
                TreeModelNode node = this.treeView.ViewToModel(viewNode);
                if (node != null)
                {
                    this.treeView.SetNodeState(node, CheckState.Checked);
                }
            }
        }


        private void InitTree()
        {
            using (LayoutSuspender suspender = new LayoutSuspender(this))
            {
                this.treeView = new TreeModelViewChecked();
                this.treeView.BeginInit();
                this.treeView.Appearance.Empty.BackColor = SystemColors.Window;
                this.treeView.Appearance.Empty.Options.UseBackColor = true;
                this.treeView.Appearance.FocusedRow.BackColor = SystemColors.Highlight;
                this.treeView.Appearance.FocusedRow.ForeColor = SystemColors.HighlightText;
                this.treeView.Appearance.FocusedRow.Options.UseBackColor = true;
                this.treeView.Appearance.FocusedRow.Options.UseForeColor = true;
                this.treeView.Appearance.HideSelectionRow.BackColor = SystemColors.Control;
                this.treeView.Appearance.HideSelectionRow.ForeColor = SystemColors.ControlText;
                this.treeView.Appearance.HideSelectionRow.Options.UseBackColor = true;
                this.treeView.Appearance.HideSelectionRow.Options.UseForeColor = true;
                this.treeView.Appearance.Row.BackColor = SystemColors.Window;
                this.treeView.Appearance.Row.ForeColor = SystemColors.WindowText;
                this.treeView.Appearance.Row.Options.UseBackColor = true;
                this.treeView.Appearance.Row.Options.UseForeColor = true;
                this.treeView.Appearance.SelectedRow.BackColor = SystemColors.Highlight;
                this.treeView.Appearance.SelectedRow.ForeColor = SystemColors.HighlightText;
                this.treeView.Appearance.SelectedRow.Options.UseBackColor = true;
                this.treeView.Appearance.SelectedRow.Options.UseForeColor = true;
                this.treeView.BorderStyle = BorderStyles.NoBorder;
                this.treeView.Dock = DockStyle.Fill;
                //        this.treeView.Padding = Dim.TreeControlDockPadding;
                //        this.treeView.Margin = Dim.TreeControlDockPadding;
                this.treeView.Name = "myTreeView";
                this.treeView.OptionsBehavior.AutoNodeHeight = true;
                this.treeView.OptionsBehavior.Editable = false;
                this.treeView.OptionsSelection.EnableAppearanceFocusedCell = true;
                this.treeView.OptionsView.AutoWidth = true;
                this.treeView.OptionsView.ShowColumns = false;
                this.treeView.OptionsView.ShowHorzLines = false;
                this.treeView.OptionsView.ShowIndicator = false;
                this.treeView.OptionsView.ShowRoot = true;
                this.treeView.OptionsView.ShowVertLines = false;
                this.treeView.TabIndex = 3;
                //        this.treeView.DoubleClick += new EventHandler(this.OnTreeControlDoubleClick);
                this.treeView.EndInit();
                this.treeView.Presenter = new StructuredPresenter<TreeModelNode>();
                this.treeView.Presenter.Present<IMenuItemDescriptor>(
                    delegate(IMenuItemDescriptor value, IPresentableItem item, TreeModelNode structureElement,
                             PresentationState state)
                        {
                            item.RichText = value.Text ?? new JetBrains.UI.RichText.RichText("<Untitled>", new TextStyle(FontStyle.Italic));
                            item.Images.Clear();
                            if (value.Icon != null)
                            {
                                item.Images.Add(value.Icon);
                            }
                        });
                base.Controls.Add(this.treeView);
                this.treeView.Model = new TreeDemandModel2(GetItems);

            }
        }

        private IEnumerable GetItems(object datavalue)
        {
           MenuItemDescriptor descriptor = datavalue as MenuItemDescriptor;
           if(descriptor == null)
           {
               List<MenuItemDescriptor> descriptors = new List<MenuItemDescriptor>();
               Assert.CheckNotNull(searchResults, "searchResults");
               Assert.CheckNotNull(searchResults.Projects, "searchResults.Projects");
               foreach (IProject project in searchResults.Projects)
               {
                   
                   MenuItemDescriptor item = new MenuItemDescriptor(project);
                   item.Tag = project;
                   item.Text = project.Name;
                   item.Icon = ProjectModelIconManager.Instance.GetProjectModelElementImage(project);
                   descriptors.Add(item);
                   
               }
               return descriptors;
           }
           else
           {
               if (descriptor.Tag is IProject)
               {
                   List<MenuItemDescriptor> descriptors = new List<MenuItemDescriptor>();
                   foreach (IModule module in searchResults[(IProject)descriptor.Tag])
                   {

                       MenuItemDescriptor item = new MenuItemDescriptor(module);
                       item.Tag = module;
                       item.Text = module.Name;
                       item.Icon = ProjectModelIconManager.Instance.GetProjectModelElementImage(module);
                       descriptors.Add(item);

                   }
                   return descriptors;                    
               }
               else
               return EmptyArray<object>.Instance;
           }
        }

        public event EventHandler WantsResize;

        public void SetLayout(LayoutResult layout)
        {
            
        }

        public Size GetDesiredSize(IGraphicsContainer gc, Size limit)
        {
            return Rects.Constrain(new Size(300,400), limit);

        }


        public bool AutoActivate
        {
            get { return autoActivate; }
            set { autoActivate = value; }
        }
    }
}
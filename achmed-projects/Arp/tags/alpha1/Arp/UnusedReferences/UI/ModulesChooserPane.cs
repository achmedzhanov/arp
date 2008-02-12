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
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Shell.Progress;
using JetBrains.UI;
using JetBrains.UI.Controls;
using JetBrains.UI.PopupMenu;
using JetBrains.UI.RichText;
using JetBrains.UI.TreeView;
using JetBrains.Util;
using JetBrains.Util.DataStructures.TreeModel;

namespace Arp.UnusedReferences.UI
{
    public partial class ModulesChooserPane : UserControl, IConstrainableControl, ICustomPage
    {
        private bool autoActivate;
        private TreeModelViewChecked treeView;
        private readonly DeleteUnusedReferencesWorkflow workflow;

        public ModulesChooserPane(DeleteUnusedReferencesWorkflow workflow)
        {
            this.workflow = workflow;

            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.ContainerControl, true);
            InitializeComponent();

            InitTree();
           
        }


        ///<summary>
        ///
        ///            Event which is raised when the <see cref="P:JetBrains.ReSharper.Refactorings.Workflow.ICustomPage.ContinueEnabled" /> flag changes value
        ///            
        ///</summary>
        ///
        public event EventHandler ContinueEnabledChanged;

        ///<summary>
        ///
        ///            Initializes page. Called before each show of the page which is caused by "Continue" button.
        ///            
        ///</summary>
        ///
        public bool Initialize(IProgressIndicator progressIndicator)
        {
            this.treeView.CreateControl();
            CheckAll();
            return true;
        }

        ///<summary>
        ///
        ///            Used for handling probable modal stage of confirming that the dialog really needs to be shown
        ///            
        ///</summary>
        ///
        public bool ConfirmBeforeShow()
        {
            return true;
        }

        ///<summary>
        ///
        ///            Refreshes internal page pointers. Returns 
        ///<c>false</c> if refresh is unsuccessfull and refactoring should be canceled.
        ///            
        ///</summary>
        ///
        public bool RefreshContents(IProgressIndicator progressIndicator)
        {
            return true;
        }

        ///<summary>
        ///
        ///            Commits page
        ///            
        ///</summary>
        ///
        ///<returns>
        ///Next page if any
        ///</returns>
        ///
        public ICustomPage Commit(IProgressIndicator progressIndicator, CustomPageCommitFlags flags)
        {
            
            List<IModuleReference> referencesToDelete = GetSelectedReferences();

            workflow.RefrencesToDelete = referencesToDelete.ToArray();

            return null;
        }

        private List<IModuleReference> GetSelectedReferences()
        {
            List<IModuleReference> referencesToDelete = new List<IModuleReference>();

            foreach (TreeModelViewNode viewNode in this.treeView.IterateForward(delegate {
                                                                                             return true;
            }))
            {

                TreeModelNode node = this.treeView.ViewToModel(viewNode);
                if (node != null && treeView.GetNodeState(node) == CheckState.Checked)
                {

                    IMenuItemDescriptor descriptor = node.DataValue as IMenuItemDescriptor;

                    if (descriptor == null)
                        continue;

                    IModuleReference reference = descriptor.Tag as IModuleReference;

                    if(reference != null)
                        referencesToDelete.Add(reference);
                }
            }
            return referencesToDelete;
        }

        ///<summary>
        ///
        ///            Gets page title
        ///            
        ///</summary>
        ///
        public string Title
        {
            get
            {
                // TODO use resources
                return "Select References";
            }
        }

        ///<summary>
        ///
        ///            Gets page description
        ///            
        ///</summary>
        ///
        public string Description
        {
            get
            {
                // TODO use resources
                return "Select References";
            }
        }

        ///<summary>
        ///
        ///            Gets UI control
        ///            
        ///</summary>
        ///
        public Control UI
        {
            get
            {
                return this;
            }
        }

        ///<summary>
        ///
        ///            Gets flag indicating whether the Continue button is enabled.
        ///            Note that this is ANDed with parent statuses, that include {not running workflow execution} and {validation passes}.
        ///            This means that you don't have to check the validator. If it's the only thing you can think of — just return 
        ///<c>True</c>.
        ///            
        ///</summary>
        ///
        public bool ContinueEnabled
        {
            get
            {
                return true;
            }
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
           UnusedReferencesSearchResult searchResults = workflow.SearchResults;

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
                   foreach (IModuleReference moduleReference in searchResults[(IProject)descriptor.Tag])
                   {
                       MenuItemDescriptor item = new MenuItemDescriptor(moduleReference);
                       item.Tag = moduleReference;
                       item.Text = moduleReference.Name;
                       item.Icon = ProjectModelIconManager.Instance.GetProjectModelElementImage(moduleReference);
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
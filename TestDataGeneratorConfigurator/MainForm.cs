using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

using LantanaGroup.TestDataGenerator.Shared.Logic;
using LantanaGroup.TestDataGenerator.Shared.Data;
using LantanaGroup.TestDataGenerator.Configurator.Data;

namespace LantanaGroup.TestDataGenerator.Configurator
{
    public partial class MainForm : Form
    {
        private ActionAddControl actionAddControl = new ActionAddControl();
        private ActionRemoveControl actionRemoveControl = new ActionRemoveControl();
        private ActionSetControl actionSetControl = new ActionSetControl();
        private ActionCommentControl actionCommentControl = new ActionCommentControl();

        private string configLocation = string.Empty;
        private string dataSourceLocation = string.Empty;
        private string masterFileLocation = string.Empty;
        private XmlDocument masterFileDoc = null;
        private SampleDataSet dataSet = null;

        private ActionConfiguration config = null;

        TreeNode goodNode = new TreeNode("Good Instances");
        TreeNode badNode = new TreeNode("Bad Instances");

        public MainForm()
        {
            InitializeComponent();

            this.actionAddControl.Location =
                this.actionRemoveControl.Location =
                this.actionSetControl.Location =
                this.actionCommentControl.Location =
                this.actionNoneControl.Location;

            this.actionAddControl.Size =
                this.actionRemoveControl.Size =
                this.actionSetControl.Size =
                this.actionCommentControl.Size =
                this.actionNoneControl.Size;

            this.actionAddControl.Anchor =
                this.actionRemoveControl.Anchor =
                this.actionSetControl.Anchor =
                this.actionCommentControl.Anchor =
                this.actionNoneControl.Anchor;

            this.actionAddControl.XpathFormRequested += new ActionControlBase.XpathFormRequestedHandler(XpathFormRequested);
            this.actionRemoveControl.XpathFormRequested += new ActionControlBase.XpathFormRequestedHandler(XpathFormRequested);
            this.actionSetControl.XpathFormRequested += new ActionControlBase.XpathFormRequestedHandler(XpathFormRequested);
            this.actionCommentControl.XpathFormRequested += new ActionControlBase.XpathFormRequestedHandler(XpathFormRequested);

            this.Controls.AddRange( new Control[] { this.actionAddControl, this.actionRemoveControl, this.actionSetControl, this.actionCommentControl } );
            this.instancesTree.Nodes.AddRange(new TreeNode[] { goodNode, badNode });
            ShowActionControl(this.actionNoneControl, null);
            EnableForm(false);
        }

        BuildXpathForm XpathFormRequested(bool forceElementSelection)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return null;
            }

            Instance currentInstance = this.instancesTree.SelectedNode.Tag as Instance;

            return new BuildXpathForm(this.config.namespaces, this.masterFileDoc, currentInstance.rootContext, forceElementSelection);
        }

        #region Actions

        private void ShowActionControl(Control control, ActionDisplay actionDisplayObject)
        {
            if (actionDisplayObject != null)
            {
                actionDisplayObject.PropertyChanged += new PropertyChangedEventHandler(ActionPropertyChanged);
            }

            this.actionAddControl.Visible = this.actionAddControl == control ? true : false;
            this.actionRemoveControl.Visible = this.actionRemoveControl == control ? true : false;
            this.actionSetControl.Visible = this.actionSetControl == control ? true : false;
            this.actionCommentControl.Visible = this.actionCommentControl == control ? true : false;
            this.actionNoneControl.Visible = this.actionNoneControl == control ? true : false;

            if (control == this.actionAddControl)
            {
                this.actionAddControl.ActionDisplayObject = actionDisplayObject;
                this.actionAddControl.Focus();
            }
            else if (control == this.actionRemoveControl)
            {
                this.actionRemoveControl.ActionDisplayObject = actionDisplayObject;
                this.actionRemoveControl.Focus();
            }
            else if (control == this.actionSetControl)
            {
                this.actionSetControl.ActionDisplayObject = actionDisplayObject;
                this.actionSetControl.Focus();
            }
            else if (control == this.actionCommentControl)
            {
                this.actionCommentControl.ActionDisplayObject = actionDisplayObject;
                this.actionCommentControl.Focus();
            }
        }

        void ActionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //this.actionsBinding.ResetBindings(false);
        }

        private void AddActionMenuItemClicked(object sender, EventArgs e)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return;
            }

            Instance cInstance = this.instancesTree.SelectedNode.Tag as Instance;

            if (sender == this.addActionAddMenuItem || sender == this.contextAddActionAddMenuItem)
            {
                InstanceActionAdd actionAdd = new InstanceActionAdd();
                cInstance.Items.Add(actionAdd);

                ShowActionControl(this.actionAddControl, new ActionAddDisplay(actionAdd));
            }
            else if (sender == this.addActionRemoveMenuItem || sender == this.contextAddActionRemoveMenuItem)
            {
                InstanceActionRemove actionRemove = new InstanceActionRemove();
                cInstance.Items.Add(actionRemove);

                ShowActionControl(this.actionRemoveControl, new ActionRemoveDisplay(actionRemove));
            }
            else if (sender == this.addActionSetMenuItem || sender == this.contextAddActionSetMenuItem)
            {
                InstanceActionSet actionSet = new InstanceActionSet();
                cInstance.Items.Add(actionSet);

                ShowActionControl(this.actionSetControl, new ActionSetDisplay(actionSet));
            }
            else if (sender == this.addActionCommentMenuItem || sender == this.contextAddActionCommentMenuItem)
            {
                InstanceActionComment actionComment = new InstanceActionComment();
                cInstance.Items.Add(actionComment);

                ShowActionControl(this.actionCommentControl, new ActionCommentDisplay(actionComment));
            }

            this.actionsBinding.ResetBindings(false);
            this.actionsList.SelectedIndex = this.actionsList.Items.Count - 1;
        }

        private void ActionSelectionChanged(object sender, EventArgs e)
        {
            if (this.actionsList.SelectedIndex < 0)
            {
                ShowActionControl(this.actionNoneControl, null);
                return;
            }

            InstanceActionAdd cActionAdd = this.actionsList.SelectedItem as InstanceActionAdd;
            InstanceActionRemove cActionRemove = this.actionsList.SelectedItem as InstanceActionRemove;
            InstanceActionComment cActionComment = this.actionsList.SelectedItem as InstanceActionComment;
            InstanceActionSet cActionSet = this.actionsList.SelectedItem as InstanceActionSet;

            if (cActionAdd != null)
            {
                ShowActionControl(this.actionAddControl, new ActionAddDisplay(cActionAdd));
            }
            else if (cActionRemove != null)
            {
                ShowActionControl(this.actionRemoveControl, new ActionRemoveDisplay(cActionRemove));
            }
            else if (cActionComment != null)
            {
                ShowActionControl(this.actionCommentControl, new ActionCommentDisplay(cActionComment));
            }
            else if (cActionSet != null)
            {
                ShowActionControl(this.actionSetControl, new ActionSetDisplay(cActionSet));
            }
        }

        private void RemoveActionClicked(object sender, EventArgs e)
        {
            if (this.actionsBinding.Current == null)
            {
                return;
            }

            this.actionsBinding.Remove(this.actionsBinding.Current);
            ActionSelectionChanged(this.actionsBinding, EventArgs.Empty);
        }

        private void MoveActionClicked(object sender, EventArgs e)
        {
            if (this.actionsBinding.Current == null)
            {
                return;
            }

            bool moveUp = sender == this.moveUpToolStripMenuItem;
            List<object> actions = this.actionsBinding.DataSource as List<object>;
            object currentAction = this.actionsBinding.Current;
            int currentActionIndex = actions.IndexOf(currentAction);
            int newActionIndex = moveUp ? currentActionIndex - 1 : currentActionIndex + 1;

            // Make sure we can move the action in the direction desired.
            if ((moveUp && currentActionIndex == 0) || (!moveUp && currentActionIndex == actions.Count - 1))
            {
                return;
            }

            actions.Remove(currentAction);
            actions.Insert(newActionIndex, currentAction);
            this.actionsBinding.ResetBindings(false);
            this.actionsList.SelectedIndex = newActionIndex;
        }

        #endregion

        #region File Menu

        /// <summary>
        /// Loads the master file document into an XmlDocument instance so that it
        /// can be later passed to the BuildXpathForm
        /// </summary>
        private void LoadExtraDocuments()
        {
            try
            {
                this.masterFileDoc = new XmlDocument();
                this.masterFileDoc.Load(this.masterFileLocation);
            }
            catch
            {
                MessageBox.Show("The master file is not a valid XML file");
            }

            try
            {
                this.dataSet = SampleDataSetRetriever.GetFromXLS(new FileInfo(this.dataSourceLocation));
            }
            catch { }

            try
            {
                this.dataSet = SampleDataSetRetriever.GetFromOdbc("Driver={Microsoft Excel Driver (*.xls)};Dbq=" + this.dataSourceLocation);
            }
            catch { }

            this.filterSpecificSectionDropDown.Items.Clear();

            if (this.dataSet != null)
            {
                List<string> sections = this.dataSet.Sections.Keys.ToList();
                this.filterSpecificSectionDropDown.Items.AddRange(sections.ToArray());
            }
        }

        /// <summary>
        /// Occurs when the File menu's "New Config" menu item is clicked.
        /// Opens the NewConfigForm and gathers input from the user required for a new config file.
        /// If a config is currently loaded, it is closed first.
        /// Several namespaces are added to the new instance by default.
        /// The new instance is saved right away.
        /// </summary>
        private void NewConfigurationClicked(object sender, EventArgs e)
        {
            NewConfigForm ncf = new NewConfigForm();

            if (ncf.ShowDialog() == DialogResult.OK)
            {
                if (this.config != null)
                {
                    this.CloseConfigClicked(sender, e);
                }

                this.config = new ActionConfiguration();
                this.config.namespaces = new List<ActionConfigurationNamespace>() {
                    new ActionConfigurationNamespace()
                    {
                        prefix = "cda",
                        uri = "urn:hl7-org:v3"
                    }};
                this.config.namespaces.Add(new ActionConfigurationNamespace()
                {
                    prefix = "xsi",
                    uri = "http://www.w3.org/2001/XMLSchema-instance"
                });

                this.configLocation = ncf.ConfigLocation;
                this.masterFileLocation = ncf.MasterFileLocation;
                this.dataSourceLocation = ncf.DataSourceLocation;

                LoadExtraDocuments();

                this.Text = string.Format("{0} ({1})", this.Text, new FileInfo(this.configLocation).Name);
                SaveClicked(sender, e);
                EnableForm(true);
            }
        }

        private void OpenConfigurationClicked(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML Files (*.xml)|*.xml";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.configLocation = ofd.FileName;
                    this.config = ConfigurationFileParser.ParseConfigurationFile(
                        new FileInfo(ofd.FileName), 
                        out this.dataSourceLocation, 
                        out this.masterFileLocation);

                    if (string.IsNullOrEmpty(this.masterFileLocation) || string.IsNullOrEmpty(this.dataSourceLocation))
                    {
                        MessageBox.Show("The master file and/or data-source file is invalid. Please select a new one.");

                        NewConfigForm ncf = new NewConfigForm(
                            ofd.FileName,
                            this.masterFileLocation == null ? string.Empty : this.masterFileLocation,
                            this.dataSourceLocation == null ? string.Empty : this.dataSourceLocation);

                        if (ncf.ShowDialog() != DialogResult.OK)
                        {
                            CloseConfigClicked(sender, e);
                            return;
                        }

                        this.masterFileLocation = ncf.MasterFileLocation;
                        this.dataSourceLocation = ncf.DataSourceLocation;
                    }

                    LoadExtraDocuments();

                    this.namespacesList.DataSource = this.config.namespaces;

                    this.Text = string.Format("Test Data Generator - Configurator ({0})", new FileInfo(this.configLocation).Name);
                    ShowActionControl(this.actionNoneControl, null);
                    DisplayInstances();
                    EnableForm(true);
                    EnableInstance(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors occurred while attmepting to load the config: " + ex.Message, "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAsClicked(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files (*.xml)|*.xml";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.configLocation = sfd.FileName;

                SaveClicked(sender, e);
            }
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            this.actionAddControl.Save();
            this.actionCommentControl.Save();
            this.actionSetControl.Save();
            this.actionRemoveControl.Save();

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ActionConfiguration));
                string outputContent = string.Empty;

                using (StringWriter sw = new StringWriter())
                {
                    serializer.Serialize(sw, this.config);
                    outputContent = sw.ToString();
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(outputContent);

                string saveDataSource = Shared.Helper.MakeRelativePath(this.configLocation, this.dataSourceLocation);
                string saveMasterFile = Shared.Helper.MakeRelativePath(this.configLocation, this.masterFileLocation);

                doc.AppendChild(doc.CreateProcessingInstruction(ConfigurationFileParser.DataSourcePI, saveDataSource));
                doc.AppendChild(doc.CreateProcessingInstruction(ConfigurationFileParser.MasterFilePI, saveMasterFile));

                doc.Save(this.configLocation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors occurred while attmepting to save the config: " + ex.Message, "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseConfigClicked(object sender, EventArgs e)
        {
            this.config = null;
            this.configLocation =
                this.masterFileLocation =
                this.dataSourceLocation = string.Empty;

            this.goodNode.Nodes.Clear();
            this.badNode.Nodes.Clear();
            this.instanceBinding.DataSource = new InstanceDisplay();
            this.namespacesList.DataSource = null;
            this.actionsBinding.DataSource = null;
            this.inheritsList.DataSource = null;

            this.Text = "Test Data Generator - Configurator";

            EnableForm(false);
        }

        private void ConfigSettingsClicked(object sender, EventArgs e)
        {
            NewConfigForm ncf = new NewConfigForm(
                this.configLocation,
                this.masterFileLocation == null ? string.Empty : this.masterFileLocation,
                this.dataSourceLocation == null ? string.Empty : this.dataSourceLocation);

            if (ncf.ShowDialog() == DialogResult.OK)
            {
                this.masterFileLocation = ncf.MasterFileLocation;
                this.dataSourceLocation = ncf.DataSourceLocation;
            }
        }

        private void EnableForm(bool enabled)
        {
            this.saveAsToolStripMenuItem.Enabled =
                this.saveConfigToolStripMenuItem.Enabled =
                this.closeConfigToolStripMenuItem.Enabled =
                this.instancesTree.Enabled =
                this.instancesToolStripMenuItem.Enabled =
                this.namespacesToolStripMenuItem.Enabled =
                this.generateTestDataMenuItem.Enabled =
                this.configSettingsToolStripMenuItem.Enabled = 
                    enabled;

            EnableInstance(enabled);
        }

        private void EnableInstance(bool enabled)
        {
            this.inheritsToolStripMenuItem.Enabled =
                this.actionsToolStripMenuItem.Enabled =
                this.instanceNameText.Enabled =
                this.instanceApplyToLast.Enabled =
                this.instanceAbstract.Enabled =
                this.instanceContextText.Enabled =
                this.instanceBuildXpathButton.Enabled =
                this.filterGroup.Enabled =
                this.actionsGroup.Enabled =
                this.inheritsGroup.Enabled = enabled;

            this.filterSpecificSectionDropDown.Enabled =
                this.filterSpecificRecordText.Enabled =
                this.filterSpecificRowRadio.Checked;
        }

        #endregion

        #region Instances

        private void DisplayInstances()
        {
            this.goodNode.Nodes.Clear();
            this.badNode.Nodes.Clear();

            foreach (Instance cInstance in this.config.GoodInstances)
            {
                TreeNode newGoodInstanceNode = new TreeNode(cInstance.name);
                newGoodInstanceNode.Tag = cInstance;

                this.goodNode.Nodes.Add(newGoodInstanceNode);
            }

            foreach (Instance cInstance in this.config.BadInstances)
            {
                TreeNode newBadInstanecNode = new TreeNode(cInstance.name);
                newBadInstanecNode.Tag = cInstance;

                this.badNode.Nodes.Add(newBadInstanecNode);
            }
        }

        private void BeforeInstanceSelected(object sender, TreeViewCancelEventArgs e)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return;
            }

            Instance currentInstance = (Instance)this.instancesTree.SelectedNode.Tag;
            e.Cancel = !ValidateInstance(currentInstance);
        }

        private void InstanceSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                this.instanceBinding.DataSource = new InstanceDisplay();
                this.actionsBinding.DataSource = null;
                this.inheritsList.DataSource = null;
                EnableInstance(false);
                return;
            }

            Instance currentInstance = (Instance)e.Node.Tag;

            this.instanceBinding.DataSource = new Data.InstanceDisplay(currentInstance);
            this.instanceBinding.ResetBindings(false);

            this.actionsBinding.DataSource = currentInstance.Items;
            this.actionsBinding.ResetBindings(false);

            this.inheritsList.DataSource = currentInstance.inherit;

            // Reset some controls
            this.actionsList.SelectedIndex = -1;
            this.inheritsList.SelectedIndex = -1;
            ShowActionControl(this.actionNoneControl, null);
            EnableInstance(true);
        }

        private void AddInstanceClicked(object sender, EventArgs e)
        {
            Instance currentInstance = this.instancesTree.SelectedNode != null && this.instancesTree.SelectedNode.Tag != null ? (Instance)this.instancesTree.SelectedNode.Tag : null;
            Instance newInstance = new Instance();
            newInstance.name = "N/A";

            if (this.copyValuesToolStripMenuItem.Checked && currentInstance != null)
            {
                int nameCount = this.config.AllInstances.Count(y => y.name == currentInstance.name) + 1;

                newInstance.name = currentInstance.name + nameCount.ToString();
                newInstance.isAbstract = currentInstance.isAbstract;
                newInstance.createNew = currentInstance.createNew;
                newInstance.rootContext = currentInstance.rootContext;

                if (currentInstance.filter != null)
                    newInstance.filter = Shared.Helper.Clone<InstanceFilter>(currentInstance.filter);

                foreach (object cItem in currentInstance.Items)
                {
                    if (cItem is InstanceActionAdd)
                        newInstance.Items.Add(Shared.Helper.Clone<InstanceActionAdd>(cItem as InstanceActionAdd));
                    else if (cItem is InstanceActionComment)
                        newInstance.Items.Add(Shared.Helper.Clone<InstanceActionComment>(cItem as InstanceActionComment));
                    else if (cItem is InstanceActionRemove)
                        newInstance.Items.Add(Shared.Helper.Clone<InstanceActionRemove>(cItem as InstanceActionRemove));
                    else if (cItem is InstanceActionSet)
                        newInstance.Items.Add(Shared.Helper.Clone<InstanceActionSet>(cItem as InstanceActionSet));
                }
            }
            
            TreeNode newInstanceNode = new TreeNode(newInstance.name);
            newInstanceNode.Tag = newInstance;
            bool isGood = sender == this.goodToolStripMenuItem;

            if (isGood)
            {
                this.config.AddGoodInstance(newInstance);
                this.goodNode.Nodes.Add(newInstanceNode);
            }
            else
            {
                this.config.AddBadInstance(newInstance);
                this.badNode.Nodes.Add(newInstanceNode);
            }

            this.instancesTree.SelectedNode = newInstanceNode;
            this.instanceNameText.Focus();
        }

        private bool ValidateInstance(Instance instance)
        {
            // TODO
            return true;
        }

        private void InstanceNameTextChanged(object sender, EventArgs e)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return;
            }

            Instance currentInstance = (Instance)this.instancesTree.SelectedNode.Tag;
            this.instancesTree.SelectedNode.Text = this.instanceNameText.Text;
        }

        private void RemoveInstanceClicked(object sender, EventArgs e)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return;
            }

            Instance currentInstance = (Instance)this.instancesTree.SelectedNode.Tag;
            TreeNode selectedNode = this.instancesTree.SelectedNode;

            bool isGood = selectedNode.Parent.Text == "Good Instances";

            if (isGood)
            {
                foreach (ActionConfigurationGood cGoodConfig in this.config.good)
                    cGoodConfig.instances.Remove(currentInstance);
            }
            else
            {
                foreach (ActionConfigurationBad cBadConfig in this.config.bad)
                    cBadConfig.instances.Remove(currentInstance);
            }

            selectedNode.Parent.Nodes.Remove(selectedNode);
        }

        private void MoveInstanceClicked(object sender, EventArgs e)
        {
            // TODO
        }

        #endregion

        #region Inherits

        private void AddInheritedClicked(object sender, EventArgs e)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return;
            }

            Instance currentInstance = this.instancesTree.SelectedNode.Tag as Instance;
            InheritSelectForm isf = new InheritSelectForm(this.config.AllInstances);

            if (isf.ShowDialog() == DialogResult.OK)
            {
                InstanceInherit inherit = new InstanceInherit();
                inherit.instanceName = isf.SelectedInstance.name;

                currentInstance.inherit.Add(inherit);

                this.inheritsList.DataSource = null;
                this.inheritsList.DataSource = currentInstance.inherit;
            }
        }

        private void RemoveInheritedClicked(object sender, EventArgs e)
        {
            if (this.inheritsList.SelectedIndex < 0)
            {
                return;
            }

            List<InstanceInherit> inherits = this.inheritsList.DataSource as List<InstanceInherit>;
            inherits.Remove(inherits[this.inheritsList.SelectedIndex]);

            this.inheritsList.DataSource = null;
            this.inheritsList.DataSource = inherits;
        }

        #endregion

        #region Namespaces

        private void AddNamespaceClick(object sender, EventArgs e)
        {
            if (this.config == null)
            {
                return;
            }

            NamespaceForm nf = new NamespaceForm();

            if (nf.ShowDialog() == DialogResult.OK)
            {
                ActionConfigurationNamespace newNamespace = new ActionConfigurationNamespace()
                {
                    prefix = nf.NamespacePrefix,
                    uri = nf.NamespaceUri
                };
                this.config.namespaces.Add(newNamespace);

                this.namespacesList.DataSource = null;
                this.namespacesList.DataSource = this.config.namespaces;
            }
        }

        private void RemoveNamespaceClick(object sender, EventArgs e)
        {
            if (this.namespacesList.SelectedIndex < 0)
            {
                return;
            }

            List<ActionConfigurationNamespace> namespaces = this.namespacesList.DataSource as List<ActionConfigurationNamespace>;
            namespaces.RemoveAt(this.namespacesList.SelectedIndex);

            this.namespacesList.DataSource = null;
            this.namespacesList.DataSource = namespaces;
        }

        #endregion

        private void ListMouseDown(object sender, MouseEventArgs e)
        {
            if (sender is ListBox && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListBox listBox = sender as ListBox;
                Point pt = new Point(e.X, e.Y);
                int newIndex = listBox.IndexFromPoint(pt);

                if (newIndex >= 0)
                {
                    listBox.SelectedIndex = newIndex;
                }
            }
        }

        private void InstanceBuildContextXpathClicked(object sender, EventArgs e)
        {
            if (this.instancesTree.SelectedNode == null || this.instancesTree.SelectedNode.Tag == null)
            {
                return;
            }

            Instance currentInstance = this.instancesTree.SelectedNode.Tag as Instance;
            BuildXpathForm xpathForm = new BuildXpathForm(this.config.namespaces, this.masterFileDoc, null, true);

            if (xpathForm.ShowDialog() == DialogResult.OK)
            {
                currentInstance.rootContext = xpathForm.SelectedXpath;
                this.instanceContextText.Text = currentInstance.rootContext;
            }
        }

        private void FilterApplySpecificChecked(object sender, EventArgs e)
        {
            this.filterSpecificSectionDropDown.Enabled =
                this.filterSpecificRecordText.Enabled =
                this.filterSpecificRowRadio.Checked;
        }

        private void generateTestDataMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.configLocation))
                return;

            string baseDir = new FileInfo(configLocation).Directory.FullName;
            string goodDir = Path.Combine(baseDir, "output-good");
            string badDir = Path.Combine(baseDir, "output-bad");
            string messages = Path.Combine(baseDir, "output-info.xml");

            TestDataGenerator.TestDataGenerationTool generator = new TestDataGenerationTool();
            generator.GenerateTestData(this.configLocation, goodDir, badDir, string.Empty, messages, -1);

            if (File.Exists(messages))
            {
                DisplayGenerationOutput dgo = new DisplayGenerationOutput(messages);
                dgo.ShowDialog();
            }
            else
            {
                MessageBox.Show("Generation complete, without messages to display!");
            }
        }

        #region Databinding Saves

        private void filterSpecificSectionDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filterSpecificSectionDropDown.DataBindings["Text"].WriteValue();
        }

        private void filterSpecificRecordText_TextChanged(object sender, EventArgs e)
        {
            this.filterSpecificRecordText.DataBindings["Text"].WriteValue();
        }

        #endregion

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestDataGenerator.Shared.LogFactory.InitializeLog();
            MessageBox.Show("Cleared log!");
        }
    }
}

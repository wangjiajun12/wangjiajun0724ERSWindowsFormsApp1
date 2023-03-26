using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace wangjiajun0724ERSWindowsFormsApp1
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
        }
        private void statusBar1_PanelClick(object sender, EventArgs e)
        {

        }
        private void PopulateTreeView(object tvRootNode)
        {
            statusBarPanel1.Tag = "Refreshing Employee Code. Please wait...";
            this.Cursor = Cursors.WaitCursor; treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add((TreeNode)tvRootNode);

            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("D:\\nishant\\case 2020\\my repos\\wangjiajun0724ERSWindowsFormsApp1\\wangjiajun0724ERSWindowsFormsApp1\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();//<EmpRecordsData>
                        reader.MoveToElement();//<Ecode

                        reader.MoveToAttribute("Id");//Id="E001"
                        String strVal = reader.Value;//E001

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();

                        }
                        TreeNode EcodeNode = new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);




                    }
                }
                statusBarPanel1.Tag = "Click on an employee code to see their record.";
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void initalizeListView()
        {
            listView1.Clear();
            listView1.Columns.Add("Emplyoee Name", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Gread", 105, HorizontalAlignment.Left);
            listView1.Columns.Add("Salary", 105, HorizontalAlignment.Left);
        }
        protected void PopulateListView(TreeNode crrNode)
        {
            initalizeListView();
            XmlTextReader listrRead = new XmlTextReader("C:\\Users\\ASUS\\source\\repos\\Luqing0812ERSWindowsFormsApp\\Luqing0812ERSWindowsFormsApp\\EmpRec.xml");
            listrRead.MoveToElement();
            while (listrRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new String[4];
                listrRead.MoveToFirstAttribute();//Id="E001"
                strNodeName = listrRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if (strNodeName == strNodePath)
                {
                    ListViewItem lvi;
                    listrRead.MoveToNextAttribute();
                    name = listrRead.Value;//neme "Michael Preey"
                    lvi = listView1.Items.Add(name);

                    listrRead.Read();
                    listrRead.Read();

                    listrRead.MoveToFirstAttribute();
                    doj = listrRead.Value;//DateofJoin="02-02-1999"
                    lvi.SubItems.Add(doj);

                    listrRead.MoveToNextAttribute();
                    gread = listrRead.Value;//Gread="A"
                    lvi.SubItems.Add(gread);

                    listrRead.MoveToNextAttribute();
                    sal = listrRead.Value;//Salary="1750"
                    lvi.SubItems.Add(sal);

                    listrRead.MoveToNextAttribute();
                    listrRead.MoveToElement();
                    listrRead.ReadString();

                }
            }
        }//end

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                statusBarPanel1.Text = "Double Click the Employee Records";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an Emplyoee code to view individual record";
            }
            PopulateListView(currNode);
        }
    }
}

    




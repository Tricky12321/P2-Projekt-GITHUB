using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramTilBusselskab;

namespace ProgramTilBusselskab
{
    public partial class OprettedeObjekter : Form
    {
        public OprettedeObjekter()
        {
            InitializeComponent();
        }

        private void OprettedeObjekter_Load(object sender, EventArgs e)
        {
            TreeNode parent;
            TreeNode child;
            TreeNode subChild;

            foreach (Bus bus in Lists.listWithBusses)
            {
                parent = new TreeNode(bus.ToString());

                child = new TreeNode(bus.Rute.ToString());

                foreach (StoppestedMTid combo in bus.busPassagerDataListe)
                {
                    subChild = new TreeNode(combo.Stop.ToString());

                    foreach (AfPåTidCombi tid in combo.AfPåTidComb)
                    {
                        subChild.Nodes.Add(tid.Tidspunkt.ToString());
                    }
                    child.Nodes.Add(subChild);
                }
                parent.Nodes.Add(child);

                treview_oprettedeObjekter.Nodes.Add(parent);
            }
        }

        private void treview_oprettedeObjekter_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}

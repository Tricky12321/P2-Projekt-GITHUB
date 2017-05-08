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
    public partial class OpretBus : Form
    {
        const int busNameMaxLenght = 200;
        const int busNameMinLenght = 1;
        const int busIDMaxLenght = 100000000;
        const int busIDMinLenght = 0;
        const int capacityMax = 1000;
        const int capacityMin = 0;

        const int timeMin = 0;
        const int hourMax = 23;
        const int minutMax = 59;

        public OpretBus()
        {
            InitializeComponent();
        }

        private void bnt_opretBus_Click(object sender, EventArgs e)
        {
            int capacitySitPlaceholder = 0;
            int capacityStaPlaceholder = 0;
            int busIDplaceholder = 0;

            if (txtbox_busName.Text.Length > busNameMinLenght && txtbox_busName.Text.Length <= busNameMaxLenght)
            {
                if (int.TryParse(txt_busID.Text, out busIDplaceholder) && busIDplaceholder < busIDMaxLenght && busIDplaceholder >= busIDMinLenght)
                {
                    if (!Lists.listWithBusses.Any(x => x.BusID == busIDplaceholder))
                    {
                        if (int.TryParse(txtbox_capacitySit.Text, out capacitySitPlaceholder) && int.TryParse(txtbox_capacityStå.Text, out capacityStaPlaceholder) 
                            && capacitySitPlaceholder >= capacityMin && capacityStaPlaceholder >= capacityMin && capacitySitPlaceholder <= capacityMax && capacityStaPlaceholder <= capacityMax)
                        {
                            Bus busPlaceholder = new Bus(txtbox_busName.Text, busIDplaceholder, capacitySitPlaceholder, capacityStaPlaceholder, (Rute)combox_vælgRute.SelectedItem, stoppestederPlaceholder.ToArray());
                            Lists.listWithBusses.Add(busPlaceholder);

                            MessageBox.Show("Bus er oprettet.");
                            ActiveForm.Close();
                        }
                        else { MessageBox.Show("Der er fejl i kapacitetsværdierne."); }
                    }
                    else { MessageBox.Show("Bus er allerede oprettet med dette busID."); }
                }
                else { MessageBox.Show("Der er fejl i busID'et."); }
            }
            else { MessageBox.Show("Der er fejl i bus navnet'et."); }
        }

        private void OpretBus_Load(object sender, EventArgs e)
        {
            foreach (Rute rute in Lists.listWithRoutes)
            {
                combox_vælgRute.Items.Add(rute);
            }

            bnt_opretBus.Enabled = false;

            lbl_ruteSucces.Visible = false;
        }

        private void btn_refreshStoppesteder_Click(object sender, EventArgs e)
        {
            if (combox_vælgRute.SelectedItem != null)
            {
                Rute placeholder = (Rute)combox_vælgRute.SelectedItem;

                foreach (Stoppested stop in placeholder.AfPåRuteList)
                {
                    combox_vælgStop.Items.Add(stop);
                    lbl_ruteSucces.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Der er ikke valgt en rute.");
            }
        }

        List<AfPåTidCombi> tiderPlaceholder = new List<AfPåTidCombi>();

        private void btn_tilføjTidspunkt_Click_1(object sender, EventArgs e)
        {
            string[] textTime = txtbox_tidspunkt.Text.Split(',', ':', '.');

            if (textTime != null && textTime[0].Length == 2 && textTime[1].Length == 2 && (int.Parse(textTime[0])) <= hourMax && (int.Parse(textTime[1])) <= minutMax && (int.Parse(textTime[0])) >= timeMin && (int.Parse(textTime[1])) >= timeMin)
            {
                AfPåTidCombi tidspunktPlaceholder = new AfPåTidCombi(new Tidspunkt(int.Parse(textTime[0]), int.Parse(textTime[1])));

                if (tiderPlaceholder.Any(x => x.Tidspunkt.hour == tidspunktPlaceholder.Tidspunkt.hour && x.Tidspunkt.minute == tidspunktPlaceholder.Tidspunkt.minute))
                {
                    MessageBox.Show("Tidspunktet er allerede tilføjet.");
                }
                else
                {
                    tiderPlaceholder.Add(tidspunktPlaceholder);

                    lstbox_tidspunkter.Items.Add(tidspunktPlaceholder);
                }
            }
            else
            {
                MessageBox.Show("Der er ikke indtastet et gyldigt tidspunkt.");
            }
            txtbox_tidspunkt.Text = "";
        }

        List<StoppestedMTid> stoppestederPlaceholder = new List<StoppestedMTid>();

        private void btn_tilføjStoppested_Click_1(object sender, EventArgs e)
        {
            StoppestedMTid stopMTidPlaceholder = new StoppestedMTid((Stoppested)combox_vælgStop.SelectedItem, tiderPlaceholder.ToArray());

            if (stoppestederPlaceholder.Any(x => x.Stop.StoppestedName == stopMTidPlaceholder.Stop.StoppestedName))
            {
                MessageBox.Show("Stoppested er allerede tilføjet");
            }
            else
            {
                if (stopMTidPlaceholder.Stop != null)
                {
                    if (tiderPlaceholder.Count > 0)
                    {
                        treeview_stopMTidspunkter.Nodes.Clear();

                        stoppestederPlaceholder.Add(stopMTidPlaceholder);

                        TreeNode parent;
                        foreach (StoppestedMTid stop in stoppestederPlaceholder)
                        {
                            parent = new TreeNode(stop.Stop.ToString());

                            foreach (AfPåTidCombi tid in stop.AfPåTidComb)
                            {
                                parent.Nodes.Add(tid.Tidspunkt.ToString());
                            }
                            treeview_stopMTidspunkter.Nodes.Add(parent);
                        }

                        bnt_opretBus.Enabled = true;

                        lstbox_tidspunkter.Items.Clear();
                        tiderPlaceholder.RemoveAll(x => true);
                    }
                    else{ MessageBox.Show("Tidspunkter ikke valgt."); }
                }
                else{ MessageBox.Show("Stoppested er ikke valgt."); }
            }
        }

        private void lstbox_tidspunkter_DoubleClick_1(object sender, EventArgs e)
        {
            tiderPlaceholder.RemoveAt(lstbox_tidspunkter.SelectedIndex);
            lstbox_tidspunkter.Items.RemoveAt(lstbox_tidspunkter.SelectedIndex);
        }

        private void treeview_stopMTidspunkter_DoubleClick(object sender, EventArgs e)
        {
            if (treeview_stopMTidspunkter.SelectedNode.Parent == null)
            {
                stoppestederPlaceholder.RemoveAt(treeview_stopMTidspunkter.SelectedNode.Index);
            }
            else
            {
                stoppestederPlaceholder.ElementAt(treeview_stopMTidspunkter.SelectedNode.Parent.Index).AfPåTidComb.RemoveAt(treeview_stopMTidspunkter.SelectedNode.Index);
            }

            treeview_stopMTidspunkter.SelectedNode.Remove();
        }
    }
}

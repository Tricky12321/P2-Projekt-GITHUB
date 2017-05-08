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
    public partial class OpretRute : Form
    {
        const int routeNameMaxLenght = 200;
        const int routeNameMinLenght = 1;
        const int routeIDMaxLenght = 100000000;
        const int routeIDMinLenght = 0;

        List<Stoppested> listStops = new List<Stoppested>();

        public OpretRute()
        {
            InitializeComponent();
        }

        private void OpretRute_Load(object sender, EventArgs e)
        {
            combox_chooseStops.Items.Clear();
            foreach (Stoppested stop in Lists.listWithStops)
            {
                combox_chooseStops.Items.Add(stop);
            }
        }

        private void btn_addStop_Click(object sender, EventArgs e)
        {
            if (!listStops.Any(x => x.StoppestedID == ((Stoppested)combox_chooseStops.SelectedItem).StoppestedID))
            {
                lstbox_Ruter.Items.Add(combox_chooseStops.SelectedItem);
                listStops.Add((Stoppested)combox_chooseStops.SelectedItem);
            }
            else
            {
                MessageBox.Show("Stoppested allerede tilføjet til rute.");
            }
        }

        private void btn_opretRute_Click(object sender, EventArgs e)
        {
            int ruteIDPlaceholder = 0;

            if (txtbox_ruteName.Text.Length > routeNameMinLenght && txtbox_ruteName.Text.Length <= routeNameMaxLenght)
            {
                if (int.TryParse(txtbox_ruteID.Text, out ruteIDPlaceholder) && ruteIDPlaceholder < routeIDMaxLenght && ruteIDPlaceholder >= routeIDMinLenght)
                {
                    if (!Lists.listWithRoutes.Any(x => x.RuteID == ruteIDPlaceholder))
                    {
                        Rute newRute = new Rute(txtbox_ruteName.Text, int.Parse(txtbox_ruteID.Text), listStops.ToArray());

                        Lists.listWithRoutes.Add(newRute);

                        RealClient TestClient = new RealClient();
                        TestClient.SendObject(newRute, typeof(Rute));

                        MessageBox.Show("Rute oprettet og uploadet til database.");
                        ActiveForm.Close();
                    }
                    else { MessageBox.Show("Rute med dette ruteID er allerede oprettet."); }
                }
                else { MessageBox.Show("Der er fejl i ruteID'et."); }
            }
            else { MessageBox.Show("Der er fejl i busnavnet."); }
        }

        private void lstbox_Ruter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lstbox_Ruter.SelectedIndex;
            listStops.RemoveAt(index);

            lstbox_Ruter.Items.RemoveAt(index);
        }
    }
}

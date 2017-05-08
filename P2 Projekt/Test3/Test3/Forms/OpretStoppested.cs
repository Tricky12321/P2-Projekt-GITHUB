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
    public partial class OpretStoppested : Form
    {
        const int stopNameMinLenght = 0;
        const int stopNameMaxLenght = 200;
        const int stopIDMaxLenght = 100000000;
        const int stopIDMinLenght = 0;


        List<AfPåTidCombi> listTimes = new List<AfPåTidCombi>();
        public OpretStoppested()
        {
            InitializeComponent();
        }

        private void bnt_opretStop_Click(object sender, EventArgs e)
        {
            int stopIDPlaceholder = 0;

            if (txtbox_stopName.Text.Length > stopNameMinLenght && txtbox_stopName.Text.Length <= stopNameMaxLenght)
            {
                if (int.TryParse(txtbox_stoppestedID.Text, out stopIDPlaceholder) && stopIDPlaceholder < stopIDMaxLenght && stopIDPlaceholder >= stopIDMinLenght)
                {
                    if (!Lists.listWithStops.Any(x => x.StoppestedID == stopIDPlaceholder))
                    {
                        double xCoordinate = 0;
                        double yCoordinate = 0;

                        string[] textCoordinate = txtbox_coordinate.Text.Split(',');

                        if (double.TryParse(textCoordinate[0], out xCoordinate) && double.TryParse(textCoordinate[1], out yCoordinate))
                        {
                            Stoppested StopSted = new Stoppested(txtbox_stopName.Text, int.Parse(txtbox_stoppestedID.Text), new GPS(xCoordinate, yCoordinate));
                            Lists.listWithStops.Add(StopSted);
                            RealClient TestClient = new RealClient();
                            TestClient.SendObject(StopSted, typeof(Stoppested));
                            MessageBox.Show("Stoppested oprettet og oprettet til database.");
                            ActiveForm.Close();
                        }
                        else { MessageBox.Show("Der er fejl i indtastningen af koordinaterne."); }
                    }
                    else { MessageBox.Show("Der er allerede oprettet et stoppested med dette stoppestedsID."); }
                }
                else { MessageBox.Show("Der er fejl ind indtastningen af stoppestedsID'et."); }
            }
            else { MessageBox.Show("Der er fejl i indtastnigen af stoppestednavn"); }
        }
    }
}

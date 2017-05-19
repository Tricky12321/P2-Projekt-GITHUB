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
        private const int stopNameMinLength = 0;
        private const int stopNameMaxLength = 200;
        private const int stopIDMaxLength = 100000000;
        private const int stopIDMinLength = 0;

        List<AfPåTidCombi> listTimes = new List<AfPåTidCombi>();
        public OpretStoppested()
        {
            InitializeComponent();
        }

        private void bnt_opretStop_Click(object sender, EventArgs e)
        {
            int stopIDPlaceholder = 0;

            if (txtbox_stopName.Text.Length > stopNameMinLength && txtbox_stopName.Text.Length <= stopNameMaxLength)
            {
                if (int.TryParse(txtbox_stoppestedID.Text, out stopIDPlaceholder) && stopIDPlaceholder < stopIDMaxLength && stopIDPlaceholder >= stopIDMinLength)
                {
                    if (!Lists.listWithStops.Any(x => x.StoppestedID == stopIDPlaceholder))
                    {
                        double xCoordinate = 0;
                        double yCoordinate = 0;

                        string[] textCoordinate = txtbox_coordinate.Text.Split(',');

                        if (double.TryParse(textCoordinate[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out xCoordinate) && double.TryParse(textCoordinate[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out yCoordinate))
                        {
                            Stoppested newStoppested = new Stoppested(txtbox_stopName.Text, int.Parse(txtbox_stoppestedID.Text), new GPS(xCoordinate, yCoordinate));
                            Lists.listWithStops.Add(newStoppested);

                            RealClient TestClient = new RealClient();
                            TestClient.SendObject(newStoppested, typeof(Stoppested));

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

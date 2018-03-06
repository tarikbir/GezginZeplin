using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GezginZeplin
{
    public partial class Menu : Form
    {
        private void drawPin(int x, int y, int plate)
        {
            PictureBox pin = new PictureBox
            {
                Name = "pin" + ++pinCount,
                Size = new Size(24, 24),
                Location = new Point(x, y),
                Visible = true
            };

            Label pinPlate = new Label
            {
                Name = "pinLabel" + pinCount,
                Location = new Point(2, 4),
                Visible = true,
                Text = plate.ToString("D2"),
                AutoSize = true
            };

            mapImage.Controls.Add(pin);
            pin.BackColor = Color.Transparent;
            pin.Image = Properties.Resources.pin;
            pin.Controls.Add(pinPlate);
        }

        private Point convertCoord(double lng, double lat)
        {
            int mapX = 974;
            int mapY = 427;
            double minLat = 35.4849;
            double minLng = 26.0549;
            double maxLat = 42.1104;
            double maxLng = 44.8277;

            int x = (int)(((lng - minLng) / (maxLng - minLng)) * (mapX - 1));
            int y = mapY - (int)(((lat - minLat) / (maxLat - minLat)) * (mapY - 1));

            return new Point(x, y);
        }

        //General variables
        public int startPlate=1;
        public int endPlate=1;
        public int passenger=5;
        public static int pinCount = 0;
        public static bool mapUsedOnce = false;
        private Graphics g;
        private Pen pen = new Pen(Color.DarkOliveGreen, 2f);

        public Menu()
        {
            InitializeComponent();
            for (int i = 0; i < Program.cityArray.Length; i++)
            {
                Point p = convertCoord(Program.cityArray[i].city.lng, Program.cityArray[i].city.lat);
                drawPin(p.X, p.Y, i+1);
            }
            mapImage.ContextMenuStrip = contextMenuStrip1;
            Console.WriteLine("DEBUG: Menu initialized.");
        }

        private void drawMap(LinkedList<Node> route)
        {
            //Redraw pins that are not lit with the selection and clear any lines already drawn.
            mapImage.Refresh();
            if (route == null) return;
            for (int i = 1; i <= Program.cityArray.Length; i++)
            {
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + i];
                pin.Image = Properties.Resources.pin;
            }
            //Light the new route pins. Connect them with lines in-between.
            int x = 0, y = 0;
            for (int i = 0; i < route.Count; i++)
            {
                Node n = route.ElementAt(i);
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + (n.city.plate)];
                if (i != 0 && i != route.Count - 1)
                {
                    pin.Image = Properties.Resources.pinLit;
                    g.DrawLine(pen, x, y, pin.Location.X + 12, pin.Location.Y + 12);
                }
                else if (i != 0)
                {
                    pin.Image = Properties.Resources.pinLitS;
                    g.DrawLine(pen, x, y, pin.Location.X + 12, pin.Location.Y + 12);
                }
                else
                {
                    pin.Image = Properties.Resources.pinLitE;
                }
                x = pin.Location.X + 12;
                y = pin.Location.Y + 12;
            }
            mapUsedOnce = true; //Makes sure the map has been drawn once to be able to refresh without errors.
        }

        private void buttonDrawRoad_Click(object sender, EventArgs e)
        {
            Program.stopWatch.Restart();                                                    //Restarts time to enable time-calculations.
            g = mapImage.CreateGraphics();                                                  //Readies ability to draw on MAP.
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;                        //Sets pen to dash style so lines look dashed.
            Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);    //Gets two nodes with start, end plates given.
            LinkedList<Node> route = Program.shortestPath(start, end, passenger);           //Generates a short path from start to end.
            drawMap(route);                                                                 //Draws the map with the route given.
            Int64 time = Program.stopWatch.ElapsedMilliseconds;                             //Calls main stopWatch to get elapsed time.
            outputTextBox.Text = String.Format("Process took {0} miliseconds.", time) +
                "\r\nRoute: " + Program.getList(route);                                     //Updates the outputBox to show info to user.
        }

        private void buttonCalculateSol_Click(object sender, EventArgs e)
        {
            Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);
            Program.stopWatch.Restart();
            //First problem solution (passenger)
            int firstQ = 5;
            double price = 100d, maxProfit = 0d, fuelCostPerKM = 10d;
            for (int i = 5; i <= 50; i++)
            {
                //For first problem i = passenger, since passenger is varied.
                double income = price * i;
                LinkedList<Node> routeT = Program.shortestPath(start, end, i);
                if (routeT == null) return;
                double outcome = fuelCostPerKM * Program.distanceAsKM(routeT);
                double profit = income - outcome;
                if (maxProfit < profit)
                {
                    maxProfit = profit;
                    firstQ = i;
                }
                //TODO: File output.
                //Console.WriteLine("DEBUG: For "+i+" passengers income is: "+gain+" and price is: "+tempPrice);
            }
            //Second problem solution (price)
            maxProfit = 0d;
            for (int i = 10; i <= 50; i = i + 10)
            {
                //For second problem i*10 = passenger, since passenger is varied.
                LinkedList<Node> routeT = Program.shortestPath(start, end, i);
                if (routeT == null) return;
                double outcome = fuelCostPerKM * Program.distanceAsKM(routeT);
                double income = (50 - outcome) / 100 + outcome;
                double variedPrice = income / i;
                //TODO: File output.
                //Console.WriteLine("DEBUG: For " + i + " passengers income is: " + income + " and price is: " + variedPrice);
            }
            Int64 time = Program.stopWatch.ElapsedMilliseconds;
            outputTextBox.Text = String.Format("Process took {0} miliseconds.", time)+"\r\nSolution files have been updated.";
        }

        private void textBoxCityStart_TextChanged(object sender, EventArgs e)
        {
            bool errorGenerated=false;
            try
            {
                startPlate = Int32.Parse(textBoxCityStart.Text);
                if (startPlate <= 0 || startPlate > 81) throw new Exception(startPlate + " is not a valid plate number.");
            }
            catch(Exception E)
            {
                errorGenerated = true;
                outputTextBox.Text = "Error: Start city ("+E.Message+")";
            }
            if (!errorGenerated)
            {
                outputTextBox.Text = "Set start city plate to " + startPlate + ".";
            }
        }

        private void textBoxCityEnd_TextChanged(object sender, EventArgs e)
        {
            bool errorGenerated = false;
            try
            {
                endPlate = Int32.Parse(textBoxCityEnd.Text);
                if (endPlate <= 0 || endPlate > 81) throw new Exception(endPlate + " is not a valid plate number.");
            }
            catch (Exception E)
            {
                errorGenerated = true;
                outputTextBox.Text = "Error: End city (" + E.Message + ")";
            }
            if (!errorGenerated)
            {
                outputTextBox.Text = "Set end city plate to " + endPlate + ".";
            }
        }

        private void textBoxPassengers_TextChanged(object sender, EventArgs e)
        {
            bool errorGenerated = false;
            try
            {
                passenger = Int32.Parse(textBoxPassengers.Text);
                if (passenger < 5 || passenger > 50) throw new Exception("Passenger count must be between 5 and 50.");
            }
            catch (Exception E)
            {
                errorGenerated = true;
                outputTextBox.Text = "Error: Passengers (" + E.Message + ")";
            }
            if (!errorGenerated) outputTextBox.Text = "Set passengers to " + passenger + ".";
        }

        private void textBoxCityStart_Click(object sender, EventArgs e)
        {
            textBoxCityStart.Text = "";
        }

        private void textBoxCityEnd_Click(object sender, EventArgs e)
        {
            textBoxCityEnd.Text = "";
        }

        private void textBoxPassengers_Click(object sender, EventArgs e)
        {
            textBoxPassengers.Text = "";
        }

        private void textBoxCityStart_Leave(object sender, EventArgs e)
        {
            textBoxCityStart.Text = startPlate.ToString("D2");
        }

        private void textBoxCityEnd_Leave(object sender, EventArgs e)
        {
            textBoxCityEnd.Text = endPlate.ToString("D2");
        }

        private void textBoxPassengers_Leave(object sender, EventArgs e)
        {
            textBoxPassengers.Text = passenger.ToString("D2");
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapUsedOnce)
            {
                Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);
                LinkedList<Node> route = Program.shortestPath(start, end, passenger);
                drawMap(route);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapUsedOnce)
            {
                mapImage.Refresh();
                for (int i = 1; i <= Program.cityArray.Length; i++)
                {
                    PictureBox pin = (PictureBox)mapImage.Controls["pin" + i];
                    pin.Image = Properties.Resources.pin;
                }
                startPlate = 1;
                textBoxCityStart.Text = startPlate.ToString("D2");
                endPlate = 1;
                textBoxCityEnd.Text = endPlate.ToString("D2");
                passenger = 5;
                textBoxPassengers.Text = passenger.ToString("D2");
                outputTextBox.Text = "Cleared.";
            }
        }
    }
}

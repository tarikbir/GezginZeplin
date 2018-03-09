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
        private static int pinCount = 0;
        private static bool mapUsedOnce = false;
        private static bool calculateSolutionOrder = false;
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
            g = mapImage.CreateGraphics(); //Readies ability to draw on MAP.
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; //Sets pen to dash style so lines look dashed.
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
                pen.ResetTransform();
                Node n = route.ElementAt(i);
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + (n.city.plate)];
                //Console.WriteLine("DRAWING: Plate: " + n.city.plate + " x: " + pin.Location.X + "(" + x + ") y: " + pin.Location.Y + "(" + y + ").");
                if (i != 0 && i != route.Count-1)
                {
                    pin.Image = Properties.Resources.pinLit;
                    g.DrawLine(pen, x, y, pin.Location.X + 12, pin.Location.Y + 12);
                }
                else if (i != 0)
                {
                    pin.Image = Properties.Resources.pinLitRed;
                    g.DrawLine(pen, x, y, pin.Location.X + 12, pin.Location.Y + 12);
                }
                else
                {
                    pin.Image = Properties.Resources.pinLitGreen;
                }
                x = pin.Location.X + 12;
                y = pin.Location.Y + 12;
            }
            mapUsedOnce = true; //Makes sure the map has been drawn once to be able to refresh without errors.
        }

        private void buttonDrawRoad_Click(object sender, EventArgs e)
        {
            Program.stopWatch.Restart();                                                    //Restarts time to enable time-calculations.
            Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);    //Gets two nodes with start, end plates given.
            LinkedList<Node> route = Program.shortestPath(start, end, passenger);           //Generates a short path from start to end.
            drawMap(route);                                                                 //Draws the map with the route given.
            Int64 time = Program.stopWatch.ElapsedMilliseconds;                             //Calls main stopWatch to get elapsed time.
            outputTextBox.Text = String.Format("Process took {0} miliseconds.", time) +
                "\r\nRoute: " + Program.getList(route) + "\r\nTotal Distance: " + Program.distanceZeppelin(route, passenger);
        }

        private void buttonCalculateSol_Click(object sender, EventArgs e)
        {
            Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);
            Program.stopWatch.Restart();
            calculateSolutionOrder = !calculateSolutionOrder; //First click first question, second click second question.
            //First problem solution (passenger)
            if (calculateSolutionOrder)
            {
                outputTextBox.Text = "";
                int minimumPassenger = 5;
                double price = 100d, maxProfit=Double.MinValue, fuelCost = 10d;
                using (StreamWriter sw = new StreamWriter("firstQ.txt", false))
                {
                    for (int p = 5; p <= 50; p++)
                    {
                        //For first problem i = passenger, since passenger is varied.
                        double income = price * p;
                        LinkedList<Node> routeT = Program.shortestPath(start, end, p);
                        if (routeT == null)
                        {
                            sw.WriteLine("For " + p + " passengers, the flight path cannot be found.");
                        }
                        else
                        {
                            double expense = fuelCost * Program.distanceZeppelin(routeT, p);
                            double profit = income - expense;
                            if (maxProfit <= profit)
                            {
                                maxProfit = profit;
                                minimumPassenger = p;
                            }
                            sw.WriteLine("For " + p + " passengers, the flight costs " + profit + ".");
                        }
                    }
                }
                LinkedList<Node> routeMin = Program.shortestPath(start, end, minimumPassenger);
                drawMap(routeMin);
                double distance = Program.distanceZeppelin(routeMin, minimumPassenger);
                Int64 time = Program.stopWatch.ElapsedMilliseconds;
                outputTextBox.Text = String.Format("Process took {0} miliseconds.", time) + "\r\nFirst Question:\r\nTo achieve maximum profit, " +
                    "how many passengers can be carried with a fixed cost? (at least 5 and at most 50 can be carried)\r\nDrawing the map for most profitable one.\r\n" +
                    "Minimum Passengers: " + minimumPassenger + " passengers with shown route.\r\nDistance: " + distance + " kms.\r\n" +
                    "Maximum Profit: ₺" + String.Format("{0:0.0#}", (price * minimumPassenger - fuelCost * distance));
            }
            else //Second problem solution (price)
            {
                outputTextBox.Text = "";
                int maximumPassenger = 5;
                double bestCost = Double.MinValue, fuelCost = 10d;
                using (StreamWriter sw = new StreamWriter("secondQ.txt", false))
                {
                    for (int p = 5; p <= 50; p++)
                    {
                        LinkedList<Node> routeT = Program.shortestPath(start, end, p);
                        if (routeT == null)
                        {
                            sw.WriteLine("For " + p + " passengers, the flight path cannot be found.");
                        }
                        else
                        {
                            double outcome = fuelCost * Program.distanceZeppelin(routeT, p);
                            double income = (50 - outcome) / 100 + outcome;
                            double variedPrice = income / p;
                            if (bestCost <= variedPrice) { bestCost = variedPrice; maximumPassenger = p; }
                            sw.WriteLine("For " + p + " passengers ₺" + String.Format("{0:0.0#}", variedPrice) + ".");
                        }                        
                    }
                }
                LinkedList<Node> routeMin = Program.shortestPath(start, end, maximumPassenger);
                drawMap(routeMin);
                double distance = Program.distanceZeppelin(routeMin, maximumPassenger);
                Int64 time = Program.stopWatch.ElapsedMilliseconds;
                outputTextBox.Text = String.Format("Process took {0} miliseconds.", time) + "\r\nSecond Question:\r\nTo achieve %50 profit, " +
                    "how much should the cost be? (for 10,20,30,40,50 passengers)\r\nDrawing the map for the best result.\r\n" +
                    "Cost for one: ₺" + bestCost + "\r\nFor " + maximumPassenger + " passengers.\r\nDistance: " + distance + " kms.";
            }
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

        private void clearMap()
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

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearMap();
        }
    }
}

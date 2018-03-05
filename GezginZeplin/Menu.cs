using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            /*
            lng -= 25.9872; //Turkey longitude start
            lat = 35.7465 - lat; //Turkey latitude start
            double mapLongitude = 44.8519 - lng, mapLatitude = lat - 25.9872;
            // Map x & y hard coded.
            int x = (int)(974 * (lng / mapLongitude));
            int y = (int)(427 * (lat / mapLatitude));
            */
            int y = 427 - (int)((lat - 35.7465) * 66.394);
            int x = (int)((lng - 25.9872) * 51.6308);
            return new Point(x, y);
        }

        //General variables
        public int startPlate=1;
        public int endPlate=1;
        public int passenger=5;
        public static int pinCount = 0;
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
            Console.WriteLine("DEBUG: Menu initialized.");
        }

        private void buttonDrawRoad_Click(object sender, EventArgs e)
        {
            //Initialize graphics for line drawing and calculate the route.
            g = mapImage.CreateGraphics();
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);
            LinkedList<Node> route = Program.shortestPath(start, end, passenger);
            
            //Redraw pins that are not lit with the selection and clear any lines already drawn.
            mapImage.Refresh();
            for (int i = 1; i <= Program.cityArray.Length; i++)
            {
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + i];
                pin.Image = Properties.Resources.pin;
            }
            //Light the new route pins. Connect them with lines in-between.
            int x=0, y=0;
            for (int i = 0; i < route.Count; i++)
            {
                Node n = route.ElementAt(i);
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + (n.city.plate)];
                if (i != 0 && i != route.Count - 1)
                {
                    pin.Image = Properties.Resources.pinLit;
                    g.DrawLine(pen, x, y, pin.Location.X+12, pin.Location.Y+12);
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
                x = pin.Location.X+12;
                y = pin.Location.Y+12;
            }
            outputTextBox.Text = "Route: "+Program.getList(route);
        }

        private void buttonCalculateSol_Click(object sender, EventArgs e)
        {
            Node start = Program.findCity(startPlate), end = Program.findCity(endPlate);
            //First problem solution (passenger)
            int firstQ = 5;
            double price = 100d, maxProfit = 0d, fuelCostPerKM = 10d;
            for (int i = 5; i <= 50; i++)
            {
                //For first problem i = passenger, since passenger is varied.
                double income = price * i;
                LinkedList<Node> routeT = Program.shortestPath(start, end, i);
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
                double outcome = fuelCostPerKM * Program.distanceAsKM(routeT);
                double income = (50 - outcome) / 100 + outcome;
                double variedPrice = income / i;
                //TODO: File output.
                //Console.WriteLine("DEBUG: For " + i + " passengers income is: " + income + " and price is: " + variedPrice);
            }

            outputTextBox.Text = "Solution files have been updated.";
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

        private void mapImage_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}

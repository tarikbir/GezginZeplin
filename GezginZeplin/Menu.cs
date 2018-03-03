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
                int y =  427 - (int)((Program.cityArray[i].city.lat-35.7465)*66.394);
                int x = (int)((Program.cityArray[i].city.lng-25.9872)*51.6308);
                drawPin(x, y, i+1);
                //Console.WriteLine(Program.cityArray[i].city.plate + " is drawn at (" + x + "," + y + ").");
            }
            Console.WriteLine("Menu initialized.");
        }

        private void buttonDrawRoad_Click(object sender, EventArgs e)
        {
            g = mapImage.CreateGraphics();
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            LinkedList<Node> route = Program.shortestPath(Program.findCity(startPlate), Program.findCity(endPlate), passenger);
            outputTextBox.Text = "Route: " + Program.getList(route);

            //Redraw pins that are not lit with the selection.
            for (int i = 1; i <= Program.cityArray.Length; i++)
            {
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + i];
                pin.Image = Properties.Resources.pin;
            }
            //Light the new route pins. Draw the lines between them
            int x=0, y=0;
            for (int i = 0; i < route.Count; i++)
            {
                Node n = route.ElementAt(i);
                PictureBox pin = (PictureBox)mapImage.Controls["pin" + (n.city.plate)];
                pin.Image = Properties.Resources.pinLit;
                if (i!=0)
                {
                    g.DrawLine(pen, x, y, pin.Location.X+12, pin.Location.Y+12);
                }
                x = pin.Location.X+12;
                y = pin.Location.Y+12;
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

        private void mapImage_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}

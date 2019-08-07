using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Snake
{
    public partial class Form1 : Form
    {
        enum Position
        {
            Left, Right, Up, Down, Stop
        }
        public int x;
        public int y;
        public int count = 1;
        static int ApfelPos = new Random().Next(0, 10) * 10;
        static int ApfelPos1 = new Random().Next(0, 10) * 10;
        public Rectangle Apfel = new Rectangle(new Point(ApfelPos,ApfelPos1), new Size(10, 10)); //später mit form height und width arbeiten
        private bool vollesekunde;
        private Position pos;
        static int ApfelEATEN = 0;
        Point TempPoint; //die position des vorherigen points muss temporär über einen tick gespeichert werden und dann in die liste aufgenommen werden

        Label Score;
        Rectangle Segment = new Rectangle(new Point(), new Size(10, 10));
        List<Point> AkuellePositionen = new List<Point>(); //Fuck me please, daddy uwu //this is gonna hurt
        List<Point> TempPoints = new List<Point>(); //hier werden die punkte gespeichert, die immer nach hinten in der schlange durchgereicht werden
        //public Pen APPLEPEN = new Pen(Color.Red);
        public Form1()
        {
            InitializeComponent();
            x = 10;
            y = 10;
            pos=Position.Stop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            foreach (Point item in AkuellePositionen)
            {
                gr.FillRectangle(Brushes.Green, item.X, item.Y, 10, 10);
            }
            if (count%5000 == 0 || vollesekunde)
            {
                vollesekunde = true;
                gr.FillRectangle(Brushes.Red, Apfel);
            }
            gr.FillRectangle(Brushes.Red, Apfel);
            
          
            Score = new Label()
            {
                Text = ApfelEATEN.ToString(),
                Name = "lblScore",
                Size = new Size(15,15),
                Location = new Point(),
                //Font = new Font(Score.Font,FontStyle.Bold),
            };
            foreach (Point item in AkuellePositionen)
            {
                gr.FillRectangle(Brushes.Green,new Rectangle(new Point(item.X,item.Y),new Size(10,10)));
            }
            Controls.Add(Score);            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if (pos==Position.Left)
            {
                x -= 10;
            }
            else if (pos==Position.Right)
            {
                x+= 10;
            }
            else if (pos == Position.Up)
            {
                y -= 10;
            }
            else if (pos == Position.Down)
            {
                y += 10;
            }
            else if (pos == Position.Stop)
            {
                
            }
            if (x <= 0)
            {
                x = Height;
            }
            if (y <= 0)
            {
                y = Width;
            }
            TempPoint = new Point(x, y);

            if (x == Apfel.X && y == Apfel.Y)
            {
                vollesekunde = false;
                ApfelPos = new Random().Next(0, 10) * 10;
                ApfelPos1 = new Random().Next(0, 10) * 10;
                Apfel.Location = new Point(ApfelPos,ApfelPos1);
                ApfelEATEN++;
                
            }
            switch (pos)
            {
                case Position.Left:
                    AkuellePositionen.Add(new Point(x - 10, y));
                    break;
                case Position.Right:
                    AkuellePositionen.Add(new Point(x + 10, y));
                    break;
                case Position.Up:
                    AkuellePositionen.Add(new Point(x, y + 10));
                    break;
                case Position.Down:
                    AkuellePositionen.Add(new Point(x, y - 10));
                    break;
                case Position.Stop:
                    break;
                default:
                    break;
            }
            //  AkuellePositionen[1] = new Point();
            AkuellePositionen.Add(new Point(x, y));
            Invalidate();

            //hier fehlt noch, dass die snake nicht außerhalb des forms gehen darfffffffffff F
            //Update(); //geht nicht lol
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                pos = Position.Left;
            }
            else if (e.KeyCode ==Keys.Right)
            {
                pos = Position.Right;
            }
            else if (e.KeyCode == Keys.Up)
            {
                pos = Position.Up;
            }
            else if (e.KeyCode == Keys.Down)
            {
                pos = Position.Down;
            }
            else if (e.KeyCode == Keys.Space)
            {
                pos = Position.Stop;
            }

        }        
    }
}

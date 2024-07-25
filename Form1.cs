using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Морской_Бой
{
    public partial class Form1 : Form
    {
        string Letters = "ABCDEFGHIJ";
        int cellsize = 40;
        bool gameison = false;
        bool shipsareready = false;
        bool playerturn = false;
        bool computerturn = false;
        bool ifitsend = false;
        string p = Environment.CurrentDirectory + @"\data.xml";

        Battleship player = new Battleship();
        Battleship computer = new Battleship();
        ComputerGame ComputerT = new ComputerGame(0, new List<List<int>>(), 0);

        ComboBox mode = new ComboBox();
        ComboBox loc = new ComboBox();
        Label turnnow = new Label();
        Button[,] button1 = new Button[10, 10];
        Button[,] button2 = new Button[10, 10];

        public Form1()
        {
            InitializeComponent();
            Begin();
        }

        public void Begin()
        {
            CreateGameField();
            TextInGame();
        }

        public void TextInGame()
        {
            mode.Items.AddRange(new object[] {"Однопалубный",
                        "Двухпалубный",
                        "Трехпалубный",
                        "Четырехпалубный"});
            mode.Size = new Size(150, 45);
            mode.Text = "Вид корабля";
            mode.Location = new Point(200, 500);
            this.Controls.Add(mode);

            loc.Items.AddRange(new object[] {"Горизонтально",
                        "Вертикально"});
            loc.Size = new Size(150, 45);
            loc.Text = "Расположение корабля";
            loc.Location = new Point(200, 530);
            this.Controls.Add(loc);
        }

        public void CreateGameField()
        {
            Button start = new Button();
            start.Text = "Начать игру";
            start.Location = new Point(50, 500);
            start.BackColor = Color.White;
            start.Size = new Size(100, 45);
            start.Click += new EventHandler(StartGame);
            this.Controls.Add(start);

            Label turnpc = new Label();
            turnpc.Text = "Ход:";
            turnpc.Location = new Point(450, 20);
            turnpc.Size = new Size(30, 30);
            this.Controls.Add(turnpc);

            turnnow.Location = new Point(480, 20);
            turnnow.Size = new Size(60, 30);
            this.Controls.Add(turnnow);

            Button battle = new Button();
            battle.Text = "Начать бой";
            battle.Location = new Point(50, 550);
            battle.BackColor = Color.White;
            battle.Size = new Size(100, 45);
            battle.Click += new EventHandler(Battle);
            this.Controls.Add(battle);

            Button statistic = new Button();
            statistic.Text = "Статистика";
            statistic.Location = new Point(850, 550);
            statistic.BackColor = Color.White;
            statistic.Size = new Size(100, 45);
            statistic.Click += new EventHandler(Statistic);
            this.Controls.Add(statistic);

            this.Width = 1000;
            this.Height = 700;

            for (int i = 0; i < player.Gamefield.GetLength(0); i++)
            {
                Label label = new Label();
                label.Text = Letters[i].ToString();
                label.Location = new Point(60 +  i * cellsize, 30);
                label.Size = new Size(cellsize, 15);
                this.Controls.Add(label);

                Label label1 = new Label();
                label1.Text = (i + 1).ToString();
                label1.Location = new Point(30, 65 + i * cellsize);
                label1.Size = new Size(20, 15);
                this.Controls.Add(label1);

                for (int j = 0; j < player.Gamefield.GetLength(0); j++)
                {
                    button1[i, j] = new Button();
                    button1[i,j].Location = new Point(50 + i * cellsize, 50 + j * cellsize);
                    button1[i, j].Size = new Size(cellsize, cellsize);
                    button1[i, j].BackColor = Color.White;
                    this.Controls.Add(button1[i, j]);
                    button1[i, j].Click += new EventHandler(Game);
                }
            }

            for (int i = 0; i < computer.Gamefield.GetLength(0); i++)
            {
                Label label = new Label();
                label.Text = Letters[i].ToString();
                label.Location = new Point(500 + 60 + i * cellsize, 30);
                label.Size = new Size(cellsize, 15);
                this.Controls.Add(label);

                Label label1 = new Label();
                label1.Text = (i + 1).ToString();
                label1.Location = new Point(500 + 30, 65 + i * cellsize);
                label1.Size = new Size(20, 15);
                this.Controls.Add(label1);

                for (int j = 0; j < player.Gamefield.GetLength(0); j++)
                {
                    button2[i, j] = new Button();
                    button2[i, j].Location = new Point(500 + 50 + i * cellsize, 50 + j * cellsize);
                    button2[i, j].Size = new Size(cellsize, cellsize);
                    button2[i, j].BackColor = Color.White;
                    this.Controls.Add(button2[i, j]);
                    button2[i, j].Click += new EventHandler(Game);

                }
            }
            Label p1 = new Label();
            p1.Text = "Ваше поле";
            p1.Location = new Point(50, 465);
            p1.Size = new Size(70, 15);
            p1.Width = 200;
            this.Controls.Add(p1);

            Label p2 = new Label();
            p2.Text = "Поле компьютера";
            p2.Location = new Point(50 + 500, 465);
            p2.Size = new Size(70, 15);
            p2.Width = 200;
            this.Controls.Add(p2);
        }

        public void StartGame(object sender, EventArgs e)
        {
            if (gameison == true)
            {
                DialogResult dialogResult = MessageBox.Show("Вы точно хотите начать новую игру?", "Сообщение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    player.NewGame();
                    computer.NewGame();
                    shipsareready = false;
                    playerturn = false;
                    computerturn = false;
                    ifitsend = false;
                    turnnow.Text = "";
                    for (int i = 0; i < player.Gamefield.GetLength(0); i++)
                        for (int j = 0; j < player.Gamefield.GetLength(1); j++)
                        {
                            button1[i, j].BackColor = Color.White;
                            button2[i, j].BackColor = Color.White;
                            button1[i, j].Text = "";
                            button2[i, j].Text = "";
                        }
                }
                else if(computer.GameOver() == true || player.GameOver() == true) ifitsend = true;
            }
            gameison = true;
        }

        public void Statistic(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        public void Battle(object sender, EventArgs e)
        {
            if (player.Isdone() && computer.Isdone() == false)
            {
                ComputerAdd();
                shipsareready = true;
                playerturn = true;
            }
            else if(computer.Isdone()) MessageBox.Show("Начните новую игру.", "\tСообщение");
            else MessageBox.Show("Не все корабли расставлены.", "\tСообщение");
            turnnow.Text = "player";
        }

        public async void ComputerTurn()
        {
            turnnow.Text = "computer";
            int x = -1;
            int y = -1;
            Random rnd = new Random();
            int n = 0;
            while (computerturn == true && player.GameOver() == false)
            {
                await Task.Delay(300);
                if (ComputerT.Turn == 0)
                {
                    x = rnd.Next(0, 10);
                    y = rnd.Next(0, 10);
                }
                else
                {
                    while ((x == -1 && y == -1) || (x == ComputerT.X && y == ComputerT.Y))
                    {
                        List<List<int>> listxy = new List<List<int>>();
                        switch (ComputerT.Location)
                        {
                            case 0: //местоположение неизвестно
                                for (int i = 0; i < ComputerT.Coordinates.Count; i++)
                                {
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0] + 1, ComputerT.Coordinates[i][1] });
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0] - 1, ComputerT.Coordinates[i][1] });
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0], ComputerT.Coordinates[i][1] - 1 });
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0], ComputerT.Coordinates[i][1] + 1 });
                                }
                                break;
                            case 1: //расположено горизонтально
                                for (int i = 0; i < ComputerT.Coordinates.Count; i++)
                                {
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0] + 1, ComputerT.Coordinates[i][1] });
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0] - 1, ComputerT.Coordinates[i][1] });
                                }
                                break;
                            case 2: //расположено вертикально
                                for (int i = 0; i < ComputerT.Coordinates.Count; i++)
                                {
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0], ComputerT.Coordinates[i][1] - 1 });
                                    listxy.Add(new List<int> { ComputerT.Coordinates[i][0], ComputerT.Coordinates[i][1] + 1 });
                                }
                                break;
                        }

                        n = rnd.Next(0, listxy.Count);
                        if (listxy[n][0] > -1 && listxy[n][1] > -1 && listxy[n][0] < 10 && listxy[n][1] < 10 && player.Gamefield[listxy[n][0], listxy[n][1]] != 2 && player.Gamefield[listxy[n][0], listxy[n][1]] != 3)
                        {
                            x = listxy[n][0];
                            y = listxy[n][1];
                        }
                    }
                }

                if (x != -1 && y != -1 && player.Gamefield[x, y] == 1)
                {
                    player.Gamefield[x, y] = 3;
                    if (ComputerT.Turn == 1 && (y - 1 > -1 && (player.Gamefield[x, y] == player.Gamefield[x, y - 1]) || (y + 1 < 10 && player.Gamefield[x, y] == player.Gamefield[x, y + 1]))) ComputerT.Location = 2;
                    if (ComputerT.Turn == 1 && (x - 1 > -1 && (player.Gamefield[x, y] == player.Gamefield[x - 1, y]) || (x + 1 < 10 && player.Gamefield[x, y] == player.Gamefield[x + 1, y]))) ComputerT.Location = 1;
                    ComputerT.Turn = 1;
                    ComputerT.Coordinates.Add(new List<int>(){ x, y });
                    if (player.IsEmpty(x, y) == true)
                    {
                        player.Empty(x, y);
                        ComputerT.IsOk();
                        ChangeButton();
                        MessageBox.Show("Упс, ваш корабль был потоплен(", "Сообщение");
                    }
                    ChangeButton();
                }
                else
                {
                    if (x != -1 && y != -1 && player.Gamefield[x, y] != 2 && player.Gamefield[x, y] != 3)
                    {
                        player.Gamefield[x, y] = 2;
                        ChangeButton();
                        computerturn = false;
                        playerturn = true;
                        turnnow.Text = "player";
                    }
                }
                ComputerT.X = x;
                ComputerT.Y = y;
            }

            if (player.GameOver() == true)
            {
                MessageBox.Show("Вы проиграли: противник победил.", "\tСообщение");
                (new Data("computer")).WriteToFile(p);
            }
        }
        public void ComputerAdd()
        {
            while(computer.m1 < 4)
            {
                Random rnd = new Random();
                int x = rnd.Next(0, 10);
                int y = rnd.Next(0, 10);
                int loc = rnd.Next(1, 3);
                if (computer.Gamefield[x,y] == 0) computer.Addship(x, y, 1, loc);
            }
            while (computer.m2 < 3)
            {
                Random rnd = new Random();
                int x = rnd.Next(0, 10);
                int y = rnd.Next(0, 10);
                int loc = rnd.Next(1, 3);
                if (computer.Gamefield[x, y] == 0) computer.Addship(x, y, 2, loc);
            }
            while (computer.m3 < 2)
            {
                Random rnd = new Random();
                int x = rnd.Next(0, 10);
                int y = rnd.Next(0, 10);
                int loc = rnd.Next(1, 3);
                if (computer.Gamefield[x, y] == 0) computer.Addship(x, y, 3, loc);
            }
            while (computer.m4 < 1)
            {
                Random rnd = new Random();
                int x = rnd.Next(0, 10);
                int y = rnd.Next(0, 10);
                int loc = rnd.Next(1, 3);
                if (computer.Gamefield[x, y] == 0) computer.Addship(x, y, 4, loc);
            }
        }
        public void Game(object sender, EventArgs e)
        {
            if (gameison == true && ifitsend == false)
            {
                Button pressb = (Button)sender;
                int xp = (pressb.Location.Y - 50) / cellsize;
                int yp = (pressb.Location.X - 50) / cellsize;
                int xc = (pressb.Location.Y - 50) / cellsize;
                int yc = (pressb.Location.X - 50 - 500) / cellsize;
                if (shipsareready == false && xp < 10 && yp < 10)
                {
                    int flag = player.Addship(xp, yp, Mode(), Loc());
                    ChangeButton();
                    Messages(flag);
                }
                if (playerturn == true && yc > -1)
                {
                    if (computer.Gamefield[xc, yc] == 1 )
                    {
                        computer.Gamefield[xc, yc] = 3;
                        if (computer.IsEmpty(xc, yc) == true)
                        {
                            computer.Empty(xc, yc);
                            ChangeButton();
                            MessageBox.Show("Поздравляю: вы потопили корабль противника!", "Сообщение");
                        }
                        else
                        {
                            ChangeButton();
                            MessageBox.Show("Поздравляю: вы подбили корабль противника!", "Сообщение");
                        }
                    }
                    else
                    {
                        if (computer.Gamefield[xc, yc] != 2 && computer.Gamefield[xc, yc] != 3)
                        {
                            computer.Gamefield[xc, yc] = 2;
                            computerturn = true;
                            playerturn = false;
                            ChangeButton();
                            ComputerTurn();
                        }
                    }
                }
                if (computer.GameOver() == true)
                {
                    MessageBox.Show("Ура: вы победили!", "\tСообщение");
                    (new Data("player")).WriteToFile(p);
                    gameison = false;
                }
            }  
            else MessageBox.Show("Начните игру.", "\tСообщение");
        }

        public void ChangeButton()
        {
            for (int i = 0; i < player.Gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < player.Gamefield.GetLength(0); j++)
                {
                    if (gameison == true && shipsareready == false)
                    {
                        if (player.Gamefield[i, j] == 1) button1[j, i].BackColor = Color.DarkCyan;
                    }
                    if (shipsareready == true)
                    {
                        if (computer.Gamefield[i, j] == 3)
                        {
                            button2[j, i].BackColor = Color.DarkCyan;
                            button2[j, i].Text = "X";
                        }
                        if (computer.Gamefield[i, j] == 2)
                        {
                            button2[j, i].BackColor = Color.LightGray;
                            button2[j, i].Text = "*";
                        }
                        if (player.Gamefield[i, j] == 3)
                        {
                            button1[j, i].BackColor = Color.DarkCyan;
                            button1[j, i].Text = "X";
                        }
                        if (player.Gamefield[i, j] == 2)
                        {
                            button1[j, i].BackColor = Color.LightGray;
                            button1[j, i].Text = "*";
                        }
                    }
                }
            }
        }

        public void Messages(int num)
        {
            if (num == 3) MessageBox.Show("Расставлено максимум кораблей данного типа.", "\tСообщение");
            if (num == 4 && Mode() == 0) MessageBox.Show("Не выбран размер корабля.", "\tСообщение"); 
                else if (num == 4 && Loc() == 0) MessageBox.Show("Не выбрано расположение корабля.", "\tСообщение");
                        else if(num == 4) MessageBox.Show("На данную позицию невозможно поставить корабль.", "\tСообщение");
        } 

        public int Mode()
        {
            int flag = 0;
            if (mode.Text == "Однопалубный") flag = 1;
            if (mode.Text == "Двухпалубный") flag = 2;
            if (mode.Text == "Трехпалубный") flag = 3;
            if (mode.Text == "Четырехпалубный") flag = 4;
            return flag;
        }

        public int Loc()
        {
            int flag = 0;
            if (loc.Text == "Горизонтально") flag = 1;
            if (loc.Text == "Вертикально") flag = 2;
            return flag;
        }
    }
}

using System.Windows.Forms;
using System.Drawing;

namespace CheckersAI.View;

using Model;
using AI.Majors;
using AI.Marshals;

public class CheckerGame
{
    Form form;
    Bitmap bmp;
    Graphics g;
    PictureBox pb;
    Timer tm;
    Game game;

    public Player White { get; set; }
    public Player Black { get; set; }

    public CheckerGame(Player white, Player black)
    {
        this.White = white;
        this.Black = black;

        ApplicationConfiguration.Initialize();
        form = new Form();
        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Text = "Checkers AI";

        form.KeyDown += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        };

        pb = new PictureBox();
        pb.Dock = DockStyle.Fill;

        Point? p = null;
        Piece piece = Piece.Empty;
        pb.MouseDown += delegate (object sender, MouseEventArgs e)
        {
            p = e.Location;
            (White as HumanPlayer).Origin = (e.Location.X - bmp.Width / 2 + 360) / 180 +
                    4 * (7 - (e.Location.Y - bmp.Height / 2 + 360) / 90);
            piece = game.State[(White as HumanPlayer).Origin];
        };
        pb.MouseMove += delegate (object sender, MouseEventArgs e)
        {
            if (!p.HasValue)
                return;
            p = e.Location;
        };
        pb.MouseUp += delegate (object sender, MouseEventArgs e)
        {
            p = null;
            (White as HumanPlayer).Target = (e.Location.X - bmp.Width / 2 + 360) / 180 +
                    4 * (7 - (e.Location.Y - bmp.Height / 2 + 360) / 90);
            piece = Piece.Empty;
        };
        form.Controls.Add(pb);
        
        tm = new Timer();
        tm.Interval = 20;
        tm.Tick += delegate
        {
            g.Clear(Color.White);

            Pen piecepen = new Pen(Brushes.Black, 1f);
            Rectangle toprec;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var rec = new Rectangle(bmp.Width / 2 - 360 + 90 * x,
                                bmp.Height / 2 + 270 - 90 * y, 90, 90);
                    if ((x + y) % 2 == 0)
                    {
                        g.FillRectangle(Brushes.DarkGreen, rec);
                        g.DrawRectangle(Pens.Black, rec);
                    
                        rec = new Rectangle(rec.X + 10, rec.Y + 10, 70, 70);
                        switch (game.State[x / 2 + 4 * y])
                        {
                            case Piece.White:
                                g.FillEllipse(Brushes.White, rec);
                                g.DrawEllipse(piecepen, rec);
                                break;
                            case Piece.Black:
                                g.FillEllipse(Brushes.DarkGray, rec);
                                g.DrawEllipse(piecepen, rec);
                                break;
                            case Piece.WhiteChecker:
                                toprec =new Rectangle(rec.X + 5, rec.Y - 5, 70, 70);
                                g.FillEllipse(Brushes.White, rec);
                                g.DrawEllipse(piecepen, rec);
                                g.FillEllipse(Brushes.White, toprec);
                                g.DrawEllipse(piecepen, toprec);
                                break;
                            case Piece.BlackChecker:
                                toprec =new Rectangle(rec.X + 5, rec.Y - 5, 70, 70);
                                g.FillEllipse(Brushes.DarkGray, rec);
                                g.DrawEllipse(piecepen, rec);
                                g.FillEllipse(Brushes.DarkGray, toprec);
                                g.DrawEllipse(piecepen, toprec);
                                break;
                        }
                    }
                    else 
                    {
                        g.FillRectangle(Brushes.WhiteSmoke, rec);
                        g.DrawRectangle(Pens.Black, rec);
                    }
                }
            }

            if (p.HasValue)
            {
                var currec = new Rectangle(p.Value.X - 35, p.Value.Y - 35, 70, 70);
                var currectop = new Rectangle(p.Value.X - 30, p.Value.Y - 40, 70, 70);
                switch (piece)
                {
                    case Piece.WhiteChecker:
                        g.FillEllipse(Brushes.White, currec);
                        g.DrawEllipse(piecepen, currec);
                        g.FillEllipse(Brushes.White, currectop);
                        g.DrawEllipse(piecepen, currectop);
                        break;
                    case Piece.White:
                        g.FillEllipse(Brushes.White, currec);
                        g.DrawEllipse(piecepen, currec);
                        break;
                    case Piece.BlackChecker:
                        g.FillEllipse(Brushes.DarkGray, currec);
                        g.DrawEllipse(piecepen, currec);
                        g.FillEllipse(Brushes.DarkGray, currectop);
                        g.DrawEllipse(piecepen, currectop);
                        break;
                    case Piece.Black:
                        g.FillEllipse(Brushes.DarkGray, currec);
                        g.DrawEllipse(piecepen, currec);
                        break;
                }
            }

            g.DrawRectangle(new Pen(Color.Black, 3f), 
                new Rectangle(bmp.Width / 2 - 360, bmp.Height / 2 - 360, 720, 720));
            pb.Image = bmp;
        };

        game = new Game();
        game.BlackPlayer = this.Black;
        game.WhitePlayer = this.White;
        
        form.Load += async delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            pb.Image = bmp;
            g = Graphics.FromImage(bmp);
            tm.Start();

            PlayResult result;
            do
            {
                result = await game.Play();
            } while(result == PlayResult.Nothing);
        };
    }

    public void Open()
        => Application.Run(form);
}
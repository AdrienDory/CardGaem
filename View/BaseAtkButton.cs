using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Players;
using Controller.BoardCtrlr;

namespace View;

public class BaseAtkButton : TileButton
{
    private Pawn? pawn;
    public Pawn? Pawn
    {
        set => pawn = value;
    }

    private BoardController ctrlr;

    public BaseAtkButton(BoardController ctrlr, Pnb n, TileLabel? p = null) : base(new Coordinates(0,0), p)
    {
        this.ctrlr = ctrlr;
        if (n == Pnb.P1)
        {
            Margin = new System.Windows.Thickness(71, 95, 655, 161);
            Content = "🢀";
        }
        else
        {
            Margin = new System.Windows.Thickness(655, 95, 71, 161);
            Content = "🢂";
            this.C = new Coordinates(0, 6);
        }
        this.FontSize = 20;
        this.BorderBrush = new SolidColorBrush(Colors.White);
        this.Foreground = new SolidColorBrush(Colors.White);
    }

    public void AtkBase()
    {
        ctrlr.MovePawn(pawn, C);
    }
}

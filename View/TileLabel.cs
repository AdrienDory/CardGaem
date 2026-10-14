using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using CardGaem.Boards;
using CardGaem.Pawns;

namespace View;

public class TileLabel : Label
{
    private Pawn pawn;
    public Pawn Pawn
    {
        get => pawn;
        set => pawn = value;
    }

    public void UpdateContent()
    {
        this.Content = pawn.Life;
    }

    private Coordinates c;
    public Coordinates C
    {
        get => c;
        set => c = value;
    }

    public TileLabel(Coordinates c, Pawn p)
    {
        this.c = c;
        this.pawn = p;
        this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        this.Foreground = ColorHelper.ColorFromPlayer(p.Player);
    }
}

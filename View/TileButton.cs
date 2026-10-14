using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using CardGaem.Boards;

namespace View;

public class TileButton : Button
{
    private Coordinates c;
    public Coordinates C
    {
        get => c;
        protected set => c = value;
    }

    public TileButton(Coordinates c, TileLabel? p = null)
    {
        this.c = c;
        if (p is not null)
        {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            this.Foreground = p.Foreground;
            this.Content = p.Content;
        }
        
    }

    
}

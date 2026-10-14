using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using CardGaem.Players;

namespace View;

public class ColorHelper
{
    public static SolidColorBrush ColorFromPlayer(Pnb nb, byte A = 255)
    {
        return new SolidColorBrush()
        {
            Color = new Color()
            {
                A = A,
                B = Convert.ToByte(50 + (int)nb * 150),
                G = 20,
                R = Convert.ToByte(200 - (int)nb * 150)
            }
        };
    }
}

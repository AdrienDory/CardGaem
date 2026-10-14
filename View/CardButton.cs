using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CardGaem.Cards;
using CardGaem.Players;

namespace View;

public class CardButton : Button
{
    private Card model;
    public Card Model => model;

    private Pnb nb;
    public Pnb Nb => nb;

    public CardButton(Card model, int index, Pnb nb)
    {
        this.model = model;
        this.nb = nb;
        Grid.SetRow(this, index);
        this.Content = model.ToString();
        this.BorderBrush = ColorHelper.ColorFromPlayer(nb);
        this.Background = ColorHelper.ColorFromPlayer(nb, 50);
        this.UnSelected();
    }

    public void Selected()
    {
        this.Margin = new Thickness(nb.Direction * 5, 1, -nb.Direction * 5, 1);
    }

    public void UnSelected()
    {
        this.Margin = new Thickness(-nb.Direction * 5, 1, nb.Direction * 5, 1);
    }
}

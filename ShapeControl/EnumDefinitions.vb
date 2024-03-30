' ****************************************************************************************************************
' EnumDefinitions.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

''' <summary>Legt fest welce Form gezeichnet wird</summary>
Public Enum ShapeModes

    ''' <summary>Horizontale Linie</summary>
    HorizontalLine = 0

    ''' <summary>Vertikale Linie</summary>
    VerticalLine = 1

    ''' <summary>diagonale Linie</summary>
    DiagonalLine = 2

    ''' <summary>Rechteck</summary>
    Rectangle = 3

    ''' <summary>gefülltes Rechteck</summary>
    FilledRectangle = 4

    ''' <summary>Kreis oder Ellipse</summary>
    Elypse = 5

    ''' <summary>gefüllter Kreis oder gefüllte Ellipse</summary>
    FilledElypse = 6

End Enum

''' <summary>legt fest wie die diagonale Linie gezeichnet wird</summary>
Public Enum DiagonalLineModes

    ''' <summary>von links oben nach rechts unten</summary>
    TopLeftToBottomRight = 0

    ''' <summary>von links unten nach rechts oben</summary>
    BottomLeftToTopRight = 1

End Enum


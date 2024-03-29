' ****************************************************************************************************************
' Shape.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System
Imports System.Drawing
Imports System.Windows.Forms

''' <summary></summary>
<ProvideToolboxControl("SchlumpfSoft Controls", False)>
Public Class Shape

    ''' <summary></summary>
    Public Property ShapeModus() As ShapeModes
        Get
            Return _ShapeModus
        End Get
        Set(value As ShapeModes)
            _ShapeModus = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary></summary>
    Public Property LineWidth() As Single
        Get
            Return _LineWidth
        End Get
        Set(value As Single)
            _LineWidth = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary></summary>
    Public Property LineColor() As Color
        Get
            Return _LineColor
        End Get
        Set(value As Color)
            _LineColor = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary></summary>
    Public Property FillColor() As Color
        Get
            Return _FillColor
        End Get
        Set(value As Color)
            _FillColor = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary></summary>
    Public Property DiagonalLineModus() As DiagonalLineModes
        Get
            Return _DiagonalLineModus
        End Get
        Set(value As DiagonalLineModes)
            _DiagonalLineModus = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary>Legt spezielle Parameter für das ShapeControl fest</summary>
    ''' <remarks>https://stackoverflow.com/questions/511320/transparent-control-backgrounds-on-a-vb-net-gradient-filled-form</remarks>
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            'WS EX TRANSPARENT aktivieren
            'https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
            cp.ExStyle = cp.ExStyle Or &H20
            Return cp
        End Get
    End Property

    Public Sub New()

        'Dieser Aufruf ist für den Designer erforderlich.
        Me.InitializeComponent()

        'Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.InitializeVariables()
        Me.InitializeStyles()

    End Sub

    ''' <summary>Initialisiert die Styles für das ShapeControl</summary>
    Private Sub InitializeStyles()

        Me.SetStyle(ControlStyles.Opaque, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, False)

    End Sub

    ''' <summary>Initialisiert die Standardwerte für das ShapeControl</summary>
    Private Sub InitializeVariables()

        'Horizontale Linie
        _ShapeModus = ShapeModes.HorizontalLine

        'diagonale Linie von links oben nach rechts unten
        _DiagonalLineModus = DiagonalLineModes.TopLeftToBottomRight

        'schwarze Linie für Linie und Rahmenlinie bei Ellipse und Rechteck
        _LineColor = Color.Black

        'Breite der Linie
        _LineWidth = 2

        'Füllfarbe für Ellipse und Rechteck
        _FillColor = Color.Gray

    End Sub

    ''' <summary>zeichnet das ShapeControl neu</summary>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        MyBase.OnPaint(e)

        Dim g As Graphics = Me.CreateGraphics

        'Benutzerdefinierten Zeichnungscode hier einfügen
        Select Case _ShapeModus

            Case ShapeModes.HorizontalLine
                'horizontale Linie zeichnen (mittig im Rahmen des Controls)
                g.DrawLine(New Pen(_LineColor, _LineWidth), 0, CInt(Me.Height / 2), Me.Width, CInt(Me.Height / 2))

            Case ShapeModes.VerticalLine
                'vertikale Linie zeichnen (mittig im Rahmen des Controls)
                g.DrawLine(New Pen(_LineColor, _LineWidth), CInt(Me.Width / 2), 0, CInt(Me.Width / 2), Me.Height)

            Case ShapeModes.DiagonalLine
                'diagonale Linie zeichnen
                Select Case _DiagonalLineModus

                    Case DiagonalLineModes.BottomLeftToTopRight
                        'von links unten nach rechts oben
                        g.DrawLine(New Pen(_LineColor, _LineWidth), 0, Me.Height, Me.Width, 0)

                    Case DiagonalLineModes.TopLeftToBottomRight
                        'von links oben nach rechts unten
                        g.DrawLine(New Pen(_LineColor, _LineWidth), 0, 0, Me.Width, Me.Height)

                End Select


                '-----------------------------------------------------------------------------------
                'TODO: Die Werte für die Linienbreite müssen an die Zeichefläche angepasst werden da
                'nur die Hälfte der Linien im Bereich des Controls liegt.
                'TODO: Die werte für den Füllbereich müssen ebenfalls angpasst werden da
                'der zu füllende Bereich innerhalb der Rahmenlinie liegen soll.
                '------------------------------------------------------------------------------------



            Case ShapeModes.Rectangle
                'einfaches Rechteck zeichnen
                g.DrawRectangle(New Pen(_LineColor, _LineWidth), Me.ClientRectangle)

            Case ShapeModes.FilledRectangle
                'gefülltes Rechteck zeichnen
                g.DrawRectangle(New Pen(_LineColor, _LineWidth), Me.ClientRectangle)
                g.FillRectangle(New SolidBrush(_FillColor), Me.ClientRectangle)

            Case ShapeModes.Elypse
                'einfache Ellipse zeichnen
                g.DrawEllipse(New Pen(_LineColor, _LineWidth), Me.ClientRectangle)

            Case ShapeModes.FilledElypse
                'gefüllte Ellipe zeichnen
                g.DrawEllipse(New Pen(_LineColor, _LineWidth), Me.ClientRectangle)
                g.FillEllipse(New SolidBrush(_FillColor), Me.ClientRectangle)
        End Select

    End Sub

End Class

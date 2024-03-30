' ****************************************************************************************************************
' Shape.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.</summary>
<ProvideToolboxControl("SchlumpfSoft Controls", False)>
<Description("Steuerelement zum Darstellen einer Linie, eines Rechtecks oder einer Ellipse.")>
<ToolboxItem(True)>
<ToolboxBitmap(GetType(Shape), "Shape.bmp")>
Public Class Shape

    ''' <summary>Legt die anzuzeigende Form fest oder gibt diese zurück.</summary>
    <Browsable(True)>
    <Category("Appearance")>
    <Description("Legt die anzuzeigende Form fest oder gibt diese zurück.")>
    Public Property ShapeModus() As ShapeModes
        Get
            Return _ShapeModus
        End Get
        Set(value As ShapeModes)
            _ShapeModus = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary>Legt die Breite der Linie oder Rahmenlinie fest oder gibt diese zurück.</summary>
    <Browsable(True)>
    <Category("Appearance")>
    <Description("Legt die Breite der Linie oder Rahmenlinie fest oder gibt diese zurück.")>
    Public Property LineWidth() As Single
        Get
            Return _LineWidth
        End Get
        Set(value As Single)
            _LineWidth = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary>Legt die Farbe der Linie oder Rahmenlinie fest oder gibt diese zurück.</summary>
    <Browsable(True)>
    <Category("Appearance")>
    <Description("Legt die Farbe der Linie oder Rahmenlinie fest oder gibt diese zurück.")>
    Public Property LineColor() As Color
        Get
            Return _LineColor
        End Get
        Set(value As Color)
            _LineColor = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary>Legt die Füllfarbe für die Form fest oder gibt diese zurück.</summary>
    <Browsable(True)>
    <Category("Appearance")>
    <Description("Legt die Füllfarbe für die Form fest oder gibt diese zurück.")>
    Public Property FillColor() As Color
        Get
            Return _FillColor
        End Get
        Set(value As Color)
            _FillColor = value
            Me.RecreateHandle()
        End Set
    End Property

    ''' <summary>Legt fest ob eine diagonale Linie von links oben nach rechts unten oder umgekehrt verläuft oder gibt dieses zurück.</summary>
    <Browsable(True)>
    <Category("Appearance")>
    <Description("Legt fest ob eine diagonale Linie von links oben nach rechts unten oder umgekehrt verläuft oder gibt dieses zurück.")>
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

    ''' <summary>Hintergrundfarbe (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(value As Color)
            MyBase.BackColor = value
        End Set
    End Property

    ''' <summary>Hintergrundbild (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property BackgroundImage As Image
        Get
            Return MyBase.BackgroundImage
        End Get
        Set(value As Image)
            MyBase.BackgroundImage = value
        End Set
    End Property

    ''' <summary>Leout Hintergrundbild (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property BackgroundImageLayout As ImageLayout
        Get
            Return MyBase.BackgroundImageLayout
        End Get
        Set(value As ImageLayout)
            MyBase.BackgroundImageLayout = value
        End Set
    End Property

    ''' <summary>Schriftart (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
        End Set
    End Property

    ''' <summary>Vordergrundfarbe (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(value As Color)
            MyBase.ForeColor = value
        End Set
    End Property

    ''' <summary>Rechrs - Links Schreibweise (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property RightToLeft As RightToLeft
        Get
            Return MyBase.RightToLeft
        End Get
        Set(value As RightToLeft)
            MyBase.RightToLeft = value
        End Set
    End Property

    ''' <summary>Text (nicht relevant für dieses Control)</summary>
    <Browsable(False)>
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(value As String)
            MyBase.Text = value
        End Set
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
                g.DrawLine(New Pen(_LineColor, _LineWidth),
                           0,
                           CInt(Me.Height / 2),
                           Me.Width,
                           CInt(Me.Height / 2))

            Case ShapeModes.VerticalLine
                'vertikale Linie zeichnen (mittig im Rahmen des Controls)
                g.DrawLine(New Pen(_LineColor, _LineWidth),
                           CInt(Me.Width / 2),
                           0,
                           CInt(Me.Width / 2),
                           Me.Height)

            Case ShapeModes.DiagonalLine
                'diagonale Linie zeichnen
                Select Case _DiagonalLineModus

                    Case DiagonalLineModes.BottomLeftToTopRight
                        'von links unten nach rechts oben
                        g.DrawLine(New Pen(_LineColor, _LineWidth),
                                   0,
                                   Me.Height,
                                   Me.Width,
                                   0)

                    Case DiagonalLineModes.TopLeftToBottomRight
                        'von links oben nach rechts unten
                        g.DrawLine(New Pen(_LineColor, _LineWidth),
                                   0,
                                   0,
                                   Me.Width,
                                   Me.Height)

                End Select

            Case ShapeModes.Rectangle
                'einfaches Rechteck zeichnen
                g.DrawRectangle(
                    New Pen(_LineColor, _LineWidth),
                    _LineWidth / 2,
                    _LineWidth / 2,
                    Me.Width - _LineWidth,
                    Me.Height - _LineWidth)

            Case ShapeModes.FilledRectangle
                'einfaches Rechteck zeichnen
                g.DrawRectangle(
                    New Pen(_LineColor, _LineWidth),
                    _LineWidth / 2,
                    _LineWidth / 2,
                    Me.Width - _LineWidth,
                    Me.Height - _LineWidth)

                'Rechteck ausfüllen
                g.FillRectangle(New SolidBrush(_FillColor),
                               _LineWidth,
                               _LineWidth,
                               Me.Width - (2 * _LineWidth),
                               Me.Height - (2 * _LineWidth))

            Case ShapeModes.Elypse
                'einfache Ellipse zeichnen
                g.DrawEllipse(
                    New Pen(_LineColor, _LineWidth),
                    _LineWidth / 2,
                    _LineWidth / 2,
                    Me.Width - _LineWidth,
                    Me.Height - _LineWidth)

            Case ShapeModes.FilledElypse
                'einfache Ellipe zeichnen
                g.DrawEllipse(
                    New Pen(_LineColor, _LineWidth),
                    _LineWidth / 2,
                    _LineWidth / 2,
                    Me.Width - _LineWidth,
                    Me.Height - _LineWidth)

                'Ellipse ausfüllen
                g.FillEllipse(New SolidBrush(_FillColor),
                              _LineWidth,
                              _LineWidth,
                              Me.Width - (2 * _LineWidth),
                              Me.Height - (2 * _LineWidth))

        End Select

    End Sub

End Class

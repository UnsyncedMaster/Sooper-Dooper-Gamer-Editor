Imports System.Windows.Forms
Imports System.Windows.Forms.Integration
Imports ICSharpCode.AvalonEdit
Imports ICSharpCode.AvalonEdit.Highlighting
Imports System.IO
Imports ICSharpCode

Public Class Form1
    Inherits Form

    Private textEditor As AvalonEdit.TextEditor
    Private WithEvents btnOpen As Button
    Private WithEvents btnSave As Button
    Private elementHost As ElementHost

    Private isDragging As Boolean = False
    Private startPoint As Point
    Public Sub New()
        InitializeComponent()
        InitializeTextEditor()
    End Sub

    Private Sub InitializeComponent()
        Me.btnOpen = New Button()
        Me.btnSave = New Button()
        Me.elementHost = New ElementHost()

        Me.btnOpen.Location = New Point(10, 10)
        Me.btnOpen.Size = New Size(100, 30)
        Me.btnOpen.Text = "Open"

        Me.btnSave.Location = New Point(120, 10)
        Me.btnSave.Size = New Size(100, 30)
        Me.btnSave.Text = "Save"

        Me.elementHost.Location = New Point(10, 50)
        Me.elementHost.Size = New Size(600, 400)

        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.elementHost)

        Me.Text = "AvalonEdit thingy Sooper Dooper Editor"
        Me.Size = New Size(640, 480)
    End Sub

    Private Sub InitializeTextEditor()
        textEditor = New AvalonEdit.TextEditor()
        textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#") ' To Be Edited Ofc
        textEditor.ShowLineNumbers = True
        textEditor.WordWrap = True

        elementHost.Child = textEditor
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        textEditor.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim saveFileDialog As New SaveFileDialog()

        saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        saveFileDialog.Title = "Sooper Dooper Editor - Save File"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = saveFileDialog.FileName

            Dim textToSave As String = textEditor.Text
            System.IO.File.WriteAllText(filePath, textToSave)

            MessageBox.Show("File saved successfully!", "Basic Editor - Save File Succes")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        openFileDialog.Title = "Sooper Dooper Editor - Open File"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openFileDialog.FileName
            Dim fileContent As String = System.IO.File.ReadAllText(filePath)
            textEditor.Text = fileContent
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        isDragging = True
        startPoint = New Point(e.X, e.Y)
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If isDragging Then
            Dim endPoint As Point = PointToScreen(e.Location)
            Me.Location = New Point(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y)
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        isDragging = False
    End Sub
End Class

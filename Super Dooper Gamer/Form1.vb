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
        ' Initialize components
        InitializeComponent()
        InitializeTextEditor()
    End Sub

    Private Sub InitializeComponent()
        Me.btnOpen = New Button()
        Me.btnSave = New Button()
        Me.elementHost = New ElementHost()

        ' Button Open
        Me.btnOpen.Location = New Point(10, 10)
        Me.btnOpen.Size = New Size(100, 30)
        Me.btnOpen.Text = "Open"

        ' Button Save
        Me.btnSave.Location = New Point(120, 10)
        Me.btnSave.Size = New Size(100, 30)
        Me.btnSave.Text = "Save"

        ' ElementHost settings
        Me.elementHost.Location = New Point(10, 50)
        Me.elementHost.Size = New Size(600, 400)

        ' Adding controls to the form
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.elementHost)

        ' Form settings
        Me.Text = "AvalonEdit Example"
        Me.Size = New Size(640, 480)
    End Sub

    Private Sub InitializeTextEditor()
        ' Create and configure the TextEditor
        textEditor = New AvalonEdit.TextEditor()
        textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#") ' or "VB"
        textEditor.ShowLineNumbers = True
        textEditor.WordWrap = True

        ' Add TextEditor to the ElementHost
        elementHost.Child = textEditor
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        textEditor.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim saveFileDialog As New SaveFileDialog()

        saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        saveFileDialog.Title = "Basic Editor - Save File"

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
        openFileDialog.Title = "Basic Editor - Open File"

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
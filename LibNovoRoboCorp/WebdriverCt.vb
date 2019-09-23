
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI

Public Class WebdriverCt
    Private Shared ProcessosNAvegador As New List(Of Process)
    Private Shared _driver As ChromeDriver

    Public Shared ReadOnly Property Driver As ChromeDriver
        Get
            If _driver Is Nothing Then
                _driver = PrepararWebDriver()
            End If
            Return _driver
        End Get
    End Property

    Private Shared _wait As WebDriverWait
    Public Shared ReadOnly Property Wait As WebDriverWait
        Get
            If _wait Is Nothing Then
                _wait = PrepararEsperador()
            End If
            Return _wait
        End Get
    End Property

    Private Shared Function PrepararEsperador() As WebDriverWait

        Dim esperador As New WebDriverWait(Driver, New TimeSpan(0, 0, 59))
        Return esperador

    End Function

    Shared Sub ResetarWebdriver()
        For Each Window In Driver.WindowHandles
            Driver.SwitchTo.Window(Window)
            Driver.Close()
        Next


        For Each processo In ProcessosNAvegador
            Try
                processo.Kill()
            Catch

            End Try
        Next

        Driver.Quit()

        _driver = PrepararWebDriver()


    End Sub

    Private Shared Function IniciarNavegador() As ChromeDriver

        Dim ChromeOptions = New ChromeOptions()
        ChromeOptions.AddArgument("no-sandbox")
        ChromeOptions.AddArgument("--headless")
        Dim Driver = New ChromeDriver(ChromeOptions)
        Driver.Manage.Timeouts.ImplicitWait = New TimeSpan(0, 0, 5)
        Driver.Manage.Timeouts.PageLoad = New TimeSpan(0, 10, 0)
        Driver.Manage.Timeouts.AsynchronousJavaScript = New TimeSpan(0, 3, 0)
        Driver.Manage.Window.Maximize()

        Return Driver

    End Function


    Private Shared Function PrepararWebDriver() As ChromeDriver

        Dim ProcessosAnteriores As Process() = Process.GetProcessesByName("FireFox")

        Dim navegador = IniciarNavegador()

        Dim ProcessosPosteriores As Process() = Process.GetProcessesByName("FireFox")

        For Each processo In ProcessosPosteriores
            If Not ProcessosAnteriores.Contains(processo) Then
                ProcessosNAvegador.Add(processo)
            End If
        Next

        Return navegador
    End Function

End Class

Imports CrawlerCorp
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeLogin

    Dim Crawler As Crawler
    Dim drive As IWebDriver
    Public wait As WebDriverWait

    Sub New(crawler As Crawler)
        Me.drive = crawler.Drive
        Me.wait = WebdriverCt.Wait
        Login()
    End Sub


    Function Login()
        Dim UserField, UserPass, BtnEntrar As IWebElement

Login:

        Console.WriteLine("Tentando conectar ao vivo Corp às " + Now.TimeOfDay.ToString)
        Try
            drive.Navigate.GoToUrl("https://vivocorp-parceiro.vivo.com.br/vivocorp_oui/start.swe")
        Catch ex As Exception
            GoTo Login
        End Try

        drive.Manage.Window.Maximize()

        UserField = drive.FindElement(By.XPath("//*[@id='username']"))
        UserPass = drive.FindElement(By.XPath("//*[@id='password']"))
        BtnEntrar = drive.FindElement(By.XPath("//*[@id='form']/div[3]/div/input"))

        UserField.SendKeys("80032734")
        UserPass.SendKeys("AS@2109tel")

        BtnEntrar.Click()
        Console.WriteLine("Vivo Corp conectado, iniciando pesquisa de CNPJS")

        Return True
    End Function


End Class

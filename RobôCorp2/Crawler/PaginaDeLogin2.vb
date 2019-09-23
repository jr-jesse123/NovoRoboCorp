Imports CrawlerCorp
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports CrawlerCorp.CrawlerClass
Imports OpenQA.Selenium.Support.UI
Imports SeleniumExtras.WaitHelpers

Public Class PaginaDeLogin2

    Dim Crawler As Crawler2
    Dim drive As IWebDriver
    Public wait As WebDriverWait

    Sub New(crawler As Crawler2)
        Me.drive = crawler.Drive
        Me.wait = crawler.Wait
    End Sub


    Function Login()
        Dim UserField, UserPass, BtnEntrar As IWebElement

        Console.WriteLine("Tentando conectar ao vivo Corp às " + Now.TimeOfDay.ToString)
        drive.Navigate.GoToUrl("https://vivocorp-parceiro.vivo.com.br/vivocorp_oui/start.swe")
        drive.Manage.Window.Maximize()

        UserField = drive.FindElement(By.XPath("//*[@id='username']"))
        UserPass = drive.FindElement(By.XPath("//*[@id='password']"))
        BtnEntrar = drive.FindElement(By.XPath("//*[@id='form']/div[3]/div/input"))

        UserField.SendKeys("80032734")
        UserPass.SendKeys("VIVO!2244")

        Try
            BtnEntrar.Click()
        Catch ex As Exception



        End Try


        Return True
    End Function


End Class

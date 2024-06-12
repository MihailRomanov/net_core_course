using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    public class FlaUISample
    {
        private AutomationBase automation;
        private const string CalculatorAppName
            = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        Window window;
        private Application app;

        [SetUp]
        public void InitCalculator()
        {
            automation = new UIA3Automation();
            app = FlaUI.Core.Application
                .LaunchStoreApp(CalculatorAppName);
            window = app.GetMainWindow(automation);
            Thread.Sleep(1000);
        }

        [TearDown]
        public void CloseCalculator()
        {
            app?.Close();
            automation?.Dispose();
            Thread.Sleep(1000);
        }

        [Test]
        public void SwitchToScientificAndRevert()
        {
            Thread.Sleep(1000);

            var toggleMenuButton = window.FindFirstDescendant(
                c => c.ByAutomationId("TogglePaneButton"))?.AsToggleButton();
            toggleMenuButton?.Click();

            var paneRoot = window.FindFirstDescendant(
                c => c.ByAutomationId("PaneRoot"));

            var scientificCalculatorMenu = paneRoot.FindFirstDescendant(
                c => c.ByControlType(ControlType.ListItem)
                    .And(c.ByName("Scientific Calculator")))?.AsButton();
            
            scientificCalculatorMenu?.Click();
            Thread.Sleep(4000);

            toggleMenuButton?.Click();

            var standardCalculatorMenu = paneRoot.FindFirstDescendant(
                c => c.ByControlType(ControlType.ListItem)
                    .And(c.ByName("Standard Calculator")))?.AsButton();

            standardCalculatorMenu?.Click();
            Thread.Sleep(4000);

            app.Close();
        }

        [Test]
        [STAThread]
        public void PlusOpertions_ReturnSumm()
        {
            var calculator = window.As<Calculator>();

            calculator.NumButtons[1].Click();
            calculator.NumButtons[4].Click();
            Thread.Sleep(4000);
            calculator.Plus.Click();
            calculator.NumButtons[5].Click();
            calculator.NumButtons[7].Click();
            Thread.Sleep(4000);
            calculator.Equal.Click();

            var result = calculator.DisplayResult.Text.Replace("Display is ", "");
            result.Should().Be("71");
            Thread.Sleep(4000);
        }

    }
}
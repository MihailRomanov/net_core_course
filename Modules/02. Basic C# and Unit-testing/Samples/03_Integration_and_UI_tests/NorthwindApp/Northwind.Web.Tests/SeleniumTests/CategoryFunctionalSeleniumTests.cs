using FluentAssertions;
using HtmlElements;
using Northwind.Web.Tests.SeleniumTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Northwind.Web.Tests.SeleniumTests
{
    public class CategoryFunctionalSeleniumTests : SeleniumTestsBase
    {
        public CategoryFunctionalSeleniumTests(BrowserTypes browserType)
            : base(browserType)
        { }

        [Test]
        [Ignore("")]
        public void ShowCategoriesList()
        {
            webDriver.Navigate().GoToUrl("http://localhost:5000/");
            Thread.Sleep(1000);
            var categoryLink = webDriver
                .FindElement(By.CssSelector("a[href*='Categories'].nav-link"));
            categoryLink.Click();
            Thread.Sleep(1000);
            var categoryNames = webDriver
                .FindElements(By.CssSelector("td[data-tid='category-name']"))
                .Select(e => e.Text);

            var names = new[] {
                "Beverages", "Condiments", "Confections", "Dairy Products",
                "Grains/Cereals", "Meat/Poultry", "Produce", "Seafood" };

            categoryNames.Should().BeEquivalentTo(names);
        }

        [Test]
        public void CreateNewCategory()
        {
            webDriver.Navigate().GoToUrl("http://localhost:5000/");
            Thread.Sleep(1000);
            IPageObjectFactory pageFactory = new PageObjectFactory();

            var mainPage = pageFactory.Create<MainPage>(webDriver);

            var categoriesList = mainPage.GoToCategoriesListPage();
            Thread.Sleep(1000);
            var currentCategoryCount = categoriesList.Categories.Count;

            var categoryForAdd = new
            {
                Name = $"New Category {currentCategoryCount + 1}",
                Description = "New Description",
            };

            var createNewPage = categoriesList.GoToCreateNewCategoryPage();
            Thread.Sleep(1000);
            createNewPage.CategoryName = categoryForAdd.Name;
            createNewPage.Description = categoryForAdd.Description;
            createNewPage.AddPictureFile(Path.Combine(testFilesPath, "json.jpg"));
            Thread.Sleep(1000);

            categoriesList = createNewPage.CreateAndGoToList();
            Thread.Sleep(1000);
            var newCategoryRow = categoriesList.Categories.Last();
            var newCategory = new
            {
                Name = newCategoryRow.CategoryName,
                Description = newCategoryRow.Description,
            };

            categoriesList.Categories.Count.Should().Be(currentCategoryCount + 1);
            newCategory.Should().BeEquivalentTo(categoryForAdd);
        }
    }
}


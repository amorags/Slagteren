using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class Tests : PageTest
{

    
    [Test]
    public async Task FindLoginElements()
    {
        await Page.GotoAsync("http://localhost:4200/home");

        await Page.Locator("a:nth-child(2)").ClickAsync();

        await Page.GetByLabel("Email").ClickAsync();

        await Page.GetByLabel("Email").FillAsync("user@email.com");

        await Page.GetByLabel("Password").ClickAsync();

        await Page.GetByLabel("Password").FillAsync("Ab@mkl89de");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Log In" }).ClickAsync();
    }

	[Test]
    public async Task FindSignupElements()
    {
         await Page.GotoAsync("http://localhost:4200/home");

        await Page.Locator("a:nth-child(2)").ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "Sign Up" }).ClickAsync();

        await Page.GetByLabel("Fornavn").ClickAsync();

        await Page.GetByLabel("Fornavn").FillAsync("test");

        await Page.GetByLabel("Efternavn").ClickAsync();

        await Page.GetByLabel("Efternavn").FillAsync("test");

        await Page.GetByLabel("Email").ClickAsync();

        await Page.GetByLabel("Email").FillAsync("test@test.dk");

        await Page.GetByLabel("Adresse").ClickAsync();

        await Page.GetByLabel("Adresse").FillAsync("testing");

        await Page.GetByLabel("Postnummer").ClickAsync();

        await Page.GetByLabel("Postnummer").FillAsync("1234");

        await Page.GetByLabel("By").ClickAsync();

        await Page.GetByLabel("By").FillAsync("testingby");

        await Page.GetByLabel("Land").ClickAsync();

        await Page.GetByLabel("Land").FillAsync("testingland");

        await Page.GetByLabel("Telefon").ClickAsync();

        await Page.GetByLabel("Telefon").FillAsync("55555555");

        await Page.GetByLabel("Adgangskode").ClickAsync();

        await Page.GetByLabel("Adgangskode").FillAsync("Etlille123#");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Opret Dig" }).ClickAsync();

    }

	[Test]
    public async Task FindProductDetailElements()
    {
        await Page.GotoAsync("http://localhost:4200/home");

        await Page.Locator("ion-card").Filter(new() { HasText = "Hotdog Det er en Hotdog kr." }).GetByRole(AriaRole.Link).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { Name = "Produkt information" }).ClickAsync();

        await Page.GetByText("Varenummer:").ClickAsync();

        await Page.GetByText("Opdragelses i: Holland").ClickAsync();

        await Page.GetByText("Slagtet i: Danmark").ClickAsync();

        await Page.GetByText("Forventet holdbarhed: 30 dage").ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { Name = "Hotdog" }).ClickAsync();

        await Page.GetByText("Det er en Hotdog").ClickAsync();

        await Page.GetByText("DKK/per kg").ClickAsync();

        await Page.GetByRole(AriaRole.Spinbutton).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Læg i kurv" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "din slagter logo" }).ClickAsync();

    }
    
    
}
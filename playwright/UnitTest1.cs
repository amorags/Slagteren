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
    public async Task MyTest()
    {
        await Page.GotoAsync("http://localhost:4200/");

        await Page.GetByRole(AriaRole.Button).First.ClickAsync();

        await Page.GetByRole(AriaRole.Button).Nth(1).ClickAsync();

        await Page.GetByPlaceholder("Search").ClickAsync();

        await Page.GetByPlaceholder("Search").FillAsync("yes");

        await Page.GetByLabel("reset").ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "Oksekød" }).ClickAsync();

        await Page.GetByRole(AriaRole.Img, new() { Name = "din slagter logo" }).ClickAsync();

    }
    
    
}
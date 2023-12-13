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
        
    }
    
    
}
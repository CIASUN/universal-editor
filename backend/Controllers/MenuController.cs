using Microsoft.AspNetCore.Mvc;

namespace UEdit.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    [HttpGet]
    public IActionResult GetMenu()
    {
        var menu = new[]
        {
            new { key = "incoming-plan", title = "Входная план (из сетевого графика)" },
            new { key = "incoming-fact", title = "Входная факт (из ДОУМ)" },
            new { key = "commercial-plan", title = "Коммерческий план" },
            new { key = "plan-no-dz", title = "План без ДЗ" },
            new { key = "fact-first-day", title = "Факт на 1-е число месяца" },
            new { key = "network-comments", title = "Сетевой график (комментарии)" },
            new { key = "util-coeff", title = "Коэффициенты эксплуатации" },
            new { key = "quotas", title = "Квоты и директивы" },
            new { key = "month-expected", title = "Ожидаемая на месяц (посуточно)" },
            new { key = "business-plan", title = "Бизнес план" },
            new { key = "network", title = "Сетевой график" }
        };

        return Ok(menu);
    }
}

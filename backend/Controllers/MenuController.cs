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
            new { key = "incoming-plan", title = "Входная план (из сетевого графика)" , className = "ctrlProd_UEdt_ValueEditor"},
            new { key = "incoming-fact", title = "Входная факт (из ДОУМ)", className = "ctrlProd_UEdt_ValueEditor" },
            new { key = "commercial-plan", title = "Коммерческий план", className = "ctrlProd_UEdt_ValueEditor2" },
            new { key = "plan-no-dz", title = "План без ДЗ", className = "ctrlProd_UEdt_ValueEditor2" },
            new { key = "fact-first-day", title = "Факт на 1-е число месяца", className = "ctrlProd_UEdt_ValueEditor3" },
            new { key = "network-comments", title = "Сетевой график (комментарии)" , className = "ctrlProd_UEdt_ValueEditor"},
            new { key = "util-coeff", title = "Коэффициенты эксплуатации" , className = "ctrlProd_UEdt_ValueEditor"},
            new { key = "quotas", title = "Квоты и директивы" , className = "ctrlProd_UEdt_ValueEditor"},
            new { key = "month-expected", title = "Ожидаемая на месяц (посуточно)", className = "ctrlProd_UEdt_ValueEditor" },
            new { key = "business-plan", title = "Бизнес план" , className = "ctrlProd_UEdt_ValueEditor"},
            new { key = "network", title = "Сетевой график", className = "ctrlProd_UEdt_ValueEditor" }
        };

        return Ok(menu);
    }
}

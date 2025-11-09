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

    [HttpGet("items/{key}")]
    public IActionResult GetMenuItems(string key)
    {
        var items = Enumerable.Range(1, 8)
            .Select(i => new { id = i, name = $"{key} item {i}" })
            .ToList();

        return Ok(items);
    }


    [HttpGet("itemdetails/{key}/{id}")]
    public IActionResult GetMenuItemDetails(string key, int id)
    {
        var random = new Random();
        var cityNames = new[]
        {
        "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань",
        "Нижний Новгород", "Челябинск", "Самара", "Ростов-на-Дону", "Уфа"
    };

        var city = cityNames.ElementAtOrDefault((id - 1) % cityNames.Length) ?? "Неизвестный город";

        var details = Enumerable.Range(1, 10).Select(i => new
        {
            doName = $"{city} - Показатель {i}",
            value = Math.Round(random.NextDouble() * 100000, 2)
        });

        return Ok(new
        {
            key,
            id,
            city,
            date = DateTime.Now.ToString("dd.MM.yyyy"),
            details
        });
    }

}



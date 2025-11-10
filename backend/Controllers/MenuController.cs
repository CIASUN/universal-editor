using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UEdit.Data;

namespace UEdit.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{

    private readonly AppDbContext _db;
    public MenuController(AppDbContext db)
    {
        _db = db;
    }

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
        var names = new[]
        {
            "Входная добыча нефти и Г/К",
            "Входные ВНР",
            "Входные ВСП",
            "Входные Разрядки"
        };

        var items = names
            .Select((name, index) => new { id = index + 1, name })
            .ToList();

        return Ok(items);
    }


    //[HttpGet("itemdetails/{menuKey}/{id}")]
    //public async Task<IActionResult> GetItemDetails(string menuKey, int id, [FromQuery] string date)
    //{
    //    if (!DateTime.TryParse(date, out var monthDate))
    //        return BadRequest("Некорректная дата");

    //    // Явно задаём Kind=Utc
    //    var monthStart = DateTime.SpecifyKind(new DateTime(monthDate.Year, monthDate.Month, 1), DateTimeKind.Utc);
    //    var monthEnd = DateTime.SpecifyKind(monthStart.AddMonths(1), DateTimeKind.Utc);

    //    var items = await _db.ItemDetails
    //        .Where(d => d.MenuKey == menuKey
    //                    && d.SubId == id
    //                    && d.Month >= monthStart
    //                    && d.Month < monthEnd)
    //        .Select(d => new
    //        {
    //            doName = d.CityName,
    //            value = d.Value
    //        })
    //        .ToListAsync();

    //    return Ok(new
    //    {
    //        date = monthStart.ToString("yyyy-MM"),
    //        details = items
    //    });
    //}

    [HttpGet("itemdetails/{menuKey}/{id}")]
    public IActionResult GetItemDetails(string menuKey, int id, [FromQuery] string date)
    {
        if (!DateTime.TryParse(date, out var monthDate))
            return BadRequest("Некорректная дата");

        var monthStart = DateTime.SpecifyKind(new DateTime(monthDate.Year, monthDate.Month, 1), DateTimeKind.Utc);
        var rnd = new Random();

        // Значение "РОСНЕФТЬ ВСЕГО" от 0 до 70000
        decimal totalValue = Math.Round((decimal)(rnd.NextDouble() * 70000), 2);

        var lowerDoNames = new[]
        {
        "Роснефть (Российские активы)",
        "Роснефть (Зарубежные активы)",
        "EQUTY + ПРОЕКТЫ ВНЕ РФ",
        "РФ БЕЗ EQUTY",
        "ЮганскНефтегаз",
        "ЛУ Томской области",
        "Пурнефтегаз",
        "Харампурнефтегаз",
        "СевКомНефтегаз",
        "СамараНефтегаз",
        "в т.ч. СамараНефтегаз собств",
        "Удмуртнефть",
        "Сахалин-1",
        "Северное Чайво",
        "РН Ванкор ВСЕГО",
        "Ванкорнефть",
        "Сузун",
        "Тагульское",
        "в т.ч. Лодочное",
        "Восток-Ойл",
        "ТаймырНетегаз"
    };

        int lowerItemsCount = lowerDoNames.Length;
        decimal[] lowerValues = new decimal[lowerItemsCount];
        decimal remaining = totalValue;

        // Генерируем случайные значения для нижних строк
        for (int i = 0; i < lowerItemsCount - 1; i++)
        {
            // Минимум 500, чтобы не было 0
            decimal maxValue = remaining - (lowerItemsCount - i - 1) * 500;
            if (maxValue < 500) maxValue = 500;
            lowerValues[i] = Math.Round((decimal)(rnd.NextDouble() * (double)(maxValue - 500) + 500), 2);
            remaining -= lowerValues[i];
        }
        // Последняя строка чтобы сумма совпадала
        lowerValues[lowerItemsCount - 1] = Math.Round(remaining, 2);

        var items = new List<object>
    {
        new { doName = "РОСНЕФТЬ ВСЕГО", value = totalValue.ToString("N2") }
    };

        for (int i = 0; i < lowerItemsCount; i++)
        {
            items.Add(new { doName = lowerDoNames[i], value = lowerValues[i].ToString("N2") });
        }

        return Ok(new
        {
            date = monthStart.ToString("yyyy-MM"),
            details = items
        });
    }


}



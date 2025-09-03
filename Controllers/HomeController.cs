using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aok_s.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aok_s.Areas.Identity.Data;

namespace aok_s.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly aok_sIdentityDbContext _context;

    public HomeController(ILogger<HomeController> logger, aok_sIdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var semesters = _context.Semesters;

        if (semesters == null || !semesters.Any())
        {
            ViewBag.Semesters = new SelectList(Enumerable.Empty<SelectListItem>());
        }
        else
        {
            ViewBag.Semesters = new SelectList(semesters, "Id", "SemesterName");
            ViewBag.ClassFormations = new SelectList(_context.ClassFormations, "Id", "Name");
        }

        return View(); // Viewには何も渡さなくてもOK
    }

    public async Task<IActionResult> Result(int? semesterId, string? classFormationIds, string? departmentIds, string? majorIds, string className)
    {
        var startTime = DateTime.Now;
        //Classテーブルからデータを取得
        var classesQuery = _context.Classes
            .Include(c => c.ClassMajors)
                .ThenInclude(cm => cm.Major)
                    .ThenInclude(m => m.Department)
                        .ThenInclude(d => d.ClassFormation)
                            .ThenInclude(cf => cf.Semester)
            .AsQueryable();
        
        //semesterのプルダウンで選択された時の処理
        if (semesterId.HasValue)
        {
            classesQuery = classesQuery.Where(c => c.ClassMajors
                .Any(cm => cm.Major.Department.ClassFormation.SemesterId == semesterId));
        }

        //semesterのデータをプルダウンに渡す
        var semesters = await _context.Semesters.ToListAsync();
        ViewBag.Semesters = new SelectList(semesters, "Id", "SemesterName");


        // //classFormationのプルダウンで選択された時の処理
        // if (classFormationId.HasValue)
        // {
        //     classesQuery = classesQuery.Where(c => c.ClassMajors
        //         .Any(cm =>cm.Major.Department.ClassFormation.Id == classFormationId));
        // }

        //classFormationのプルダウンで選択された時の処理
        if (!string.IsNullOrEmpty(classFormationIds))
        {
            var ids = classFormationIds.Split(',').Select(id => int.Parse(id)).ToList();

            classesQuery = classesQuery.Where(c => c.ClassMajors
                .Any(cm => ids.Contains(cm.Major.Department.ClassFormationId)));       
        }

        //Departmentのプルダウンで選択された時の処理
        if (!string.IsNullOrEmpty(departmentIds))
        {
            var ids = departmentIds.Split(',').Select(id => int.Parse(id)).ToList();

            classesQuery = classesQuery.Where(c => c.ClassMajors
                .Any(cm => ids.Contains(cm.Major.DepartmentId)));
        }

        //Majorのプルダウンで選択された時の処理
        if (!string.IsNullOrEmpty(majorIds))
        {
            var ids = majorIds.Split(',').Select(id => int.Parse(id)).ToList();

            classesQuery = classesQuery.Where(c => c.ClassMajors
                .Any(cm => ids.Contains(cm.MajorId)));
        }


        if (!string.IsNullOrEmpty(className))
        {
            classesQuery = classesQuery.Where(c => c.ClassName.Contains(className));
        }

        var endTime = DateTime.Now;
        var elapsed = (endTime - startTime).TotalMilliseconds;
        Console.WriteLine($"検索時間: {elapsed} ms");

        return View(await classesQuery.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

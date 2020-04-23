using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Phd.Models;
using Phd.ViewModels;

namespace Phd.Controllers
{
    public class BRStudentGroupsController : BaseController
    {
        public BRStudentGroupsController(UserManager<User> userManager, SignInManager<User> signInManager, PhdContext context) : base(userManager, signInManager, context)
        {

        }

        // GET: BRStudentGroups
        public async Task<IActionResult> Index()
        {
            var phdContext = Context.BRStudentGroup.Include(b => b.BMajor);
            return View(await phdContext.ToListAsync());
        }

        // GET: BRStudentGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bRStudentGroup = await Context.BRStudentGroup
                .Include(b => b.BMajor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bRStudentGroup == null)
            {
                return NotFound();
            }

            return View(bRStudentGroup);
        }

        // GET: BRStudentGroups/Create
        public IActionResult Create()
        {
            ViewData["BMajorId"] = new SelectList(Context.BMajor, "Id", "Id");
            return View();
        }

        // POST: BRStudentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BMajorId")] BRStudentGroup bRStudentGroup)
        {
            if (ModelState.IsValid)
            {
                Context.Add(bRStudentGroup);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BMajorId"] = new SelectList(Context.BMajor, "Id", "Id", bRStudentGroup.BMajorId);
            return View(bRStudentGroup);
        }

        // GET: BRStudentGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bRStudentGroup = await Context.BRStudentGroup.FindAsync(id);
            if (bRStudentGroup == null)
            {
                return NotFound();
            }
            ViewData["BMajorId"] = new SelectList(Context.BMajor, "Id", "Id", bRStudentGroup.BMajorId);
            return View(bRStudentGroup);
        }

        // POST: BRStudentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BMajorId")] BRStudentGroup bRStudentGroup)
        {
            if (id != bRStudentGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context.Update(bRStudentGroup);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BRStudentGroupExists(bRStudentGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BMajorId"] = new SelectList(Context.BMajor, "Id", "Id", bRStudentGroup.BMajorId);
            return View(bRStudentGroup);
        }

        // GET: BRStudentGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bRStudentGroup = await Context.BRStudentGroup
                .Include(b => b.BMajor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bRStudentGroup == null)
            {
                return NotFound();
            }

            return View(bRStudentGroup);
        }

        // POST: BRStudentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bRStudentGroup = await Context.BRStudentGroup.FindAsync(id);
            Context.BRStudentGroup.Remove(bRStudentGroup);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BRStudentGroupExists(int id)
        {
            return Context.BRStudentGroup.Any(e => e.Id == id);
        }











        public async Task<IActionResult> CreateStudentAsync(int id)
        {
            ViewBag.Id = id;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentAsync([Bind("Iin,Fname,Mname,Lname,ThesisTopicRus,ThesisTopicKaz,ThesisTopicEng,ResearchSupervisorFname,ResearchSupervisorMname,ResearchSupervisorLname,ResearchSupervisorWorkPlace,ResearchSupervisorPosition,ReviewerFname,ReviewerMname,ReviewerLname,ReviewerWorkPlace,ReviewerPosition,ReviewerGrade,ConsultantFname,ConsultantMname,ConsultantLname,ConsultantWorkPlace,ConsultantPosition,BRStudentGroupId")] BRStudent bRStudent)
        {
            if (ModelState.IsValid)
            {
                Context.Add(bRStudent);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }
            return View(bRStudent);
        }


        public async Task<IActionResult> GetStudentsAsync(int id)
        {
            ViewBag.UserId = UserManager.GetUserId(HttpContext.User);
            var students = await Context.BRStudent
                                   .Where(m => m.BRStudentGroupId == id)
                                   .ToListAsync();

            return View(students);
        }




        /// 
        /// 
        /// 
        /// 
        /// 




        public async Task<IActionResult> CreateStudentGradeAsync(int studentId, string userId)
        {

            var bRStudentGrades = await Context.BRStudentGrade.ToListAsync();
            var condition = bRStudentGrades != null && bRStudentGrades.Any(x => x.UserId == userId && x.BRStudentId == studentId);
            if (!condition)
            {
                ViewBag.StudentId = studentId;
                ViewBag.UserId = UserManager.GetUserId(HttpContext.User);
            }
            else
            {
                return RedirectToAction(nameof(NotSuccess));
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudentGradeAsync([Bind("Value,BRStudentId,UserId")] BRStudentGrade bRStudentGrade)
        {


            if (ModelState.IsValid)
            {

                Context.Update(bRStudentGrade);
                await Context.SaveChangesAsync();


                return RedirectToAction(nameof(Success));
            }
            return View(bRStudentGrade);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentGradeAsync(int studentId)
        {


            var userId = UserManager.GetUserId(HttpContext.User); // пока не использую

            var student = await Context.BRStudent.Include(x => x.BRStudentGrade)
                                           .FirstOrDefaultAsync(x => x.Id == studentId);

            var grades = Context.BRStudentGrade.Include(x => x.User)
                                                //.ThenInclude(x => x.Roles)
                                                .Where(x => x.BRStudentId == studentId)
                                                .ToList();

            var users = Context.Users.Include(x => x.BRStudentGrades)
                                     .ToList();

            var usersss = UserManager.Users.Include(x => x.BRStudentGrades).ToList();

            var roles = Context.Roles.ToList();

            StudentGradeViewModel model = new StudentGradeViewModel
            {
                StudentName = student.Lname,
                AverageGrade = student.BRStudentGrade.Average(x => x.Value),
                Users = users,
                Grades = grades
            };

            return View(model);
        }













        public async Task<IActionResult> Success()
        {
            return View();
        }

        public async Task<IActionResult> NotSuccess()
        {
            return View();
        }














    }
}

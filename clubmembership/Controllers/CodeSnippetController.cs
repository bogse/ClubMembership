using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;
using clubmembership.Repository;
using clubmembership.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace clubmembership.Controllers
{
    
    public class CodeSnippetController : Controller
    {
        private CodeSnippetRepository codeSnippetRepository;
        private MemberRepository memberRepository;
        public CodeSnippetController(ApplicationDbContext dbContext)
        {
            memberRepository = new MemberRepository(dbContext);
            codeSnippetRepository = new CodeSnippetRepository(dbContext);
        }
        // GET: CodeSnippetController
        public ActionResult Index()
        {
            var list = codeSnippetRepository.GetAllCodeSnippets();
            var viewModelList = new List<CodeSnippetViewModel>(); //2
            foreach(var codeSnippet in list) //1
            {
                viewModelList.Add(new CodeSnippetViewModel(codeSnippet, memberRepository));
            }

            return View(viewModelList);
        }

        // GET: CodeSnippetController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = codeSnippetRepository.GetCodeSnippetById(id);
            return View("DetailsCodeSnippet", model);
        }

        // GET: CodeSnippetController/Create
        public ActionResult Create()
        {
            var members = memberRepository.GetAllMembers();
            var memberList = members.Select(x => new SelectListItem(x.Name, x.Idmember.ToString()));
            ViewBag.MemberList = memberList;
            var model = new CodeSnippetModel();
            model.IdsnippetPreviousVersion = codeSnippetRepository.GetLatestCodeSnippet().IdcodeSnippet;
            //ViewBag.LastCodeSnippetVersion = codeSnippetRepository.GetLatestCodeSnippet().IdcodeSnippet;

            return View("CreateCodeSnippet", model);
        }

        // POST: CodeSnippetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new CodeSnippetModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    codeSnippetRepository.InsertCodeSnippet(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCodeSnippet");
            }
        }

        // GET: CodeSnippetController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = codeSnippetRepository.GetCodeSnippetById(id);
            var members = memberRepository.GetAllMembers();
            var memberList = members.Select(x => new SelectListItem(x.Name, x.Idmember.ToString()));
            ViewBag.MemberList = memberList;
            return View("EditCodeSnippet", model);
        }

        // POST: CodeSnippetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new CodeSnippetModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    codeSnippetRepository.UpdateCodeSnippet(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: CodeSnippetController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = codeSnippetRepository.GetCodeSnippetById(id);
            return View("DeleteCodeSnippet", model);
        }

        // POST: CodeSnippetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                codeSnippetRepository.DeleteCodeSnippet(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}

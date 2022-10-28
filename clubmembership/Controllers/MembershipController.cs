using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clubmembership.Controllers
{
    public class MembershipController : Controller
    {
        private MembershipRepository membershipRepository;
        private MemberRepository memberRepository;
        private MembershipTypeRepository membershipTypeRepository;
        public MembershipController(ApplicationDbContext dbContext)
        {
            membershipTypeRepository = new MembershipTypeRepository(dbContext);
            memberRepository = new MemberRepository(dbContext);
            membershipRepository = new MembershipRepository(dbContext);
        }
        // GET: MembershipController
        public ActionResult Index()
        {
            var model = membershipRepository.GetAllMemberships();
            return View("Index", model);
        }

        // GET: MembershipController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = membershipRepository.GetMembershipById(id);
            return View("DetailsMembership", model);
        }

        // GET: MembershipController/Create
        public ActionResult Create()
        {
            var members = memberRepository.GetAllMembers();
            var memberList = members.Select(x => new SelectListItem(x.Name, x.Idmember.ToString()));
            ViewBag.MemberList = memberList;

            var membershipTypes = membershipTypeRepository.GetAllMembershipTypes();
            var membershipTypeList = membershipTypes.Select(x => new SelectListItem(x.Name, x.IdmembershipType.ToString()));
            ViewBag.MembershipTypeList = membershipTypeList;

            return View("CreateMembership");
        }

        // POST: MembershipController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MembershipModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    membershipRepository.InsertMembership(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMembership");
            }
        }

        // GET: MembershipController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = membershipRepository.GetMembershipById(id);
            return View("EditMembership", model);
        }

        // POST: MembershipController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MembershipModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    membershipRepository.UpdateMembership(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: MembershipController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = membershipRepository.GetMembershipById(id);
            return View("DeleteMembership", model);
        }

        // POST: MembershipController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                membershipRepository.DeleteMembership(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize]  // Must be logged in
    public class MembersController : Controller
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        // GET - List all members
        public IActionResult Index()
        {
            var allMembers = _context.Members
                .OrderByDescending(m => m.MembershipDate)
                .ToList();

            ViewBag.MemberCount = allMembers?.Count ?? 0;
            return View(allMembers);
        }

        // CREATE - Display form (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - Handle form submission (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email,PhoneNumber,Address,Status")] Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    member.MembershipDate = DateTime.Now;

                    _context.Members.Add(member);
                    _context.SaveChanges();

                    TempData["Success"] = $"Member '{member.Name}' added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error saving member: {ex.Message}");
                }
            }

            return View(member);
        }

        // EDIT - Display edit form (GET)
        public IActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // EDIT - Handle edit submission (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("MemberID,Name,Email,PhoneNumber,Address,Status,MembershipDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingMember = _context.Members.Find(member.MemberID);
                    if (existingMember == null)
                    {
                        return NotFound();
                    }

                    existingMember.Name = member.Name;
                    existingMember.Email = member.Email;
                    existingMember.PhoneNumber = member.PhoneNumber;
                    existingMember.Address = member.Address;
                    existingMember.Status = member.Status;

                    _context.SaveChanges();

                    TempData["Success"] = $"Member '{member.Name}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating member: {ex.Message}");
                }
            }

            return View(member);
        }

        // DELETE - Show delete confirmation (GET)
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // DELETE - Handle delete (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var member = _context.Members.Find(id);
                if (member != null)
                {
                    _context.Members.Remove(member);
                    _context.SaveChanges();
                    TempData["Success"] = "Member deleted successfully!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting member: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
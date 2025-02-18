﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smouhaclub.Models;

namespace Smouhaclub.Areas.CPanel.Controllers
{
    [Area("CPanel")]
    public class MembersController : Controller
    {
        private readonly SmouhaclubContext _context;

        public MembersController(SmouhaclubContext context)
        {
            _context = context;
        }

        // GET: CPanel/Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblMembers.ToListAsync());
        }

        // GET: CPanel/Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMember = await _context.TblMembers
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (tblMember == null)
            {
                return NotFound();
            }

            return View(tblMember);
        }

        // GET: CPanel/Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPanel/Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FirstName,LastName,MemberCode,PhoneNumber,MemberEmail,MemberAddress,DateBirth,JoinDate,RenewalDate,MembershipCost")] TblMember tblMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblMember);
        }

        // GET: CPanel/Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMember = await _context.TblMembers.FindAsync(id);
            if (tblMember == null)
            {
                return NotFound();
            }
            return View(tblMember);
        }

        // POST: CPanel/Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FirstName,LastName,MemberCode,PhoneNumber,MemberEmail,MemberAddress,DateBirth,JoinDate,RenewalDate,MembershipCost")] TblMember tblMember)
        {
            if (id != tblMember.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMemberExists(tblMember.MemberId))
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
            return View(tblMember);
        }

        // GET: CPanel/Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMember = await _context.TblMembers
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (tblMember == null)
            {
                return NotFound();
            }

            return View(tblMember);
        }

        // POST: CPanel/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMember = await _context.TblMembers.FindAsync(id);
            if (tblMember != null)
            {
                _context.TblMembers.Remove(tblMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMemberExists(int id)
        {
            return _context.TblMembers.Any(e => e.MemberId == id);
        }
    }
}

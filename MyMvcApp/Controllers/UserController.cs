using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Display the list of users
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Retrieve the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Display the Create view
        return View(new User());
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Add the new user to the list
            user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1; // Generate a new ID
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user); // Return the view with validation errors
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Retrieve the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        if (ModelState.IsValid)
        {
            // Find and update the user
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            return RedirectToAction(nameof(Index));
        }
        return View(user); // Return the view with validation errors
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Retrieve the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        // Find and remove the user
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}

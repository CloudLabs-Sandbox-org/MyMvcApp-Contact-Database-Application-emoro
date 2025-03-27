using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Linq;

namespace MyMvcApp.Tests;

public class UserControllerTests
{
    [Fact]
    public void Index_ReturnsViewWithUserList()
    {
        // Arrange
        UserController.userlist.Clear();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });
        var controller = new UserController();

        // Act
        var result = controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(UserController.userlist, result.Model);
    }

    [Fact]
    public void Details_ReturnsViewWithUser_WhenUserExists()
    {
        // Arrange
        UserController.userlist.Clear();
        var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
        UserController.userlist.Add(user);
        var controller = new UserController();

        // Act
        var result = controller.Details(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user, result.Model);
    }

    [Fact]
    public void Details_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        UserController.userlist.Clear();
        var controller = new UserController();

        // Act
        var result = controller.Details(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_PostAddsUserAndRedirects_WhenModelStateIsValid()
    {
        // Arrange
        UserController.userlist.Clear();
        var controller = new UserController();
        var user = new User { Name = "Jane Doe", Email = "jane@example.com" };

        // Act
        var result = controller.Create(user) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(nameof(controller.Index), result.ActionName);
        Assert.Single(UserController.userlist);
        Assert.Equal("Jane Doe", UserController.userlist.First().Name);
    }

    [Fact]
    public void Create_ReturnsViewWithModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var controller = new UserController();
        controller.ModelState.AddModelError("Name", "Required");
        var user = new User { Email = "jane@example.com" };

        // Act
        var result = controller.Create(user) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user, result.Model);
    }

    [Fact]
    public void Edit_PostUpdatesUserAndRedirects_WhenModelStateIsValid()
    {
        // Arrange
        UserController.userlist.Clear();
        var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
        UserController.userlist.Add(user);
        var controller = new UserController();
        var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };

        // Act
        var result = controller.Edit(1, updatedUser) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(nameof(controller.Index), result.ActionName);
        Assert.Equal("John Smith", UserController.userlist.First().Name);
    }

    [Fact]
    public void Edit_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        UserController.userlist.Clear();
        var controller = new UserController();
        var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };

        // Act
        var result = controller.Edit(1, updatedUser);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_PostRemovesUserAndRedirects_WhenUserExists()
    {
        // Arrange
        UserController.userlist.Clear();
        var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
        UserController.userlist.Add(user);
        var controller = new UserController();

        // Act
        var result = controller.Delete(1, null) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(nameof(controller.Index), result.ActionName);
        Assert.Empty(UserController.userlist);
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        UserController.userlist.Clear();
        var controller = new UserController();

        // Act
        var result = controller.Delete(1, null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

using DateAppAPI.Data;
using DateAppAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DateAppAPI.Controllers;

public class BuggyController(DataContext context):BaseAPIController{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAutho(){
        return "ccc";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound(){
        var result = context.AppUsers.Find(-1);
        if(result == null) return NotFound();
        return result;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError(){
        var result = context.AppUsers.Find(-1) ?? throw new Exception("A bad thing has happened");
        return result;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest(){
        return BadRequest("This is was not good request");
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoDachi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DojoDachi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
             if(HttpContext.Session.GetObjectFromJson<Dachi>("Dachi") == null)
            {
                HttpContext.Session.SetObjectAsJson("Dachi", new Dachi());
            }
            ViewBag.Dachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            if(ViewBag.msg == null) {
                ViewBag.msg = "Please do something!";
            }
            return View();
        }
        [HttpGet("feed")]
        public IActionResult FeedRedirect() {
            Dachi dachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            ViewBag.msg = dachi.Feed();
            Console.WriteLine(ViewBag.msg);
            HttpContext.Session.SetObjectAsJson("Dachi", dachi);
            return RedirectToAction("Index");
            }
        [HttpGet("play")]
        public IActionResult PlayRedirect() {
            Dachi dachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            ViewBag.msg = dachi.Play();
            HttpContext.Session.SetObjectAsJson("Dachi", dachi);
            return RedirectToAction("Index");
        }
        [HttpGet("work")]
        public IActionResult WorkRedirect() {
            Dachi dachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            ViewBag.msg = dachi.Work();
            HttpContext.Session.SetObjectAsJson("Dachi", dachi);
            return RedirectToAction("Index");
        }
        [HttpGet("sleep")]
        public IActionResult SleepRedirect() {
            Dachi dachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            ViewBag.msg = dachi.Sleep();
            HttpContext.Session.SetObjectAsJson("Dachi", dachi);
            return RedirectToAction("Index");
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
    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
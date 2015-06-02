using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Models;

namespace Tasks.Controllers
{
    public class SubTaskController : Controller
    {
        private TasksContext db = new TasksContext();

        public PartialViewResult _GetSubTasks(int id = 0)
        {
            ViewBag.TaskId = id;
            var subTasks = db.SubTasks.Where(x => x.TaskId == id);
            return PartialView(subTasks.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult _SubmitForm(int id = 0)
        {
            var model = new SubTask { TaskId = id };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Submit(SubTask subtask)
        {
            if (ModelState.IsValid)
            {
                db.SubTasks.Add(subtask);
                CheckCompleted(subtask.TaskId);
                db.SaveChanges();
            }

            ViewBag.TaskId = subtask.TaskId;
            var subTasks = db.SubTasks.Where(x => x.TaskId == subtask.TaskId);
            return PartialView("_GetSubTasks", subTasks.ToList());
        }

        //
        // GET: /SubTask/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // POST: /SubTask/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubTask subtask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subtask).State = EntityState.Modified;
                CheckCompleted(subtask.TaskId);
                db.SaveChanges();
                return RedirectToAction("Details", "Task", new { id = subtask.TaskId });
            }
            return View(subtask);
        }

        //
        // GET: /SubTask/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // POST: /SubTask/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubTask subtask = db.SubTasks.Find(id);
            var taskId = subtask.TaskId;
            db.SubTasks.Remove(subtask);
            db.SaveChanges();
            return RedirectToAction("Details", "Task", new { id = taskId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        internal void CheckCompleted(int taskId)
        {
            var task = db.Tasks.Where(x => x.TaskId == taskId).ToList();
            if (task.Count == 0) return;
            var notCompletedCount = task[0].SubTasks.Count(x => x.Completed == false);
            if (notCompletedCount == 0)
                task[0].Completed = true;
        }
    }
}
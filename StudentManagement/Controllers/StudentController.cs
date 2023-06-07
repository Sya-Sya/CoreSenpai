using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using StudentManagement.Models;
using StudentManagement.Services;


namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentServices _studentServices;

        public StudentController(IConfiguration configuration, IStudentServices studentServices)
        {
            _configuration = configuration;
            _studentServices = studentServices;
        }

        #region List of Students
        public IActionResult Index()
        {
            StudentVM model = new StudentVM(); // object created of StudentVM
            model.StudentList = _studentServices.GetStudentList().ToList(); // accessing the  GetStudentList() method from interface of _studentServices 
            return View(model); // returing with model @views
        }
        #endregion

        #region Updating student data
        [HttpPost]
        public IActionResult AddUpdateStudent(Student model) // must be unique identifier (StudentId) not an object
        {
            Student bj1 = new Student(); // creating new object of Student | not required to created object
            bj1.StudentId = model.StudentId; // not required unnecessary code #redudance | u can delete this block of code & can directly pass u r model into UpdateStudent method
            var updatedData = _studentServices.UpdateStudent(model);
            return Json(new { Message = "Edited Successfully ...." + updatedData });
        }
        #endregion

        #region Deleting Student records
        [HttpPost]
        public IActionResult DeleteStudent(int StudentId)
        {
            if (StudentId != 0 || StudentId > 0)
            {
                var DelData = _studentServices.DeleteStudent(StudentId);
                return Json(new { Message = "Delete Successfully ...." + StudentId });
            }
            else
            {
                return Json(new { Message = "Failed to update record." });
            }
            //string url = Request.Headers["Referer"].ToString();
            //string result = _studentServices.DeleteStudent(StudentId);
            //if (result == "Delete Successfully")
            //{
            //    return Json(new { Message = "Delete Successfully" });
            //}
            //else
            //{
            //    return Json(new { Message = "Error Occurred"});
            //}
        }
        #endregion

    }
}

// before update and insert
//StudentVM model = new StudentVM();

//student.CreatedBy = 1;
//string url = Request.Headers["Referer"].ToString();

//string result = string.Empty;

//if (student.StudentId > 0)
//{
//    result = _studentServices.UpdateStudent(student);
//}
//else
//{
//    result = _studentServices.InsertStudent(student);
//}
//if (result == "Save Successfully")
//{
//    TempData["SuccessMsg"] = "Save Successfully";
//    return Redirect(url);
//}
//else
//{
//    TempData["ErrorMsg"] = result;
//    return Redirect(url);
//}
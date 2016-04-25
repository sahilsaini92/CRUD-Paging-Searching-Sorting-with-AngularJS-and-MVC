using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapAngularMvc.Models;

namespace BootstrapAngularMvc.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Employee()
        {
            return View();
        }

        //For getting the all records from database.		
        public JsonResult getAll()
        {
            using (OmanERP_SahilEntities dataContext = new OmanERP_SahilEntities())
            {
                var employeeList = (from E in dataContext.Employees
                                    join dep in dataContext.Departments on E.DepartmentID equals dep.Id
                                    join dsg in dataContext.Designations on E.DesignationID equals dsg.Id
                                    orderby E.Id
                                    select new
                                    {
                                        E.Id,
                                        E.name,
                                        E.DOB,
                                        E.Gender,
                                        E.Email,
                                        E.Mobile,
                                        E.Address,
                                        E.JoiningDate,
                                        dep.DepartmentName,
                                        E.DepartmentID,
                                        dsg.DesignationName,
                                        E.DesignationID
                                    }).ToList();
                var JsonResult = Json(employeeList, JsonRequestBehavior.AllowGet);
                JsonResult.MaxJsonLength = int.MaxValue;
                return JsonResult;
            }
        }

        public JsonResult getEmployeeByNo(string id)
        {
            try
            {
                using (OmanERP_SahilEntities dataContext = new OmanERP_SahilEntities())
                {
                    int no = Convert.ToInt32(id);
                    var employeeList = dataContext.Employees.Find(no);
                    return Json(employeeList, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exp)
            {
                return Json("Error in getting record !", JsonRequestBehavior.AllowGet);
            }

        }


        public string UpdateEmployee(Employee Emp)
        {
            if (Emp != null)
            {
                using (OmanERP_SahilEntities dataContext = new OmanERP_SahilEntities())
                {
                    int no = Convert.ToInt32(Emp.Id);
                    var employeeList = dataContext.Employees.Where(x => x.Id == no).FirstOrDefault();
                    employeeList.name = Emp.name;
                    employeeList.DOB = Convert.ToDateTime(Emp.DOB).ToString("yyyy/MM/dd");
                    employeeList.Gender = Emp.Gender;
                    employeeList.Email = Emp.Email;
                    employeeList.Mobile = Emp.Mobile;
                    employeeList.Address = Emp.Address;
                    employeeList.JoiningDate = Convert.ToDateTime(Emp.JoiningDate).ToString("yyyy/MM/dd");
                    employeeList.DepartmentID = Emp.DepartmentID;
                    employeeList.DesignationID = Emp.DesignationID;
                    dataContext.SaveChanges();
                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Invalid Employee";
            }
        }


        public string AddEmployee(Employee Emp)
        {
            if (Emp != null)
            {
                using (OmanERP_SahilEntities dataContext = new OmanERP_SahilEntities())
                {
                    Employee employee = new Employee();
                    employee.name = Emp.name;
                    employee.DOB = Convert.ToDateTime(Emp.DOB).ToString("yyyy/MM/dd");
                    employee.Gender = Emp.Gender;
                    employee.Email = Emp.Email;
                    employee.Mobile = Emp.Mobile;
                    employee.Address = Emp.Address;
                    employee.JoiningDate = Convert.ToDateTime(Emp.JoiningDate).ToString("yyyy/MM/dd");
                    employee.DepartmentID = Emp.DepartmentID;
                    employee.DesignationID = Emp.DesignationID;
                    dataContext.Employees.Add(employee);
                    dataContext.SaveChanges();
                    return "Employee added Successfully";
                }
            }
            else
            {
                return "Addition of Employee unsucessfull !";
            }
        }

        public JsonResult GetDesignations()
        {
            using (OmanERP_SahilEntities context = new OmanERP_SahilEntities())
            {
                var desg = context.Designations.ToList();
                return Json(desg, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDepartments()
        {
            using (OmanERP_SahilEntities data = new OmanERP_SahilEntities())
            {
                var deps = data.Departments.ToList();
                return Json(deps, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

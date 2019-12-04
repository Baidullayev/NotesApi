using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gala.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Gala.Controllers
{
    
    
    public class HomeController : Controller
    {
        
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="patronymic"></param>
        /// <param name="birth_date_begin"></param>
        /// <param name="birth_date_end"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult SearchPerson(string requestType, string iin, string lastname, string firstname, string patronymic, string birth_date_begin, string birth_date_end)
        {
            if(requestType != null && iin != null)
            {
                if(requestType == "П")
                {
                    using (projectzeroContext db = new projectzeroContext())
                    {
                        var person = db.Persons2.Where(i => i.Iin == iin).First();
                        int DistrictCode = person.DistrictCode;
                        int RegionCode = person.RegionCode;
                        string RegAddressCity = person.RegAddressCity;
                        string RegAddressStreet = person.RegAddressStreet;
                        string RegAddressBuilding = person.RegAddressBuilding;
                        string RegAddressCorpus = person.RegAddressCorpus;
                        string RegAddressFlat = person.RegAddressFlat;
                        if (RegAddressStreet != null && RegAddressBuilding != null && RegAddressStreet.Length > 2)
                        {
                            if (RegAddressCorpus != null)
                            {
                                if(RegAddressFlat != null && RegAddressFlat != "")
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                            Where(a => a.DistrictCode == DistrictCode).
                                            Where(a => a.RegionCode == RegionCode).
                                            Where(a => a.RegAddressCity == RegAddressCity).
                                            Where(a => a.RegAddressBuilding == RegAddressBuilding).
                                            Where(a => a.RegAddressCorpus == RegAddressCorpus).
                                            Where(a => a.RegAddressFlat == RegAddressFlat).
                                            Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }else
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                            Where(a => a.DistrictCode == DistrictCode).
                                            Where(a => a.RegionCode == RegionCode).
                                            Where(a => a.RegAddressCity == RegAddressCity).
                                            Where(a => a.RegAddressBuilding == RegAddressBuilding).
                                            Where(a => a.RegAddressCorpus == RegAddressCorpus).                                            
                                            Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }

                            }
                            else
                            {                               
                                if (RegAddressFlat != null)
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.DistrictCode == DistrictCode).
                                        Where(a => a.RegionCode == RegionCode).
                                        Where(a => a.RegAddressCity == RegAddressCity).
                                        Where(a => a.RegAddressBuilding == RegAddressBuilding).                                        
                                        Where(a => a.RegAddressFlat == RegAddressFlat).
                                        Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }
                                else
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.DistrictCode == DistrictCode).
                                        Where(a => a.RegionCode == RegionCode).
                                        Where(a => a.RegAddressCity == RegAddressCity).
                                        Where(a => a.RegAddressBuilding == RegAddressBuilding).                                        
                                        Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }
                            }
                        }
                        return Json(null);
                    }
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if(requestType == "Р")
                {
                    using (projectzeroContext db = new projectzeroContext())
                    {
                        var person = db.Persons2.Where(i => i.Iin == iin).First();
                        string Firstname = person.Firstname;
                        string Lastname = person.Surname;
                        string Patronymic = person.Patronymic;
                        if (Lastname.ToLower().Contains("ов") || Lastname.ToLower().Contains("ев") || Lastname.ToLower().Contains("ёв") || Lastname.ToLower().Contains("ова") || Lastname.ToLower().Contains("ева") || Lastname.ToLower().Contains("ёва") || Lastname.ToLower().Contains("ский") || Lastname.ToLower().Contains("цкий") || Lastname.ToLower().Contains("ская") || Lastname.ToLower().Contains("цкая"))
                        {
                            //var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation);

                            string LastnameM = null;
                            if (Lastname.EndsWith("ский") || Lastname.EndsWith("цкий") || Lastname.EndsWith("ская") || Lastname.EndsWith("цкая"))
                            {
                                LastnameM = Lastname.Remove(Lastname.Length - 5, 5);
                            }else LastnameM = Lastname.Remove(Lastname.Length - 2, 2);

                            if (Patronymic != null && Patronymic != "")
                            {
                                    string PatronymicM = null;
                                    _ = Patronymic.Length > 5 ? (PatronymicM = Patronymic.Remove(Patronymic.Length - 4)) : (PatronymicM = Patronymic.Remove(Patronymic.Length - 2));
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.Surname.StartsWith(LastnameM) || a.Surname == Lastname).
                                        Where(b => b.Patronymic.StartsWith(PatronymicM) || b.Patronymic == PatronymicM || b.Patronymic.StartsWith(firstname)).
                                        Where(i => i.Iin != iin).ToList();

                                    var result2 = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.Surname == firstname + "ұлы" || a.Surname == firstname + "улы" || a.Surname == firstname + "қызы" || a.Surname == firstname + "кызы").
                                        Where(b => b.Patronymic == null).
                                        Where(i => i.Iin != iin).ToList();

                                    result.AddRange(result2);
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                    /////////////////////////////////////////////////////////////////
                                    //if (Patronymic.ToLower().EndsWith("на") || Patronymic.ToLower().EndsWith("кызы") || Patronymic.ToLower().EndsWith("қызы"))
                                    //{
                                    //    string PatronymicM = Patronymic.Remove(Patronymic.Length - 4);
                                    //    //var query = from persons2 in db.Persons2 where EF.Functions.Like(person.Surname, LastnameM + "%") &&
                                    //    //            (EF.Functions.Like(person.Patronymic,Patronymic) || 
                                    //    //            EF.Functions.Like(person.Patronymic, PatronymicM + "%")) select persons2;
                                    //    //query.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation);

                                    //    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                    //        Where(a => a.Surname.StartsWith(LastnameM) || a.Surname == LastnameM).
                                    //        Where(b => b.Patronymic.StartsWith(PatronymicM) || b.Patronymic == PatronymicM).
                                    //        Where(i => i.Iin != iin).ToList();
                                    //    foreach (var item in result)
                                    //    {
                                    //        item.RegionCodeNavigation.Persons2 = null;
                                    //        item.DistrictCodeNavigation.Persons2 = null;
                                    //    }
                                    //    return Json(result);
                                    //}
                                    //else if (patronymic.ToLower().EndsWith("ич") || patronymic.ToLower().EndsWith("ович") || patronymic.ToLower().EndsWith("евич") || patronymic.ToLower().EndsWith("ович"))
                                    //{
                                    //    string PatronymicM = null;
                                    //    _ = patronymic.Length > 5 ? (PatronymicM = patronymic.Remove(patronymic.Length - 4)) : (PatronymicM = patronymic.Remove(patronymic.Length - 2));

                                    //    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                    //       Where(a => a.Surname.StartsWith(LastnameM) || a.Surname == LastnameM).
                                    //       Where(b => b.Patronymic.StartsWith(PatronymicM) || b.Patronymic == PatronymicM).
                                    //       Where(i => i.Iin != iin).ToList();
                                    //    foreach (var item in result)
                                    //    {
                                    //        item.RegionCodeNavigation.Persons2 = null;
                                    //        item.DistrictCodeNavigation.Persons2 = null;
                                    //    }
                                    //    return Json(result);
                                    //}
                                    ////////////////////////////////////////////////////////
                                }
                                else
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.Surname.StartsWith(LastnameM) || a.Surname == Lastname).
                                        Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }
                        } else
                        {
                            if (Patronymic != null && Patronymic != "")
                            {
                                string PatronymicM = null;
                                _ = patronymic.Length > 5 ? (PatronymicM = patronymic.Remove(patronymic.Length - 4)) : (PatronymicM = patronymic.Remove(patronymic.Length - 2));
                                var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.Surname == Lastname || a.Surname == Lastname).
                                        Where(b => b.Patronymic.StartsWith(PatronymicM) || b.Patronymic == PatronymicM || b.Patronymic.StartsWith(firstname)).
                                        Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                            }
                            else
                            {
                                var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.Surname == Lastname).
                                        Where(i => i.Iin != iin).ToList();
                                foreach (var item in result)
                                {
                                    item.RegionCodeNavigation.Persons2 = null;
                                    item.DistrictCodeNavigation.Persons2 = null;
                                }
                                return Json(result);
                            }
                        }

                    }                   
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (requestType == "С")
                {

                    using (projectzeroContext db = new projectzeroContext())
                    {
                        var person = db.Persons2.Where(i => i.Iin == iin).First();
                        int DistrictCode = person.DistrictCode;
                        int RegionCode = person.RegionCode;
                        string RegAddressCity = person.RegAddressCity;
                        string RegAddressStreet = person.RegAddressStreet;
                        string RegAddressBuilding = person.RegAddressBuilding;
                        string RegAddressCorpus = person.RegAddressCorpus;
                        string RegAddressFlat = person.RegAddressFlat;
                        if (RegAddressStreet != null && RegAddressBuilding != null && RegAddressStreet.Length > 2)
                        {
                            if (RegAddressFlat == null)
                            {
                                int outBuilding;
                                if (Int32.TryParse(RegAddressBuilding, out outBuilding))
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.DistrictCode == DistrictCode).
                                        Where(a => a.RegionCode == RegionCode).
                                        Where(a => a.RegAddressCity == RegAddressCity).
                                        Where(a => a.RegAddressStreet == RegAddressStreet).
                                        Where(a => a.RegAddressBuilding == (outBuilding - 1).ToString() || a.RegAddressBuilding == (outBuilding + 1).ToString() || a.RegAddressBuilding.StartsWith(RegAddressBuilding + "/") || a.RegAddressBuilding == (outBuilding + 2).ToString() || a.RegAddressBuilding == (outBuilding - 2).ToString()).
                                        Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }
                                else
                                {
                                    var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                        Where(a => a.DistrictCode == DistrictCode).
                                        Where(a => a.RegionCode == RegionCode).
                                        Where(a => a.RegAddressCity == RegAddressCity).
                                        Where(a => a.RegAddressStreet == RegAddressStreet).
                                        Where(a => a.RegAddressBuilding != RegAddressBuilding).
                                        Where(i => i.Iin != iin).ToList();
                                    foreach (var item in result)
                                    {
                                        item.RegionCodeNavigation.Persons2 = null;
                                        item.DistrictCodeNavigation.Persons2 = null;
                                    }
                                    return Json(result);
                                }
                            }
                            else
                            {
                                int outFLat;
                                if (RegAddressCorpus != null && RegAddressCorpus != "")
                                {
                                    if (Int32.TryParse(RegAddressFlat, out outFLat))
                                    {
                                        var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                            Where(a => a.DistrictCode == DistrictCode).
                                            Where(a => a.RegionCode == RegionCode).
                                            Where(a => a.RegAddressCity == RegAddressCity).
                                            Where(a => a.RegAddressBuilding == RegAddressBuilding).
                                            Where(a => a.RegAddressCorpus == RegAddressCorpus).
                                            Where(a => a.RegAddressFlat == (outFLat - 1).ToString() || a.RegAddressBuilding == (outFLat + 1).ToString()).
                                            Where(i => i.Iin != iin).ToList();
                                        foreach (var item in result)
                                        {
                                            item.RegionCodeNavigation.Persons2 = null;
                                            item.DistrictCodeNavigation.Persons2 = null;
                                        }
                                        return Json(result);
                                    }
                                }
                                else
                                {
                                    if (Int32.TryParse(RegAddressFlat, out outFLat))
                                    {
                                        var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                            Where(a => a.DistrictCode == DistrictCode).
                                            Where(a => a.RegionCode == RegionCode).
                                            Where(a => a.RegAddressCity == RegAddressCity).
                                            Where(a => a.RegAddressBuilding == RegAddressBuilding).
                                            Where(a => a.RegAddressFlat == (outFLat - 1).ToString() || a.RegAddressBuilding == (outFLat + 1).ToString()).
                                            Where(i => i.Iin != iin).ToList();
                                        foreach (var item in result)
                                        {
                                            item.RegionCodeNavigation.Persons2 = null;
                                            item.DistrictCodeNavigation.Persons2 = null;
                                        }
                                        return Json(result);
                                    }
                                }
                            }
                        }
                        return Json(null);
                    }
                    
                }                
            }

            else if (requestType == null && iin == null)
            {
                using (projectzeroContext db = new projectzeroContext())
                {
                    DateTime bdate_end = DateTime.UtcNow.Date;
                    DateTime bdate_begin = DateTime.UtcNow.Date;

                    if (birth_date_begin != null)
                    {
                        bdate_begin = DateTime.Parse(birth_date_begin);
                    }
                    if (birth_date_end != null)
                    {
                        bdate_end = DateTime.Parse(birth_date_end);
                    }

                    if (birth_date_begin != null && birth_date_end == null)
                    {
                        bdate_end = DateTime.UtcNow.Date;
                        birth_date_end = DateTime.UtcNow.Date.ToString();
                    }
                    if (birth_date_begin == null && birth_date_end != null)
                    {
                        bdate_begin = new DateTime(1800, 1, 1);
                        birth_date_begin = "01.01.1800";
                    }

                    if (lastname != null && firstname != null && patronymic != null && birth_date_begin == null && birth_date_end == null)
                    {
                        var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).
                           Where(b => b.Firstname == firstname).Where(c => c.Patronymic == patronymic).ToList();
                        foreach (var item in result)
                        {
                            item.RegionCodeNavigation.Persons2 = null;
                            item.DistrictCodeNavigation.Persons2 = null;
                        }
                        return Json(result);
                    }
                    else if (lastname != null && firstname != null && patronymic == null && birth_date_begin == null && birth_date_end == null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).
                                Where(b => b.Firstname == firstname).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }
                    else if (lastname != null && firstname == null && patronymic == null && birth_date_begin == null && birth_date_end == null)
                    {
                        try
                        {
                            if (lastname == "*")
                            {
                                var all = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).ToList();
                                foreach (var item in all)
                                {
                                    item.RegionCodeNavigation.Persons2 = null;
                                    item.DistrictCodeNavigation.Persons2 = null;

                                }
                                return Json(all);
                            }
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;

                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }

                    }
                    else if (lastname == null && firstname == null && patronymic != null && birth_date_begin == null && birth_date_end == null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Patronymic == patronymic).ToList();
                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }

                    }
                    else if (lastname != null && firstname != null && patronymic != null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).ToList().
                                Where(b => b.Firstname == firstname).
                                Where(c => c.Patronymic == patronymic).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end);

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }

                    }
                    else if (lastname != null && firstname != null && patronymic == null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).ToList().
                                Where(b => b.Firstname == firstname).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end);

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }

                    }
                    else if (birth_date_begin != null && birth_date_end != null && lastname == null && firstname == null && patronymic == null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }


                    }
                    else if (lastname != null && firstname == null && patronymic == null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).
                                 Where(d => d.BirthDate >= bdate_begin).
                                 Where(e => e.BirthDate <= bdate_end).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }
                    else if (lastname == null && firstname != null && patronymic == null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Firstname == firstname).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }
                    else if (lastname == null && firstname == null && patronymic != null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Patronymic == patronymic).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }
                    else if (lastname == null && firstname != null && patronymic != null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Firstname == firstname).
                                Where(a => a.Patronymic == patronymic).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }
                    else if (lastname == null && firstname != null && patronymic != null && birth_date_begin == null && birth_date_end == null)
                    {
                        try
                        {
                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Firstname == firstname).
                                Where(a => a.Patronymic == patronymic).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }
                    else if (lastname != null && firstname == null && patronymic != null && birth_date_begin != null && birth_date_end != null)
                    {
                        try
                        {


                            var result = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).Where(a => a.Surname == lastname).
                                Where(a => a.Patronymic == patronymic).
                                Where(d => d.BirthDate >= bdate_begin).
                                Where(e => e.BirthDate <= bdate_end).ToList();

                            foreach (var item in result)
                            {
                                item.RegionCodeNavigation.Persons2 = null;
                                item.DistrictCodeNavigation.Persons2 = null;
                            }
                            return Json(result);
                        }
                        catch (Exception ex)
                        {
                            return Json(ex.Message);
                        }
                    }


                    return Json(null);
                }
            }
            return Json(null);
        }
        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        /// <returns></returns>

        [Authorize]
        [HttpGet]
        public JsonResult GetPersonsJson()
        {

            //var result = db.Persons2.Include(destName => destName.RegionCodeNavigation).ToList();
            using (projectzeroContext db = new projectzeroContext())
            {
                var persons = db.Persons2.Include(r => r.RegionCodeNavigation).Include(d => d.DistrictCodeNavigation).ToList();
                //var persons = db.Persons2.ToList();
                foreach (var item in persons)
                {
                    item.RegionCodeNavigation.Persons2 = null;
                    item.DistrictCodeNavigation.Persons2 = null;
                }
                //var persons = db.Persons2.Where(s => s.Firstname == "ҚАНАТ").First();
                //db.Entry(persons).Collection(d => d.DistrictCodeNavigation)
                return Json(persons);
            }
            // var result = db.Persons2.ToList();

            //result[0].DistrictName = db.Districts.Where(a => a.Id == result[0].DistrictCode).First().Name;
            //result[0].RegionName = db.Regions.Where(a => a.Id == result[0].RegionCode).First().Name;
            //  return Json(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

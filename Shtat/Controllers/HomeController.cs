using Shtat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using Rotativa.MVC;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Shtat.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        public shtatEntities db = new shtatEntities();

        public ViewResult Index(string sortOrder)
        {

            SelectList podrazdD = new SelectList(db.Podrazdel, "IDp", "Podrazd");
            ViewBag.ppp = podrazdD;

            SelectList doljn = new SelectList(db.Doljnost.OrderBy(d=>d.Doljnost1), "IDDolj", "Doljnost1");
            ViewBag.doljn = doljn;

            //SelectList otdelL = new SelectList(db.Otdel, "IDot","Otdel1");
            //ViewBag.ooo = otdelL;

            List<Podrazdel> podrazdList = new List<Podrazdel>();
            podrazdList = db.Podrazdel.ToList();
            ViewBag.Podr = podrazdList;


            List<Otdel> otdelList = new List<Otdel>();
            otdelList = db.Otdel.ToList();
            ViewBag.Otd = otdelList;

            List<Gruppa> gruppalList = new List<Gruppa>();
            gruppalList = db.Gruppa.ToList();
            ViewBag.gr = gruppalList;

            List<Uchastok> uchastoklList = new List<Uchastok>();
            uchastoklList = db.Uchastok.ToList();
            ViewBag.uch = uchastoklList;

            ViewBag.PodrSortParm = sortOrder == "IDPodrPrUp" ? "IDPodrPrDown" : "IDPodrPrUp";
            ViewBag.SotrSortParm = sortOrder == "IDSotrUp" ? "IDSotrDown" : "IDSotrUp";
            ViewBag.DoljSortParm = sortOrder == "IDDoljUp" ? "IDDoljDown" : "IDDoljUp";
            ViewBag.StavSortParm = sortOrder == "IDStavUp" ? "IDStavDown" : "IDStavUp";
            var vacant = from s in db.vacancy
                         select s;
            switch (sortOrder)
            {
                case "IDPodrPrUp":
                    vacant = vacant.OrderByDescending(s => s.Podrazdel.Podrazd);
                    break;
                case "IDPodrPrDown":
                    vacant = vacant.OrderBy(s => s.Podrazdel.Podrazd);
                    break;

                case "IDSotrUp":
                    vacant = vacant.OrderByDescending(s => s.Sotrudnik.Sotrudnik1);
                    break;
                case "IDSotrDown":
                    vacant = vacant.OrderBy(s => s.Sotrudnik.Sotrudnik1);
                    break;
                case "IDDoljUp":
                    vacant = vacant.OrderByDescending(s => s.Doljnost.Doljnost1);
                    break;
                case "IDDoljDown":
                    vacant = vacant.OrderBy(s => s.Doljnost.Doljnost1);
                    break;

                case "IDStavUp":
                    vacant = vacant.OrderByDescending(s => s.BazTarStavka.Stavka);
                    break;
                case "IDStavDown":
                    vacant = vacant.OrderBy(s => s.BazTarStavka.Stavka);
                    break;
                default:
                    vacant = vacant.OrderBy(s => s.IDPodrPr).ThenBy(a => a.Otdel.priznak).ThenBy(h=> h.Gruppa) .ThenBy(b=> b.IDPodrUch).ThenBy(k=> k.Doljnost.Priznak);
                    //vacant = vacant.OrderBy(s => s.IDPodrPr).ThenBy(a => a.IDPodrOtd);
                    break;

            }

            //vacant = db.vacancy.OrderBy(x => x.Podrazdel.Podrazd).ThenBy(m => m.Otdel).ThenBy(n => n.Gruppa).ThenBy(z => z.Uchastok).ThenBy(s => s.Doljnost.Doljnost1).ToList();
            return View(vacant.ToList());
        }



        //////////////////// СПИСОК отделов в зависимости от подразделения ////////////////////
        public ActionResult GetOtdel(int ID)
        {
            List<Otdel> Otdelll = new List<Otdel>();
            Otdelll = db.Otdel.OrderBy(x => x.Otdel1).Where(a => a.IDpodr == ID).ToList();
            return PartialView(Otdelll);
        }

        //список отделов в зависимоти от подразделения в добавлении вакансии
        public ActionResult GetOtdel1(int ID)
        {
            List<Otdel> Otdelll1 = new List<Otdel>();
            Otdelll1 = db.Otdel.OrderBy(x => x.Otdel1).Where(a => a.IDpodr == ID).ToList();
            return PartialView(Otdelll1);
        }

        //список отделов в зависимости от подразделений

        public ActionResult GetOtdel2(string ID)
        {
            List<string> Otdelll2 = new List<string>();
            Otdelll2 = db.History.Where(b => b.Podrazd.Equals(ID)).OrderBy(a => a.Otdel).Select(x => x.Otdel).Distinct().ToList();

            return PartialView(Otdelll2);
        }

        //список отделов в зависимости от подразделений
        public ActionResult GetGruppa2(string ID)
        {
            List<string> gruppaa2 = new List<string>();
            //gruppaa2 = db.Gruppa.OrderBy(x => x.Gruppa1).Where(a => a.IDOtdel == ID).ToList();
            gruppaa2 = db.History.Where(b => b.Otdel.Equals(ID)).OrderBy(a => a.Gruppa).Select(x => x.Gruppa).Distinct().ToList();
            return PartialView(gruppaa2);
        }


        //public ActionResult GetOtdel2(string ID)
        //{
        //    List<Otdel> Otdelll2 = new List<Otdel>();

        //    //Otdelll2 = db.Otdel.OrderBy(x => x.Otdel1).Where(a => a.Otdel1 == ID).ToList();
        //    Otdelll2 = db.Otdel.OrderBy(x => x.Otdel1).ToList();
        //    return PartialView(Otdelll2);
        //}

        //////////////////// СПИСОК групп в зависимости от отдела ////////////////////
        public ActionResult GetGruppa(int ID)
        {
            List<Gruppa> gruppaa = new List<Gruppa>();
            gruppaa = db.Gruppa.OrderBy(x => x.Gruppa1).Where(a => a.IDOtdel == ID).ToList();
            return PartialView(gruppaa);
        }

        //список групп в зависимости от отдела в добавлении вакансии
        public ActionResult GetGruppa1(int ID)
        {
            List<Gruppa> gruppaa1 = new List<Gruppa>();
            gruppaa1 = db.Gruppa.OrderBy(x => x.Gruppa1).Where(a => a.IDOtdel == ID).ToList();
            return PartialView(gruppaa1);
        }

        //////////////////// СПИСОК участков в зависимости от групп ////////////////////
        public ActionResult GetUch(int ID)
        {
            List<Uchastok> Uch = new List<Uchastok>();
            Uch = db.Uchastok.OrderBy(x => x.Uchastok1).Where(a => a.IDGrup == ID).ToList();
            return PartialView(Uch);
        }


        public ActionResult GetUch2(int ID)
        {
            List<Uchastok> Uch2 = new List<Uchastok>();
            Uch2 = db.Uchastok.OrderBy(x => x.Uchastok1).Where(a => a.IDGrup == ID).ToList();
            return PartialView(Uch2);
        }


        //список участков в зависимости от групп в добавлении вакансии
        public ActionResult GetUch1(int ID)
        {
            List<Uchastok> Uch1 = new List<Uchastok>();
            Uch1 = db.Uchastok.OrderBy(x => x.Uchastok1).Where(a => a.IDGrup == ID).ToList();
            return PartialView(Uch1);
        }

        //public ActionResult About()
        //{
        //    List<Sotrudnik> Sotrudniks = new List<Sotrudnik>();
        //    Sotrudniks = db.Sotrudnik.ToList();
        //    return View(Sotrudniks);            
        //}

        public ViewResult About(string sortSotr)
        {
            ViewBag.SotrudSortParm = sortSotr == "IDSotrUp" ? "IDSotrDown" : "IDSotrUp";
            var sotr = from s in db.Sotrudnik
                       select s;
            switch (sortSotr)
            {
                case "IDSotrUp":
                    sotr = sotr.OrderByDescending(s => s.Sotrudnik1);
                    break;

                case "IDSotrDown":
                    sotr = sotr.OrderBy(s => s.Sotrudnik1);
                    break;

                default:
                    sotr = sotr.OrderBy(s => s.Sotrudnik1);
                    break;
            }
            return View(sotr.ToList());
        }
        //public ActionResult Contact()
        //{
        //    List<Doljnost> Doljnosts = new List<Doljnost>();
        //    Doljnosts = db.Doljnost.ToList();
        //    return View(Doljnosts);

        //}
        public ViewResult Contact(string sortDolj)
        {
            ViewBag.DoljSortParm = sortDolj == "IDDoljUp" ? "IDDoljDown" : "IDDoljUp";
            var dolj = from s in db.Doljnost
                       select s;
            switch (sortDolj)
            {
                case "IDDoljUp":
                    dolj = dolj.OrderByDescending(s => s.Doljnost1);
                    break;

                case "IDDoljDown":
                    dolj = dolj.OrderBy(s => s.Doljnost1);
                    break;

                default:
                    dolj = dolj.OrderBy(s => s.Doljnost1);
                    break;
            }
            return View(dolj.ToList());
        }


        public ActionResult Mk()
        {
            List<Razr> Razrs = new List<Razr>();
            Razrs = db.Razr.ToList();
            return View(Razrs);
        }

        public ActionResult Bts()
        {
            List<BazTarStavka> BazTarStavkas = new List<BazTarStavka>();
            BazTarStavkas = db.BazTarStavka.ToList();
            return View(BazTarStavkas);
        }
        public ActionResult Report()
        {
            SelectList podrazdD = new SelectList(db.Podrazdel, "Podrazd", "Podrazd");
            ViewBag.ppps = podrazdD;

            List<HistoryDat> HD = new List<HistoryDat>();
            HD = db.HistoryDat.ToList();
            ViewBag.HD = HD;

            List<History> hist = new List<History>();
            //hist = db.History.OrderBy(x => x.Podrazd).ThenBy(m => m.Otdel).ThenBy(n => n.Gruppa).ThenBy(z => z.Uch).ThenBy(s => s.Dolj).ToList();
            //hist = db.History.OrderBy(b=>b.Dat).ThenBy(x => x.Podrazd).ThenBy(m => m.Otdel).ThenBy(n => n.Gruppa).ThenBy(z => z.Uch).ThenBy(s => s.Dolj).ToList();
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            hist = db.History.OrderBy(l=>l.Dat).ThenBy(b => b.Podrazd).ThenBy(x => x.PrizOtdel).ThenBy(g=>g.Gruppa).ThenBy(h=>h.Uch).ThenBy(k=>k.PrizDolj).ToList();
            //hist = db.History.OrderBy(x=>x.Podrazd).ToList();
            //hist = db.History.ToList();
            return View(hist);
        }

        public ActionResult AddSotrudnik()
        {
            return PartialView();
        }


        //Добавить должность//

        public ActionResult AddDolj()
        {
            return PartialView();
        }
        //--------------------------//

        //Добавить разрядМК//

        public ActionResult AddRazr()
        {
            return PartialView();
        }
        //--------------------------//


        //Добавить БТС//

        public ActionResult AddBts()
        {
            return PartialView();
        }
        //--------------------------//

        public ActionResult AddSotrudnikSave()
        {

            return PartialView();

        }

        [HttpPost]
        public ActionResult SotrudnikAddSave(string Sotrudnik1)
        {
            try
            {
                Sotrudnik Sotrudniki = new Sotrudnik();
                Sotrudniki.Sotrudnik1 = Sotrudnik1.Trim();
                db.Sotrudnik.Add(Sotrudniki);

                db.SaveChanges();

                ViewBag.Message = "Сотрудник добавлен";
            }
            catch (Exception ex)
            {


                ViewBag.Message = ex.ToString();
            }

            return PartialView();
        }


        //Сохранение должности в базе данных//

        [HttpPost]
        public ActionResult DoljAddSave(string doljnost,string dat)
        {
            try
            {
                Doljnost doljnostt = new Doljnost();
                doljnostt.Doljnost1= doljnost.Trim();

                doljnostt.Datein = Convert.ToDateTime(dat);
                doljnostt.Priznak = 2;
                db.Doljnost.Add(doljnostt);

                db.SaveChanges();

                ViewBag.Message = "Должность добавлена";
            }
            catch (Exception ex)
            {


                ViewBag.Message = ex.ToString();
            }

            return PartialView();
        }

        //----------------------------------//


        //Сохранение разряда МК в базе данных//

        [HttpPost]
        public ActionResult RazrAddSave(string razr, string ETC, string mk, string dat)
        {
            try
            {
                Razr razrr = new Razr();
                razrr.Razr1 = Convert.ToInt32(razr.Trim());
                razrr.RazrETC = Convert.ToInt32(ETC.Trim());
                razrr.MK = Convert.ToInt32(mk.Trim());
                razrr.Datein = Convert.ToDateTime(dat);
                db.Razr.Add(razrr);

                db.SaveChanges();

                ViewBag.Message = "Разряд добавлен";
            }
            catch (Exception ex)
            {


                ViewBag.Message = ex.ToString();
            }

            return PartialView();
        }

        //----------------------------------//


        //Сохранение БТС в базе данных//

        [HttpPost]
        public ActionResult BtsAddSave(string gruppa, string bts, string dat)
        {
            try
            {
                BazTarStavka btss = new BazTarStavka();
                btss.Gruppa = Convert.ToInt32(gruppa.Trim());
                btss.Stavka = Convert.ToInt32(bts.Trim());
                btss.Datein = Convert.ToDateTime(dat);
                db.BazTarStavka.Add(btss);

                db.SaveChanges();

                ViewBag.Message = "Базовая тарифная ставка добавлена";
            }
            catch (Exception ex)
            {


                ViewBag.Message = ex.ToString();
            }

            return PartialView();
        }

        //----------------------------------//

        public ActionResult EditSotrudnik(int ID)
        {
            Sotrudnik Sotrudniki = new Sotrudnik();
            Sotrudniki = db.Sotrudnik.FirstOrDefault(a => a.IDSot == ID);
            return PartialView(Sotrudniki);

        }

        // Редактирование должности//

        public ActionResult EditDolj(int ID)
        {
            Doljnost dolj = new Doljnost();
            dolj = db.Doljnost.FirstOrDefault(a => a.IDDolj == ID);
            return PartialView(dolj);
        }

        //-------------------------//

        // Редактирование разряда МК//

        public ActionResult EditRazr(int ID)
        {
            Razr razr = new Razr();
            razr = db.Razr.FirstOrDefault(a => a.IDRazr == ID);
            return PartialView(razr);

        }
        //-------------------------//

        // Редактирование БТС//

        public ActionResult EditBts(int ID)
        {
            BazTarStavka bts = new BazTarStavka();
            bts = db.BazTarStavka.FirstOrDefault(a => a.IDStavka == ID);
            return PartialView(bts);

        }
        //-------------------------//


        [HttpPost]
        public ActionResult SotrudnikSave(int ID, string Sotrudnik1)
        {
            try
            {
                Sotrudnik Sotrudniki = new Sotrudnik();
                Sotrudniki = db.Sotrudnik.FirstOrDefault(a => a.IDSot == ID);
                Sotrudniki.Sotrudnik1 = Sotrudnik1.Trim();
                db.SaveChanges();

                ViewBag.Message = "Фамилия изменена";

            }
            catch
            {
                ViewBag.Message = "Ошибка";
            }

            return PartialView();
        }


        // сохранение редактирования должности//

        [HttpPost]
        public ActionResult DoljSave(int ID, string Doljnost1, string Datein, string Dateout)
        {
            try
            {
                Doljnost dol = new Doljnost();
                dol = db.Doljnost.FirstOrDefault(a => a.IDDolj == ID);
                dol.Doljnost1 = Doljnost1.Trim();
                dol.Datein = Convert.ToDateTime(Datein.Trim());
                if (Dateout == "")
                {
                    dol.Dateout = null;
                }
                else
                {
                    dol.Dateout = Convert.ToDateTime(Dateout.Trim());
                }
                    db.SaveChanges();

                ViewBag.Message = "Должность изменена";

            }
            catch
            {
                ViewBag.Message = "Ошибка";
            }

            return PartialView();
        }
        //--------------------------------------//

        // сохранение редактирования разряда МК//

        [HttpPost]
        public ActionResult RazrSave(int ID, string Razr1, string RazrETC, string MK, string Datein, string Dateout)
        {
            try
            {
                Razr razrr = new Razr();
                razrr = db.Razr.FirstOrDefault(a => a.IDRazr == ID);
                razrr.Razr1 = Convert.ToInt32(Razr1.Trim());
                razrr.RazrETC = Convert.ToInt32(RazrETC.Trim());
                razrr.MK = Convert.ToInt32(MK.Trim());
                razrr.Datein = Convert.ToDateTime(Datein.Trim());
                if (Dateout == "")
                {
                    razrr.Dateout = null;
                }
                else
                {
                    razrr.Dateout = Convert.ToDateTime(Dateout.Trim());
                }
                db.SaveChanges();

                ViewBag.Message = "Разряд МК изменен";

            }
            catch
            {
                ViewBag.Message = "Ошибка";
            }

            return PartialView();
        }
        //--------------------------------------//

        // сохранение редактирования БТС//

        [HttpPost]
        public ActionResult BtsSave(int ID, string Gruppa, string Stavka, string Datein, string Dateout)
        {
            try
            {
                BazTarStavka btss = new BazTarStavka();
                btss = db.BazTarStavka.FirstOrDefault(a => a.IDStavka == ID);
                btss.Gruppa = Convert.ToInt32(Gruppa.Trim());
                btss.Stavka = Convert.ToInt32(Stavka.Trim());
                btss.Datein = Convert.ToDateTime(Datein.Trim());
                if (Dateout == "")
                {
                    btss.Dateout = null;
                }
                else
                {
                    btss.Dateout = Convert.ToDateTime(Dateout.Trim());
                }
                 db.SaveChanges();

                ViewBag.Message = "БТС изменена";

            }
            catch
            {
                ViewBag.Message = "Ошибка";
            }

            return PartialView();
        }
        //--------------------------------------//


            //Редактирование вакансии

        [HttpPost]
        public ActionResult IndexSaveEdite(int ID, int IDPodrPr, int IDPodrOtd, int IDPodrPodr, int IDPodrUch,int KOD, int IDDolj, int IDSotr, int IDStav, string AUP, decimal Kol, decimal C_093_107, int IDRazr, decimal KtVisKval, decimal KtTehVid,
        decimal KtHarSpec, decimal KtFilial, decimal KtZaKateg, decimal KtZaStarsh, decimal OkladPovish, decimal KtZaContract, decimal Post1748,
        decimal DoljnOklad, decimal KtZaProfMast, decimal KtZaVisDostij, decimal KtProchaya, decimal Itog, string Priznak)
        {
            try
            {
                BazTarStavka b1 = new BazTarStavka();
                b1 = db.BazTarStavka.FirstOrDefault(s => s.IDStavka == IDStav);

                Razr r1 = new Razr();
                r1 = db.Razr.FirstOrDefault(a => a.IDRazr == IDRazr);


                vacancy vac = new vacancy();
                vac = db.vacancy.FirstOrDefault(a => a.IDVac == ID);
                vac.IDPodrPr = IDPodrPr;
                vac.IDPodrOtd = IDPodrOtd;
                vac.IDPodrPodr = IDPodrPodr;
                vac.IDPodrUch = IDPodrUch;
                vac.IDOKRB = KOD;
                vac.IDDolj = IDDolj;
                vac.IDSotr = IDSotr;
                vac.IDStav = IDStav;
                vac.AUP = AUP;
                vac.Kol = Kol;
                vac.VsegoZarpl = b1.Stavka * r1.MK;
                vac.C0_931_07 = C_093_107;
                vac.IDRazr = IDRazr;
                vac.KtVisKval = KtVisKval;
                vac.KtTehVid = KtTehVid;
                vac.KtHarSpec = KtHarSpec;
                vac.KtFilial = KtFilial;
                vac.KtZaKateg = KtZaKateg;
                vac.KtZaStarsh = KtZaStarsh;
                vac.OkladPovish = vac.VsegoZarpl + vac.VsegoZarpl * (vac.C0_931_07 + vac.KtTehVid + vac.KtHarSpec + vac.KtFilial + vac.KtZaKateg + vac.KtZaStarsh) / 100; ;
                vac.KtZaContract = KtZaContract;
                vac.Post1748 = Post1748;
                vac.DoljnOklad = (vac.OkladPovish + vac.VsegoZarpl * vac.KtZaContract / 100) * vac.Kol;
                vac.KtZaProfMast = KtZaProfMast;
                vac.KtZaVisDostij = KtZaVisDostij;
                vac.KtProchaya = KtProchaya;
                vac.Itog = vac.DoljnOklad + vac.DoljnOklad * (vac.KtZaProfMast + vac.KtProchaya) / 100;
                vac.Priznak = Priznak;

                db.SaveChanges();

                ViewBag.Message = "Вакансия изменена";

            }
            catch (Exception e)
            {
                ViewBag.Message = "Ошибка. Текст ошибки: " + e.ToString();
            }

            return PartialView();
        }

        //---------------------------------------------------------------------

        public ActionResult SelectDeleteSotrudnik(int ID)
        {
            Sotrudnik Sotrudniki = new Sotrudnik();
            Sotrudniki = db.Sotrudnik.FirstOrDefault(a => a.IDSot == ID);
            return PartialView(Sotrudniki);
        }

        // удаление с запросом должности//

        public ActionResult SelectDeleteDolj(int ID)
        {
            Doljnost dol = new Doljnost();
            dol = db.Doljnost.FirstOrDefault(a => a.IDDolj == ID);
            return PartialView(dol);
        }
        //-----------------------------//

        // удаление с запросом разряда МК//

        public ActionResult SelectDeleteRazr(int ID)
        {
            Razr raz = new Razr();
            raz = db.Razr.FirstOrDefault(a => a.IDRazr == ID);
            return PartialView(raz);
        }
        //-----------------------------//

        // удаление с запросом БТС//

        public ActionResult SelectDeleteBts(int ID)
        {
            BazTarStavka btss = new BazTarStavka();
            btss = db.BazTarStavka.FirstOrDefault(a => a.IDStavka == ID);
            return PartialView(btss);
        }
        //-----------------------------//


        public ActionResult SelectDeleteVac(int ID)
        {
            vacancy Vac = new vacancy();
            Vac = db.vacancy.FirstOrDefault(a => a.IDVac == ID);
            return PartialView(Vac);
        }


        public ActionResult DeleteSotrudnik(int ID)
        {
            try
            {

                Sotrudnik Sotrudniki = new Sotrudnik();
                Sotrudniki = db.Sotrudnik.FirstOrDefault(a => a.IDSot == ID);
                db.Sotrudnik.Remove(Sotrudniki);
                db.SaveChanges();

                ViewBag.Message = "Сотрудник удален";

            }
            catch
            {
                ViewBag.Message = "Ошибка удаления";
            }

            return PartialView();
        }


        // Удаление должности//

        public ActionResult DeleteDolj(int ID)
        {
            try
            {

                Doljnost doljj = new Doljnost();
                doljj = db.Doljnost.FirstOrDefault(a => a.IDDolj == ID);
                db.Doljnost.Remove(doljj);
                db.SaveChanges();

                ViewBag.Message = "должность удалена";

            }
            catch
            {
                ViewBag.Message = "Ошибка удаления";
            }

            return PartialView();
        }
        //--------------------//


        // Удаление разряда МК//

        public ActionResult DeleteRazr(int ID)
        {
            try
            {

                Razr razrr = new Razr();
                razrr = db.Razr.FirstOrDefault(a => a.IDRazr == ID);
                db.Razr.Remove(razrr);
                db.SaveChanges();

                ViewBag.Message = "разряд удален";

            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.ToString();
                //ViewBag.Message = "Ошибка удаления";
            }

            return PartialView();
        }
        //--------------------//


        // Удаление БТС//

        public ActionResult DeleteBts(int ID)
        {
            try
            {

                BazTarStavka btss = new BazTarStavka();
                btss = db.BazTarStavka.FirstOrDefault(a => a.IDStavka == ID);
                db.BazTarStavka.Remove(btss);
                db.SaveChanges();

                ViewBag.Message = "БТС удалена";

            }
            catch
            {
                ViewBag.Message = "Ошибка удаления";
            }

            return PartialView();
        }
        //--------------------//


        //Удаление вакансии//

        public ActionResult DeleteVac(int ID)
        {
            try
            {

                vacancy Vac = new vacancy();
                Vac = db.vacancy.FirstOrDefault(a => a.IDVac == ID);
                db.vacancy.Remove(Vac);
                db.SaveChanges();

                ViewBag.Message = "Вакансия удалена";

            }
            catch
            {
                ViewBag.Message = "Ошибка удаления";
            }

            return PartialView();
        }


        //Работа с таблицей Vacancy//

        public ActionResult IndexAdd(int ID)
        {

            vacancy Vacancy = new vacancy();
            Vacancy = db.vacancy.FirstOrDefault(a => a.IDVac == ID);

            //List<Podrazdel> podrazdList = new List<Podrazdel>();
            //podrazdList = db.Podrazdel.ToList();
            //ViewBag.Podr = podrazdList;

            SelectList podrazdD = new SelectList(db.Podrazdel, "IDp", "Podrazd");
            ViewBag.ppp = podrazdD;

            //List<Otdel> otdelList = new List<Otdel>();
            //otdelList = db.Otdel.ToList();
            //ViewBag.Otd = otdelList;

            List<Gruppa> gruppalList = new List<Gruppa>();
            gruppalList = db.Gruppa.ToList();
            ViewBag.gr = gruppalList;

            List<Uchastok> uchastoklList = new List<Uchastok>();
            uchastoklList = db.Uchastok.ToList();
            ViewBag.uch = uchastoklList;

            List<OKRB> okrbList = new List<OKRB>();
            okrbList = db.OKRB.ToList();
            ViewBag.kod = okrbList;

            List<Doljnost> doljnostList = new List<Doljnost>();
            doljnostList = db.Doljnost.OrderBy(x => x.Doljnost1).ToList();
            ViewBag.dolj = doljnostList;

            List<Sotrudnik> sotrudnikList = new List<Sotrudnik>();
            sotrudnikList = db.Sotrudnik.OrderBy(x => x.Sotrudnik1).ToList();
            ViewBag.sot = sotrudnikList;

            List<BazTarStavka> btsList = new List<BazTarStavka>();
            btsList = db.BazTarStavka.ToList();
            ViewBag.bts = btsList;

            List<Razr> razr = new List<Razr>();
            razr = db.Razr.OrderBy(q=> q.Razr1).ToList();
            ViewBag.razr = razr;

            return PartialView(Vacancy);
        }

        public ActionResult IndexEdit(int ID)
        {
            vacancy Vacancy = new vacancy();
            Vacancy = db.vacancy.FirstOrDefault(a => a.IDVac == ID);

            IEnumerable<Podrazdel> podrazdList = db.Podrazdel;
            //List<Podrazdel> podrazdList = new List<Podrazdel>();
            podrazdList = db.Podrazdel.OrderBy(x=> x.Podrazd).ToList();
            ViewBag.Podr = podrazdList;


            IEnumerable<Otdel> otdelList = db.Otdel;
            otdelList = db.Otdel.OrderBy(y=> y.Otdel1).ToList();
            ViewBag.Otd = otdelList;

            IEnumerable<Gruppa> gruppalList = db.Gruppa;
            gruppalList = db.Gruppa.OrderBy(z=> z.Gruppa1).ToList();
            ViewBag.gr = gruppalList;

            IEnumerable<Uchastok> uchastoklList =db.Uchastok;
            uchastoklList = db.Uchastok.OrderBy(d=> d.Uchastok1).ToList();
            ViewBag.uch = uchastoklList;

            List<OKRB> kodokrb = new List<OKRB>();
            kodokrb = db.OKRB.ToList();
            ViewBag.kod = kodokrb;

            List<Doljnost> doljList = new List<Doljnost>();
            doljList = db.Doljnost.ToList();
            ViewBag.dolj = doljList;

            List<Sotrudnik> sotrudnik = new List<Sotrudnik>();
            sotrudnik = db.Sotrudnik.OrderBy(x => x.Sotrudnik1).ToList();
            ViewBag.sotr = sotrudnik;

            List<BazTarStavka> bts = new List<BazTarStavka>();
            bts = db.BazTarStavka.ToList();
            ViewBag.bts = bts;

            List<Razr> razr = new List<Razr>();
            razr = db.Razr.ToList();
            ViewBag.razr = razr;

            return PartialView(Vacancy);

        }


        [HttpPost]
        public ActionResult IndexSave(int IDPodrPr, int IDPodrOtd, int IDPodrPodr, int IDPodrUch, int IDDolj, int IDSotr, int IDStav, string AUP, decimal Kol, decimal C_093_107,
        int IDRazr, decimal KtVisKval, decimal KtTehVid,
        decimal KtHarSpec, decimal KtFilial, decimal KtZaKateg, decimal KtZaStarsh, decimal KtZaContract, decimal Post1748,
        decimal KtZaProfMast, decimal KtZaVisDostij, decimal KtProchaya, int OKRB)
        {
            try
            {

                BazTarStavka b1 = new BazTarStavka();
                b1 = db.BazTarStavka.FirstOrDefault(s => s.IDStavka == IDStav);

                Razr r1 = new Razr();
                r1 = db.Razr.FirstOrDefault(a => a.IDRazr == IDRazr);

                vacancy vacancyy = new vacancy();
                vacancyy.IDPodrPr = IDPodrPr;
                if (IDPodrOtd == 0)
                {
                    vacancyy.IDPodrOtd = null;
                }
                else
                    vacancyy.IDPodrOtd = IDPodrOtd;
                if (IDPodrPodr == 0)
                {
                    vacancyy.IDPodrPodr = null;
                }
                else
                    vacancyy.IDPodrPodr = IDPodrPodr;
                if (IDPodrUch == 0)
                {
                    vacancyy.IDPodrUch = null;
                }
                else
                    vacancyy.IDPodrUch = IDPodrUch;
                vacancyy.IDDolj = IDDolj;
                vacancyy.IDSotr = IDSotr;
                vacancyy.IDStav = IDStav;
                vacancyy.AUP = AUP;
                vacancyy.Kol = Kol;
                vacancyy.VsegoZarpl = b1.Stavka * r1.MK;
                //vacancyy.VsegoZarpl = KtVisKval;
                vacancyy.C0_931_07 = C_093_107;
                vacancyy.IDRazr = IDRazr;
                vacancyy.KtVisKval = KtVisKval;
                vacancyy.KtTehVid = KtTehVid;
                vacancyy.KtHarSpec = KtHarSpec;
                vacancyy.KtFilial = KtFilial;
                vacancyy.KtZaKateg = KtZaKateg;
                vacancyy.KtZaStarsh = KtZaStarsh;
                vacancyy.OkladPovish = vacancyy.VsegoZarpl + vacancyy.VsegoZarpl * (vacancyy.C0_931_07 + vacancyy.KtTehVid + vacancyy.KtHarSpec + vacancyy.KtFilial + vacancyy.KtZaKateg + vacancyy.KtZaStarsh) / 100;
                vacancyy.KtZaContract = KtZaContract;
                vacancyy.Post1748 = Post1748;
                vacancyy.DoljnOklad = (vacancyy.OkladPovish + vacancyy.VsegoZarpl * vacancyy.KtZaContract / 100) * vacancyy.Kol;
                vacancyy.KtZaProfMast = KtZaProfMast;
                vacancyy.KtZaVisDostij = KtZaVisDostij;
                vacancyy.KtProchaya = KtProchaya;
                vacancyy.Itog = vacancyy.DoljnOklad + vacancyy.DoljnOklad * (vacancyy.KtZaProfMast + vacancyy.KtProchaya) / 100;
                vacancyy.Datein = DateTime.Now;     
                vacancyy.IDOKRB = OKRB;

                db.vacancy.Add(vacancyy);

                db.SaveChanges();

                ViewBag.Message = "Запись добавлена";
            }
            catch (Exception ex)
            {


                ViewBag.Message = ex.ToString();
            }

            return PartialView();
        }



        // Запись в историю  //
        //[HttpPost]
        public ActionResult SaveHistory(string dat)
        {
            List<History> His = new List<History>();
            DateTime checkDate = Convert.ToDateTime(dat);
            His = db.History.Where(a => a.Dat == DbFunctions.TruncateTime(checkDate)).ToList();


            if (His.Count > 0)
            {
                ViewBag.Message = "На данную дату штатное расписание уже существует!!! Выберите другую дату!!!";

            }

            else
            {
                try

                {



                    //HistoryDat HD = new HistoryDat();
                    //HD.HisDat = dat;
                    //db.HistoryDat.Add(HD);
                    //db.SaveChanges();

                    List<vacancy> vac = new List<vacancy>();
                    vac = db.vacancy.ToList();
                    foreach (var data in vac)
                    {

                        History H = new History();
                        H.Podrazd = data.Podrazdel.Podrazd;
                        if (data.IDPodrOtd == null)
                        {
                            H.Otdel = "";
                        }
                        else
                        {
                            H.Otdel = data.Otdel.Otdel1;
                        }
                        if (data.IDPodrPodr == null)
                        {
                            H.Gruppa = "";
                        }
                        else
                        {
                            H.Gruppa = data.Gruppa.Gruppa1;
                        }
                        if (data.IDPodrUch == null)
                        {
                            H.Uch = "";
                        }
                        else
                        {
                            H.Uch = data.Uchastok.Uchastok1;
                        }
                        if (data.IDOKRB == null)
                        {
                            H.OKRB = "";
                        }
                        else {
                            //H.OKRB = data.IDOKRB.ToString();
                            H.OKRB = data.OKRB.OKRB1.ToString();
                        }
                        
                        //if (data.IDPodrOtd == null)
                        //{
                        //    H.OKRB = "";
                        //}
                        //else
                        //{
                        //    H.OKRB = data.Otdel.priznak.ToString();
                        //}
                        H.PrizDolj = data.Doljnost.Priznak;
                        if (data.IDPodrOtd == null)
                        {
                            H.PrizOtdel = null;
                        }
                        else
                        {
                        H.PrizOtdel = data.Otdel.priznak;
                        }
                        H.Dolj = data.Doljnost.Doljnost1.ToString();
                        H.Sotr = data.Sotrudnik.Sotrudnik1.ToString();
                        H.AUP = data.AUP.ToString();
                        H.Bts = data.BazTarStavka.Stavka.ToString();
                        H.Kol = data.Kol.ToString();
                        H.Vsego = data.VsegoZarpl.ToString();
                        H.Vsego = data.VsegoZarpl.ToString();
                        H.C093107 = data.C0_931_07.ToString();
                        H.VisKval = data.KtVisKval.ToString();
                        H.TehVid = data.KtTehVid.ToString();
                        H.HarSpec = data.KtHarSpec.ToString();
                        H.ZaFil = data.KtFilial.ToString();
                        H.ZaKat = data.KtZaKateg.ToString();
                        H.ZaStar = data.KtZaStarsh.ToString();
                        H.OkPov = data.OkladPovish.ToString();
                        H.ZaKontr = data.KtZaContract.ToString();
                        H.C1748 = data.Post1748.ToString();
                        H.DoljOk = data.DoljnOklad.ToString();
                        H.ProfMast = data.KtZaProfMast.ToString();
                        H.VisDost = data.KtZaVisDostij.ToString();
                        H.Proch = data.KtProchaya.ToString();
                        H.Itog = data.Itog.ToString();
                        H.Dat = Convert.ToDateTime(dat);
                        //H.PrizOtdel = data.Otdel.priznak;
                        //H.PrizDolj = data.Doljnost.Priznak;
                        if (data.Priznak == null)
                        {
                            H.Priznak = "";
                        }
                        else
                        {
                            H.Priznak = data.Priznak.ToString();
                        }

                        db.History.Add(H);
                        db.SaveChanges();
                    }

                    HistoryDat HD = new HistoryDat();
                    HD.HisDat = dat;
                    db.HistoryDat.Add(HD);
                    db.SaveChanges();

                    ViewBag.Message = "Запись добавлена!!!";
                }
                catch (Exception ex)
                {


                    ViewBag.Message = ex.ToString();
                }
            }

            return PartialView();
        }


        //  Выбор истории в зависимости от даты  //

        public ActionResult SaveHisDat(string dat)
        {

            DateTime checkDate = Convert.ToDateTime(dat);
            List<History> His = new List<History>();
            His = db.History.Where(a => a.Dat == DbFunctions.TruncateTime(checkDate)).OrderBy(x => x.Podrazd).ThenBy(y=> y.PrizOtdel).ThenBy(d=> d.Gruppa).ThenBy(r=>r.Uch).ThenBy(f=>f.PrizDolj).ToList();
            // His = db.History.Where(a => a.Dat == DbFunctions.TruncateTime(checkDate)).ToList();
            return PartialView(His);
        }




        //  Выбор вакансий в зависимости от фильтра  //

        public ActionResult SaveVac(int IDPodrPr, int IDPodrOtd, int IDPodrPodr, int IDPodrUch)
        {

            List<vacancy> Vac = new List<vacancy>();
            Vac = db.vacancy.ToList();

            List<vacancy> Vac_after_filter = new List<vacancy>();
            //Vac_after_filter = filter(Vac, IDPodrPr, IDPodrOtd, IDPodrPodr, IDPodrUch).OrderBy(x => x.Podrazdel).ThenBy(m => m.Otdel).ThenBy(n => n.Gruppa).ThenBy(z => z.Uchastok).ThenBy(s => s.Doljnost).ToList();
            Vac_after_filter = filter(Vac, IDPodrPr, IDPodrOtd, IDPodrPodr, IDPodrUch).ToList();
            return PartialView(Vac_after_filter);
        }

        //Выбор вакансий в зависимости от должности //

        public ActionResult SaveDolj(int IDdoljn)
        {

            List<vacancy> Dolj = new List<vacancy>();
            Dolj = db.vacancy.ToList();

            List<vacancy> Dolj_after_filter = new List<vacancy>();
            Dolj_after_filter = Dolj.Where(g => g.IDDolj == IDdoljn).ToList();
            //Dolj_after_filter = Dolj.Where(g=>g.IDDolj==IDdoljn).OrderBy(n=>n.Podrazdel).ThenBy(m=>m.Otdel.priznak).ThenBy(b=>b.Gruppa).ThenBy(v=>v.Uchastok).ThenBy(c=> c.Doljnost.Priznak).ToList();
            return PartialView(Dolj_after_filter);
        }

        //----------------------------------------------------//

        public List<vacancy> filter(List<vacancy> listForFolter, int IDPodrPr, int IDPodrOtd, int IDPodrPodr, int IDPodrUch)
        {
            List<vacancy> Vac_filter_1 = new List<vacancy>();
            if (IDPodrPr == 0)
            {
                Vac_filter_1 = listForFolter;
            }
            else
            {
                Vac_filter_1 = listForFolter.Where(x => x.IDPodrPr == IDPodrPr).ToList();
            }

            List<vacancy> Vac_filter_2 = new List<vacancy>();
            if (IDPodrOtd == 0)
            {
                Vac_filter_2 = Vac_filter_1;
            }
            else
            {
                //Vac_filter_2 = Vac_filter_1.Where(x => x.IDPodrOtd == IDPodrOtd).OrderBy(x => x.Podrazdel).ThenBy(m => m.Otdel.priznak).ThenBy(n => n.Gruppa).ThenBy(z => z.Uchastok).ThenBy(s => s.Doljnost.Priznak).ToList();
                Vac_filter_2 = Vac_filter_1.Where(x => x.IDPodrOtd == IDPodrOtd).ToList();
            }

            List<vacancy> Vac_filter_3 = new List<vacancy>();
            if (IDPodrPodr == 0)
            {
                Vac_filter_3 = Vac_filter_2;
            }
            else
            {
                //Vac_filter_3 = Vac_filter_2.Where(x => x.IDPodrPodr == IDPodrPodr).OrderBy(x => x.Podrazdel).ThenBy(m => m.Otdel.priznak).ThenBy(n => n.Gruppa).ThenBy(z => z.Uchastok).ThenBy(s => s.Doljnost.Priznak).ToList();
                Vac_filter_3 = Vac_filter_2.Where(x => x.IDPodrPodr == IDPodrPodr).ToList();
            }

            List<vacancy> Vac_filter_4 = new List<vacancy>();
            if (IDPodrUch == 0)
            {
                Vac_filter_4 = Vac_filter_3;
            }
            else
            {
                //Vac_filter_4 = Vac_filter_3.Where(x => x.IDPodrUch == IDPodrUch).ToList();
                Vac_filter_4 = Vac_filter_3.Where(x => x.IDPodrUch == IDPodrUch).OrderBy(x => x.Podrazdel).ThenBy(m => m.Otdel.priznak).ThenBy(n => n.Gruppa).ThenBy(z => z.Uchastok).ThenBy(s => s.Doljnost.Priznak).ToList();
            }


            return Vac_filter_4;
        }

        
        public ActionResult SaveH(string dat, string IDPodrPr, string IDPodrOtd, string IDPodrPodr, string IDPodrUch)
        {

            List<History> Vac = new List<History>();
            Vac = db.History.ToList();

            List<History> H_after_filter = new List<History>();
            H_after_filter = filterH(Vac, dat, IDPodrPr.Trim(), IDPodrOtd, IDPodrPodr, IDPodrUch).ToList();
            

            return PartialView(H_after_filter);


        }

        //--------------------------------------------------------------------------------------------------------------------//
        //Тестирование вывода списка в группировке Group By
        //public ActionResult SortSort(string dat, string IDPodrPr, string IDPodrOtd, string IDPodrPodr, string IDPodrUch)
          public ActionResult SortSort(string dat, string IDPodrPr, string IDPodrOtd, string IDPodrPodr, string IDPodrUch)
        {

            List<History> Vac = new List<History>();
            Vac = db.History.ToList();

            List<History> H_after_filter = new List<History>();
            H_after_filter = filterH(Vac, dat, IDPodrPr.Trim(), IDPodrOtd, IDPodrPodr, IDPodrUch).ToList();

            var histGroup = H_after_filter.GroupBy(p => p.Podrazd);
            var histGroup2 = from hist in H_after_filter
                             group hist by hist.Podrazd into g
                             select new { Podrazdel = g.Key, Count = g.Count() };
            foreach (var group in histGroup2)
                //return PartialView($"{group.Podrazdel}:{group.Count}");
                ViewBag.groups1 = "{group.Podrazdel}:{group.Count}";
            return PartialView(H_after_filter);
            
        }
        //--------------------------------------------------------------------------------------------------------------------//

        public List<History> filterH(List<History> listForFolter, string dat, string IDPodrPr, string IDPodrOtd, string IDPodrPodr, string IDPodrUch)
        {

            List<History> Vac_filter_0 = new List<History>();
            if (dat == "0")
            {
                Vac_filter_0 = listForFolter;
            }
            else
            {

                DateTime DDD = Convert.ToDateTime(dat);
                Vac_filter_0 = db.History.Where(a => a.Dat == DbFunctions.TruncateTime(DDD)).OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(e => e.PrizDolj).ToList();

                //DateTime DDD = Convert.ToDateTime(dat);
                //Vac_filter_0 = listForFolter.Where(x => x.Dat==DDD).ToList();
            }

            List<History> Vac_filter_1 = new List<History>();
                //if (IDPodrPr == "Выберите подразделение")
                if (IDPodrPr == "")
            {
                Vac_filter_1 = Vac_filter_0.OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(e => e.PrizDolj).ToList(); ;
            }
            else
            {

                Vac_filter_1 = Vac_filter_0.Where(x => x.Podrazd.Trim() == IDPodrPr.Trim()).OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(e => e.PrizDolj).ToList();
            }

            List<History> Vac_filter_2 = new List<History>();
            if (IDPodrOtd == "0")

            {
                 Vac_filter_2 = Vac_filter_1;
                 

            }
            else
            {
                //if (IDPodrOtd.Equals(null))
                if (string.IsNullOrEmpty(IDPodrOtd))
                {
                    Vac_filter_2 = Vac_filter_1.Where(x => x.Otdel == IDPodrOtd).OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(e => e.PrizDolj).ToList();
                }
                else
                {
                    Vac_filter_2 = Vac_filter_1.Where(x => x.Otdel.Trim().Equals(IDPodrOtd.Trim())).OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(e => e.PrizDolj).ToList();
                    //Vac_filter_2 = Vac_filter_1.Where(x => x.Otdel.Trim() == IDPodrOtd.Trim()).ToList();
                }
            }

            List<History> Vac_filter_3 = new List<History>();
            if (IDPodrPodr == "0")
            {
                Vac_filter_3 = Vac_filter_2;
            }
            else
            {
                Vac_filter_3 = Vac_filter_2.Where(x => x.Gruppa == IDPodrPodr).OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(e => e.PrizDolj).ToList();
            }

            List<History> Vac_filter_4 = new List<History>();
            if (IDPodrUch == "0")
            {
                Vac_filter_4 = Vac_filter_3;
            }
            else
            {
                //Vac_filter_4 = Vac_filter_3.Where(x => x.Uch == IDPodrUch).OrderBy(x=> x.Podrazd).ThenBy(s=> s.Otdel).ThenBy(f=> f.Gruppa).ToList();
                Vac_filter_4=Vac_filter_3.Where(x => x.Uch == IDPodrUch).OrderBy(e => e.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o=>o.Uch).ThenBy(e=>e.PrizDolj).ToList();
            }


            return Vac_filter_4.OrderBy(c=>c.Podrazd).ThenBy(f=>f.PrizOtdel).ToList();
        }



        

        
        //Формирование отчета (получение параметров)//
        public FileResult Export(string dat, string IDPodrPr,string IDPodrOtd, string IDPodrPodr, string IDPodrUch)
            {

            List<History> Vac = new List<History>();
            //Vac = db.History.ToList();
            //Vac = db.History.OrderBy(w => w.Podrazd).ThenBy(e => e.Otdel).ToList();
            Vac= db.History.OrderBy(q=>q.Dat).ThenBy(w => w.Podrazd).ThenBy(q=>q.PrizOtdel).ThenBy(w=>w.Gruppa).ThenBy(e=>e.Uch).ThenBy(r=>r.PrizDolj).ToList();
            List<History> H_after_filter = new List<History>();
            //H_after_filter = filterH(Vac, dat, IDPodrPr, IDPodrOtd, IDPodrPodr, IDPodrUch).OrderBy(x => x.Podrazd).ThenBy(s => s.Otdel).ThenBy(f => f.Gruppa).ToList();
            H_after_filter = filterH(Vac, dat, IDPodrPr, IDPodrOtd, IDPodrPodr, IDPodrUch).OrderBy(d=>d.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o=>o.Uch).ThenBy(j=>j.PrizDolj).ToList();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Лист1");
            double p=0;
            for (int i = 0; i < H_after_filter.Count; i++)
            {
             p += Convert.ToDouble(H_after_filter[i].Itog);
            }
                //Шапка отчета//

                worksheet.Cell("P" + 1).Value = "Открытое акционерное общество 'Гомельтранснефть Дружба'";
            worksheet.Cell("T" + 2).Value = "УТВЕРЖДАЮ";
            worksheet.Cell("T" + 2).Style.Font.FontSize = 18;
            worksheet.Cell("P" + 1).Style.Font.FontSize = 14;
            worksheet.Cell("M" + 3).Style.Font.FontSize = 14;
            worksheet.Cell("M" + 3).Value = "Штат в количестве" + "  "+H_after_filter.Count +"  " +"человек";
            worksheet.Cell("M" + 4).Style.Font.FontSize = 14;
            worksheet.Cell("M" + 4).Value = "С месячным фондом" + "  " + p + "  " + "рублей";
            worksheet.Cell("N" + 5).Style.Font.FontSize = 14;
            worksheet.Cell("N" + 5).Value = "Генеральный директор______________________Борисенко О.Л.";
            worksheet.Cell("P" + 6).Style.Font.FontSize = 14;
            worksheet.Cell("P" + 6).Value = "Приказ №________ от'____________________'2019 г.";
            worksheet.Cell("G" + 7).Style.Font.FontSize = 20;
            worksheet.Cell("G" + 7).Value = "ШТАТНОЕ РАСПИСАНИЕ";

            //создадим заголовки у столбцов
            worksheet.Cell("A" + 10).Value = "Подразделение";
            worksheet.Cell("B" + 10).Value = "Отдел";
            worksheet.Cell("C" + 10).Value = "Группа";
            worksheet.Cell("D" + 10).Value = "Участок";
            worksheet.Cell("E" + 10).Value = "Сотрудник";
            worksheet.Cell("F" + 10).Value = "ОКРБ";
            worksheet.Cell("G" + 10).Value = "Должность";
            worksheet.Cell("H" + 10).Value = "БТС";
            worksheet.Cell("I" + 10).Value = "Кол";
            worksheet.Cell("J" + 10).Value = "Всего_ЗП";
            worksheet.Cell("K" + 10).Value = "093-107";
            worksheet.Cell("L" + 10).Value = "ВысКвал";
            worksheet.Cell("M" + 10).Value = "ТехВид";
            worksheet.Cell("N" + 10).Value = "ХарСпец";
            worksheet.Cell("O" + 10).Value = "ЗаФил";
            worksheet.Cell("P" + 10).Value = "ЗаКат";
            worksheet.Cell("Q" + 10).Value = "ЗаСтарш";
            worksheet.Cell("R" + 10).Value = "ОкладПов";
            worksheet.Cell("S" + 10).Value = "ЗаКонтр";
            worksheet.Cell("T" + 10).Value = "1748";
            worksheet.Cell("U" + 10).Value = "ДолжОклад";
            worksheet.Cell("V" + 10).Value = "ВысДост";
            worksheet.Cell("W" + 10).Value = "ИТОГ";
            
            
            for (int i = 0; i < H_after_filter.Count; i++)
            {
                
                p +=Convert.ToDouble(H_after_filter[i].Itog);

                worksheet.Cell("A" + (i + 11)).Value = H_after_filter[i].Podrazd;
                worksheet.Cell("B" + (i + 11)).Value = H_after_filter[i].Otdel;
                worksheet.Cell("C" + (i + 11)).Value = H_after_filter[i].Gruppa;
                worksheet.Cell("D" + (i + 11)).Value = H_after_filter[i].Uch;
                worksheet.Cell("E" + (i + 11)).Value = H_after_filter[i].Sotr;
                worksheet.Cell("F" + (i+11)).Value = H_after_filter[i].OKRB;
                worksheet.Cell("G" + (i + 11)).Value = H_after_filter[i].Dolj;
                worksheet.Cell("H" + (i + 11)).Value = H_after_filter[i].Bts;
                worksheet.Cell("I" + (i + 11)).Value = H_after_filter[i].Kol;
                worksheet.Cell("J" + (i + 11)).Value = H_after_filter[i].Vsego;
                worksheet.Cell("K" + (i + 11)).Value = H_after_filter[i].C093107;
                worksheet.Cell("L" + (i + 11)).Value = H_after_filter[i].VisKval;
                worksheet.Cell("M" + (i + 11)).Value = H_after_filter[i].TehVid;
                worksheet.Cell("N" + (i + 11)).Value = H_after_filter[i].HarSpec;
                worksheet.Cell("O" + (i + 11)).Value = H_after_filter[i].ZaFil;
                worksheet.Cell("P" + (i + 11)).Value = H_after_filter[i].ZaKat;
                worksheet.Cell("Q" + (i + 11)).Value = H_after_filter[i].ZaStar;
                worksheet.Cell("R" + (i + 11)).Value = H_after_filter[i].OkPov;
                worksheet.Cell("S" + (i + 11)).Value = H_after_filter[i].ZaKontr;
                worksheet.Cell("T" + (i + 11)).Value = H_after_filter[i].C1748;
                worksheet.Cell("U" + (i + 11)).Value = H_after_filter[i].DoljOk;
                worksheet.Cell("V" + (i + 11)).Value = H_after_filter[i].VisDost;
                worksheet.Cell("w" + (i + 11)).Value = H_after_filter[i].Itog;

                worksheet.Cell("J" + (i+11)).Style.Fill.BackgroundColor = XLColor.AliceBlue;
                worksheet.Cell("R" + (i+11)).Style.Fill.BackgroundColor = XLColor.AliceBlue;
                worksheet.Cell("U" + (i+11)).Style.Fill.BackgroundColor = XLColor.AliceBlue;
                worksheet.Cell("w" + (i+11)).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            }

            //пример изменения стиля ячейки
            worksheet.Cell("A" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("B" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("C" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("D" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("E" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("F" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("G" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("H" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("I" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("J" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("K" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("L" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("M" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("N" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("O" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("P" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("Q" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("R" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("S" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("T" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("U" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("V" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;
            worksheet.Cell("W" + 10).Style.Fill.BackgroundColor = XLColor.AliceBlue;

            // пример создания сетки в диапазоне

            //var rngTable1 = worksheet.Range("A10:V" + 1);
            //rngTable1.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            //rngTable1.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            //rngTable1.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            //rngTable1.Style.Border.TopBorder = XLBorderStyleValues.Thin;

            var rngTable = worksheet.Range("A11:W" + (H_after_filter.Count + 10));
            rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;


            //-------------------------------------------------//
            var rngTable111 = worksheet.Range("A10:W" + 10);
            rngTable111.Style.Border.RightBorder = XLBorderStyleValues.Medium;
            rngTable111.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            rngTable111.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
            rngTable111.Style.Border.TopBorder = XLBorderStyleValues.Medium;

            //-------------------------------------------------//
            //worksheet.Columns().AdjustToContents(); //ширина столбца по содержимому
            //worksheet.Columns().Style.Alignment.WrapText = true;

            var col1 = worksheet.Column("E");
            col1.AdjustToContents();

            var col2 = worksheet.Column("A");
            col2.Width = 14;

            var col3 = worksheet.Column("B");
            col3.Width = 18;

            var col4 = worksheet.Column("C");
            col4.Width = 14;

            var col5 = worksheet.Column("F");
            col5.Width = 18;


            worksheet.Column(1).Style.Alignment.WrapText = true;
            worksheet.Column(2).Style.Alignment.WrapText = true;
            worksheet.Column(3).Style.Alignment.WrapText = true;
            worksheet.Column(4).Style.Alignment.WrapText = true;
            worksheet.Column(6).Style.Alignment.WrapText = true;
            
            // вернем пользователю файл без сохранения его на сервере
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
            }
        }


        //!!!!!!!Формирование отчета группированного!!!!!!!//
        public ActionResult ExportGroup(string dat, string IDPodrPr, string IDPodrOtd, string IDPodrPodr, string IDPodrUch)
        {

            MemoryStream workStream = new MemoryStream();

            // Имя создаваемого файла. 
            string strPDFFileName = string.Format("Report.pdf");
            Document doc = new Document();
            doc.SetPageSize(PageSize.A4.Rotate());
            PdfWriter.GetInstance(doc, workStream).CloseStream = false;

            List<History> Vac = new List<History>();
            //Vac = db.History.ToList();
            //Vac = db.History.OrderBy(w => w.Podrazd).ThenBy(e => e.Otdel).ToList();
            Vac = db.History.OrderBy(q => q.Dat).ThenBy(w => w.Podrazd).ThenBy(q => q.PrizOtdel).ThenBy(w => w.Gruppa).ThenBy(e => e.Uch).ThenBy(r => r.PrizDolj).ToList();
            List<History> H_after_filter = new List<History>();
            //H_after_filter = filterH(Vac, dat, IDPodrPr, IDPodrOtd, IDPodrPodr, IDPodrUch).OrderBy(x => x.Podrazd).ThenBy(s => s.Otdel).ThenBy(f => f.Gruppa).ToList();
            H_after_filter = filterH(Vac, dat, IDPodrPr, IDPodrOtd, IDPodrPodr, IDPodrUch).OrderBy(d => d.Dat).ThenBy(x => x.Podrazd).ThenBy(s => s.PrizOtdel).ThenBy(f => f.Gruppa).ThenBy(o => o.Uch).ThenBy(j => j.PrizDolj).ToList();

            double p = 0;
            for (int i = 0; i < H_after_filter.Count; i++)
            {
                p += Convert.ToDouble(H_after_filter[i].Itog);
            }

            // Подключение русскоязычного шрифта.
            string font = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");
            BaseFont baseFont = BaseFont.CreateFont(font, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font f12 = new Font(baseFont, 12);
            Font f14 = new Font(baseFont, 14);
            Font f16 = new Font(baseFont, 16);
            Font f12Bold = new Font(baseFont, 12, Font.BOLD);
            Font f18Bold = new Font(baseFont, 18, Font.BOLD);
            Font f20Bold = new Font(baseFont, 20, Font.BOLD);

            // Открытие документа.
            doc.Open();

            // Создание элементов.
            Paragraph par1 = new Paragraph("Открытое акционерное общество 'Гомельтранснефть Дружба'", f12Bold);
            Paragraph par2 = new Paragraph("УТВЕРЖДАЮ", f20Bold);
            Paragraph par3 = new Paragraph("Штат в количестве "+ H_after_filter.Count+" человек", f12Bold);
            Paragraph par4 = new Paragraph("С месячным фондом "+p+" рублей", f12Bold);
            Paragraph par5 = new Paragraph("Генеральный директор ___________ Борисенко О.Л.", f12Bold);
            Paragraph par6 = new Paragraph("Приказ №_____ от _____", f12Bold);
            par1.Alignment = Element.ALIGN_RIGHT;
            par2.Alignment = Element.ALIGN_RIGHT;
            par3.Alignment = Element.ALIGN_RIGHT;
            par4.Alignment = Element.ALIGN_RIGHT;
            par5.Alignment = Element.ALIGN_RIGHT;
            par6.Alignment = Element.ALIGN_RIGHT;

            //PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            //var colWidthPercentages = new[] { 8f, 42f, 8f, 42f };
            //table.SetWidths(colWidthPercentages);

            PdfPTable table = new PdfPTable(19);
            PdfPCell cell = new PdfPCell();
            table.WidthPercentage = 100f;

            float[] columnWidths = new float[] { 10f, 4.2f, 10f, 4.2f, 4.2f, 7f, 4.2f, 4.2f, 4.2f, 4.2f, 4.2f, 4.2f, 4.2f, 7f, 4.2f, 4.2f, 6f, 4.2f, 7f};
            table.SetWidths(columnWidths);

            foreach (var item in H_after_filter.GroupBy(x => x.Podrazd))
            {

                cell = new PdfPCell(new Phrase(item.Key, f20Bold));
                cell.Colspan = 19;
                table.AddCell(cell);

                foreach (var otdel in item.GroupBy(c => c.Otdel))
                {
                    cell = new PdfPCell(new Phrase(otdel.Key, f12Bold));
                    cell.Colspan = 19;
                    table.AddCell(cell);

                    foreach(var gruppa in otdel.GroupBy(g=>g.Gruppa))
                    {
                        cell = new PdfPCell(new Phrase(gruppa.Key, f12Bold));
                        cell.Colspan = 19;
                        table.AddCell(cell);

                        foreach (var uchastok in gruppa.GroupBy(u => u.Uch))
                        {
                            cell = new PdfPCell(new Phrase(uchastok.Key, f12Bold));
                            cell.Colspan = 19;
                            table.AddCell(cell);
//--------------------Шапка таблицы -------------------------------//
                            cell = new PdfPCell(new Phrase("Сотр.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("ОКРБ", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Долж.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("БТС", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Кол.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Всего_ЗП", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("093-107", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("ВысКвал", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("ТехВид", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("ХарСп", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Фил.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Катег.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Старш.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Оклад_Пов.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Контр.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("1748.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Долж_Ок.", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("Выс_Дост", f12));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("ИТОГ", f12));
                            table.AddCell(cell);

                            //------------------Конец шапки таблицы ---------------------------------------//
                            foreach (var sotr in uchastok)
                            {
                                //-------------------------Таблица с сотрудниками ----------------------//
                                cell = new PdfPCell(new Phrase(sotr.Sotr, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.OKRB, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.Dolj, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.Bts,f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.Kol, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.Vsego, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.C093107, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.VisKval, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.TehVid, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.HarSpec, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.ZaFil, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.ZaKat, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.ZaStar, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.OkPov, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.ZaKontr, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.C1748, f12));
                                table.AddCell(cell);
                                cell = new PdfPCell(new Phrase(sotr.DoljOk, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.VisDost, f12));
                                table.AddCell(cell);

                                cell = new PdfPCell(new Phrase(sotr.Itog, f12));
                                table.AddCell(cell);

                                
                                //-------------------------------Конец таблицы с сотрудниками ------------------------//
                            }
                        }
                    }
                }
            }



            // Добавление элементов в документ.
            doc.Add(par1);
            doc.Add(par2);
            doc.Add(par3);
            doc.Add(par4);
            doc.Add(par5);
            doc.Add(par6);
            doc.Add(new Paragraph(" ", f16));
            doc.Add(table);

            //doc.Add(table);
            // Закрытие документа.  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);
        }

    

    }
}

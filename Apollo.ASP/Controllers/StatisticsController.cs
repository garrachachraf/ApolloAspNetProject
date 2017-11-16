using Apollo.ASP.Models;
using Apollo.Domain.entities;
using Apollo.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Apollo.ASP.Controllers
{
    public class StatisticsController : Controller
    {

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        Statistics caller = new Statistics("http://localhost:18080/Apollo-web/app/");
        // GET: Statistics
        public ActionResult Index()
        {
            int scale = 50;
            List<LineChartViewModel> stat = new List<LineChartViewModel>();
            List<Gallery> galleires = caller.ListOfAllGalleries().OrderByDescending(o => o.maxCapacity).ToList();
            if (scale < galleires.First().maxCapacity)
            {
                int sections = (int)galleires.First().maxCapacity / scale;
                int oldScale = 0;
                int tmpScale = scale;
                for (int i = 0; i < sections + 1; i++)
                {
                    //  if (galleires.Count(g => oldScale <= g.maxCapacity && g.maxCapacity < tmpScale)!=0)
                    // {
                    LineChartViewModel tmp = new LineChartViewModel();
                    tmp.x = tmpScale;
                    tmp.y = galleires.Count(g => oldScale <= g.maxCapacity && g.maxCapacity < tmpScale);
                    stat.Add(tmp);
                    // }//

                    oldScale = tmpScale;
                    tmpScale = tmpScale + scale;
                }
            }

            JavaScriptSerializer jss2 = new JavaScriptSerializer();

            string output3 = jss2.Serialize(stat);

            string demo3 = output3.Replace("\"", "");

            float somme = 0;
            float somme2 = 0;
            float M = 0;
            float V = 0;
            float Ec = 0;
            foreach (var item in galleires)
            {
                somme = somme + (float)item.surface;
                somme2 = somme2 + (float)item.surface * (float)item.surface;
            }
            M = somme / galleires.Count;
            V = somme2 / galleires.Count - M * M;
            Ec = (float)Math.Sqrt(V);
            ViewBag.M = M;
            ViewBag.V = V;
            ViewBag.Ec = Ec;
            ViewBag.line = demo3;
            /******************************************/
            foreach (var item in galleires)
            {
                item.schedule = caller.ListOfAllSchedules(item.id);
            }
            Gallery gallery = galleires.Find(g => g.id == 2);
            Gallery galleryBeta = galleires.Find(g => g.id == 3);
            int count1_17 = 0;
            int count1_18 = 0;
            int count1_19 = 0;
            int count2_17 = 0;
            int count2_18 = 0;
            int count2_19 = 0;
            //    int z = gallery.schedule.Count(e => { DateTime t = (DateTime)e.startDate} == 2017);
            foreach (var item in gallery.schedule)
            {
                DateTime t = (DateTime)item.startDate;
                if (t.Year == 2017)
                    count1_17++;
                if (t.Year == 2018)
                    count1_18++;
                if (t.Year == 2019)
                    count1_19++;
            }
            foreach (var item in galleryBeta.schedule)
            {
                DateTime t = (DateTime)item.startDate;
                if (t.Year == 2017)
                    count2_17++;
                if (t.Year == 2018)
                    count2_18++;
                if (t.Year == 2019)
                    count2_19++;
            }

            BarChartViewModel a = new BarChartViewModel();
            BarChartViewModel b = new BarChartViewModel();
            BarChartViewModel c = new BarChartViewModel();
            a.a = count1_17;
            a.b = count2_17;
            a.y = "2017";
            b.a = count1_18;
            b.b = count2_18;
            b.y = "2018";
            c.a = count1_19;
            c.b = count2_19;
            c.y = "2019";
            List<BarChartViewModel> bar = new List<BarChartViewModel>();
            bar.Add(a);
            bar.Add(b);
            bar.Add(c);
            var json = JsonConvert.SerializeObject(bar);
          
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string output = jss.Serialize(bar);

            string demo = output.Replace("\"", "");

            ViewBag.jss = demo;

            /**********************/
          //  List<Gallery> galleires = caller.ListOfAllGalleries();

            List<schedule> schedules = new List<schedule>();
            List<DonutChart> stat2 = new List<DonutChart>();
            DonutChart donut = new DonutChart();
            DonutChart donut2 = new DonutChart();
            DonutChart donut3 = new DonutChart();
            foreach (var item in galleires)
            {
                foreach (var tmp in item.schedule)
                {
                    schedules.Add(tmp);
                }
            }
            //Holiday,Event,Renovations,Other

            donut.value = schedules.Count(s => s.type == "Holiday");
            donut.label = "Holiday";
            donut2.value = schedules.Count(s => s.type == "Event");
            donut2.label = "Event";
            donut3.value = schedules.Count(s => s.type == "Renovations");
            donut3.label = "Renovations";
            donut3.value = schedules.Count(s => s.type == "Other");
            donut3.label = "Other";

            stat2.Add(donut);
            stat2.Add(donut2);
            stat2.Add(donut3);
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            /*string output = jss.Serialize(stat);

            string demo = output.Replace("\"", "");*/

            ViewBag.dot = stat2;

            /*********************/
            return View();
        }

        public ActionResult GalleriesLineChart()
        {
            int scale = 50;
            List<LineChartViewModel> stat = new List<LineChartViewModel>() ;
            List<Gallery> galleires = caller.ListOfAllGalleries().OrderByDescending(o => o.maxCapacity).ToList();
            if ( scale < galleires.First().maxCapacity )
            {
                int sections = (int)galleires.First().maxCapacity /   scale;
                int oldScale = 0;
                int tmpScale = scale;
                for (int i = 0; i < sections+1; i++)
                {
                  //  if (galleires.Count(g => oldScale <= g.maxCapacity && g.maxCapacity < tmpScale)!=0)
                   // {
                        LineChartViewModel tmp = new LineChartViewModel();
                        tmp.x = tmpScale;
                        tmp.y = galleires.Count(g => oldScale <= g.maxCapacity && g.maxCapacity < tmpScale);
                        stat.Add(tmp);
                   // }//

                    oldScale = tmpScale;
                    tmpScale = tmpScale + scale;
                }
            }
            return View(stat);
        }

        public ActionResult Gallery(int id)
        {
            List<Gallery> galleires = caller.ListOfAllGalleries();
            foreach (var item in galleires)
            {
                item.schedule = caller.ListOfAllSchedules(item.id);
            }
            Gallery gallery = galleires.Find(g => g.id == id);

            return View(gallery);
        }

        public ActionResult GalleriesSchedulesCompare()
        {
            List<Gallery> galleires = caller.ListOfAllGalleries();
            foreach (var item in galleires)
            {
                item.schedule = caller.ListOfAllSchedules(item.id);
            }
            Gallery gallery = galleires.Find(g => g.id == 2);
            Gallery galleryBeta = galleires.Find(g => g.id == 3);
            int count1_17 = 0;
            int count1_18 = 0;
            int count1_19 = 0;
            int count2_17 = 0;
            int count2_18 = 0;
            int count2_19 = 0;
            //    int z = gallery.schedule.Count(e => { DateTime t = (DateTime)e.startDate} == 2017);
            foreach (var item in gallery.schedule)
            {
                DateTime t = (DateTime)item.startDate;
                if (t.Year == 2017)
                    count1_17++;
                if (t.Year == 2018)
                    count1_18++;
                if (t.Year == 2019)
                    count1_19++;
            }
            foreach (var item in galleryBeta.schedule)
            {
                DateTime t = (DateTime)item.startDate;
                if (t.Year == 2017)
                    count2_17++;
                if (t.Year == 2018)
                    count2_18++;
                if (t.Year == 2019)
                    count2_19++;
            }

            BarChartViewModel a = new BarChartViewModel();
            BarChartViewModel b = new BarChartViewModel();
            BarChartViewModel c = new BarChartViewModel();
            a.a = count1_17;
            a.b = count2_17;
            a.y = "2017";
            b.a = count1_18;
            b.b = count2_18;
            b.y = "2018";
            c.a = count1_19;
            c.b = count2_19;
            c.y = "2019";
            List<BarChartViewModel> bar = new List<BarChartViewModel>();
            bar.Add(a);
            bar.Add(b);
            bar.Add(c);
            var json = JsonConvert.SerializeObject(bar);
            ViewBag.data = json;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string output = jss.Serialize(bar);

            string demo = output.Replace("\"", "");

            ViewBag.jss = demo;

            return View(bar);

        }
      
        public ActionResult SchedulesCircle ()
        {
            List<Gallery> galleires = caller.ListOfAllGalleries();
            foreach (var item in galleires)
            {
                item.schedule = caller.ListOfAllSchedules(item.id);
            }
            List<schedule> schedules = new List<schedule>();
            List<DonutChart> stat = new List<DonutChart>();
            DonutChart donut = new DonutChart();
            DonutChart donut2 = new DonutChart();
            DonutChart donut3 = new DonutChart();
            foreach (var item in galleires)
            {
                foreach (var tmp in item.schedule)
                {
                    schedules.Add(tmp);
                } 
            }
            //Holiday,Event,Renovations,Other

           donut.value = schedules.Count(s => s.type == "Holiday");
            donut.label = "Holiday" ;
            donut2.value = schedules.Count(s => s.type=="Event");
            donut2.label = "Event";
            donut3.value = schedules.Count(s => s.type=="Renovations");
            donut3.label = "Renovations";
            donut3.value = schedules.Count(s => s.type=="Other");
            donut3.label = "Other";

            stat.Add(donut);
            stat.Add(donut2);
            stat.Add(donut3);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(stat);

            string demo = output.Replace("\"", "");

            ViewBag.dot = demo;

            return View(stat);
        }
        
        // GET: Statistics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Statistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistics/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Statistics/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Statistics/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Statistics/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Statistics/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

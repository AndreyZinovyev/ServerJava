using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace ServerJava
{
    internal class Repository
    {
        Connector db;

        public Repository()
        {
            
            db = new Connector();
            db.Cars.Load();

        }
        public string Create(string a)
        {
            string c="0";
            try
            {
                Cars car = new Cars();
                car.Number = a;
                car.TimeStart = DateTime.Now.ToString();
                db.Cars.Add(car);
                db.SaveChanges();
            }
            catch { c = "Добавление не удалось"; }
            
                return c;
            
           
        }
        public List<string> Read(string b)
        {
            var car = db.Cars.ToList();
            List<string> listCar = new List<string>();
            foreach (Cars u in car )
            {
                listCar.Add(u.Number +" ; "+ u.TimeStart);
                Console.WriteLine($"{u.Number}.{u.TimeStart} \n");

            }
            return  listCar;
        }
        public string Update(string c)
        {
            var car = db.Cars.ToList();
            List<string> listCar = new List<string>();
            foreach (Cars u in car) { if(u.Number == c) {
                    u.TimeEnd = "12.00";break; }
            }
            db.SaveChanges();

                return "0";
        }
        public string Delete(string d)
        {
            var car = db.Cars.ToList();
            List<string> listCar = new List<string>();
            foreach (Cars u in car)
            {
                if (u.Number == d)
                {
                    db.Cars.Remove(u);
                    break;
                }
            }
            db.SaveChanges();
            return "0";
        }
        
    }

}

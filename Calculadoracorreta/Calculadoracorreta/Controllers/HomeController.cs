using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //inicializa o valor inicial
            ViewBag.Ecra = "0";
            Session["primeiraVezOperador"] = true;
            Session["privalor"] = 0;
            Session["operacao"] = "";
            Session["seg"] = false;
            return View();
        }

        // GET: Post
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            
            //var. auxiliar
            string ecra = visor;
            //verifica se 
            if ((bool)Session["seg"] == true)
            {
                ecra = "0";
                Session["seg"] = false;
            }
            float privalor;
            //identificar o valor na variável 'bt'
            switch (bt)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    //se entrei aqui é porque foi selecionado um algarismo

                    //vou decidir o que fazer quando no visor só existe o 0
                    if (visor == "0")
                    { //visor.Equals("0")
                        ecra = bt;
                    }
                    else
                    {
                        ecra = ecra + bt;
                    }

                    break;
                case ",":
                    //processar o caso da virgula
                    if (!visor.Contains(","))
                        ecra = ecra + bt;
                    break;

                case "+":
                case "-":
                case "x":
                case ":":

                    if ((bool)Session["primeiraVezOperador"] == true)
                    {
                        Session["privalor"] = float.Parse(ecra);
                        ecra = "0";
                        Session["operacao"] = bt;
                        Session["primeiraVezOperador"] = false;
                    }
                    else
                    {
                        if (Session["operacao"].Equals("+")){
                            privalor = (float)Session["privalor"];
                            privalor =  privalor + float.Parse(ecra);
                            ecra = string.Format("{0:0,0.0000000}", privalor); ;
                        }
                        if (Session["operacao"].Equals("-"))
                        {
                            privalor = (float)Session["privalor"];
                            privalor = privalor - float.Parse(ecra);
                            ecra = string.Format("{0:0,0.0000000}", privalor); ;
                        }
                        if (Session["operacao"].Equals("x"))
                        {
                            privalor = (float)Session["privalor"];
                            privalor = privalor * float.Parse(ecra);
                            ecra = string.Format("{0:0,0.0000000}", privalor); ;
                        }
                        if (Session["operacao"].Equals(":"))
                        {
                            privalor = (float)Session["privalor"];
                            privalor = privalor / float.Parse(ecra);
                            ecra = string.Format("{0:0,0.0000000}", privalor); ;
                        }
                        Session["privalor"] = float.Parse(ecra);
                        Session["operacao"] = bt;
                        Session["seg"] = true;
                    }


                    break;
                case "+/-":
                    ecra = string.Format("{0:,0.0000000}", float.Parse(ecra)*-1);

                    break;
                        
                case "=":
                    if (Session["operacao"].Equals("+"))
                    {
                        privalor = (float)Session["privalor"];
                        privalor = privalor + float.Parse(ecra);
                        ecra = string.Format("{0:0,0.0000000}", privalor); ;
                    }
                    if (Session["operacao"].Equals("-"))
                    {
                        privalor = (float)Session["privalor"];
                        privalor = privalor - float.Parse(ecra);
                        ecra = string.Format("{0:0,0.0000000}", privalor); ;
                    }
                    if (Session["operacao"].Equals("x"))
                    {
                        privalor = (float)Session["privalor"];
                        privalor = privalor * float.Parse(ecra);
                        ecra = string.Format("{0:0,0.0000000}", privalor); ;
                    }
                    if (Session["operacao"].Equals(":"))
                    {
                        privalor = (float)Session["privalor"];
                        privalor = privalor / float.Parse(ecra);
                        ecra = string.Format("{0:0,0.0000000}", privalor); ;
                    }
                    Session["privalor"] = 0;
                    Session["operacao"] = "";
                    Session["primeiraVezOperador"] = true;
                    Session["seg"] = true;
                    break;
                case "C":
                    ecra = "0";
                    Session["privalor"] = 0;
                    Session["operacao"] = "";
                    Session["primeiraVezOperador"] = true;
                    Session["seg"] = true;
                    break;
            }
            //prepara o conteúdo a aparecer no VISOR da View
            ViewBag.Ecra = ecra;


            return View();
        }
    }
}
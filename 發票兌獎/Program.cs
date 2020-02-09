using System; 
using System.Net; 
using System.IO; 
using System.Text.RegularExpressions; 
using System.Collections.Generic; 

 

namespace hw_01 
{ 
    class Program { 
        // 取得發票對獎號碼 
        static List<string> GetString(){ 
        
            List<string> prizes = new List<string>(); 
            string html = URLrequest(); 
            Match block = Regex.Match(html, @"display: special(.*?)領獎期間自"); 

            //特別獎[0] 
            string reg =">特別獎<(.*?)</span>"; 
            Match special = Regex.Match(block.Value, @reg); 
            prizes.Add(special.Value.Substring(66, 8)); 

            //特獎[1] 
            string reg2=">特獎<(.*?)</span>"; 
            Match best = Regex.Match(block.Value, @reg2); 
            prizes.Add(best.Value.Substring(65, 8)); 



            //頭獎[2:4] 
            string reg3="newFirstPrize(.*?)</span>"; 
            Match first = Regex.Match(block.Value, @reg3); 
            prizes.Add(first.Value.Substring(30, 8)); 
            prizes.Add(first.Value.Substring(39, 8)); 
            prizes.Add(first.Value.Substring(48, 8)); 

            //二獎~六獎 
            for (int i=0; i<5; i++){ 
            prizes.Add(first.Value.Substring(31+i, 7-i)); 
            prizes.Add(first.Value.Substring(40+i, 7-i)); 
            prizes.Add(first.Value.Substring(48+i, 7-i)); 
            } 

            //加開六獎[20, 21] 
            string reg4="newAddSixPrize(.*?)</span>"; 
            Match six = Regex.Match(block.Value, @reg4); 
            prizes.Add(six.Value.Substring(31, 3)); 
            prizes.Add(six.Value.Substring(35, 3)); 

            /*----- debugging -----*/ 
            //foreach(string i in prizes) Console.WriteLine("prizes:"+i); 
            return prizes; 
        } 

        // 取得財政部兌獎網站資料內容 
        static string Compare(List<string> prizes, List<string> candidate){ 
            string output = ""; 
    
            for (int i=0; i<candidate.Count; i++){ 
                string cand=candidate[i]; 
                    for (int j = 0; j<prizes.Count; j++){ 
                        if (Regex.IsMatch(cand, Regex.Escape(prizes[j])+@"\z")){ 
                            output += (j+" "); 
                            break; 
                        } 
                    }
            } 
            return output; 
        } 

        // 程式碼來源：http://zetcode.com/articles/csharpreadwebpage/ 
        // 取得網站內文全文 
        static string URLrequest(){ 
            string html = ""; 
            string url = "http://invoice.etax.nat.gov.tw/"; 
            HttpWebRequest request = (HttpWebRequest) 
            WebRequest.Create(url); 
    
            using (HttpWebResponse response = (HttpWebResponse) 
            request.GetResponse()) 
    
            using (Stream stream = response.GetResponseStream()) 
    
            using (StreamReader reader = new StreamReader(stream)) { 
                html = reader.ReadToEnd(); 
            } 
            return html;
        }

        // 處理發票兌獎主程式 
        static void Prizing(ref long money){ 
            List<string> candidate = new List<string>(); 
            List<string> prizes = new List<string>(); 
            string foo = ""; 
            bool prized = false; 
            long deposit = 0; 
            Console.WriteLine("輸入發票號碼，輸入完畢請輸入 q。"); 
 
            // 取得使用者發票資訊 
            while(foo!="q"){ 
                foo= Console.ReadLine(); 

                if (foo.Length == 8){
                    candidate.Add(foo);} 
                else if (foo!="q") {
                    Console.WriteLine("請輸入 8 位數字！！！\n");}
                }
                Console.WriteLine(); 

                // 取得財政部發票兌獎資訊 
                prizes = GetString(); 

                /*----- 開始兌獎 -----*/ 
                foo = Compare(prizes, candidate); 
                foreach (string i in foo.Split(" ")) { 
                switch (i){ 
                    case "0": {Console.WriteLine("恭喜您，中特別獎！");deposit+=10000000; prized = true; break;} 
                    case "1": {Console.WriteLine("恭喜您，中特別獎！");deposit+=2000000; prized = true; break;} 
                    case "2": case "3": 
                    case "4": {Console.WriteLine("恭喜您，中頭獎！");deposit+=200000; prized = true; break;} 
                    case "5": case "6": 
                    case "7": {Console.WriteLine("恭喜您，中二獎！");deposit+=40000; prized = true; break;} 
                    case "8": case "9": 
                    case "10": {Console.WriteLine("恭喜您，中三獎！");deposit+=10000; prized = true; break;} 
                    case "11":case "12": 
                    case "13": {Console.WriteLine("恭喜您，中四獎！");deposit+=4000; prized = true; break;} 
                    case "14":case "15": 
                    case "16": {Console.WriteLine("恭喜您，中五獎！");deposit+=1000; prized = true; break;} 
                    case "17":case "18": case "19": case "20": 
                    case "21": {Console.WriteLine("恭喜您，中六獎！");deposit+=200; prized = true; break;} 
                }
            }
            // 將獎金存入 money 


            if (!prized){ Console.WriteLine("可惜沒中~~再接再厲!!");} 
                Console.WriteLine(); 
                money = ChangeMoney(money, deposit); 
                //return money; 
            } 

 

        // 存提款程式 
        static long ChangeMoney(long money, long deposit){ 
            // 存提款 
            if (money+deposit < 0){ 
                Console.WriteLine("餘額不足！！！現在餘額為："+Convert.ToString(money)+"元。\n"); 
                return money; 
            } 
            else money += deposit; 
        
            // 輸出存提款明細 
            if (deposit>0) Console.WriteLine("已存款"+Convert.ToString(deposit)+"元，現在存款為："+Convert.ToString(money)+"元。"); 
            else if (deposit<0) Console.WriteLine("已提出"+Convert.ToString(-deposit)+"元，現在存款為："+Convert.ToString(money)+"元。"); 
            else Console.WriteLine("存款為："+Convert.ToString(money)+"元。\n"); 
        
            return money; 
        } 

 

        // 存提款主程式 
        static void Banking(ref long money){ 
            bool entered = true; 
            long deposit=0; 

            // 取得存提款款項 
            do { 
                Console.WriteLine("請輸入金額(欲提款請輸入負值)："); 
                
                try{ // try this code 
                    deposit = Convert.ToInt64(Console.ReadLine()); 
                    entered = true; 
                } 
                catch{ // if failed, then exectue this code 
                    Console.WriteLine("請輸入數字！！！\n"); 
                    entered=false; 
                } 
            }while (!entered); 

            //存提款 
            money = ChangeMoney(money, deposit); 

            //return money; 
        } 

        // 主控制程式 
        static void Main(string[] args) { 
            long money = 0; 
            while (true){ 
                Console.WriteLine("請輸入欲進行之服務：(Banking 存提款 / Prizing 發票對獎)"); 
                string service = Console.ReadLine(); 

                switch (service){ 
                    case "Banking": Banking(ref money);break; 
                    case "Prizing": Prizing(ref money);break; 
                    case "q": Console.WriteLine("ByeBye!"); return; 
                    default : Console.WriteLine("請輸入 Banking/Prizing/q！\n");break; 
                } 
            } 
        } 
    } 
} 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoRunSnapShot
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan TsStart = new TimeSpan(DateTime.Now.Ticks);
            String[] CmdArgs= System.Environment.GetCommandLineArgs();
            if (CmdArgs.Length > 1)
            {
                //参数0是它本身的路径
                String FileName = CmdArgs[1].ToString();
                String Dir = CmdArgs[2].ToString();
                String Url = CmdArgs[3].ToString();
                SnapShot sp = new SnapShot();
                sp.FileName = FileName;// "test.jpg";
                sp.Dir = Dir; //@"D:\web\7.03\eweb_lifetour";
                sp.Url = Url; //@"http://lifetour20.travelindex.com.tw/eWeb/GO/V_GO_Detail.asp?MGRUP_CD=0000&GRUP_CD=0000141015A&SUB_CD=GO&JOIN_TP=1";
                sp.Save();
                
                
            }
            
            System.Timers.Timer tmr = new System.Timers.Timer(20000);
            tmr.Elapsed += delegate
            {
                Environment.Exit(0);
            };
            tmr.Start();
            Console.ReadKey();
        }
    }
}

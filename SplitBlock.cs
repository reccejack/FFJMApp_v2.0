using System;
using System.Collections;
using System.Collections.Generic;

namespace FreeFallJumpmasterApp
{
    public class SplitBlock
    {
        public string OpType { get; set; }
        public int AltitudeBlock { get; set; }
        public int WindDirection { get; set; }
        public int WindVelocity { get; set; }

        //Splitlist 
        public List<AltBlock> SplitList = new List<AltBlock>();

        //Empty altitude block lists
        public List<int> alt_beginningList = new List<int>();
        public List<int> alt_dogLegList1 = new List<int> ();
        public List<int> alt_dogLegList2 = new List<int>();

        //Empty wind direction lists
        public List<int> beginningList = new List<int>();
        public List<int> dogLegList1 = new List<int>();
        public List<int> dogLegList2 = new List<int>();

        //Empty wind velocity lists
        public List<int> v_beginningList = new List<int>();
        public List<int> v_dogLegList1 = new List<int>();
        public List<int> v_dogLegList2 = new List<int>();

        //Reform calculation lists once doglegs have been split up
        public List<List<int>> full_beginning_list = new List<List<int>> { };
        public List<List<int>> full_dogLegList1 = new List<List<int>> { };
        public List<List<int>> full_dogLegList2 = new List<List<int>> { };

        public SplitBlock(string optype)
        {
            OpType = optype;
        }

        public void ShowSplitBlock()
        {
            Console.WriteLine("[Altitude]\t[Direction]\t[Velocity]");
            Console.WriteLine("-----------------------------------");
            foreach (AltBlock block in SplitList) { Console.WriteLine("Alt: {0}\t\tdir: {1}\tspeed: {2}", block.AltitudeBlock, block.WindDirection, block.WindVelocity); }
            Console.WriteLine(" ");
        }

        public void ShowList(List<int> list)
        {
            Console.WriteLine("Showing List: ");
            foreach(int i in list)
            {
                Console.WriteLine(i);
            }
        }

        //Create full calc lists
        public void PopulateCalcList()
        {
            //input PopulateCalcList method here...
            this.full_beginning_list.Add(alt_beginningList);
            this.full_beginning_list.Add(beginningList);
            this.full_beginning_list.Add(v_beginningList);
            this.full_dogLegList1.Add(alt_dogLegList1);
            this.full_dogLegList1.Add(dogLegList1);
            this.full_dogLegList1.Add(v_dogLegList1);
            this.full_dogLegList2.Add(alt_dogLegList2);
            this.full_dogLegList2.Add(dogLegList2);
            this.full_dogLegList2.Add(v_dogLegList2);
        }


    }
}
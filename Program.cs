using System;
using System.Security.Principal;

namespace FreeFallJumpmasterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Introduction screen
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine("||||||||||||FREE FALL JUMPMASTER CALCULATOR                |||||||||||||||||||||||||");
            Console.WriteLine("||||||||||||--Version 5.0----------------------------------|||||||||||||||||||||||||");
            Console.WriteLine("||||||||||||CREATED BY: JACK H HAUSMANN                    |||||||||||||||||||||||||");
            Console.WriteLine("||||||||||||HAUSMANN MULTIMEDIA & DESIGN SOLUTIONS (2023)  |||||||||||||||||||||||||");
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");

            //Prompt user for operation type: 'HALO' or 'HAHO'
            Console.Write("Select operation type>> HALO or HAHO: ");
            string opType = Console.ReadLine();

            
            //Prompt user for initial inputs
            Console.Write("Input jump altitude: ");
            int jump = int.Parse(Console.ReadLine());
            
            //Prompt user for pull altitude
            Console.Write("Input pull altitude: ");
            int pull = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------------------------------------------------------");

            //instantiate the operational data and split block objects
        
            OpDataField op1 = new OpDataField(opType, jump, pull);
            op1.ApplyOperationType();
            SplitBlock FreeFallDriftData = new SplitBlock(opType);
            SplitBlock CanopyDriftData = new SplitBlock(opType);

            //implement OpDataField methods
            op1.AddBlock(jump);
            op1.DisplayInputs();
            op1.SplitBlock(CanopyDriftData,FreeFallDriftData);

            //unit test to determine data split
            FreeFallDriftData.ShowSplitBlock();
            CanopyDriftData.ShowSplitBlock();

            //implementation of OpDataField.ExtractDogLeg method
            op1.ExtractDogLeg(CanopyDriftData.SplitList, CanopyDriftData.alt_beginningList, CanopyDriftData.alt_dogLegList1, CanopyDriftData.alt_dogLegList2, CanopyDriftData.beginningList, CanopyDriftData.dogLegList1, CanopyDriftData.dogLegList2, CanopyDriftData.v_beginningList, CanopyDriftData.v_dogLegList1, CanopyDriftData.v_dogLegList2);
            op1.ExtractDogLeg(FreeFallDriftData.SplitList, FreeFallDriftData.alt_beginningList, FreeFallDriftData.alt_dogLegList1, FreeFallDriftData.alt_dogLegList2, FreeFallDriftData.beginningList, FreeFallDriftData.dogLegList1, FreeFallDriftData.dogLegList2, FreeFallDriftData.v_beginningList, FreeFallDriftData.v_dogLegList1, FreeFallDriftData.v_dogLegList2);
            op1.CalcIncompatibleWind(FreeFallDriftData.beginningList);
            op1.CalcIncompatibleWind(FreeFallDriftData.dogLegList1);
            op1.CalcIncompatibleWind(FreeFallDriftData.dogLegList2);
            op1.CalcIncompatibleWind(CanopyDriftData.beginningList);
            op1.CalcIncompatibleWind(CanopyDriftData.dogLegList1);
            op1.CalcIncompatibleWind(CanopyDriftData.dogLegList2);

            //populate calculation lists
            FreeFallDriftData.PopulateCalcList();
            CanopyDriftData.PopulateCalcList();

            //unit tests for list data
            Console.WriteLine("----------------------Freefall Drift Inputs----------------------------");
            Console.WriteLine("Altitude blocks:");
            FreeFallDriftData.ShowList(FreeFallDriftData.alt_beginningList);
            Console.WriteLine("Wind direction and velocity:");
            FreeFallDriftData.ShowList(FreeFallDriftData.beginningList);
            FreeFallDriftData.ShowList(FreeFallDriftData.dogLegList1);
            FreeFallDriftData.ShowList(FreeFallDriftData.dogLegList2);
            Console.WriteLine("----------------------Canopy Drift Inputs------------------------------");
            Console.WriteLine("Altitude blocks:");
            CanopyDriftData.ShowList(CanopyDriftData.alt_beginningList);
            Console.WriteLine("Wind direction and velocity:");
            CanopyDriftData.ShowList(CanopyDriftData.beginningList);
            CanopyDriftData.ShowList(CanopyDriftData.dogLegList1);
            CanopyDriftData.ShowList(CanopyDriftData.dogLegList2);
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Individual Lists:");
            foreach (List<int> list in FreeFallDriftData.full_beginning_list)
            {
                foreach(int i in list)
                {
                    Console.WriteLine(i);
                }
            }




        }
    }
}
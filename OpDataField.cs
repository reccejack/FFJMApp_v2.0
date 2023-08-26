using System;
using System.Collections;
using System.Collections.Generic;

namespace FreeFallJumpmasterApp
{
    public class OpDataField
    {
        int JumpAltitude { get; set; }
        int PullAltitude { get; set;  }
        string OpType { get; set; }
        int K_VALUE_CD { get; set; }
        int K_VALUE_FFD { get; set; }

        List<AltBlock> OperationalData = new List<AltBlock>();
        List<AltBlock> FreeFallDriftSplitBlock = new List<AltBlock>();
        List<AltBlock> CanopyDriftSplitBlock = new List<AltBlock>();


        public OpDataField(string opType, int jumpAltitude, int pullAltitude)
        {
            OpType = opType;
            JumpAltitude = jumpAltitude;
            PullAltitude = pullAltitude;
        }

        //This method will take the 'operation type', input lists, and assign appropriate K values.
        public void ApplyOperationType()
        {
            if(OpType == "HALO" || OpType == "halo")
            {
                OpType = OpType.ToUpper();
                K_VALUE_CD = 25;
                K_VALUE_FFD = 3;
            } else if(OpType == "HAHO" || OpType == "haho")
            {
                OpType = OpType.ToUpper();
                K_VALUE_CD = 46;
                K_VALUE_FFD = 46;
            }
        }

        //Based on the 'opType' property, the wind data obtained must be adjusted.
        public void AddBlock(int jumpAltitude)
        {
            int direction;
            int velocity;

            for(int i = JumpAltitude; i >= 0; i--)
            {
                if(i == 0)
                {
                    Console.Write("Input wind direction at Surface: ");
                    direction = int.Parse(Console.ReadLine());
                    Console.Write("Input wind velocity at Surface: ");
                    velocity = int.Parse(Console.ReadLine());
                    OperationalData.Add(new AltBlock(i, direction, velocity));
                    Console.WriteLine($"ADDED: Surface winds: {direction} degrees at {velocity} KTS");
                } else if(i > PullAltitude && i % 2 == 0)
                {
                    Console.Write("Input wind direction at {0} thousand feet: ", i);
                    direction = int.Parse(Console.ReadLine());
                    Console.Write("Input wind velocity at {0} thousand feet: ", i);
                    velocity = int.Parse(Console.ReadLine());
                    OperationalData.Add(new AltBlock(i, direction, velocity));
                    Console.WriteLine($"{i} thousand feet: {direction} degrees at {velocity} KTS");
                } else if(i <= PullAltitude)
                {
                    Console.Write("Input wind direction at {0} thousand feet: ", i);
                    direction = int.Parse(Console.ReadLine());
                    Console.Write("Input wind velocity at {0} thousand feet: ", i);
                    velocity = int.Parse(Console.ReadLine());
                    OperationalData.Add(new AltBlock(i, direction, velocity));
                    Console.WriteLine($"{i} thousand feet: {direction} degrees at {velocity} KTS");
                }
            }
        }

        public void DisplayInputs()
        {
            foreach(AltBlock block in OperationalData)
            {
                Console.WriteLine(block);
                Console.WriteLine(block.AltitudeBlock);
                Console.WriteLine(block.WindDirection);
                Console.WriteLine(block.WindVelocity);
            }
        }

        public void SplitBlock(SplitBlock splitBlock_FFD, SplitBlock splitBlock_CD)
        {
            //create a method that splits the OperationalData along 'pull altitude' parameters
            for(int i = 0; i < OperationalData.Count(); i++)
            {
                if(i <= PullAltitude)
                {
                    splitBlock_CD.SplitList.Add(OperationalData[i]);
                } else if(i > PullAltitude && i % 2 == 0)
                {
                    splitBlock_FFD.SplitList.Add(OperationalData[i]);
                }
            }
        }

        public void ExtractDogLeg(List<AltBlock> splitList, List<int> alt_beginningList, List<int> alt_dogLegList1, List<int> alt_dogLegList2, List<int> beginningList, List<int> dogLegList1, List<int> dogLegList2, List<int> v_beginningList, List<int> v_dogLegList1, List<int> v_dogLegList2)
        {
            int counter = 1;

            for (int i = 1; i < splitList.Count(); i++)
            {
                bool diff = Math.Abs(splitList[i].WindDirection - splitList[i - 1].WindDirection) >= 90;
                if (diff)
                {
                    if (dogLegList1.Count() == 0)
                    {
                        alt_dogLegList1.Add(splitList[i].AltitudeBlock);
                        dogLegList1.Add(splitList[i].WindDirection);
                        v_dogLegList1.Add(splitList[i].WindVelocity);

                    }
                    else if (dogLegList1.Count() > 0)
                    {
                        alt_dogLegList2.Add(splitList[i].AltitudeBlock);
                        dogLegList2.Add(splitList[i].WindDirection);
                        v_dogLegList2.Add(splitList[i].WindVelocity);
                    }

                }
                else if (!diff && beginningList.Count() < 1)
                {
                    {
                        alt_beginningList.Add(splitList[0].AltitudeBlock);
                        beginningList.Add(splitList[0].WindDirection);
                        v_beginningList.Add(splitList[0].WindVelocity);
                        alt_beginningList.Add(splitList[i].AltitudeBlock);
                        beginningList.Add(splitList[i].WindDirection);
                        v_beginningList.Add(splitList[i].WindVelocity);
                        counter++;
                    }
                }
                else if (!diff && dogLegList1.Count() == 0)
                {
                    alt_beginningList.Add(splitList[i].AltitudeBlock);
                    beginningList.Add(splitList[i].WindDirection);
                    v_beginningList.Add(splitList[i].WindVelocity);
                    counter++;
                }
                else if (!diff && dogLegList1.Count() >= 1)
                {
                    if (dogLegList2.Count() > 0)
                    {
                        alt_dogLegList2.Add(splitList[i].AltitudeBlock);
                        dogLegList2.Add(splitList[i].WindDirection);
                        v_dogLegList2.Add(splitList[i].WindVelocity);
                    }
                    else
                    {
                        alt_dogLegList1.Add(splitList[i].AltitudeBlock);
                        dogLegList1.Add(splitList[i].WindDirection);
                        v_dogLegList1.Add(splitList[i].WindVelocity);
                    }
                }
            }
        }

        public void CalcIncompatibleWind(List<int> winds)
        {
            for (int i = 0; i < winds.Count; i++)
            {
                int value = winds[i];
                //int index = winds.IndexOf(i);
                if (value < 10 && value >= 0)
                {
                    winds[i] = value + 360;
                    Console.WriteLine($"{value} was added to 360 to now equal {value + 360}");
                }
            }
        }


    }
}
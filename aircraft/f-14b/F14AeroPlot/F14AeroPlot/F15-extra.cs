﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F14AeroPlot
{
    /// <summary>
    /// extra aerodynamics; based on those in the F-14 but modified to the F-15
    /// </summary>
    public class F15_extra
    {
        static List<string> get_elements(string line)
        {
            var f = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return f.ToList();
        }

        static List<BreakPoint> parse(DataElement Parent, int dimensions, string s)
        {
            int lc = 0;
            List<string> iv1_bp = new List<string>();
            List<string> iv2_bp = new List<string>();
            List<BreakPoint> bpl = new List<BreakPoint>();
            foreach (var line in s.Replace("\r", "\n").Replace("\n\n", "\n").Split('\n'))
            {
                var iv1_pos = 0;
                if (!String.IsNullOrEmpty(line))
                {
                    var elems = get_elements(line);
                    if (dimensions == 2 && lc == 0)
                    {
                        lc++;
                        iv1_bp = elems.ToList();
                        continue;
                    }
                    System.Console.WriteLine("{0}: {1}", lc, String.Join(",", elems));
                    var iv2 = double.Parse(elems[0]);
                    foreach (var ee in elems.Skip(1))
                    {
                        if (dimensions == 2)
                            bpl.Add(new BreakPoint(Parent, Double.Parse(iv1_bp[iv1_pos++]), iv2, Double.Parse(ee)));
                        else
                            bpl.Add(new BreakPoint(Parent, iv2, Double.Parse(ee)));
                    }

                    lc++;
                }
            }
            return bpl;
        }
        public static void AddExtra(Aerodata aero)
        {
            var ad = aero.Add("Lift Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "ClMach", "mach");
            ad.data = parse(ad, 1, @"0	1
                                    0.2	1
                                    0.6	0.95837601
                                    0.9	0.5
                                    1.1	1
                                    2.4	0.5
");
            ad = aero.Add("Yaw factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CyMach", "mach");
            ad.data = parse(ad, 1, @"0	1
                                    0.2	1
                                    0.6	0.95837601
                                    0.9	0.5
                                    1.1	1
                                    2.4	0.5
");
            ad = aero.Add("Roll Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CLMach", "mach");
            ad.data = parse(ad, 1, @"0	1
                                    0.2	1
                                    0.6	0.95837601
                                    0.9	0.5
                                    1.1	1
                                    2.4	0.5
");
            ad = aero.Add("Pitch Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CMMach", "mach");
            ad.data = parse(ad, 1, @"0	1
                                    0.2	1
                                    0.6	0.95837601
                                    0.9	0.5
                                    1.1	1
                                    2.4	0.5
");
            ad = aero.Add("Yaw Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CNMach", "mach");
            ad.data = parse(ad, 1, @"0	1
                                    0.2	1
                                    0.6	0.95837601
                                    0.9	0.5
                                    1.1	1
                                    2.4	0.5
");
            //            var ad = aero.Add("Lift Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "ClMach", "mach", "alpha");

            //            ad.data = parse(ad, 2, @"   0.0         0.2         0.6         0.9         1.1         1.3         1.5         1.8         2.1         2.6         3.0
            //                           -20	1.00000000	1.00000000	0.96610485	1.23950808	1.41632259	1.34327421	1.13294356	0.93062904	0.76851372	0.58968226	0.51614275
            //                           -15	1.00000000	1.00000000	0.96116198	1.23316641	1.40907629	1.33640164	1.12714710	0.92586768	0.76458179	0.58666528	0.51350202
            //                           -10	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                            -5	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                             4	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //                             8	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                            12	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                            16	1.00000000	1.00000000	0.98015728	1.25753729	1.43692365	1.36281274	1.14942274	0.94416546	0.77969210	0.59825946	0.52365028
            //                            20	1.00000000	1.00000000	0.96074823	1.23263556	1.40846972	1.33582635	1.12666189	0.92546912	0.76425265	0.58641274	0.51328097
            //                            24	1.00000000	1.00000000	0.96503140	1.23813085	1.41474891	1.34178168	1.13168474	0.92959501	0.76765981	0.58902706	0.51556926
            //                            28	1.00000000	1.00000000	0.96044147	1.23224199	1.40802001	1.33539984	1.12630217	0.92517363	0.76400864	0.58622551	0.51311708
            //                            32	1.00000000	1.00000000	0.96642959	1.23992472	1.41679867	1.34372573	1.13332438	0.93094186	0.76877204	0.58988048	0.51631624
            //                            40	1.00000000	1.00000000	0.95223257	1.22171001	1.39598566	1.32398617	1.11667565	0.91726616	0.75747865	0.58121503	0.50873147
            //                            45	1.00000000	1.00000000	0.95617790	1.22677185	1.40176956	1.32947176	1.12130231	0.92106662	0.76061706	0.58362314	0.51083926
            //                            50	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //");
            //            ad = aero.Add("Sideforce Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CyMach", "mach", "alpha");

            //            ad.data = parse(ad, 2, @"    0.0         0.2         0.6         0.9         1.1         1.3         1.5         1.8         2.1         2.6         3.0
            //                                -20	1.00000000	1.00000000	0.96610485	1.23950808	1.41632259	1.34327421	1.13294356	0.93062904	0.76851372	0.58968226	0.51614275
            //                                -15	1.00000000	1.00000000	0.96116198	1.23316641	1.40907629	1.33640164	1.12714710	0.92586768	0.76458179	0.58666528	0.51350202
            //                                -10	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                -5	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                 4	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //                                 8	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                12	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                16	1.00000000	1.00000000	0.98015728	1.25753729	1.43692365	1.36281274	1.14942274	0.94416546	0.77969210	0.59825946	0.52365028
            //                                20	1.00000000	1.00000000	0.96074823	1.23263556	1.40846972	1.33582635	1.12666189	0.92546912	0.76425265	0.58641274	0.51328097
            //                                24	1.00000000	1.00000000	0.96503140	1.23813085	1.41474891	1.34178168	1.13168474	0.92959501	0.76765981	0.58902706	0.51556926
            //                                28	1.00000000	1.00000000	0.96044147	1.23224199	1.40802001	1.33539984	1.12630217	0.92517363	0.76400864	0.58622551	0.51311708
            //                                32	1.00000000	1.00000000	0.96642959	1.23992472	1.41679867	1.34372573	1.13332438	0.93094186	0.76877204	0.58988048	0.51631624
            //                                40	1.00000000	1.00000000	0.95223257	1.22171001	1.39598566	1.32398617	1.11667565	0.91726616	0.75747865	0.58121503	0.50873147
            //                                45	1.00000000	1.00000000	0.95617790	1.22677185	1.40176956	1.32947176	1.12130231	0.92106662	0.76061706	0.58362314	0.51083926
            //                                50	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //");
            //            ad = aero.Add("Roll Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CLMach", "mach", "alpha");

            //            ad.data = parse(ad, 2, @"    0.0         0.2         0.6         0.9         1.1         1.3         1.5         1.8         2.1         2.6         3.0
            //                                -20	1.00000000	1.00000000	0.96610485	1.23950808	1.41632259	1.34327421	1.13294356	0.93062904	0.76851372	0.58968226	0.51614275
            //                                -15	1.00000000	1.00000000	0.96116198	1.23316641	1.40907629	1.33640164	1.12714710	0.92586768	0.76458179	0.58666528	0.51350202
            //                                -10	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                -5	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                 4	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //                                 8	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                12	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                16	1.00000000	1.00000000	0.98015728	1.25753729	1.43692365	1.36281274	1.14942274	0.94416546	0.77969210	0.59825946	0.52365028
            //                                20	1.00000000	1.00000000	0.96074823	1.23263556	1.40846972	1.33582635	1.12666189	0.92546912	0.76425265	0.58641274	0.51328097
            //                                24	1.00000000	1.00000000	0.96503140	1.23813085	1.41474891	1.34178168	1.13168474	0.92959501	0.76765981	0.58902706	0.51556926
            //                                28	1.00000000	1.00000000	0.96044147	1.23224199	1.40802001	1.33539984	1.12630217	0.92517363	0.76400864	0.58622551	0.51311708
            //                                32	1.00000000	1.00000000	0.96642959	1.23992472	1.41679867	1.34372573	1.13332438	0.93094186	0.76877204	0.58988048	0.51631624
            //                                40	1.00000000	1.00000000	0.95223257	1.22171001	1.39598566	1.32398617	1.11667565	0.91726616	0.75747865	0.58121503	0.50873147
            //                                45	1.00000000	1.00000000	0.95617790	1.22677185	1.40176956	1.32947176	1.12130231	0.92106662	0.76061706	0.58362314	0.51083926
            //                                50	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //");
            //            ad = aero.Add("Pitch Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CMMach", "mach", "alpha");

            //            ad.data = parse(ad, 2, @"    0.0         0.2         0.6         0.9         1.1         1.3         1.5         1.8         2.1         2.6         3.0
            //                                -20	1.00000000	1.00000000	0.96610485	1.23950808	1.41632259	1.34327421	1.13294356	0.93062904	0.76851372	0.58968226	0.51614275
            //                                -15	1.00000000	1.00000000	0.96116198	1.23316641	1.40907629	1.33640164	1.12714710	0.92586768	0.76458179	0.58666528	0.51350202
            //                                -10	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                -5	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                 4	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //                                 8	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                12	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                16	1.00000000	1.00000000	0.98015728	1.25753729	1.43692365	1.36281274	1.14942274	0.94416546	0.77969210	0.59825946	0.52365028
            //                                20	1.00000000	1.00000000	0.96074823	1.23263556	1.40846972	1.33582635	1.12666189	0.92546912	0.76425265	0.58641274	0.51328097
            //                                24	1.00000000	1.00000000	0.96503140	1.23813085	1.41474891	1.34178168	1.13168474	0.92959501	0.76765981	0.58902706	0.51556926
            //                                28	1.00000000	1.00000000	0.96044147	1.23224199	1.40802001	1.33539984	1.12630217	0.92517363	0.76400864	0.58622551	0.51311708
            //                                32	1.00000000	1.00000000	0.96642959	1.23992472	1.41679867	1.34372573	1.13332438	0.93094186	0.76877204	0.58988048	0.51631624
            //                                40	1.00000000	1.00000000	0.95223257	1.22171001	1.39598566	1.32398617	1.11667565	0.91726616	0.75747865	0.58121503	0.50873147
            //                                45	1.00000000	1.00000000	0.95617790	1.22677185	1.40176956	1.32947176	1.12130231	0.92106662	0.76061706	0.58362314	0.51083926
            //                                50	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //");
            //            ad = aero.Add("Yaw Mach factor (NASA-TM-84643 Figure 3 Clalpha) and values for AOA derived from NASA-aaia-2000-0900 Figure 4", "CNMach", "mach", "alpha");

            //            ad.data = parse(ad, 2, @"    0.0         0.2         0.6         0.9         1.1         1.3         1.5         1.8         2.1         2.6         3.0
            //                                -20	1.00000000	1.00000000	0.96610485	1.23950808	1.41632259	1.34327421	1.13294356	0.93062904	0.76851372	0.58968226	0.51614275
            //                                -15	1.00000000	1.00000000	0.96116198	1.23316641	1.40907629	1.33640164	1.12714710	0.92586768	0.76458179	0.58666528	0.51350202
            //                                -10	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                -5	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                 4	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //                                 8	1.00000000	1.00000000	0.99142346	1.27199174	1.45344001	1.37847726	1.16263449	0.95501794	0.78865408	0.60513601	0.52966925
            //                                12	1.00000000	1.00000000	0.96759116	1.24141501	1.41850155	1.34534078	1.13468655	0.93206078	0.76969605	0.59058947	0.51693681
            //                                16	1.00000000	1.00000000	0.98015728	1.25753729	1.43692365	1.36281274	1.14942274	0.94416546	0.77969210	0.59825946	0.52365028
            //                                20	1.00000000	1.00000000	0.96074823	1.23263556	1.40846972	1.33582635	1.12666189	0.92546912	0.76425265	0.58641274	0.51328097
            //                                24	1.00000000	1.00000000	0.96503140	1.23813085	1.41474891	1.34178168	1.13168474	0.92959501	0.76765981	0.58902706	0.51556926
            //                                28	1.00000000	1.00000000	0.96044147	1.23224199	1.40802001	1.33539984	1.12630217	0.92517363	0.76400864	0.58622551	0.51311708
            //                                32	1.00000000	1.00000000	0.96642959	1.23992472	1.41679867	1.34372573	1.13332438	0.93094186	0.76877204	0.58988048	0.51631624
            //                                40	1.00000000	1.00000000	0.95223257	1.22171001	1.39598566	1.32398617	1.11667565	0.91726616	0.75747865	0.58121503	0.50873147
            //                                45	1.00000000	1.00000000	0.95617790	1.22677185	1.40176956	1.32947176	1.12130231	0.92106662	0.76061706	0.58362314	0.51083926
            //                                50	1.00000000	1.00000000	0.95837601	1.22959201	1.40499202	1.33252801	1.12388001	0.92318401	0.76236561	0.58496481	0.51201361
            //");
            ad = aero.Add("Incremental Cd due to external tanks", "CdTNK", "mach");
            ad.AddFactor("fcs/external-tanks-fitted");
            ad.data = parse(ad, 1, @"0.4 	0.0005
                        0.6 	0.0005
                        0.7 	0.0005
                        0.8 	0.0005
                        0.85	0.0011
                        0.9 	0.0065
                        0.94	0.0074
                        0.98	0.0077
                        1   	0.0078
                        1.1 	0.0079
                        1.2 	0.0080
                        2   	0.0081");

            ad = aero.Add("Delta Cl due to mean  flap position", "ClDFM", "alpha", "fcs/flap-pos-deg");
            ad.data = parse(ad, 2, @"                            	-20     	  -5	      0 	  5 	 10 	 25 	 50
                          0	 0.00000	 0.00000	0.00000	0.00000	0.00000	0.00000	0.00000
                         10	-0.01400	-0.13160	0.13440	0.13160	0.12880	0.10920	0.01400
                         35	-0.09016	-0.25872	0.25480	0.25872	0.29872	0.21560	0.22540
                         50	-0.24500	-0.60200	0.59500	0.60200	0.58800	0.46200	0.24500
");
            ad = aero.Add("Cl increment due to gear", "ClUC", "alpha");
            ad.AddFactor("gear/gear-pos-norm");
            ad.data = parse(ad, 1, @"-10	 -0.00
                        0 	 -0.0012
                        8 	 -0.0010
                        12    -0.0004
                        14    -0.0002
                        16    -0.0001
                        18    -0.000");

            ad = aero.Add("Cd increment due to gear", "CdUC", "alpha");
            ad.AddFactor("gear/gear-pos-norm");

            ad.data = parse(ad, 1, @"                        -10 0
                        -5	0.00017
                        0	0.00001
                        5	0.00005
                        10  0");
            ad = aero.Add("Drag due to speedbrakes", "CdDBRK", "alpha");
            ad.AddFactor("fcs/speedbrake-pos-norm");
            ad.data = parse(ad, 1, @"                        -25	0.0210
                        -20	0.0210
                        -15	0.0414
                        -5	0.0650
                        0	0.0784
                        5	0.0650
                        15	0.0514
                        25	0.0210
                        40	0.0210");

            ad = aero.Add("Drag mach factor (NASA CR-152391-VOL-1 Figure 3-2 p54)", "CdMach", "mach");
            ad.data = parse(ad, 1, @"
0.202	1.2579450
0.306	1.1412150
0.400	1.0627178
0.503	1.0219124
0.608	1.0000000
0.705	0.9909509
0.802	1.0008701
0.901	1.0107093
0.923	1.0417084
0.934	1.0793344
0.945	1.1295980
1.009	1.8800483
1.029	2.0249037
1.048	2.1444697
1.092	2.2317526
1.170	2.2611620
1.603	2.1857642
1.797	2.1360697
1.948	2.0812627
1.999	2.0608600
2.091	2.0330027
");
            ad = aero.Add("Delta Cd due to mean flap position", "CdDFM", "alpha", "fcs/flap-pos-deg");
            ad.data = parse(ad, 2, @"                            	-20 	 -5 	  0 	  5 	 10 	 25	 50
                       0	0.00000	0.00000	0.00000	0.00000	0.00000	0.00000	0.00000
                      10	0.01400	0.02268	0.01932	0.02268	0.02548	0.02856	0.09590
                      35	0.09016	0.06894	0.06222	0.06894	0.09862	0.14286	0.55734");
            ad = aero.Add("Pitch moment increment due to aim missiles", "DCMAIM", "alpha", "mach");
            ad.AddFactor("fcs/aim-fitted");
            ad.data = parse(ad, 2, @"-6     	-5     	-4     	-3     	-2     	-1     	0     	1     	2     	3     	4     	5     	6     	7     	8     	9     	10     	20
                        0.80	-0.0150	-0.0150	-0.0150	-0.0100	-0.0050	-0.0030	-0.0020	-0.0010	0.0000	0.0020	0.0030	0.0050	0.0100	0.0100	0.0050	0.0030	0.0000	0.0000
                        0.85	-0.0330	-0.0310	-0.0300	-0.0220	-0.0100	-0.0040	-0.0030	-0.0020	0.0000	0.0030	0.0060	0.0070	0.0080	0.0100	0.0100	0.0060	0.0040	0.0040
                        0.90	-0.0320	-0.0310	-0.0300	-0.0280	-0.0200	-0.0060	-0.0020	0.0000	0.0020	0.0040	0.0070	0.0070	0.0060	0.0160	0.0120	0.0120	0.0120	0.0120");
            //            ad = aero.Add("CM increment due speedbrakes", "CMBRK", "alpha");
            //            ad.AddFactor("fcs/speedbrake-pos-norm");
            //            ad.data = parse(ad, 1, @"                        -14   0
            //                        -5   0.0219
            //                        -3	 0
            //                        0	 0.0003
            //                        5	 0.0000
            //                        8	-0.0180
            //                        12	-0.0220
            //                        14	 0");
            ad = aero.Add("CM increment due to gear", "CMUC", "alpha");
            ad.AddFactor("gear/gear-pos-norm");
            ad.data = parse(ad, 1, @"-10	 0.00000
                        -4	 0.00000
                        0	-0.01750
                        5	-0.01000
                        10	-0.00250
                        20	 0.00000");
            ad = aero.Add("CM increment due to flap mean position", "CMDFM", "alpha", "fcs/flap-pos-deg");
            ad.data = parse(ad, 2, @"                    	          0		  5	 	 10		 15		 20	 	 60
                         0	0.00000	0.00000	0.00000	0.00000	0.00000	0.00000
                         5	0.01320	0.00396	0.00589	0.00762	0.00798	0.00342
                        15	0.01520	0.00456	0.00649	0.01045	0.00798	0.00342
                        35	0.01696	0.01523	0.01355	0.01092	0.01148	0.01148");

            ad = aero.Add("Engine 0 EGT Rankine", "propulsion/engine[0]/EGT-R", "propulsion/engine[0]/n1");
            ad.AddComponent("atmosphere/T-R");
            ad.data = parse(ad, 1, @"
                         0.0	0.0
                        20.0	0.0
                        30.0	541.7
                        49.0	541.7
                        62.0	631.7
                        78.0	1171.7
                        95.0	1405.7
                        99.9	1684.7
                       104.0	1684.7");

            ad = aero.Add("Engine 1 EGT Rankine", "propulsion/engine[1]/EGT-R", "propulsion/engine[1]/n1");
            ad.AddComponent("atmosphere/T-R");
            ad.data = parse(ad, 1, @"
                         0.0	0.0
                        20.0	0.0
                        30.0	541.7
                        49.0	541.7
                        62.0	631.7
                        78.0	1171.7
                        95.0	1405.7
                        99.9	1684.7
                       104.0	1684.7");
        }
    }
}
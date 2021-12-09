using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Days.Puzzles
{
    public class Day4 : Day, IDay
    {
        public void RunPartOne()
        {
            var input = ReadInput(nameof(Day4));

            var lines = 0;

            var rawData = new List<Passport>();

            var temp = new Passport();

            while (lines < input.Length)
            {
                var d = input[lines];

                if (d.Length == 0)
                {
                    rawData.Add(temp);
                    temp = new Passport();
                }
                else
                {
                    temp.RawData += $"{d} ";
                }


                lines++;
            }

            rawData.Add(temp);

            rawData.ForEach(r => r.Parse());


            Console.WriteLine(rawData.Count(rd => rd.IsValid));
        }

        public void RunPartTwo()
        {
            var input = ReadInput(nameof(Day4));

            var lines = 0;

            var rawData = new List<Passport>();

            var temp = new Passport();

            while (lines < input.Length)
            {
                var d = input[lines];

                if (d.Length == 0)
                {
                    rawData.Add(temp);
                    temp = new Passport();
                }
                else
                {
                    temp.RawData += $"{d} ";
                }


                lines++;
            }

            rawData.Add(temp);

            rawData.ForEach(r => r.Parse());


            Console.WriteLine(rawData.Count(rd => rd.IsValid));
        }

        private class Passport
        {
            public string RawData { get; set; }

            public bool Byr { get; set; }
            public bool Iyr { get; set; }
            public bool Eyr { get; set; }
            public bool Hgt { get; set; }
            public bool Hcl { get; set; }
            public bool Ecl { get; set; }
            public bool Pid { get; set; }
            public bool Cid { get; set; }

            public bool ByrPres { get; set; }
            public bool IyrPres { get; set; }
            public bool EyrPres { get; set; }
            public bool HgtPres { get; set; }
            public bool HclPres { get; set; }
            public bool EclPres { get; set; }
            public bool PidPres { get; set; }
            public bool CidPres { get; set; }

            public bool IsValid => Byr &&
                                   Iyr &&
                                   Eyr &&
                                   Hgt &&
                                   Hcl &&
                                   Ecl &&
                                   Pid &&
                                   ByrPres &&
                                   IyrPres &&
                                   EyrPres &&
                                   HgtPres &&
                                   HclPres &&
                                   EclPres &&
                                   PidPres;

            public void Parse()
            {
                var split = RawData.Split(' ').Where(s => !string.IsNullOrEmpty(s));
                foreach (var s in split)
                {
                    var kv = s.Split(':');
                    switch (kv[0])
                    {
                        case "byr":
                            ByrPres = true;
                            var byr = int.Parse(kv[1]);
                            if (byr >= 1920 && byr <= 2002) Byr = true;

                            break;
                        case "iyr":
                            IyrPres = true;
                            var iyr = int.Parse(kv[1]);
                            if (iyr >= 2010 && iyr <= 2020) Iyr = true;

                            break;
                        case "eyr":
                            EyrPres = true;
                            var eyr = int.Parse(kv[1]);
                            if (eyr >= 2020 && eyr <= 2030) Eyr = true;
                            break;
                        case "hgt":
                            HgtPres = true;
                            var hg = kv[1];

                            if (hg.Contains("cm"))
                            {
                                var hgc = kv[1].Substring(0, hg.Length - 2);
                                var cm = int.Parse(hgc);
                                if (cm >= 150 && cm <= 193)
                                    Hgt = true;
                            }
                            else if (hg.Contains("in"))
                            {
                                var hgi = kv[1].Substring(0, hg.Length - 2);
                                var inc = int.Parse(hgi);
                                if (inc >= 59 && inc <= 76)
                                    Hgt = true;
                            }

                            break;
                        case "hcl":
                            HclPres = true;
                            var regNbr = new Regex("[#][0-9a-f]+");
                            var trim = kv[1];
                            if (trim.Length == 7 && regNbr.IsMatch(trim))
                                Hcl = true;
                            break;
                        case "ecl":
                            EclPres = true;
                            switch (kv[1])
                            {
                                case "amb":
                                case "blu":
                                case "brn":
                                case "gry":
                                case "grn":
                                case "hzl":
                                case "oth":
                                    Ecl = true;
                                    break;
                            }
                            break;
                        case "pid":
                            PidPres = true;
                            var rgNbr = new Regex("[0-9]+");
                            if (kv[1].Length == 9 && rgNbr.IsMatch(kv[1]))
                                Pid = true;
                            break;
                        case "cid":
                            Cid = true;
                            break;
                    }
                }
            }
        }
    }
}
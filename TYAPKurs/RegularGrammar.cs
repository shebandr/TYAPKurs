using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TYAPKurs
{
	internal class RegularGrammar
	{
		public HashSet<List<List<string>>> AllChains = new HashSet<List<List<string>>>();
		private static string FinalChar = "_";
		public static string CheckAllSettings(List<string> settings)
		{
			if (settings[0] != settings[0].Replace(" ", ""))
			{
				return "Присутствуют пробелы в начальной подцепочке";
			}
			if (settings[1] != settings[1].Replace(" ", ""))
			{
				return "Присутствуют пробелы в конечной подцепочке";
			}
			if (settings[2].Length == 0)
			{
				return "Алфавит пуст";
			}

			//проверка алфавита на то, что все нетерминальные символы имеют длину в 1 символ и уникальные

			if (settings[2].Replace(" ", "").Length != settings[2].Split(' ').Length)
			{
				return "Какой-то из нетерминальных символов является строкой";
			}
			List<string> TempListAlphabet = settings[2].Split(' ').ToList();
			for (int i = 0; i < TempListAlphabet.Count - 1; i++)
			{
				for (int q = i + 1; q < TempListAlphabet.Count; q++)
				{
					if (TempListAlphabet[i] == TempListAlphabet[q])
					{
						return "Символы алфавита номер " + i + " и номер " + q + " одинаковые";
					}
				}
			}
			// проверка начальной и конечной строки на соответствие алфавиту
			for (int q = 0; q < settings[0].Length; q++)
			{
				bool tempCheckFlag = true;
				for (int i = 0; i < TempListAlphabet.Count; i++)
				{

					if (TempListAlphabet[i] == settings[0][q].ToString())
					{
						tempCheckFlag = false;

					}

				}
				if (tempCheckFlag)
				{
					return "В начальной подстроке символ номер " + q + " (" + settings[0][q] + ") отсутствует в алфавите";
				}
			}
			for (int q = 0; q < settings[1].Length; q++)
			{
				bool tempCheckFlag = true;
				for (int i = 0; i < TempListAlphabet.Count; i++)
				{

					if (TempListAlphabet[i] == settings[1][q].ToString())
					{
						tempCheckFlag = false;
					}

				}
				if (tempCheckFlag)
				{
					return "В конечной подстроке символ номер " + q + " (" + settings[1][q] + ") отсутствует в алфавите";
				}
			}


			if (int.TryParse(settings[3], out int result))
			{

			}
			else
			{
				return "значение инпута кратности цепочки не переводится в число";
			}

			if (int.TryParse(settings[4], out int result2))
			{

			}
			else
			{
				return "значение инпута минимальной длины цепочки не переводится в число";
			}

			if (int.TryParse(settings[5], out int result3))
			{

			}
			else
			{
				return "значение инпута максимальной длины цепочки не переводится в число";
			}

			if (Int32.Parse(settings[3]) > Int32.Parse(settings[4]))
			{
				return "Кратность длины цепочки не может быть больше длины цепочки";
			}
			if (Int32.Parse(settings[3]) == 0)
			{
				return "Кратность длины цепочки не может быть равна нулю";
			}

			if (result2 > result3)
			{
				return "Минимальная длина цепочки больше максимальной";
			}
			if (result2 < settings[0].Length + settings[1].Length)
			{
				return "Минимальная длина меньше длины необходимых подцепочек";
			}



			return "ok";
		}

		public static List<List<string>> CalculateRegularGrammar(string startChain, string endChain, string alphabet, int multiple, int minChainLength, int maxChainLength, int side)
		{

			List<List<string>>output = new List<List<string>>();
			//шизоплан, строка правила должна выглядеть как 3 подстроки, разделенные проеблом при записи в файл, 1 подстрока - название правила, вторая и третья - нетерминальный и терминальный символы
			//длина будет выбираться как кратная нужному числу в указанном диапазоне, то есть (длина начала + длина конца + добивка их до кратности + нужное число кратных итераций
			
			List<string> alphabetList = alphabet.Split(' ').ToList();

			
			int q = 0; //переменная для обозначения текущего шага в переходе
			

			if(side == 1)
			{
				string a = startChain;
				startChain = endChain;
				endChain = a;
			}

			
			output.Add(new List<string> {"S", startChain, "Q"+q});
			//тут должно идти разветвление на 3 пути: 
			// 1 - нам не нужен цикл в правилах по добавлению кратных цепочек, но нужно добивать кратность
			// 2 - нам не нужно добавление нескольких символов, чтобы добить кратность основной цепочки, но нужен цикл в правилах для получения разной кратной длины
			// 3 - нам нужно это все



			//2 добавление цикла для кратности:
			if (maxChainLength - minChainLength >= multiple)
			{
				for (int i = 0; i < multiple-1; i++)
				{
					foreach (string symbol in alphabetList)
					{
						output.Add(new List<string> { "Q" + (i), symbol, "Q" + (i + 1) });
					}
					q = i;
				}
				foreach (string symbol in alphabetList)
				{
					output.Add(new List<string> { "Q" + (q+1), symbol, "Q" + (0) });
				}
				q++;
			}


			//1: 
			//внезапная переделка: тут нужно делать q таким, чтобы после него кратность шла как надо
			if (startChain.Length + endChain.Length % multiple != 0)
			{
				foreach (string symbol in alphabetList)
				{
					output.Add(new List<string> { "Q" + (0), symbol, "Q" + (q + 1) });

				}
				q++;
				for (int i = startChain.Length + endChain.Length; i < (multiple - ((startChain.Length + endChain.Length) % multiple))+1 ; i++)
				{
					foreach (string symbol in alphabetList)
					{
						output.Add(new List<string> { "Q" + (q), symbol, "Q" + (q+1) });
						
					}
					q++;
				}

				
			}





			output.Add(new List<string> { "Q"+(q ), endChain, FinalChar });
			q++;

			


			if(side == 0)//леволинейная - сначала язык, потом переходы
			{

			} else
			{
				foreach(List<string> o in output)
				{
					string a = o[1];
					string b = o[2];
					o[2] = a;
					o[1] = b;
				}

			}


			return output;
		}





		public  List<List<List<string>>> CalculateStrings(List<List<string>> rules, int minChainLength, int maxChainLength, int side)
		{
			List<string> startChain = new List<string>();
			if (side == 1)
			{
				startChain = new List<string> { "" , rules[0][0]};
			}
			else
			{
				startChain = new List<string> { rules[0][0], "" };

			}
			RecursiveCalc(rules, minChainLength, maxChainLength, side, startChain, new List<List<string>>(), 0);

			return AllChains.ToList();
		}





		private  void RecursiveCalc(List<List<string>> rules, int minChainLength, int maxChainLength, int side, List<string> currentChainList, List<List<string>> allChangesChain, int recursionLevel)
		{
			Console.WriteLine(currentChainList[currentChainList.Count-1]);
			if (recursionLevel > 50)
			{
				Console.WriteLine("Превышена глубина рекурсии");
				return;
			}

			if (side == 0)
			{// шизоидея - гонять тут список списков, где в списке первым (или вторым) элементом будет состояние, а другим элементом - текущая строка

				for(int i = 0; i < rules.Count; i++)
				{
					if(currentChainList[1].Length > maxChainLength)
					{
						break;
					}
					if (currentChainList[0] == rules[i][0].ToString() || currentChainList[0].ToString() == FinalChar)
					{
						/*Console.WriteLine(currentChainList[0] + " " + rules[i][0].ToString());*/
						if (currentChainList[0] == FinalChar && currentChainList[1].Length >= minChainLength)
						{
							Console.WriteLine("добавляю шизу в общий список: " + allChangesChain[allChangesChain.Count - 1][0]);
							foreach(List<string> change in allChangesChain)
							{
								Console.Write(change[0] + change[1] + "->");
							}
							Console.WriteLine("\n");
							AllChains.Add(allChangesChain);
						}
						else if(currentChainList[0].ToString() != FinalChar)
						{
							List<string> currentChainList2 = new List<string> { rules[i][2].ToString(), rules[i][1].ToString() + currentChainList[1] };
							List<List<string>> allChangesChain2 = allChangesChain.ToList();
							allChangesChain2.Add(new List<string> {rules[i][1].ToString() + currentChainList[1], rules[i][2].ToString() });
							RecursiveCalc(rules, minChainLength, maxChainLength, side, currentChainList2, allChangesChain2, recursionLevel+1);
						}
					} else
					{
					}
				}
				
			}
			else
			{
				
				for (int i = 0; i < rules.Count; i++)
				{
					if (currentChainList[0].Length > maxChainLength)
					{
						break;
					}
					if (currentChainList[1] == rules[i][0].ToString() || currentChainList[1].ToString() == FinalChar)
					{
						if (currentChainList[1] == FinalChar && currentChainList[0].Length >= minChainLength)
						{
							Console.WriteLine("добавляю шизу в общий список: " + allChangesChain[allChangesChain.Count - 1][1]);
							foreach (List<string> change in allChangesChain)
							{
								Console.Write(change[0] + change[1] + "->");
							}
							Console.WriteLine("\n");
							AllChains.Add(allChangesChain);
						}
						else if (currentChainList[1].ToString() != FinalChar)
						{
							List<string> currentChainList2 = new List<string> { currentChainList[0] + rules[i][2].ToString(), rules[i][1].ToString()};
							List<List<string>> allChangesChain2 = allChangesChain.ToList();
							allChangesChain2.Add(new List<string> {rules[i][1].ToString(),currentChainList[0] + rules[i][2].ToString() });
							RecursiveCalc(rules, minChainLength, maxChainLength, side, currentChainList2, allChangesChain2, recursionLevel + 1);
						}
					}
					else
					{
						/*						Console.WriteLine("Нет вызова рекурсии" + currentChainList[currentChainList.Count - 1] + " " + rules[i][0].ToString() + " " + rules[i][1].ToString() + " " + rules[i][2].ToString() + " " + currentChainList[1].Length + " " + maxChainLength);*/
					}
				}

			}
			/*{

				for (int i = 0; i < rules.Count; i++)
				{
					if (currentChainList[0] == rules[i][0].ToString() && currentChainList[2].Length <= maxChainLength)
					{
						Console.WriteLine(currentChainList[0] + " " + rules[i][0].ToString() + " " + currentChainList[2].Length);
						if (currentChainList[0] == "" && currentChainList[2].Length <= maxChainLength && currentChainList[2].Length >= minChainLength)
						{
							Console.WriteLine("добавляю шизу в общий список: " + allChangesChain[allChangesChain.Count - 1]);
							AllChains.Add(allChangesChain);
						}
						else
						{

							List<string> currentChainList2 = new List<string> { rules[i][1].ToString(), rules[i][2].ToString() + currentChainList[currentChainList.Count - 1] };
							List<string> allChangesChain2 = allChangesChain;
							allChangesChain2.Add(rules[i][2].ToString() + currentChainList[currentChainList.Count - 1]);
							RecursiveCalc(rules, minChainLength, maxChainLength, side, currentChainList2, allChangesChain2, recursionLevel + 1);
						}
					}
				}
			}*/

		}

	}
}

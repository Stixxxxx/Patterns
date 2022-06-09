using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp1
{
    internal class Utilits
    {

        internal static async Task<List<ItemWord>> DoSortAsync(string path)
        {            
            return await DoSort(path);
        }
    

    internal static async Task<List<ItemWord>?> DoSort(string path)
        {

            string fullTextFile;
            
            char[] alphabetLatin = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '\r' };
            List<char> fullTextFileWorlds;
            List<string> fullTextFileWorldsString;
            ItemWord word;
            List<ItemWord> words;
            

            fullTextFile = String.Empty;
            fullTextFileWorlds = new List<char>();
            fullTextFileWorldsString = new List<string>();
            words = new List<ItemWord>();

            if (path == null)
                return null;

            FileInfo fileInfo = new FileInfo(path);

            string[] readText = File.ReadAllLines(path);

            foreach (string s in readText)
            {
                Console.WriteLine(s);

                if (fullTextFile == String.Empty) fullTextFile = s;
                else
                {
                    fullTextFile = fullTextFile + "\r" + s;
                }
            }
                       

            //перебрать в цикле всю строку для удаления запятых и других символов

            foreach (char s in fullTextFile)
            {
                for (int i = 0; i < alphabetLatin.Length; i++)

                {
                    if (s == alphabetLatin[i])
                    {
                        fullTextFileWorlds.Add(s);
                    }
                }
            }
            fullTextFileWorlds.Add(' ');


            //из листа чаров fullTextFileWorlds перебрать буквы до пробела или энтера, если заканчивается пробелом или энтером добавить эти буквы в новый лист из стрингов как отдельные слова



            List<char> buffer = new List<char>();
            string item = String.Empty;

            for (int i = 0; i < fullTextFileWorlds.Count; i++)
            {
                if (fullTextFileWorlds[i] == ' ' || fullTextFileWorlds[i] == '\r')
                {
                    item = new string(buffer.ToArray());
                    if (item != "")
                    {
                        fullTextFileWorldsString.Add(item);
                        buffer.Clear();
                    }
                }
                else
                {
                    buffer.Add(fullTextFileWorlds[i]);
                }
            }



            for (int i = 0; i < fullTextFileWorldsString.Count; i++)
            {
                word = new ItemWord();
                word.Word = fullTextFileWorldsString[i];
                words.Add(word);
            }



            for (int j = 0; j < words.Count; j++)
            {
                int count = 0;

                if (words[j].IsUsed == false)
                {
                    for (int k = 0; k < words.Count; k++)
                    {
                        if (words[j].Word.Equals(words[k].Word, StringComparison.Ordinal))
                        {
                            count++;

                        };
                    }
                }

                words[j].IsUsed = true;
                words[j].Count = count;
                
            }

            // удалить одинаковые записи из листа

            for (int i = 0; i < words.Count; i++)            //Циклы удаления лишних элементов
                for (int j = i + 1; j < words.Count; j++)
                    if (words[i].Word == words[j].Word)
                    {
                        words.RemoveAt(j);
                        j--;

                    }


            ItemWord itemBuffer = new ItemWord();



            for (int i = 0; i < words.Count; i++)            //Циклы удаления лишних элементов
                for (int j = i + 1; j < words.Count; j++)

                    if (words[i].Count > words[j].Count)
                    {
                        itemBuffer = words[i];
                        words[i] = words[j];
                        words[j] = itemBuffer;
                    }


            return words;
        }
    }
}
